using System;
using System.Collections.Generic;

namespace Noyau
{
    [Serializable]
    class AddComplet : CircCombinatoire
    {
        // les add complet comportent 3 entrees : bit de A, bit de B, bit de la retenue precedente ( entrente )
        // On a deux sortie, la somme et la retenue sortante

        // Ceci est le constructeur de AddComplet, il est utilisé pour la création de ce dernier
        // Chaque nouvel instance est représenté comme le décrit le corps du constructeur
        // Autrement dit, avec 3 d'entées et 2 sorties
        public AddComplet() : base()
        {
            this.nb_entrees = 3;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("A", 1, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("B", 2, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Retenue entrante", 3, Disposition.up, false, false));
            this.liste_sorties.Add(new Sortie("Somme", 0, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Retenue sortante", 0, Disposition.down, false, new List<OutStruct>()));
        }


        public override void calcul_sorties()
        {
            // Soit le nombre A mis dans liste_entrees[0] et le nombre B mis dans liste_entrees[1]
            // La retenue entrante est dans liste_entrees[2]
            // La somme des deux Bit est mis dans liste_sorties[0]
            // La retenue sortante est dans le bit liste_sorties[1]        

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == false)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true && liste_entrees[2].isEtat() == true)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(true);
            }
        }

        public void setEntree(int ind, bool entree)
        {
            liste_entrees[ind].setEtat(entree);
        }

        public bool getSortie()
        {
            return liste_sorties[0].isEtat();
        }

        public bool getRetenue()
        {
            return liste_sorties[1].getEtat();
        }
    }
}
