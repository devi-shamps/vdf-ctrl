using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class Couleur
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public Couleur(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }
    }
}
