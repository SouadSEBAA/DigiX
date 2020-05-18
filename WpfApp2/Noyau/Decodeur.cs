using System;
using System.Collections.Generic;
namespace Noyau
{
    class Decodeur : CircCombinatoire
    {
        public Decodeur()
        {
            this.nb_entrees = 1;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();

            this.liste_entrees.Add(new ClasseEntree("Entrée 1", 0, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Sortie 1", 0, Disposition.right, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sortie 2", 1, Disposition.right, false, new List<OutStruct>()));
        }


        public override void calcul_sorties()
        {
            foreach (Sortie unesortie in this.liste_sorties)
            {
                unesortie.setEtat(false);
            }

            // Decodeur 1 -> 2
            if (nb_entrees == 1)
            {
                if (liste_entrees[0].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(true);
                }
            }

            // Decodeur 2 -> 4
            if (nb_entrees == 2)
            {
                if (liste_entrees[0].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(true);
                }
            }
            // Decodeur 2 -> 4
            if (nb_entrees == 2)
            {
                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(true);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(true);
                    liste_sorties[3].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(true);
                }
            }

            // decodeur 3 -> 8
            if (nb_entrees == 3)
            {
                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                    liste_sorties[4].setEtat(false);
                    liste_sorties[5].setEtat(false);
                    liste_sorties[6].setEtat(false);
                    liste_sorties[7].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(true);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                    liste_sorties[4].setEtat(false);
                    liste_sorties[5].setEtat(false);
                    liste_sorties[6].setEtat(false);
                    liste_sorties[7].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(true);
                    liste_sorties[3].setEtat(false);
                    liste_sorties[4].setEtat(false);
                    liste_sorties[5].setEtat(false);
                    liste_sorties[6].setEtat(false);
                    liste_sorties[7].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(true);
                    liste_sorties[4].setEtat(false);
                    liste_sorties[5].setEtat(false);
                    liste_sorties[6].setEtat(false);
                    liste_sorties[7].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                    liste_sorties[4].setEtat(true);
                    liste_sorties[5].setEtat(false);
                    liste_sorties[6].setEtat(false);
                    liste_sorties[7].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                    liste_sorties[4].setEtat(false);
                    liste_sorties[5].setEtat(true);
                    liste_sorties[6].setEtat(false);
                    liste_sorties[7].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                    liste_sorties[4].setEtat(false);
                    liste_sorties[5].setEtat(false);
                    liste_sorties[6].setEtat(true);
                    liste_sorties[7].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == true)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                    liste_sorties[3].setEtat(false);
                    liste_sorties[4].setEtat(false);
                    liste_sorties[5].setEtat(false);
                    liste_sorties[6].setEtat(false);
                    liste_sorties[7].setEtat(true);
                }
            }
        }
    }
}
