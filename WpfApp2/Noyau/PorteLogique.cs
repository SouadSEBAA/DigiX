using System.Collections.Generic;

namespace logisimConsole
{
     class PorteLogique : Outils
    {

        protected const int entreeMax = 5;

        public PorteLogique(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e, dispo)
        {   //on peut faire aussi this.nb_sorties = 1;
            this.setnb_sorties(1); // pour fixe le nb de sortie = 1;
        }
        public PorteLogique()
        {
            this.nb_entrees = 2;
            this.nb_sorties = 1;
            List<ClasseEntree> liste_e = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            liste_e.Add(new ClasseEntree(0, Disposition.left, false, false));
            liste_e.Add(new ClasseEntree(1, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie(0, Disposition.right, false, new List<OutStruct>()));
            this.liste_entrees = liste_e;
        }

        public PorteLogique(int i, int n) : base(i, n) { }


        public override void calcul_sorties()
        {
            
        }
        //pour la porte NON
        /*public PorteLogique(string etiq, List<ClasseEntree> liste_e) : base(etiq, liste_e)
        {
            this.nb_entrees = 1;
        }
        */

    }
}
