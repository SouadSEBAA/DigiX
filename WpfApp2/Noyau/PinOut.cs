using logisimConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Noyau
{
    class PinOut : Outils
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
            liste_sorties.Add(new Sortie(0, Disposition.down, false, null));
            liste_entrees.Add(new ClasseEntree(1, Disposition.left, false, false));


        }

        public override void calcul_sorties()
        {
            throw new NotImplementedException();
        }
    }

}

