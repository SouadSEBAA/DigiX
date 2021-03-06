﻿using System;
using System.Collections.Generic;


namespace Noyau
{
    
    /// <summary>
    /// Conventions :
    /// liste_entrees[1] -> Remise à zéro
    /// </summary>
    class Compteur : CircSequentielle
    {
            /// liste des entrées : [clock ,raz]
            /// liste des sorties : [les sories du compteur]

        public Compteur()
        {
            this.nb_entrees = 2;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Clock", 0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear", 1, Disposition.left, false, false));
            this.liste_sorties.Add(new Sortie("Sortie 1", 0, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sortie 2", 1, Disposition.down, false, new List<OutStruct>()));
        }
        public override void calcul_sorties()
        {

            if (((this.getListeentrees())[1]).isEtat() == true)
            {//remise à 0


                (this.getListeentrees())[1].setEtat(false);

                int i1 = 0;
                while (i1 < nb_sorties)
                {
                    (this.getListesorties())[i1].setEtat(false); i1++;

                }
            }
            else
            {
                if (front)
                {//cas d'incrementation au top d'horloge
                    bool stop = false;
                    int i = 0;
                    while (!stop && i < this.nb_sorties)
                    {
                        //incrementation du compteur +1
                        if ((this.getListesorties())[i].isEtat() == false) { (this.getListesorties())[i].setEtat(true); stop = true; }
                        else { (this.getListesorties())[i].setEtat(false); i = i + 1; }
                    }
                    front = false;
                }
            }

        }

    }
}
