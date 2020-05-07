using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfApp2;

namespace logisimConsole
{
    [Serializable]
    public abstract class Bascule : CircSequentielle
    {
        Disposition dd = Disposition.down;

        public Bascule(int nb_entrees, string etiquette, Disposition dispo) : base(nb_entrees, 2, etiquette, dispo)
        {
            Sortie[] tab = new Sortie[2];
            tab[0] = new Sortie("sortie",1, dd, false, null);
            tab[1] = new Sortie("sortie",1, dd, false, null);

            liste_sorties = new List<Sortie>(tab);

        }
        public Bascule() :base()
        {
            
        }

        public override void calcul_sorties()
        {
            try
            {
                if (!liste_entrees[1].getEtat() && !liste_entrees[2].getEtat())
                    throw new PresetClearException(liste_entrees[1], liste_entrees[2], (((liste_entrees[1].Parent as Grid).Parent as Canvas).Parent as Gate).Parent as Canvas);
            }catch(PresetClearException e)
            { e.Gerer(); }
        }

        public void calcul_sorties_asynch()
        {
            //Asynchrone
            //Calculer Q
            if (!liste_entrees[1].getEtat() && liste_entrees[2].getEtat()) //Preset = 0 et Clear = 1
                liste_sorties[0].setEtat(false);
            else if (liste_entrees[1].getEtat() && !liste_entrees[2].getEtat()) //Preset = 1 et Clear = 0
                liste_sorties[0].setEtat(true);

            //Calculer !Q
            liste_sorties[1].setEtat(!liste_sorties[0].getEtat());
        }


    }
}
