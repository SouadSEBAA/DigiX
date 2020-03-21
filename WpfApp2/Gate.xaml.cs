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

            T t = new T("e", Disposition.down);

            path.StrokeThickness = 2;
            path.Stroke = Brushes.Black;
            path.Data = StreamGeometry.Parse(ph);

            OutilShape.Children.Add(path);

            //GateElement.Height = 30;
            //GateElement.Width = 30;
            //TO-DO :  Width and Height proportionnels à la grille (y compris les entrees et sorties)
        }

        public Gate(Outils outil, String type, RectangleGeometry rec)
        {   //in this case the height and width of the boxes are already defined 60*70
                InitializeComponent();
                //the text that defines the type of gate 
                var text = new TextBlock
                {
                    Text = type,
                    TextAlignment = TextAlignment.Center,
                };
                Canvas.SetLeft(text, 25); //aligning the text somewhat inside the box
                Canvas.SetTop(text, 20);
                OutilShape.Children.Add(text);
               //end of text
                path.Stroke = Brushes.Black;
                path.StrokeThickness = 2;
                path.Data = rec;
                OutilShape.Children.Add(path);  
        }

    }


    //la partie portes logiques
   class et : Gate
   {
        public et(PorteLogique et, String path) : base(et, "M 17,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15") { }
   }

    class ou : Gate 
    {
        public ou(PorteLogique ou, String path) : base(ou, "M 15,17 h 10 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -10 c 5,-10 5,-20 0,-30") { }
    }

    class non : Gate 
    {
        public non(PorteLogique non,String path) : base(non, "M 15,17 v 30 l 30,-15 l -30,-15 M 46,33.5 a 3,3 1 1 1 0.1,0.1") { }
    }

    //la partie Combinatoires
    class Add_C : Gate
    {
        public Add_C(CircCombinatoire addcom, String type = null, RectangleGeometry rec = null) : base(addcom, "ADD_C", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 60, 70))) { } //the 25/2 is to position the box somewhat inside our gate control
    }
    class Add_N : Gate
    {
        public Add_N(CircCombinatoire addnbit, String type = null, RectangleGeometry rec = null) : base(addnbit, "ADD_N", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 60, 70))) { } //the 25/2 is to position the box somewhat inside our gate control
    }
    class D_Add : Gate
    {
        public D_Add(CircCombinatoire demiadd, String type = null, RectangleGeometry rec = null) : base(demiadd, "D_ADD", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 60, 70))) { } //the 25/2 is to position the box somewhat inside our gate control
    }

    //la partie sequentielle
    class Compteur : Gate 
    {
        public Compteur(CircSequentielle com,String type = null, RectangleGeometry rec = null) : base(com,"COMPT", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 60, 70))) { }
    }
    class Horl : Gate
    {
        public Horl(Horloge h, String type = null, RectangleGeometry rec = null) : base(h, "H", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 30, 30))) { }
    }
    class Jk : Gate
    {
        public Jk(Bascule b, String type = null, RectangleGeometry rec = null) : base(b, "J", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 30, 30))) 
        {
            var Text = new TextBlock { Text = "K" };
            Canvas.SetLeft(Text, 25); //aligning the text somewhat inside the box
            Canvas.SetTop(Text, 60);
            OutilShape.Children.Add(Text);
        }
    }
    class Rst : Gate
    {
        public Rst(Bascule b, String type = null, RectangleGeometry rec = null) : base(b, "R", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 30, 30)))
        {
            var Text = new TextBlock { Text = "S" };
            Canvas.SetLeft(Text, 25); //aligning the text somewhat inside the box
            Canvas.SetTop(Text, 60);
            OutilShape.Children.Add(Text);
        }
    }
    class Reg : Gate
    {
        public Reg(CircSequentielle reg, String type = null, RectangleGeometry rec = null) : base(reg, "RegDec", new RectangleGeometry(new Rect(25 / 2, 25 / 2, 30, 30))){}
    }
}
