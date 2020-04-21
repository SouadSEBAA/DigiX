using System;
using System.Collections.Generic;

namespace logisimConsole
{
    [Serializable]
    class JK : Bascule
    {
        Disposition dd = Disposition.down;
        //liste_entrees[3] == J
        //liste_entrees[4] == K
        
        public JK(string etiquette, Disposition dispo) : base(2, etiquette, dispo)
        {
            Sortie[] tab = new Sortie[2];
            tab[0] = new Sortie("Q",1, dd, false, null);
            tab[1] = new Sortie("|Q",1, dd, false, null);

            liste_sorties = new List<Sortie>(tab);
        }
        public JK() : base() 
        {
            this.nb_entrees = 5;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("K",0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Preset",1, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear",2, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clock",3, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("J",4, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Q",0, Disposition.right, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("|Q",1, Disposition.right, false, new List<OutStruct>()));
        }

        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat()) //Preset = 1 et Clear = 1
            {
                //Synchrone
                if (front)
                {
                    if (liste_entrees[3].isEtat() && liste_entrees[4].isEtat())//J=1 K=1
                        liste_sorties[0].setEtat(!liste_sorties[0].isEtat());
                    else if (liste_entrees[3].isEtat() && !liste_entrees[4].isEtat())//J=1 K=0
                        liste_sorties[0].setEtat(true);
                    else if (!liste_entrees[3].isEtat() && liste_entrees[4].isEtat())//J=0 K=1
                        liste_sorties[0].setEtat(false);
                    else if (!liste_entrees[3].isEtat() && !liste_entrees[4].isEtat())//J=0 K=0
                    {
                        //Effet Mémoire
                    }
                    liste_sorties[1].setEtat(!liste_sorties[0].isEtat());
                }
            }
            else //Asynchrone
                calcul_sorties_asynch();
        }
    }
}
