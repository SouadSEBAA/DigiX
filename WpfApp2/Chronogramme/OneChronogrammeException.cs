﻿using System;
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
        Panel panel;

        public OneChronogrammeException(Panel panel) { this.panel = panel; }
        public OneChronogrammeException() { this.panel = null; }

        // Pour lever l'exception
        public void Gerer(String s)
        {
            ChronogrammeException message = new ChronogrammeException();
            message.textMessage.Text = s;
            message.Opacity = 0.5;
            message.MouseDown += Close;

            panel.Children.Add(message);
        }

        // Lorsque l'utilisateur clique sur le boutton "Close x", se dernier se ferme 
        public void Close(object sender, MouseEventArgs e)
        {
            panel.Children.Remove((ChronogrammeException)sender);
        }

    }
}
