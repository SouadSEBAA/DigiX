
using System;
using System.Collections.Generic;

namespace Noyau
{
    public abstract class Circuit : Outils
    {
        // Constructeur ..
        public Circuit(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo)
        { }

        public Circuit()
        {

        }
    }
}




