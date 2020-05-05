using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfApp2;

namespace logisimConsole
{
    public class BasculeExceptions : Exception
    {
        InputOutput io1, io2;
        public BasculeExceptions(InputOutput io1, InputOutput io2) { this.io1 = io1; this.io2 = io2; }
        public virtual void Gerer()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                io1.elSelector.Fill = Brushes.Orange; io2.elSelector.Fill = Brushes.Orange;
            });
        }
    }

    public class RSTException : BasculeExceptions
    {
        public RSTException(InputOutput io1, InputOutput io2) : base(io1, io2) { }
        public override void Gerer()
        {
            base.Gerer();

        }
    }
    public class PresetClearException : BasculeExceptions
    {
        public PresetClearException(InputOutput io1, InputOutput io2) : base(io1, io2) { }

        public override void Gerer()
        {
            base.Gerer();
        }

    }
}
