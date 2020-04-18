using System;
using System.Collections.Generic;

namespace logisimConsole
{
    class NOR : PorteLogique
    {
        Disposition dd = Disposition.down;
        //Constructeur
        public NOR(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e, dispo) { }
        public NOR() : base() { }
        //Methodes
        public override void calcul_sorties()
        {
            int i = 0;
            //while (!liste_entrees[i].getRelated().Equals(true)) //related == true si l'entree est choisi //related == faux si l'entree n'est pas choisi
            //{
              //  i++; //des qu'on trouve une entree qui est "related" on sort de la boucle
            //}
            bool output = liste_entrees[i].isEtat(); //initialiser la variable de la sortie qui va etre traiter
            int entree_traite = 1;
            i++; //au suivant
            while (entree_traite < nb_entrees)
            {
                if (i > liste_entrees.Count || i < 0) { throw new Exception(); }
              //  if (liste_entrees[i].getRelated().Equals(true))
                //{
                    output = output || liste_entrees[i].isEtat();
                    entree_traite++;
                    i++;
                //}
                //else
                //{
                  //  i++;
                //}
            }
            if (liste_sorties == null) { throw new EmptyListException(); }
            else
            {
                liste_sorties[0].setEtat(!output);
                //liste_sorties.Add(new Sortie(1, dd, !output, null));
                //Console.WriteLine(liste_sorties[0].getEtat());
            }
        }
    }


}
