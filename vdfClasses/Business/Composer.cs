using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class Composer
    {
        public VinMillesime vinMillesime { get; set; }
        public Cepage cepage { get; set; }
        public decimal Pourcentage { get; set; }

        public Composer(VinMillesime vinMillesime, Cepage cepage, decimal pourcentage)
        {
            this.vinMillesime = vinMillesime;
            this.cepage = cepage;
            Pourcentage = pourcentage;
        }
    }
}
