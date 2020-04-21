using System;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp2.Noyau;

namespace logisimConsole
{
    class RelatedException : Exception
    {
        Canvas panel;

        public RelatedException(Canvas panel) { this.panel = panel; }
        public RelatedException() { this.panel = null; }

        public void Gerer()
        {
            UnrelatedGatesMessage message = new UnrelatedGatesMessage();
            message.Opacity = 0.5;
            message.CloseButton.MouseDown += methode;
            message.MouseDown += Close;

            panel.Children.Add(message);
            Canvas.SetTop(message, 5);
            Canvas.SetLeft(message, 380);
        }

        public void methode(object sender, MouseEventArgs e)
        {
        }

        public void Close(object sender, MouseEventArgs e)
        {
            panel.Children.Remove((UnrelatedGatesMessage)sender);
        }
    }
}
