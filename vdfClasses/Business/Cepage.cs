using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class Cepage
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<Composer> Composers { get; set; } = new List<Composer>();

        public Cepage(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }
    }
}
