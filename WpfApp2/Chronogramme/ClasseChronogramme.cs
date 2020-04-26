using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2;

namespace logisimConsole.Noyau
{
    class ClasseChronogramme
    {
        public List<ChartValues<InputOutput>> ChartValues { get; set; }

        public ClasseChronogramme(CircuitPersonnalise circuit)
        {
           
        }

        public class MeasureModel
        {
            public InputOutput io { get; set; }
            public int stateNum { get; set; }
        }

    }
}
