using System;
using System.Collections.Generic;

namespace Noyau
{
    class NON : PorteLogique
    {
        public NON() : base()
        {
            setnb_entrees(1);
            setnb_sorties(1);

            List<ClasseEntree> liste_e = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            liste_e.Add(new ClasseEntree("Entrée 1", 0, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Sortie", 0, Disposition.right, false, new List<OutStruct>()));
            this.liste_entrees = liste_e;

        }

        //Methodes
        public override void calcul_sorties()
        {
            if (liste_entrees.Count > 1 || liste_entrees.Count < 0) { throw new Exception(); }
            else
            {
                if (liste_sorties == null) { throw new EmptyListException(); }
                else
                {
                    liste_sorties[0].setEtat(!liste_entrees[0].isEtat());
                }
            }
        }
    }
}
