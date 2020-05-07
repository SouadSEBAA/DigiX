using System;
using System.Collections.Generic;

namespace logisimConsole
{
    [Serializable]
    class Reg_Dec : CircSequentielle
    {//liste des sorties : [SD*SG*mem...]
     //liste des entrées : [clock*raz*chg*dd*dg*esd*esg*les entrees paraleles ]]

        Disposition dd = Disposition.down;
        public Reg_Dec(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo)
        {
            this.nb_sorties = nb_sorties + 2;
            this.nb_entrees = nb_entrees + 7;
            int i = 0;
            while (i < this.nb_entrees) { this.liste_entrees.Add(new ClasseEntree("entree",1, dd, false, false)); i++; }
            i = 0;
            while (i < this.nb_sorties) { this.liste_sorties.Add(new Sortie()); i++; }
        }

        public Reg_Dec() : base() 
        {
            this.nb_entrees = 8;
            this.nb_sorties = 4;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Decalage gauche", 0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear", 1, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Decalage droit", 2, Disposition.right, false, false));
            this.liste_entrees.Add(new ClasseEntree("Load", 3, Disposition.right, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 1", 4, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 2", 5, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 3", 6, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Entrée 4", 7, Disposition.up, false, false));
            this.liste_sorties.Add(new Sortie("Sortie 1", 0, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sorte 2", 1, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sortie 3", 2, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Sortie 4", 3, Disposition.down, false, new List<OutStruct>()));

        }
        /*
		public override bool verifiRelie(){//verifier le  nombre d'entrées paralleles est egale aunombre de sorties
			return(base.verifiRelie()	&&	(this.getnbrentrees()-6)==this.getnbrsoryies());
		}*/
        public override void calcul_sorties()
        {
            //	if (this.verifiRelie())
            //	{
            if ((this.getListeentrees())[1].isEtat() == true)
            {//remise à 0
                (this.getListeentrees())[1].setEtat(false);
                int i1 = 0;
                while (i1 < nb_sorties)
                {
                    (this.getListesorties())[i1].setEtat(false); i1++;

                }
                //a revoir
            }
            else
            {
                if ((this.getListeentrees())[2].isEtat() == true)//chargement 
                {
                    int i = 2, j = 7;
                    (this.getListeentrees())[2].setEtat(false);
                    while (i < this.nb_sorties)
                    {
                        this.getListesorties()[i].setEtat(
                            this.getListeentrees()[j].isEtat());
                        i++; j++;

                    }
                }
                else
                {

                    if ((this.getListeentrees())[0].isEtat() == true)//clock on  TO DO: on remplace (front)
                    {
                        (this.getListeentrees())[0].setEtat(false);
                        if ((this.getListeentrees())[3].isEtat() == true)//decalage droit dd (plus prioritaire )
                        {//je remes dd à 0???
                            Console.WriteLine("droooooitee");

                            int i = 2;
                            this.getListesorties()[0].setEtat((this.getListesorties()[2]).isEtat());//le premier bit à droite 
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
                                this.getListesorties()[1].setEtat((this.getListesorties()[this.getnbrsoryies() - 1]).isEtat());//le premier bit à gauche 
                                while (i > 1)
                                {
                                    this.getListesorties()[i].setEtat(this.getListesorties()[i - 1].isEtat());//decalage de la memoire...le positionnement des indices est le suivant   [n,n-1,........,1,0]
                                    i--;
                                }
                                this.getListesorties()[2].setEtat(this.getListeentrees()[6].isEtat());//le dernier bit à gauche 
                            }

                        }
                    }
                }
            }
            //}
            //le parcours de la listes des sorties pour appeler calculesorti de chaque composant relié à cette sortie 
            //this.appelCalcul();

        }


    }
}
