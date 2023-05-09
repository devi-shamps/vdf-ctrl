using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class LotEnVente
    {
        public int Id { get; set; }
        public int NbBouteillesInitial { get; set; }
        public int VolumeUnitaire { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int NbBouteillesRestant { get; set; }
        public VinMillesime VinMillesime { get; set; }
        public List<Concerner> Concerners { get; set; }

        public LotEnVente(int id, int nbBouteillesInitial, int volumeUnitaire, decimal prixUnitaire, int nbBouteillesRestant, VinMillesime vinMillesime)
        {
            Id = id;
            NbBouteillesInitial = nbBouteillesInitial;
            VolumeUnitaire = volumeUnitaire;
            PrixUnitaire = prixUnitaire;
            NbBouteillesRestant = nbBouteillesRestant;
            Concerners = new List<Concerner>();
            VinMillesime = vinMillesime;
        }

        public override string ToString()
        {
            return Id + "-" + VinMillesime.Vin.Nom + "-" + VinMillesime.Millesime;
        }
    }
}
