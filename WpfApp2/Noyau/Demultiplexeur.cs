using System;
using System.Collections.Generic;

namespace logisimConsole
{
    [Serializable]
    class Demultiplexeur : CircCombinatoire
    {
        //protected List<ClasseEntree> liste_controlleurs;

        public Demultiplexeur(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(1, nb_sorties, etiquette, dispo)
        {
            //this.liste_controlleurs = liste_commande;
        }
        public Demultiplexeur() 
        {
            this.nb_entrees = 2;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Controle 1", 0, Disposition.up, false, false));
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

                switch (this.nb_sorties)
                {
                    case 2:
                        Console.WriteLine("2 sorties || 1 commande");
                        if (liste_entrees[0].isEtat() == false)
                        { 
                            liste_sorties[0].setEtat(liste_entrees[1].isEtat());
                            Console.WriteLine(liste_entrees[1].isEtat());
                        }
                        else
                        {
                            liste_sorties[0].setEtat(liste_entrees[1].isEtat());
                            //Console.WriteLine(liste_entrees[1].isEtat();
                        }
                        break;
                    case 4:
                        Console.WriteLine("4 sorties || 2 commande");
                        if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[2].isEtat()); }
                        if (liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                        { liste_sorties[1].setEtat(liste_entrees[2].isEtat()); }
                        if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                        { liste_sorties[2].setEtat(liste_entrees[2].isEtat()); }
                        if (liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                        { liste_sorties[3].setEtat(liste_entrees[2].isEtat()); }
                        break;
                    case 8:
                        Console.WriteLine("8 sorties || 3 commande");
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                        { liste_sorties[0].setEtat(liste_entrees[3].isEtat()); }
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                        { liste_sorties[1].setEtat(liste_entrees[3].isEtat()); }
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                        { liste_sorties[2].setEtat(liste_entrees[3].isEtat()); }
                        if (liste_entrees[2].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                        { liste_sorties[3].setEtat(liste_entrees[3].isEtat()); }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == false)
                        { liste_sorties[4].setEtat(liste_entrees[3].isEtat()); }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[0].isEtat() == true)
                        { liste_sorties[5].setEtat(liste_entrees[3].isEtat()); }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == false)
                        { liste_sorties[6].setEtat(liste_entrees[3].isEtat()); }
                        if (liste_entrees[2].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[0].isEtat() == true)
                        { liste_sorties[7].setEtat(liste_entrees[3].isEtat()); }
                        break;
                    default:
                        Console.WriteLine("erreur des entrées");
                        break;
                }
           
        }




    }
}
