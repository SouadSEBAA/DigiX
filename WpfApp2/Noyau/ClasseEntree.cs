using System;
using WpfApp2;

namespace logisimConsole
{
    [Serializable]
    public class ClasseEntree : InputOutput
    {
        private bool related;
        public ClasseEntree() { } //added this for serialization
        public ClasseEntree(String e, int ID, Disposition disposi, bool rel, bool etat) : base(e,ID, disposi)
        {
            this.related = rel;
            this.etat = etat;
            IsInput = true;
        }

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
