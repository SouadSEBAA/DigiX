﻿using System;
using System.Collections.Generic;



namespace Noyau
{
    class ET : PorteLogique
    {
        //Constructeur
        public ET() : base() { }

        //Methodes
        public override void calcul_sorties()
        {
            int i = 0;
            bool output = liste_entrees[i].isEtat(); //initialiser la variable de la sortie qui va etre traiter
            int entree_traite = 1;
            i++; //au suivant
            while (entree_traite < nb_entrees)
            {
                if (i > liste_entrees.Count || i < 0)
                {
                    throw new RelatedException();
                }
                output = output && liste_entrees[i].isEtat();
                entree_traite++;
                i++;
            }
            if (liste_sorties == null) { throw new EmptyListException(); }
            else
            {
                liste_sorties[0].setEtat(output);
            }
        }
    }
}
