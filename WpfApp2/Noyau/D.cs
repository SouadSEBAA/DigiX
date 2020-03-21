using System;
using System.Collections.Generic;
using System.Text;

namespace logisimConsole
{
    class D : Bascule
    {//liste des entrées :[clock,Pr,Clr,D]
     //liste des sorties :[Q,-Q]
        private const int nb_sorties = 2;

        public D(int nb_entrees, string etiquette, Disposition dispo) : base(nb_entrees, etiquette,dispo) { }


        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat())
            {
                //Synchrone
                if (front)
                {
                    this.getListesorties()[0].setEtat(this.getListeentrees()[0].isEtat());
                    this.getListesorties()[1].setEtat(!(this.getListeentrees()[0].isEtat()));

                }
            }
            else //Asynchrone
                calcul_sorties_asynch();
        }


    }
}
