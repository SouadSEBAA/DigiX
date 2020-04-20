﻿using System.Collections.Generic;
using WpfApp2;
namespace logisimConsole
{
    public class Sortie : InputOutput
    {
        private List<OutStruct> Sorties;
        public Sortie() : base() { Sorties = new List<OutStruct>(); IsInput = false; }

        public List<OutStruct> getSortie() { return this.Sorties; }

        public Sortie(int ID, Disposition disposi, bool etat, List<OutStruct> Sorties) : base(ID, disposi)
        {
            this.etat = etat;
            this.Sorties = Sorties;
            IsInput = false;
        }

        public void set_Sorties(List<OutStruct> Sorties) 
        { 
            this.Sorties = Sorties; 
        }

        //public  void set_Etat_Class_Sortie(bool etat) { this.etat = etat; }
        //public  bool get_Etat_Class_Sortie() { return etat; }
        public List<OutStruct> get_OutStruct() { return Sorties; }



        public virtual OutStruct getSortieSpecifique(int i)
        {
            return Sorties[i];
        }

        //essai
        public override void setEtat(bool etat)  
        {
            base.setEtat(etat);
            foreach (OutStruct outstruct in Sorties)
            {
                //outstruct.getEntree().setEtat(etat);
                outstruct.getOutils().setEntreeSpe(outstruct.getNum_entree(), etat);
            }
        }

    }
}
