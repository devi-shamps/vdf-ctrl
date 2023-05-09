using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class Client
    {
        public int Id { get; set; }
        public string Courriel { get; set; }
        public string MotDePasse { get; set; }
        public string Nom { get; set; }
        public string Rue { get; set; }
        public string Cp { get; set; }
        public string Ville  { get; set; }
        public List<Commande> Commandes { get; set; }

        public Client(int id, string courriel, string motDePasse, string nom, string rue, string cp, string ville)
        {
            Id = id;
            Courriel = courriel;
            MotDePasse = motDePasse;
            Nom = nom;
            Rue = rue;
            Cp = cp;
            Ville = ville;
            Commandes = new List<Commande>();
        }
    }
}
