using System;
using System.Collections.Generic;

namespace Noyau
{
    /// <summary>
    /// Les conventions : 
    /// liste_entrees[1] -> Remise à zéro
    /// liste_entrees[2] -> Chargement (Load)
    /// liste_entrees[3] -> dd (Décalage vers la droite)
    /// liste_entrees[4] -> dg (Décalage vers la gauche)
    /// liste_entrees[5] -> esd (entrée série droite)
    /// liste_entrees[6] -> esg (entrée série gauche)
    /// </summary>
    class Reg_Dec : CircSequentielle
    {
        //liste des sorties : [mem...]
        //liste des entrées : [clock*raz*chg*dd*dg*esd*esg*les entrees paraleles ]]

        public Reg_Dec() : base()
        {
            this.nb_entrees = 11;
            this.nb_sorties = 4;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Clock", 0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear", 1, Disposition.right, false, false));
            this.liste_entrees.Add(new ClasseEntree("Load", 2, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Decalage droite", 3, Disposition.right, false, false));
            this.liste_entrees.Add(new ClasseEntree("Decalage gauche", 4, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée série droite", 5, Disposition.right, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée série gauche", 6, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 1", 7, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 2", 8, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 3", 9, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 4", 10, Disposition.up, false, false));
            //ortie g et d 0.1
            this.liste_sorties.Add(new Sortie("Sortie 1", 1, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sorte 2", 2, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sortie 3", 3, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sortie 4", 4, Disposition.down, false, new List<OutStruct>()));
        }

        public override void calcul_sorties()
        {

            if ((this.getListeentrees())[1].isEtat() == true)
            {//remise à 0

                int i1 = 0;
                while (i1 < nb_sorties)
                {
                    (this.getListesorties())[i1].setEtat(false); i1++;

                }
            }
            else
            {
                if ((this.getListeentrees())[2].isEtat() == true)//chargement 
                {
                    int i = 0, j = 7;

                    while (i < this.nb_sorties)
                    {
                        this.getListesorties()[i].setEtat(
                            this.getListeentrees()[j].isEtat());
                        i++; j++;

                    }
                }
                else
                {

                    if (front)
                    {
                        if ((this.getListeentrees())[3].isEtat() == true)//decalage droit dd (plus prioritaire )
                        {
                            int i = 0;
                            //le premier bit à droite 
                            while (i < (this.getnbrsoryies() - 1))
                            {
                                this.getListesorties()[i].setEtat(this.getListesorties()[i + 1].isEtat());//decalage de la memoire...le positionnement des indices  [n,n-1,........,1,0]
                                i++;
                            }
                            this.getListesorties()[i].setEtat(this.getListeentrees()[5].isEtat());//le dernier bit à gauche 

                        }
                        else
                        {
                            if ((this.getListeentrees())[4].isEtat() == true)//decalage gauche dg (moins prioritaire que dd)
                            {

                                int i = this.getnbrsoryies() - 1;
                                //le premier bit à gauche 
                                while (i > 0)
                                {
                                    this.getListesorties()[i].setEtat(this.getListesorties()[i - 1].isEtat());//decalage de la memoire...le positionnement des indices est le suivant   [n,n-1,........,1,0]
                                    i--;
                                }
                                this.getListesorties()[0].setEtat(this.getListeentrees()[6].isEtat());//le dernier bit à gauche 
                            }

                        }
                        front = false;
                    }
                }
            }
        }


    }
}
