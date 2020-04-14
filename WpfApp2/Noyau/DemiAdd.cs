namespace logisimConsole
{
    class DemiAdd : CircCombinatoire
    {
        // les demi additionneurs n'existe que sur 1 bit
        // on additionne 2 nombre d'un bit chacun 
        // Comme entree, on a 2 bits
        // Comme sortie, on a 2 bits, bit de some et bit de retenue
        public DemiAdd(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(2, 2, etiquette, dispo) { }

        public override void calcul_sorties()
        {
            //throw new NotImplementedException();
            // Soit le nombre A mis dans liste_entrees[0] et le nombre B mis dans liste_entrees[1]
            // La somme des deux Bit est mis dans liste_sorties[0]
            // La retenue est dans le bit liste_sorties[1]
            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }
        }
    }
}
