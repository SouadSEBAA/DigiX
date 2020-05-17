using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noyau
{
    public class Constantes : Outils
    {
        public Constantes() : base()
        {
            nb_entrees = 0; nb_sorties = 1;
            liste_entrees = new List<ClasseEntree>(nb_entrees);
            liste_sorties = new List<Sortie>(nb_sorties);
        }

        public override void calcul_sorties()
        {
            liste_sorties[0].setEtat(liste_entrees[0].getEtat());
        }
    }
}
