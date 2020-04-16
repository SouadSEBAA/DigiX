using logisimConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Noyau
{
    class PinIn : Outils
    {
        public PinIn(int entree, string etiq, List<ClasseEntree> liste_e, Disposition dispo) : base(entree, etiq, liste_e, dispo)
        {
            /*  this.nb_entrees = 2;
              this.nb_sorties = 1;
              this.etiquette = "Pin entrée";
              this.liste_entrees = new List<ClasseEntree>();
              this.liste_sorties = new List<Sortie>();
              this.disposition = Disposition.right;
              liste_sorties.Add(new Sortie(1, Disposition.down, false, null));
              liste_entrees.Add(new ClasseEntree(0, Disposition.left, false, false));*/


        }
        public PinIn()
        {
            this.nb_entrees = 0;
            this.nb_sorties = 1;
            this.etiquette = "Pin entrée";
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            disposition = Disposition.right;
            liste_sorties.Add(new Sortie(1, Disposition.down, false, null));
            liste_entrees.Add(new ClasseEntree(0, Disposition.left, false, false));
        }

        public override void calcul_sorties()
        {
            throw new NotImplementedException();
        }
    }
}
