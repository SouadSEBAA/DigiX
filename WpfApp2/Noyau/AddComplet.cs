namespace logisimConsole
{
    class AddComplet : CircCombinatoire
    {
        // les add complet comportent 3 entrees : bit de A, bit de B, bit de la retenue precedente
        // On a deux sortie, la somme et la retenue sortante
        public AddComplet(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(3, 2, etiquette, dispo) { }
        public AddComplet() : base() { }


        public override void calcul_sorties()
        {
            // Soit le nombre A mis dans liste_entrees[0] et le nombre B mis dans liste_entrees[1]
            // La retenue sortante est dans liste_entrees[2]
            // La somme des deux Bit est mis dans liste_sorties[0]
            // La retenue est dans le bit liste_sorties[1]        

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(true);
            }
        }

        public void setEntree(int ind, bool entree)
        {
            liste_entrees[ind].setEtat(entree);
        }

        public bool getSortie()
        {
            return liste_sorties[0].isEtat();
        }

        public bool getRetenue()
        {
            return liste_sorties[1].getEtat();
        }
    }
}
