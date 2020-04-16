using System;
using System.Collections.Generic;

namespace logisimConsole
{
    class NON : PorteLogique
    {
        Disposition dd = Disposition.down;
        public NON(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e, dispo)
        {
            setnb_entrees(1);
        }
        public NON() : base() { }

        //essai
        public NON(String s) : base(1, 1) { }

        //Methodes
        public override void calcul_sorties()
        {
            if (liste_entrees.Count > 1 || liste_entrees.Count < 0) { throw new Exception(); }
            else
            {
                if (liste_sorties == null) { throw new EmptyListException(); }
                else
                {
                    //liste_sorties.Add(new Sortie(1, dd, !liste_entrees[0].isEtat(), null));
                    liste_sorties[0].setEtat(!liste_entrees[0].isEtat());
                    Console.WriteLine("NON :");
                    Console.WriteLine(liste_entrees[0].getEtat());
                    Console.WriteLine(liste_sorties[0].getEtat());
                }
            }
        }
    }
}
