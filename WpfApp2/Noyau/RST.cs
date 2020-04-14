namespace logisimConsole
{
    class RST : Bascule
    {
        //liste_entrees[3] == R
        //liste_entrees[4] == S
        //liste_entrees[0] == T
        private const int nb_entrees = 2;

        public RST(string etiquette, Disposition dispo) : base(nb_entrees, etiquette, dispo) { }

        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat())
            {
                //Synchrone
                if (front)
                {
                    if (!liste_entrees[3].isEtat() && liste_entrees[4].isEtat()) //R=0 S=1
                        liste_sorties[0].setEtat(true);
                    else if (liste_entrees[3].isEtat() && !liste_entrees[4].isEtat()) //R=1 S=0
                        liste_sorties[0].setEtat(false);
                    else if (liste_entrees[3].isEtat() && liste_entrees[4].isEtat()) //R=1 S=1
                    {
                        //throw exception
                    }
                    else if (!liste_entrees[3].isEtat() && !liste_entrees[4].isEtat()) //R=0 S=0
                    {
                        //Effet Mémoire
                    }
                    liste_sorties[1].setEtat(liste_sorties[0].isEtat());
                }
            }
            else //Asynchrone
                calcul_sorties_asynch();
        }
    }
}
