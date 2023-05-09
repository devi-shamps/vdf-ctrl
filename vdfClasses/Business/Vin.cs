using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdfClasses.Business
{
    public class Vin
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Couleur Couleur { get; set; }

        public Vin(int id, string nom, Couleur couleur)
        {
            Id = id;
            Nom = nom;
            Couleur = couleur;
        }
    }
}
