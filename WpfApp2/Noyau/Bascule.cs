using System;
using System.Collections.Generic;

namespace logisimConsole
{
    [Serializable]
    abstract class Bascule : CircSequentielle
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
            //if (!Preset.isRelated() || !Clear.isRelated()) throw exception
            try
            {
                if (!liste_entrees[1].getEtat() && !liste_entrees[2].getEtat())
                    throw new PresetClearException(liste_entrees[1], liste_entrees[2]);
            }catch(PresetClearException e)
            {
                e.Gerer();
            }
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
