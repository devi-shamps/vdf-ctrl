using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime DateHeureCommande { get; set; }
        public Client? Client { get; set; }
        public List<Concerner> Concerners { get; set; }
            
        public bool Validee { get; set;}

        public Commande(int id, DateTime dateHeureCommande, Client client, bool Validee)
        {
            Id = id;
            this.DateHeureCommande = dateHeureCommande;
            this.Client = client;
            Concerners = new List<Concerner>();

            this.Validee = Validee;
        }

        public override string ToString()
        {
            return "Commande n°" + Id + "-" + DateHeureCommande;
        }
    }
}
