using System;
using System.Collections.Generic;
using System.ComponentModel;
using WpfApp2;

namespace logisimConsole
{
    [Serializable]
    public class Sortie : InputOutput, INotifyPropertyChanged
    {
        private List<OutStruct> Sorties;
        public Sortie() : base() { Sorties = new List<OutStruct>(); IsInput = false; }

        public List<OutStruct> getSortie() { return this.Sorties; }

        public Sortie(String e,int ID, Disposition disposi, bool etat, List<OutStruct> Sorties) : base(e,ID, disposi)
        {
            this.etat = etat;
            this.Sorties = Sorties;
            IsInput = false;
        }

        public Sortie(int ID, Disposition disposi, bool etat, List<OutStruct> Sorties) : base( ID, disposi)
        {
            this.etat = etat;
            this.Sorties = Sorties;
            IsInput = false;
        }


        public void set_Sorties(List<OutStruct> Sorties) 
        { 
            this.Sorties = Sorties; 
        }

        public void DeleteOustruct(Outils outil, int t)
        {
            for (int i = Sorties.Count - 1; i >= 0; i--)
            {
                if (Sorties[i].getOutils().Equals(outil))
                {                    
                    if (Sorties[i].getNum_entree() == t)
                    {
                        Sorties.Remove(Sorties[i]);
                    }                    
                }
            }
        }


        public void suppSortie(OutStruct outStruct)
        {
            Sorties.Remove(outStruct);
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
                Console.WriteLine(outstruct.getOutils());
            }
            NotifyPropertyChanged("etat");
        }

        protected void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;



        //Supression elemnt
        public void DeleteOustruct(Outils outil)
        {
            
            for (int i = Sorties.Count - 1; i >= 0; i--)
            {
                if (Sorties[i].getOutils().Equals(outil))
                {
                    Sorties.Remove(Sorties[i]);
                }
            }
        }
    }
}
