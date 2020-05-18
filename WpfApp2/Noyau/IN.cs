using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using WpfApp2;
using WpfApp2.Noyau;

namespace Noyau
{
    public abstract class IN : Outils
    {

        HashSet<Outils> listefin = new HashSet<Outils>();//pour eviter less redoublants qui changent less sorties
        public IN()
        {

        }

        
        /// <summary>
        /// construire la liste des elementss finaux d'un mini circuit relié à un element de départ
        /// </summary>
        /// <returns></returns>
        public HashSet<Outils> getEndListe() { return this.listefin; }
        //*************************

        public void Calcul()
        {
            if (this.circuit.getSimulation())
            {
                ICollection<Edge<Outils>> hs = new HashSet<Edge<Outils>>();
                this.EndCircuit(this, hs);
                this.circuit.EvaluateCircuit(this);
            }
        }

    }
}

