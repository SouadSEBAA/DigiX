using System.Collections.Generic;

namespace logisimConsole
{
    class JK : Bascule
    {
        Disposition dd = Disposition.down;
        //liste_entrees[3] == J
        //liste_entrees[4] == K
        private const int nb_entrees = 5;
        public JK(string etiquette, Disposition dispo) : base(nb_entrees, etiquette, dispo)
        {
            Sortie[] tab = new Sortie[2];
            tab[0] = new Sortie(1, dd, false, null);
            tab[1] = new Sortie(1, dd, false, null);

            liste_sorties = new List<Sortie>(tab);
        }

        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat()) //Preset = 1 et Clear = 1
            {
                //Synchrone
                if (front)
                {
                    if (liste_entrees[3].isEtat() && liste_entrees[4].isEtat())//J=1 K=1
                        liste_sorties[0].setEtat(!liste_sorties[0].isEtat());
                    else if (liste_entrees[3].isEtat() && !liste_entrees[4].isEtat())//J=1 K=0
                        liste_sorties[0].setEtat(true);
                    else if (!liste_entrees[3].isEtat() && liste_entrees[4].isEtat())//J=0 K=1
                        liste_sorties[0].setEtat(false);
                    else if (!liste_entrees[3].isEtat() && !liste_entrees[4].isEtat())//J=0 K=0
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
