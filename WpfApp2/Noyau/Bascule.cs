using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfApp2;

namespace Noyau
{
    /// <summary>
    /// Les bascules : JK, D, T, RST
    /// Les conventions : 
    /// liste_entrees[1] -> Preset
    /// liste_entrees[2] -> Clear
    /// Sortie[0] -> Q
    /// Sortie[1] -> non Q
    /// </summary>
    public abstract class Bascule : CircSequentielle
    {
        public Bascule() : base()
        {

        }

        public override void calcul_sorties()
        {
            try
            {
                if (!liste_entrees[1].getEtat() && !liste_entrees[2].getEtat()) //Si Preset = 0 et Clear = 0, un état interdit, un excpetion est levée
                    if ((((liste_entrees[1].Parent as Grid).Parent as Canvas).Parent as Gate).Parent as Canvas != null)
                        throw new PresetClearException(liste_entrees[1], liste_entrees[2], (((liste_entrees[1].Parent as Grid).Parent as Canvas).Parent as Gate).Parent as Canvas);
            }
            catch (PresetClearException e)
            { e.Gerer(); }
        }

        /// <summary>
        /// Calculer les sorties dans le cas asynchrone
        /// </summary>
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
