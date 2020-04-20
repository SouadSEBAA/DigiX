using WpfApp2;

namespace logisimConsole
{
    public class ClasseEntree : InputOutput
    {
        private bool related;

        public ClasseEntree(int ID, Disposition disposi, bool rel, bool etat) : base(ID, disposi)
        {
            this.related = rel;
            this.etat = etat;
            IsInput = true;
        }

        public void setRelated(bool e) { this.related = e; }

        public bool getRelated() { return this.related; }


    }
}
