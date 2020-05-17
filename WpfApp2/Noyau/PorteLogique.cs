using System;
using System.Collections.Generic;

namespace Noyau
{
    [Serializable]
    class PorteLogique : Outils
    {
        public PorteLogique()
        {
            this.nb_entrees = 2;
            this.nb_sorties = 1;
            List<ClasseEntree> liste_e = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            liste_e.Add(new ClasseEntree("Entrée 1", 0, Disposition.left, false, false));
            liste_e.Add(new ClasseEntree("Entrée 2", 1, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Sortie", 0, Disposition.right, false, new List<OutStruct>()));
            this.liste_entrees = liste_e;
        }

        public override void calcul_sorties()
        {

        }


    }
}
