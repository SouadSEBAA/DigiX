using System;
using System.Collections.Generic;
using System.Text;

namespace logisimConsole
{
    class T : Bascule
    {
        //liste_entrees[3] = T
        private const int nb_entrees = 4;
        public T(string etiquette, Disposition dispo) : base(nb_entrees, etiquette,dispo)
        {
            liste_entrees = new List<ClasseEntree>(nb_entrees);
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
