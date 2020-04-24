using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp2.Noyau;

namespace logisimConsole
{
    [Serializable]
    class RelatedException : Exception
    {
        StackPanel panel;

        public RelatedException(StackPanel panel) { this.panel = panel; }
        public RelatedException() { this.panel = null; }

        public void Gerer()
        {
            UnrelatedGatesMessage message = new UnrelatedGatesMessage();
            message.Opacity = 0.5;
            message.MouseDown += Close;

            panel.Children.Add(message);
        }

        public void Close(object sender, MouseEventArgs e)
        {
            panel.Children.Remove((UnrelatedGatesMessage)sender);
        }
    }
}
