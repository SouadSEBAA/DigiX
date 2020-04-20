using System;
using System.Collections.Generic;

namespace logisimConsole
{
    class Multiplexeur : CircCombinatoire
    {

        //protected List<ClasseEntree> liste_controlleurs;
        public Multiplexeur() { }
        public Multiplexeur(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, 1, etiquette, dispo)
        {
            //this.liste_controlleurs = liste_commande;
            Console.WriteLine("Un nouveau mux");
            //this.liste_sorties = new List<Sortie>();
        }

        public void setListEntrees(List<ClasseEntree> liste_entrees)
        {
            this.liste_entrees = liste_entrees;
        }
        public void setListSorties(List<Sortie> liste_sorties)
        {
            this.liste_sorties = liste_sorties;
        }
        /*
        public void getListeSorties()
        {
            Console.WriteLine("la sortir : ");
            Console.WriteLine(liste_sorties[0].isEtat());
        }
        */


        public override void calcul_sorties()
        {
            //throw new NotImplementedException();
            //1st, we must know nbr d'entrees

            // Case nb_entrees == 2 
            // donc il y'a un controlleur
            // if ( liste_controlleurs[0].etat == false ) liste_sorties[0].etat = liste_entrees[0].etat
            // if ( liste_controlleurs[0].etat == true ) liste_sorties[0].etat = liste_entrees[1].etat

            // Case nb_entrees == 4 
            // donc il y'a 2 controlleurs
            // if ( liste_controlleurs[0].etat == false & liste_controlleurs[1].etat == false ) liste_sorties[0].etat = liste_entrees[0].etat
            // if ( liste_controlleurs[0].etat == true & liste_controlleurs[1].etat == false ) liste_sorties[0].etat = liste_entrees[1].etat
            // if ( liste_controlleurs[0].etat == false & liste_controlleurs[1].etat == true ) liste_sorties[0].etat = liste_entrees[2].etat
            // if ( liste_controlleurs[0].etat == true & liste_controlleurs[1].etat == true ) liste_sorties[0].etat = liste_entrees[3].etat

            // ect...


            bool AllRelated = true;

            foreach (ClasseEntree uneEntree in this.liste_entrees)
            {
                if (uneEntree.getRelated() == false) { AllRelated = false; }
            }


            if (AllRelated)
            {
                switch (this.nb_entrees)
                //ArgumentOutOfRangeException
                {
                    case 2:
                        Console.WriteLine("2 entrees || 1 commande");
                        if (liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(this.liste_entrees[1].isEtat());
                            Console.WriteLine(liste_entrees[1].isEtat());
                        }
                        else
                        {
                            liste_sorties[0].setEtat(this.liste_entrees[2].isEtat());
                            Console.WriteLine(liste_entrees[2].isEtat());
                        }
                        break;
                    case 4:
                        /*
                          la listes des entrées est comme suit :
                          i0-i1-i2-i3-i4-i5
                          C0-C1-E0-E1-E2-E3  
                         */
                        Console.WriteLine("4 entrees || 2 commande");
                        if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[2].isEtat());
                            Console.WriteLine(liste_entrees[2].isEtat());
                        }
                        if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                        { liste_sorties[0].setEtat(liste_entrees[3].isEtat());
                            Console.WriteLine(liste_entrees[3].isEtat());
                        }
                        if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[4].isEtat());
                            Console.WriteLine(liste_entrees[4].isEtat());
                        }
                        if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                        { liste_sorties[0].setEtat(liste_entrees[5].isEtat()); 
                            Console.WriteLine(liste_entrees[5].isEtat());
                        }
                        break;
                    case 8:
                        /*
                            la listes des entrées est comme suit :
                            i0-i1-i2-i3-i4-i5-i6-i7-i8-i9-i10
                            C0-C1-C2-E0-E1-E2-E3-E4-E5-E6-E7  
                        */
                        Console.WriteLine("8 entrees || 3 commande");
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[3].isEtat());
                            Console.WriteLine(liste_entrees[3].isEtat());
                        }
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                        { liste_sorties[0].setEtat(liste_entrees[4].isEtat());
                            Console.WriteLine(liste_entrees[4].isEtat());
                        }
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[5].isEtat());
                            Console.WriteLine(liste_entrees[5].isEtat());
                        }
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                        { liste_sorties[0].setEtat(liste_entrees[6].isEtat());
                            Console.WriteLine(liste_entrees[6].isEtat());
                        }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[7].isEtat());
                            Console.WriteLine(liste_entrees[7].isEtat());
                        }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                        { liste_sorties[0].setEtat(liste_entrees[8].isEtat()); 
                            Console.WriteLine(liste_entrees[8].isEtat());
                        }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[9].isEtat());
                            Console.WriteLine(liste_entrees[9].isEtat());
                        }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                        { liste_sorties[0].setEtat(liste_entrees[10].isEtat());
                            Console.WriteLine(liste_entrees[10].isEtat());
                        }
                        break;
                    default:
                        Console.WriteLine("erreur des entrées");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Erreur ! Assurez que tous les entrées sont reliées");
            }




        }
    }
}
