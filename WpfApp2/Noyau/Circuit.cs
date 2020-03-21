using System;
using System.Collections.Generic;
using System.Text;

namespace logisimConsole
{
    abstract class Circuit : Outils
    {
        public Circuit(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette,dispo)
        { }

        public Circuit() { }
    }

    
}
