﻿using System.Collections.Generic;
using System;

namespace Noyau
{
    /// <summary>
    /// Les conventions : 
    /// liste_entrees[3] -> T
    /// </summary>

    class T : Bascule
    {

        //Pour gérer quel etat prendre en considération si un front a lieu au moment ou l'entrée T change
        protected bool EtatAvant_T;

        public T() : base()
        {
            this.nb_entrees = 4;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Clock", 0, Disposition.left, false, false));//Horloge
            this.liste_entrees.Add(new ClasseEntree("Preset", 1, Disposition.up, false, false));//Preset
            this.liste_entrees.Add(new ClasseEntree("Clear", 2, Disposition.up, false, false));//Clear
            //this.liste_entrees.Add(new ClasseEntree(3, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("T", 3, Disposition.left, false, false));//Entrée T
            this.liste_sorties.Add(new Sortie("Q", 0, Disposition.right, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("|Q", 1, Disposition.right, false, new List<OutStruct>()));
        }
        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat())
            {

                //Synchrone
                if (front)
                {
                    if (EtatAvant_T) //T=1
                        liste_sorties[0].setEtat(!liste_sorties[0].isEtat());
                    front = false;
                }
                liste_sorties[1].setEtat(!liste_sorties[0].isEtat());

            }
            else //Asynchrone
                calcul_sorties_asynch();

        }

        public override void setEntreeSpe(int i, bool etat)
        {
            if (i == 3)
                EtatAvant_T = liste_entrees[i].getEtat();
            base.setEntreeSpe(i, etat);
        }


    }
}
