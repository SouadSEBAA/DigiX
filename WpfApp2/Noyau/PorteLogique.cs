using System;
using System.Collections.Generic;
using System.Text;

namespace logisimConsole
{
    abstract class PorteLogique : Outils
    {

        protected const int entreeMax = 5;

        public PorteLogique(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e,dispo)
        {   //on peut faire aussi this.nb_sorties = 1;
            this.setnb_sorties(1); // pour fixe le nb de sortie = 1;
        }

        //pour la porte NON
        /*public PorteLogique(string etiq, List<ClasseEntree> liste_e) : base(etiq, liste_e)
        {
            this.nb_entrees = 1;
        }
        */

    }
}
