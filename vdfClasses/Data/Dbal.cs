using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using vdfClasses.Business;

namespace vdfClasses.Data
{
    public class Dbal
    {
        private MySqlConnection conn;
        public Dbal() 
        {
            conn = new MySqlConnection("server=localhost;user=root;database=vdf;port=3306;password=&6HAUTdanslaFauré;");
        }

        public bool CheckUser(string login, string password)
        {
            bool passOk = false;
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM user WHERE name = '{login}'", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (BCrypt.Net.BCrypt.Verify(password, reader.GetString(2)))
                {
                    passOk = true;
                }
            }
            conn.Close();
            return passOk;
        }

        public List<Commande> GetCommandes()
        {
            List<Commande> commandes = new List<Commande>();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM commande INNER JOIN client ON commande.idClient = client.id INNER JOIN concerner ON concerner.idCommande = commande.id INNER JOIN lotenvente ON concerner.idLot = lotenvente.id INNER JOIN vinmillesime ON lotenvente.millesime = vinmillesime.millesime AND lotenvente.idVin = vinmillesime.idVin INNER JOIN vin ON vinmillesime.idVin = vin.id INNER JOIN couleur ON vin.idCouleur = couleur.id INNER JOIN composer ON composer.millesime = vinmillesime.millesime AND composer.idVin = vinmillesime.idVin INNER JOIN cepage ON composer.idCepage = cepage.id", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            //s'il y a au moins une ligne récupérée, on initialise tous les objets liés à la 1ere commande
            if (reader.Read())
            {
                Cepage leCepage = new Cepage(reader.GetInt32(32), reader.GetString(33));
                Couleur laCouleur = new Couleur(reader.GetInt32(26), reader.GetString(27));
                Vin leVin = new Vin(reader.GetInt32(23), reader.GetString(24), laCouleur);
                VinMillesime leVinMillesime = new VinMillesime(reader.GetInt32(20), leVin);
                Composer laCompo = new Composer(leVinMillesime, leCepage, reader.GetDecimal(31));
                leVinMillesime.Composers.Add(laCompo);
                leCepage.Composers.Add(laCompo);

                LotEnVente leLotEnVente = new LotEnVente(reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetDecimal(17), reader.GetInt32(18), leVinMillesime);

                Client leClient = new Client(reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                Commande laCommande = new Commande(reader.GetInt32(0), reader.GetDateTime(1), leClient, reader.GetBoolean(3));
                Concerner leConcerner = new Concerner(laCommande, leLotEnVente, reader.GetInt32(13));
                laCommande.Concerners.Add(leConcerner);
                leLotEnVente.Concerners.Add(leConcerner);

                commandes.Add(laCommande);
                //on traite ensuite les lignes suivantes avec la logique suivante:
                //Cas 1: si la ligne suivante a le même idCommande et le même idVin et millesime, alors c'est que c'est une ligne pour compléter la composition du vin
                //Cas 2: si la ligne suivante a le même idCommande mais pas le même idLot, alors il faut ajouter un nouveau lot d'un nouveau vin à la commande
                //Cas 3: si la ligne suivante n'a pas le même idCommande, alors on traite la ligne comme une nouvelle commande
                while (reader.Read())
                {
                    //Cas 1
                    if (reader.GetInt32(0) == laCommande.Id && reader.GetInt32(21) == leVinMillesime.Vin.Id && reader.GetInt32(22) == leVinMillesime.Millesime)
                    {
                        leCepage = new Cepage(reader.GetInt32(32), reader.GetString(33));
                        laCompo = new Composer(leVinMillesime, leCepage, reader.GetDecimal(31));
                        leVinMillesime.Composers.Add(laCompo);
                        leCepage.Composers.Add(laCompo);
                    }
                    //Cas 2
                    else if (reader.GetInt32(0) == laCommande.Id && reader.GetInt32(14) != leLotEnVente.Id)
                    {
                        leCepage = new Cepage(reader.GetInt32(32), reader.GetString(33));
                        laCouleur = new Couleur(reader.GetInt32(26), reader.GetString(27));
                        leVin = new Vin(reader.GetInt32(23), reader.GetString(24), laCouleur);
                        leVinMillesime = new VinMillesime(reader.GetInt32(20), leVin);
                        laCompo = new Composer(leVinMillesime, leCepage, reader.GetDecimal(31));
                        leVinMillesime.Composers.Add(laCompo);
                        leCepage.Composers.Add(laCompo);

                        leLotEnVente = new LotEnVente(reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetDecimal(17), reader.GetInt32(18), leVinMillesime);
                        leConcerner = new Concerner(laCommande, leLotEnVente, reader.GetInt32(13));
                        laCommande.Concerners.Add(leConcerner);
                        leLotEnVente.Concerners.Add(leConcerner);
                    }
                    //Cas 3: on traite la ligne récupérée comme une nouvelle commande
                    else
                    {
                        leCepage = new Cepage(reader.GetInt32(32), reader.GetString(33));
                        laCouleur = new Couleur(reader.GetInt32(26), reader.GetString(27));
                        leVin = new Vin(reader.GetInt32(23), reader.GetString(24), laCouleur);
                        leVinMillesime = new VinMillesime(reader.GetInt32(20), leVin);
                        laCompo = new Composer(leVinMillesime, leCepage, reader.GetDecimal(31));
                        leVinMillesime.Composers.Add(laCompo);
                        leCepage.Composers.Add(laCompo);

                        leLotEnVente = new LotEnVente(reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetDecimal(17), reader.GetInt32(18), leVinMillesime);

                        leClient = new Client(reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                        laCommande = new Commande(reader.GetInt32(0), reader.GetDateTime(1), leClient, reader.GetBoolean(3));
                        leConcerner = new Concerner(laCommande, leLotEnVente, reader.GetInt32(13));
                        laCommande.Concerners.Add(leConcerner);
                        leLotEnVente.Concerners.Add(leConcerner);

                        //étape 1: on récupère les commandes sans le reste des objets
                        //commandes.Add(new Commande(reader.GetInt32(0), reader.GetDateTime(1));
                        commandes.Add(laCommande);
                    }
                }
            }
            
            conn.Close();

            return commandes;
        }
    }
}
