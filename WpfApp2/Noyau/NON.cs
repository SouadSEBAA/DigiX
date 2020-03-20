using System;
using System.Collections.Generic;
using System.Text;

namespace logisimConsole
{
    class NON : PorteLogique
    {
        public NON(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e,dispo)
        {
            setnb_entrees(1);
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
                    liste_sorties.Add(new Sortie(!liste_entrees[0].isEtat(), null));
                    Console.WriteLine(liste_sorties[0].getEtat());
                }
            }
        }
    }
}
