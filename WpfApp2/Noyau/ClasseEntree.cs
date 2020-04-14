//using logisimConsole;
using WpfApp2;

namespace logisimConsole
{
    public class ClasseEntree : InputOutput
    {
        private bool related;
        private bool etat;

        public ClasseEntree(int ID, Disposition disposi, bool rel, bool etat) : base(ID, disposi)
        {
            this.related = rel;
            this.etat = etat;
        }

        public bool isEtat() { return this.etat; }

        public void setEtat(bool etat) { this.etat = etat; }

        public void setRelated(bool e) { this.related = e; }

        public bool getRelated() { return this.related; }


    }
}
