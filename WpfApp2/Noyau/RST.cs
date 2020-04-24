﻿using System.Collections.Generic;
using System;
namespace logisimConsole
{
    [Serializable]
    class RST : Bascule
    {
        //liste_entrees[3] == R
        //liste_entrees[4] == S
        //liste_entrees[0] == T
       

        public RST(string etiquette, Disposition dispo) : base(2, etiquette, dispo) { }
        public RST() : base()
        {
            this.nb_entrees = 5;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("S",0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Preset",1, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear",2, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clock",3, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("R",4, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Q",0, Disposition.right, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("|Q",1, Disposition.right, false, new List<OutStruct>()));
        }
        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat())
            {
                //Synchrone
                if (front)
                {
                    if (!liste_entrees[3].isEtat() && liste_entrees[4].isEtat()) //R=0 S=1
                        liste_sorties[0].setEtat(true);
                    else if (liste_entrees[3].isEtat() && !liste_entrees[4].isEtat()) //R=1 S=0
                        liste_sorties[0].setEtat(false);
                    else if (liste_entrees[3].isEtat() && liste_entrees[4].isEtat()) //R=1 S=1
                    {
                        //throw exception
                    }
                    else if (!liste_entrees[3].isEtat() && !liste_entrees[4].isEtat()) //R=0 S=0
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
