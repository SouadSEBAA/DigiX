using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    class OneChronogrammeException : Exception
    {
        StackPanel panel;

        public OneChronogrammeException(StackPanel panel) { this.panel = panel; }
        public OneChronogrammeException() { this.panel = null; }

        public void Gerer()
        {
            ChronogrammeException message = new ChronogrammeException();
            message.Opacity = 0.5;
            message.MouseDown += Close;

            panel.Children.Add(message);
        }

        public void Close(object sender, MouseEventArgs e)
        {
            panel.Children.Remove((ChronogrammeException)sender);
        }

    }
}
