using System;
using System.Collections.Generic;



namespace Noyau
{
    /// <summary>
    /// Les conventions : 
    /// liste_entrees[3] -> J
    /// liste_entrees[4] -> K
    /// </summary>

    class JK : Bascule
    {
        
        //Pour gérer quels etats prendre en considération si un front a lieu au moment ou l'une des entrées J ou K change
        private bool EtatAvant_J, EtatAvant_K;

        public JK() : base()
        {
            this.nb_entrees = 5;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Clock", 0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Preset", 1, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear", 2, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("J", 3, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("K", 4, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Q", 0, Disposition.right, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("|Q", 1, Disposition.right, false, new List<OutStruct>()));
        }

        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat()) //Preset = 1 et Clear = 1
            {
                //Synchrone
                if (front)
                {
                    if (EtatAvant_J && EtatAvant_K)//J=1 K=1
                        liste_sorties[0].setEtat(!liste_sorties[0].isEtat());
                    else if (EtatAvant_J && !EtatAvant_K)//J=1 K=0
                        liste_sorties[0].setEtat(true);
                    else if (!EtatAvant_J && EtatAvant_K)//J=0 K=1
                        liste_sorties[0].setEtat(false);
                    else if (!EtatAvant_J && !EtatAvant_K)//J=0 K=0
                    {
                        //Effet Mémoire
                    }
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
                EtatAvant_J = liste_entrees[3].getEtat(); //J
            else if (i == 4)
                EtatAvant_K = liste_entrees[4].getEtat();// K

            base.setEntreeSpe(i, etat);
        }

    }
}
