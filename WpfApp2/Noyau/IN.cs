using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using System.Collections.Generic;
using WpfApp2;
using WpfApp2.Noyau;

namespace logisimConsole
{
    public abstract class  IN :Outils
    { 

        HashSet<Outils> listefin = new HashSet<Outils>();//pour eviter less redoublants qui changent less sorties
        public IN()
        {

        }
        //construiction de la liste des elementss finaux d'un mini circuit relié à un element de depart
        public HashSet<Outils> getEndListe() { return this.listefin; }
        //*************************
        public void Calcul()
        {
           // if (this.circuit.getSimulation())
           // {
                this.EndCircuit(this);
                this.circuit.EvaluateCircuit(this);
           // }
        }

    }
}
