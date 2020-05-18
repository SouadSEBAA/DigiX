using System;
using System.Collections.Generic;


namespace Noyau
{
    /// <summary>
    /// Les conventions : 
    /// liste_entrees[3] -> D
    /// </summary>

    class D : Bascule
    {
        //liste des entrées :[clock,Pr,Clr,D]
        //liste des sorties :[Q,-Q]

        //Pour gérer quel etat prendre en considération si un front a lieu au moment l'entrée D change
        protected bool EtatAvant_D;

        public D() : base()
        {
            this.nb_entrees = 4;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Clock", 0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Preset", 1, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear", 2, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("D", 3, Disposition.left, false, false));
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
                    this.getListesorties()[0].setEtat(EtatAvant_D);
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
                EtatAvant_D = liste_entrees[i].getEtat();
            base.setEntreeSpe(i, etat);
        }

    }
}
