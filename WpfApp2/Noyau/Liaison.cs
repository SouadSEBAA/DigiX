using System;

namespace Noyau
{
    [Serializable]
    abstract class Liaison : Outils
    {
        public Liaison(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo) { }
    }
}
