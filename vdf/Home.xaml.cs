using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vdfClasses.Data;
using vdfClasses.Business;

namespace vdf
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        Dbal MyDbal { get; set; }

        public Home(Dbal theDbal)
        {
            MyDbal = theDbal;
            InitializeComponent();
            List<Commande> lesCommandes = MyDbal.GetCommandes();            
            foreach (Commande commande in lesCommandes)
            {
                listeCommandes.Items.Add(commande);
            }

        }

        private void listeCommandes_Selected(object sender, RoutedEventArgs e)
        {
            listeLotsEnVente.Items.Clear();
            Commande selectedCommande = (Commande)listeCommandes.SelectedItem;
            if (selectedCommande != null)
            {
                foreach (Concerner lot in selectedCommande.Concerners)
                {
                    listeLotsEnVente.Items.Add(lot.LotEnVente);
                }
                InfosCommande.Text = "Client: " + selectedCommande.Client.Nom + "\n";

                string statut;
                if (selectedCommande.Validee == true)
                {
                    statut = "Validée";
                }
                else
                {
                    statut = "Non-validée";
                }
                InfosCommande.Text = InfosCommande.Text + "\nStatut: " + statut;

            }
        }

        private void listeLotsEnVente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Commande selectedCommande = (Commande)listeCommandes.SelectedItem;
            LotEnVente selectedLot = (LotEnVente)listeLotsEnVente.SelectedItem;
            if (selectedLot != null)
            {
                InfosVin.Text = "Année: " + selectedLot.VinMillesime.Millesime.ToString() +
                "\nCépages: \n";
                foreach (var cepage in selectedLot.VinMillesime.Composers)
                {
                    InfosVin.Text = InfosVin.Text + cepage.cepage.Nom + ":" + cepage.Pourcentage + "%\n";
                }
                int qte = 0;
                

                foreach (Concerner concerner in selectedLot.Concerners)
                {
                    if (concerner.Commande == selectedCommande)
                    {
                        qte = concerner.Quantite;
                    }
                }
                InfosCommande.Text = InfosCommande.Text + "\nQté: " + qte;

                
                
            }
        }

        private void btn_validee(object sender, RoutedEventArgs e)
        {

            InfosBouton.Text = "Votre commande à bien été Valider!";
        }
    }
}
