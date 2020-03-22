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
using System.Windows.Navigation;
using System.Windows.Shapes;
using logisimConsole;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Gate.xaml
    /// </summary>
    public partial class Gate : UserControl
    {
        Path path = new Path();

        public Gate(Outils outil, String ph)
        {
            InitializeComponent();

            path.StrokeThickness = 2;
            path.Stroke = Brushes.Black;
            path.Data = StreamGeometry.Parse(ph);

            OutilShape.Children.Add(path);

            //Pour effectuer des essais faut initialiser Width et Height
            //Outil.Height = 100;
            //Outil.Width = 100;
            //TO-DO :  Width and Height proportionnels à la grille (y compris les entrees et sorties)
        }
    }
}
