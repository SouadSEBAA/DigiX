namespace logisimConsole
{
    class Decodeur : CircCombinatoire
    {
        public Decodeur(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo) { }

        public override void calcul_sorties()
        {
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
                    liste_sorties[0].setEtat(true);
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
