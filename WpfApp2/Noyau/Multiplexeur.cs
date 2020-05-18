using System;
using System.Collections.Generic;




namespace Noyau
{
    class Multiplexeur : CircCombinatoire
    {

        public Multiplexeur()
        {
            this.nb_entrees = 3;
            this.nb_sorties = 1;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Controle 1", 0, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 1", 1, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 2", 2, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Sortie", 0, Disposition.right, false, new List<OutStruct>()));
        }

        public override void calcul_sorties()
        {
            switch (this.nb_entrees)
            {
                case 3:
                    if (liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(this.liste_entrees[1].isEtat());
                    }
                    else
                    {
                        liste_sorties[0].setEtat(this.liste_entrees[2].isEtat());
                    }
                    break;
                case 6:
                    /*
                      la listes des entrées est comme suit :
                      i0-i1-i2-i3-i4-i5
                      C0-C1-E0-E1-E2-E3  
                     */
                    if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[2].isEtat());
                    }
                    if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[3].isEtat());
                    }
                    if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[4].isEtat());
                    }
                    if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[5].isEtat());
                    }
                    break;
                case 11:
                    /*
                        la listes des entrées est comme suit :
                        i0-i1-i2-i3-i4-i5-i6-i7-i8-i9-i10
                        C0-C1-C2-E0-E1-E2-E3-E4-E5-E6-E7  
                    */
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[3].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[4].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[5].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[6].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[7].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[8].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[9].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[10].isEtat());
                    }
                    break;
                default:
                    Console.WriteLine("erreur des entrées");
                    break;
            }

        }
    }
}
