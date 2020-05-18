using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp2.Noyau;

namespace Noyau
{
    /// <summary>
    /// Exception levée si l'utilisateur tente de simuler ou afficher la Table de Vérité 
    /// alors qu'il existe des entrées non reliées
    /// </summary>
    public class RelatedException : Exception
    {
        Canvas panel;

        public RelatedException(Canvas p)
        {
            Application.Current.Dispatcher.Invoke(() =>
            { panel = p; });
        }
        public RelatedException() { this.panel = null; }

        public void Gerer()
        {
            ExceptionMessage message = new ExceptionMessage();
            message.textMessage.Text = "     Il existe des entrées qui n'ont pas été reliées";
            message.Opacity = 0.5;
            message.MouseDown += Close;

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
