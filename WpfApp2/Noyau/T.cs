using System.Collections.Generic;

namespace logisimConsole
{
    class T : Bascule
    {
        //liste_entrees[3] = T
        
        public T(string etiquette, Disposition dispo) : base(2, etiquette, dispo)
        {
            liste_entrees = new List<ClasseEntree>(nb_entrees);
        }
        public T() : base() 
        {
            this.nb_entrees = 4;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree(0, Disposition.left, false, false));//Horloge
            this.liste_entrees.Add(new ClasseEntree(1, Disposition.up, false, false));//Preset
            this.liste_entrees.Add(new ClasseEntree(2, Disposition.down, false, false));//Clear
            //this.liste_entrees.Add(new ClasseEntree(3, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree(3, Disposition.left, false, false));//Entrée T
            this.liste_sorties.Add(new Sortie(0, Disposition.right, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie(1, Disposition.right, false, new List<OutStruct>()));
        }
        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat())
            {
                //Synchrone
                if (front)
                {
                    if (liste_entrees[3].isEtat()) //T=1
                        liste_sorties[0].setEtat(!liste_sorties[0].isEtat());
                    liste_sorties[1].setEtat(!liste_sorties[0].isEtat());
                }
            }
            else //Asynchrone
                calcul_sorties_asynch();

        }

    }
}
