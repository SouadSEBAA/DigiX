using System;

namespace logisimConsole
{
    class Demultiplexeur : CircCombinatoire
    {
        //protected List<ClasseEntree> liste_controlleurs;

        public Demultiplexeur(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(1, nb_sorties, etiquette, dispo)
        {
            //this.liste_controlleurs = liste_commande;
        }
        public Demultiplexeur() { }

        public override void calcul_sorties()
        {
            foreach (Sortie unesortie in this.liste_sorties)
            {
                unesortie.setEtat(false);
            }

            bool AllRelated = true;

            foreach (ClasseEntree uneEntree in this.liste_entrees)
            {
                if (uneEntree.getRelated() == false) { AllRelated = false; }
            }

            // throw new NotImplementedException();
            if (AllRelated)
            {
                switch (this.nb_sorties)
                {
                    case 2:
                        Console.WriteLine("2 sorties || 1 commande");
                        if (liste_entrees[0].isEtat() == false)
                        { //liste_sorties[0].setEtat(liste_entrees[1].isEtat());
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
            else { Console.WriteLine("Assurez que tous les entrées sont reliées....."); }
        }




    }
}
