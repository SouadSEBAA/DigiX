using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp2;
using WpfApp2.Noyau;

namespace Noyau
{
    /// <summary>
    /// Représente les exceptions qui puissent etre levées à cause d'un état interdit dans la bascule
    /// </summary>
    public class BasculeExceptions : Exception, IException
    {
        /// <summary>
        /// les entrées qui lèvent l'exception 
        /// </summary>
        protected InputOutput io1, io2;
        public Canvas panel { get; set; }
        protected ExceptionMessage message;

        public BasculeExceptions(InputOutput io1, InputOutput io2, Canvas p)
        {
            this.io1 = io1; this.io2 = io2;
            Application.Current.Dispatcher.Invoke(() =>
            { panel = p; });
        }


        public virtual void Gerer()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                io1.elSelector.Fill = Brushes.Orange; io2.elSelector.Fill = Brushes.Orange;

                message = new ExceptionMessage();
                message.canvas.Background = Brushes.Orange;
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
            });
        }

        public void Close(object sender, MouseEventArgs e)
        {
            panel.Children.Remove((ExceptionMessage)sender);
            Exceptions.set.Remove(Exceptions.set[0]);

        }

    }

/// <summary>
/// L'exception levée dans la bascule RST à cause de l'état interdit : R = 1 et S = 1
/// </summary>
    public class RSTException : BasculeExceptions
    {
        public RSTException(InputOutput io1, InputOutput io2, Canvas p) : base(io1, io2, p) { }
        public override void Gerer()
        {
            base.Gerer();
            Application.Current.Dispatcher.Invoke(() =>
            {

                message.textMessage.Text = "     Des Etats Interdits pour R et S dans la bascule RST";
            });
        }
    }
    /// <summary>
    /// Lexception levée dans la bascule à cause des états interdits Preset = 0 et Clear = 0
    /// </summary>
    public class PresetClearException : BasculeExceptions
    {
        public PresetClearException(InputOutput io1, InputOutput io2, Canvas p) : base(io1, io2, p) { }

        public override void Gerer()
        {
            base.Gerer();
            Application.Current.Dispatcher.Invoke(() =>
            {
                message.textMessage.Text = "    Des Etats Interdits pour preset et clear dans une des bascules";
            });

        }

    }
}
