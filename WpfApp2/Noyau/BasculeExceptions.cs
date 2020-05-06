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

namespace logisimConsole
{
    public class BasculeExceptions : Exception
    {
        static List<ExceptionMessage> set; //Avec une seule capacité
        protected InputOutput io1, io2; protected Canvas panel;
        protected ExceptionMessage message = new ExceptionMessage();

        public BasculeExceptions(InputOutput io1, InputOutput io2, Canvas p) { this.io1 = io1; this.io2 = io2; panel = p; if (set == null) set = new List<ExceptionMessage>(1); }
        public virtual void Gerer()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                io1.elSelector.Fill = Brushes.Orange; io2.elSelector.Fill = Brushes.Orange;
            });
            message.canvas.Background = Brushes.Orange;
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

    public class RSTException : BasculeExceptions
    {
        public RSTException(InputOutput io1, InputOutput io2, Canvas p) : base(io1, io2, p) { }
        public override void Gerer()
        {
        
            base.Gerer();
            message.textMessage.Text = "     Des Etats Interdits pour preset et clear dans la bascule RST";
        }
    }
    public class PresetClearException : BasculeExceptions
    {
        public PresetClearException(InputOutput io1, InputOutput io2, Canvas p) : base(io1, io2, p) { }

        public override void Gerer()
        {
            base.Gerer();
            message.textMessage.Text = "    Des Etats Interdits pour preset et clear dans une des bascules";

        }

    }
}
