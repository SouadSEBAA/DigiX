using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logisimConsole
{
    class ConstanteFalse : Constantes
    {
        public ConstanteFalse() : base()
        {
            liste_entrees.Add(new ClasseEntree("Entrée", 0, Disposition.left, false, false));
            liste_sorties.Add(new Sortie("Sortie Constante", 0, Disposition.right, false, new List<OutStruct>()));
        }

    }
}
