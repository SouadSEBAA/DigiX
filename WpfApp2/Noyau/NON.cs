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

        //public NON() : base(1, 1) { nb_entrees = 1; }

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
                    liste_sorties[0].setEtat(!liste_entrees[0].isEtat());
                    //liste_sorties.Add(new Sortie(1, dd, !liste_entrees[0].isEtat(), null));
                    //Console.WriteLine(liste_sorties[0].getEtat());
                }
            }
        }
    }
}
