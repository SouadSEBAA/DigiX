using System;
using System.Collections.Generic;
using System.Text;

namespace logisimConsole
{
    class OUX : PorteLogique
    {
        public OUX(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e,dispo) { }
        //Methodes
        public override void calcul_sorties()
        {
            int i = 0;
            while (!liste_entrees[i].getRelated().Equals(true)) //related == true si l'entree est choisi //related == faux si l'entree n'est pas choisi
            {
                i++; //des qu'on trouve une entree qui est "related" on sort de la boucle
            }
            bool output = liste_entrees[i].isEtat(); //initialiser la variable de la sortie qui va etre traiter
            int entree_traite = 1;
            i++; //au suivant
            while (entree_traite < nb_entrees)
            {
                if (i > liste_entrees.Count || i < 0) { throw new Exception(); }
                if (liste_entrees[i].getRelated().Equals(true))
                {
                    if (output.Equals(liste_entrees[i].isEtat())) { output = false; }
                    else { output = true; }
                    entree_traite++;
                    i++;
                }
                else
                {
                    i++;
                }
            }
            if (liste_sorties == null) { throw new EmptyListException(); }
            else
            {
                liste_sorties.Add(new Sortie(output, null));
                Console.WriteLine(liste_sorties[0].getEtat());
            }
        }
    }
}
