using System;
using System.Collections.Generic;


namespace Noyau
{
    /// <summary>
    /// les demi additionneurs n'existe que sur 1 bit
    /// on additionne 2 nombre d'un bit chacun 
    /// Comme entree, on a 2 bits
    /// Comme sortie, on a 2 bits, bit de some et bit de retenue
    /// </summary>
    class DemiAdd : CircCombinatoire
    {
        public DemiAdd()
        {
            this.nb_entrees = 2;
            this.nb_sorties = 3;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("A", 0, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("B", 1, Disposition.up, false, false));
            this.liste_sorties.Add(new Sortie("Somme", 2, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Retenue", 2, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Retenue Sortante", 1, Disposition.down, false, new List<OutStruct>()));
        }
        public override void calcul_sorties()
        {
            // Soit le nombre A mis dans liste_entrees[0] et le nombre B mis dans liste_entrees[1]
            // La somme des deux Bit est mis dans liste_sorties[0]
            // La retenue est dans le bit liste_sorties[1]
            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == false)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == false)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == false && liste_entrees[1].isEtat() == true)
            {
                liste_sorties[0].setEtat(true);
                liste_sorties[1].setEtat(false);
            }

            if (liste_entrees[0].isEtat() == true && liste_entrees[1].isEtat() == true)
            {
                liste_sorties[0].setEtat(false);
                liste_sorties[1].setEtat(true);
            }
        }
    }
}
