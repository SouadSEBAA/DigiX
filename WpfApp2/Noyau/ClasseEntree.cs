using System;
//using logisimConsole;
using System.Collections.Generic;
using System.Text;

namespace logisimConsole
{
    public class ClasseEntree
    {
        private bool related;
        private bool etat;

        public ClasseEntree(bool rel,bool etat) 
        {
            this.related = rel;
            this.etat = etat;
        }

        public bool isEtat() { return this.etat;}

        public void setEtat(bool etat) { this.etat = etat;}

        public void setRelated(bool e) { this.related = e; }
        
        public bool getRelated() { return this.related; }


    }
}
