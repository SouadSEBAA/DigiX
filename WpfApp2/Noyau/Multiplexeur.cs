using System;
using System.Collections.Generic;

namespace Noyau
{
    [Serializable]
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

        public void setListEntrees(List<ClasseEntree> liste_entrees)
        {
            this.liste_entrees = liste_entrees;
        }

        public void setListSorties(List<Sortie> liste_sorties)
        {
            this.liste_sorties = liste_sorties;
        }

        public override void calcul_sorties()
        {
            switch (this.nb_entrees)
            {
                case 3:
                    Console.WriteLine("2 entrees || 1 commande");
                    if (liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(this.liste_entrees[1].isEtat());
                        Console.WriteLine(liste_entrees[1].isEtat());
                    }
                    else
                    {
                        liste_sorties[0].setEtat(this.liste_entrees[2].isEtat());
                        Console.WriteLine(liste_entrees[2].isEtat());
                    }
                    break;
                case 6:
                    /*
                      la listes des entrées est comme suit :
                      i0-i1-i2-i3-i4-i5
                      C0-C1-E0-E1-E2-E3  
                     */
                    Console.WriteLine("4 entrees || 2 commande");
                    if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[2].isEtat());
                        Console.WriteLine(liste_entrees[2].isEtat());
                    }
                    if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[3].isEtat());
                        Console.WriteLine(liste_entrees[3].isEtat());
                    }
                    if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[4].isEtat());
                        Console.WriteLine(liste_entrees[4].isEtat());
                    }
                    if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[5].isEtat());
                        Console.WriteLine(liste_entrees[5].isEtat());
                    }
                    break;
                case 11:
                    /*
                        la listes des entrées est comme suit :
                        i0-i1-i2-i3-i4-i5-i6-i7-i8-i9-i10
                        C0-C1-C2-E0-E1-E2-E3-E4-E5-E6-E7  
                    */
                    Console.WriteLine("8 entrees || 3 commande");
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[3].isEtat());
                        Console.WriteLine(liste_entrees[3].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[4].isEtat());
                        Console.WriteLine(liste_entrees[4].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[5].isEtat());
                        Console.WriteLine(liste_entrees[5].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[6].isEtat());
                        Console.WriteLine(liste_entrees[6].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[7].isEtat());
                        Console.WriteLine(liste_entrees[7].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[8].isEtat());
                        Console.WriteLine(liste_entrees[8].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                    {
                        liste_sorties[0].setEtat(liste_entrees[9].isEtat());
                        Console.WriteLine(liste_entrees[9].isEtat());
                    }
                    if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                    {
                        liste_sorties[0].setEtat(liste_entrees[10].isEtat());
                        Console.WriteLine(liste_entrees[10].isEtat());
                    }
                    break;
                default:
                    Console.WriteLine("erreur des entrées");
                    break;
            }

        }
    }
}
