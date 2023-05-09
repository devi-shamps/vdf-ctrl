using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class VinMillesime
    {
        public int Millesime { get; set; }
        public Vin Vin { get; set; }
        public List<Composer> Composers { get; set; } = new List<Composer>();

        public VinMillesime(int millesime, Vin vin)
        {
            Millesime = millesime;
            Vin = vin;
        }
    }
}
