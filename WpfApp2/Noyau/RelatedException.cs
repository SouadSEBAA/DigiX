using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp2.Noyau;

namespace logisimConsole
{
    [Serializable]
    class RelatedException : Exception
    {
        static List<ExceptionMessage> set; //Avec une seule capacité
        Canvas panel;

        public RelatedException(Canvas panel) { this.panel = panel; if (set == null) set = new List<ExceptionMessage>(1); }
        public RelatedException() { this.panel = null; }

        public void Gerer()
        {
            ExceptionMessage message = new ExceptionMessage();
            message.textMessage.Text = "     Il existe des entrées qui n'ont pas été reliées, reliez-les puis simulez !";
            message.Opacity = 0.5;
            message.MouseDown += Close;
            
            if (set.Count != 0)
                panel.Children.Remove(set[0]);

            panel.Children.Add(message);
            set.Add(message);
            Canvas.SetLeft(message, 300);
            Canvas.SetTop(message, 20);
        }

        public void Close(object sender, MouseEventArgs e)
        {
            panel.Children.Remove((ExceptionMessage)sender);
        }
    }
}
