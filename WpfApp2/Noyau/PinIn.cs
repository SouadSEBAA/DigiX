using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noyau
{
    class PinIn : IN
    {
        public PinIn()
        {
            etiquette = "PinEntrée_" + (id - 24);
            this.nb_entrees = 0;
            this.nb_sorties = 1;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            disposition = Disposition.right;
            liste_sorties.Add(new Sortie("Sortie", 0, Disposition.right, false, new List<OutStruct>()));
            liste_entrees.Add(new ClasseEntree("Entrée", 0, Disposition.left, false, false));
        }


        public override void calcul_sorties()//on l'ajoute au graphique
        {
            liste_sorties[0].setEtat(liste_entrees[0].getEtat());
        }
    }
}