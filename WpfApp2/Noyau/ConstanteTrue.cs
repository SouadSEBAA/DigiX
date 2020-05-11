using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logisimConsole
{
    class ConstanteTrue : Constantes
    {
        public ConstanteTrue() : base()
        {
            liste_entrees.Add(new ClasseEntree("Entrée", 0, Disposition.left, false, true));
            liste_sorties.Add(new Sortie("Sortie Constante", 0, Disposition.right, false, new List<OutStruct>()));
        }

        public override void calcul_sorties()
        {
            liste_entrees[0].setEtat(true);
            base.calcul_sorties();
        }
    }
}
