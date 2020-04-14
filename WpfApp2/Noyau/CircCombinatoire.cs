namespace logisimConsole
{
    abstract class CircCombinatoire : Circuit
    {


        //Constructor..
        public CircCombinatoire(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo) { }

        public CircCombinatoire() { }
    }
}
