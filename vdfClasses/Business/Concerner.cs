using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class Concerner
    {
        public Commande Commande { get; set; }
        public LotEnVente LotEnVente { get; set; }
        public int Quantite { get; set; }

        public Concerner(Commande commande, LotEnVente lotEnVente, int quantite)
        {
            Commande = commande;
            LotEnVente = lotEnVente;
            Quantite = quantite;
        }
    }
}
