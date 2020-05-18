using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp2.Noyau;
using Noyau;

namespace WpfApp2
{
    class TVCompSeqException : Exception
    {
        Canvas panel;

        public TVCompSeqException(Canvas p)
        {
            panel = p;
        }

        public void Gerer()
        {
            ExceptionMessage message = new ExceptionMessage();
            message.textMessage.Text = "  ATTENTION Il existe un composant séquentiel !";
            message.Opacity = 0.5;
            message.MouseDown += Close;
            //To remove the exceptions 
            if (Exceptions.set.Count != 0)
            {
                panel.Children.Remove(Exceptions.set[0]);
                Exceptions.set.Remove(Exceptions.set[0]);
            }

            panel.Children.Add(message);
            Exceptions.set.Add(message);
            Canvas.SetLeft(message, 300);
            Canvas.SetTop(message, 20);

        }

        public void Close(object sender, MouseEventArgs e)
        {
            panel.Children.Remove((ExceptionMessage)sender);
            Exceptions.set.Remove(Exceptions.set[0]);
        }

    }
}
