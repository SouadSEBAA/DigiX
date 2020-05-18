using System;
using System.Collections.Generic;
using System.ComponentModel;
using WpfApp2;

namespace Noyau
{
    public class Sortie : InputOutput, INotifyPropertyChanged
    {
        /// <summary>
        /// Liste des OutStruct [entree, outil] avec lesquelles cette sortie est reliée
        /// </summary>
        private List<OutStruct> Sorties;
        public Sortie() : base() { Sorties = new List<OutStruct>(); IsInput = false; }

        public List<OutStruct> getSortie() { return this.Sorties; }

        public Sortie(String e, int ID, Disposition disposi, bool etat, List<OutStruct> Sorties) : base(e, ID, disposi)
        {
            this.etat = etat;
            this.Sorties = Sorties;
            IsInput = false;
        }


        public void set_Sorties(List<OutStruct> Sorties)
        {
            this.Sorties = Sorties;
        }

        public void DeleteOustruct(Outils outil, ClasseEntree t)
        {
            for (int i = Sorties.Count - 1; i >= 0; i--)
            {
                if (Sorties[i].getOutils().Equals(outil))
                {
                    if (Sorties[i].GetEntree().Equals(t))
                    {
                        Sorties.Remove(Sorties[i]);
                    }
                }
            }
        }

        public List<OutStruct> get_OutStruct() { return Sorties; }

        public override void setEtat(bool etat)
        {
            base.setEtat(etat);
            foreach (OutStruct outstruct in Sorties)
            {
                outstruct.getOutils().setEntreeSpe(outstruct.getNumEntree(), etat);
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
