namespace logisimConsole
{
    class Encodeur : CircCombinatoire
    {
        public Encodeur(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo) { }

        public override void calcul_sorties()
        {
            // 4 -> 2
            if (nb_entrees == 4)
            {
                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true && liste_entrees[3].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(true);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == true)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(true);
                }
            }

            // 8 -> 3
            if (nb_entrees == 8)
            {
                if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false && liste_entrees[4].isEtat() == false && liste_entrees[5].isEtat() == false && liste_entrees[6].isEtat() == false && liste_entrees[7].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false && liste_entrees[4].isEtat() == false && liste_entrees[5].isEtat() == false && liste_entrees[6].isEtat() == false && liste_entrees[7].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true && liste_entrees[3].isEtat() == false && liste_entrees[4].isEtat() == false && liste_entrees[5].isEtat() == false && liste_entrees[6].isEtat() == false && liste_entrees[7].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(true);
                    liste_sorties[2].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == true && liste_entrees[4].isEtat() == false && liste_entrees[5].isEtat() == false && liste_entrees[6].isEtat() == false && liste_entrees[7].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(true);
                    liste_sorties[2].setEtat(false);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false && liste_entrees[4].isEtat() == true && liste_entrees[5].isEtat() == false && liste_entrees[6].isEtat() == false && liste_entrees[7].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(true);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false && liste_entrees[4].isEtat() == false && liste_entrees[5].isEtat() == true && liste_entrees[6].isEtat() == false && liste_entrees[7].isEtat() == false)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(false);
                    liste_sorties[2].setEtat(true);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false && liste_entrees[4].isEtat() == false && liste_entrees[5].isEtat() == false && liste_entrees[6].isEtat() == true && liste_entrees[7].isEtat() == false)
                {
                    liste_sorties[0].setEtat(false);
                    liste_sorties[1].setEtat(true);
                    liste_sorties[2].setEtat(true);
                }

                if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false && liste_entrees[3].isEtat() == false && liste_entrees[4].isEtat() == false && liste_entrees[5].isEtat() == false && liste_entrees[6].isEtat() == false && liste_entrees[7].isEtat() == true)
                {
                    liste_sorties[0].setEtat(true);
                    liste_sorties[1].setEtat(true);
                    liste_sorties[2].setEtat(true);
                }
            }
        }
    }
}
