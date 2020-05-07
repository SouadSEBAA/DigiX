using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour AjoutLabel.xaml
    /// </summary>

    [Serializable]
    public class DialogInputEventArgs : EventArgs
    {
        public string Input { get; set; }
    }

    public partial class AjoutLabel : Window
    {
        public event EventHandler<DialogInputEventArgs> InputChanged = delegate { };

        public AjoutLabel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputChanged(this, new DialogInputEventArgs() { Input = this.txtName.Text });
            this.Close();
        }
    }
}
