
﻿using System.Collections.Generic;
namespace logisimConsole

{
    class D : Bascule
    {//liste des entrées :[clock,Pr,Clr,D]
     //liste des sorties :[Q,-Q]
       

        public D(int nb_entrees, string etiquette, Disposition dispo) : base(nb_entrees, etiquette, dispo) { }
        public D() : base() 
        {
            
            this.nb_entrees = 4;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree("Clock",0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree("Preset",1, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("Clear",2, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("D",3, Disposition.left, false, false));
            //this.liste_entrees.Add(new ClasseEntree(4, Disposition.down, false, false));
            this.liste_sorties.Add(new Sortie("Q",0, Disposition.right, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("|Q",1, Disposition.right, false, new List<OutStruct>()));
        }

        public override void calcul_sorties()
        {
            base.calcul_sorties();
            if (liste_entrees[1].isEtat() && liste_entrees[2].isEtat())
            {
                //Synchrone
                if (front)
                {
                    this.getListesorties()[0].setEtat(this.getListeentrees()[3].isEtat());
                    this.getListesorties()[1].setEtat(!(this.getListeentrees()[3].isEtat()));
                }
            }
            else //Asynchrone
                calcul_sorties_asynch();
        }


    }
}
