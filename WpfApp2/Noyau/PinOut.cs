using logisimConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2.Noyau
{
    [Serializable]
    class PinOut : Outils, INotifyPropertyChanged
    {
        public PinOut(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e, dispo)
        {
            /*  this.nb_entrees = 1;
              this.nb_sorties = 0;
              this.etiquette = "Pin sortie";
              this.liste_entrees = new List<ClasseEntree>();
              this.liste_sorties = new List<Sortie>();
              this.disposition = Disposition.right;
              liste_sorties.Add(new Sortie(0, Disposition.down, false, null));
              liste_entrees.Add(new ClasseEntree(1, Disposition.left, false, false));*/

        }
        public PinOut()
        {
            this.nb_entrees = 1;
            this.nb_sorties = 0;
            this.etiquette = "Pin sortie";
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.disposition = Disposition.right;
            liste_sorties.Add(new Sortie("sortie", 0, Disposition.down, false, new List<OutStruct>()));
            liste_entrees.Add(new ClasseEntree("Entrée ", 1, Disposition.left, false, false));

        }

        public override void calcul_sorties()
        {
            //throw new NotImplementedException();

        }

        public override void setEntreeSpe(int i, bool etat)
        {
            base.setEntreeSpe(i, etat);
            NotifyPropertyChanged("liste_entrees");
            /*
            Application.Current.Dispatcher.Invoke(() => {

                if (etat)
                    (((liste_entrees[0].Parent as Grid).Parent as Canvas).Parent as Gate).path.Fill = Brushes.Green;
                else
                    (((liste_entrees[0].Parent as Grid).Parent as Canvas).Parent as Gate).path.Fill = Brushes.Red;
            });*/
        }


    protected void NotifyPropertyChanged(string property)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(property));
    }

    public event PropertyChangedEventHandler PropertyChanged;

} 
            

    }




