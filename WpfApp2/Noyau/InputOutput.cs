using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace logisimConsole
{
    public abstract class InputOutput
    {
        protected int ID;

        /// <summary>
        /// La disposition du io dans le Gate
        /// </summary>
        protected Disposition dispo;

        
        public InputOutput(int ID,Disposition disposi)
        {
            this.ID = ID;
            this.dispo = disposi;
        }
        public InputOutput() { }
    }
}
