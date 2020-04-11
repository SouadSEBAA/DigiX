using System.Collections.Generic;
using WpfApp2;
namespace logisimConsole
{
    public class Sortie : InputOutput
    {
        private bool etat;
        private List<OutStruct> Sorties;
        public Sortie() : base() { }

        public List<OutStruct> getSortie() { return this.Sorties; }

        public Sortie(int ID, Disposition disposi, bool etat, List<OutStruct> Sorties) : base(ID, disposi)
        {
            this.etat = etat;
            this.Sorties = Sorties;
        }

        public void set_Sorties(List<OutStruct> Sorties) { this.Sorties = Sorties; }
        //public  void set_Etat_Class_Sortie(bool etat) { this.etat = etat; }
        //public  bool get_Etat_Class_Sortie() { return etat; }
        public List<OutStruct> get_OutStruct() { return Sorties; }


        public void setEtat(bool etat)
        {
            this.etat = etat;
        }
        public bool getEtat()
        {
            return this.etat;
        }
        public bool isEtat() { return etat; }

        public virtual OutStruct getSortieSpecifique(int i)
        {
            return Sorties[i];
        }
    }
}
