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
using System.Windows.Navigation;
using logisimConsole;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Wire.xaml
    /// </summary>
    public partial class Wire : UserControl
    {

        /******* En cas on pense à utiliser a grille pour la liaison (vertical et horizontal) ****
        public PathFigure fil;
        Dictionary<PolyLineSegment, Gate> existingWires;//Ts les fils dérivés
        ***************************************/
        public LineSegment _ls;
        private PathFigure _fil;
        public Gate gateStart;//début du fil
        InputOutput io1, io2;
        public Gate gateEnd;
        private bool _value;

        public Wire(Point start, Gate gatePrinciple, InputOutput input)
        {
            InitializeComponent();

            _fil = new PathFigure();
            _ls = new LineSegment();
            _fil.IsClosed = false;
            StartPoint = start;
            EndPoint = start;
            //PolyLineSegment pls = new PolyLineSegment(); pls.Points.Add(start); existingWires.Add(pls, null);
            //fil.Segments.Add(pls);

            _fil.Segments.Add(_ls);

            wire.Data = new PathGeometry(new PathFigure[]{ _fil });

            this.io1 = input;
            this.gateStart = gatePrinciple;
            Value = false;
            DataContext = this;
        }

        public bool Connect(Point end, Gate gate, InputOutput io2, CircuitPersonnalise circuit)
        {
            gateEnd = gate;
            _ls.Point = end;
            this.io2 = io2;
            if (end.Equals(_fil.StartPoint) == true || io1.GetIsInput() == io2.GetIsInput() || io1.getEtat() != io2.getEtat())
                return false;
            else
<<<<<<< HEAD
             circuit.Relate(gateStart.GetOutil(), gateEnd.GetOutil(),io1.GetID(), io2.GetID());
            //circuit.Relate(gateStart.GetOutil(), gateEnd.GetOutil(), io1, io2);
            Console.WriteLine(circuit.Relate(gateStart.GetOutil(), gateEnd.GetOutil(), io1.GetID(), io2.GetID()));
=======
            {
                //Relate kernel
                if (io1 is ClasseEntree)
                    circuit.Relate(gateStart.GetOutil(), gateEnd.GetOutil(), (Sortie) io2, (ClasseEntree) io1);
                else
                    circuit.Relate(gateStart.GetOutil(), gateEnd.GetOutil(), (Sortie)io1, (ClasseEntree)io2);
>>>>>>> 604e371295169631ba4b66d42fe46c4e08821ce7
                return true;
            }
        }

        public Point StartPoint 
        { 
            get { return _fil.StartPoint; }
            set { _fil.StartPoint = value;  }
        }


        public Point EndPoint
        {
            get { return _ls.Point;}
            set {_ls.Point = value;}
        }

        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (value == true)
                    wire.Stroke = Brushes.Red; //définir la couleur
                else
                    wire.Stroke = Brushes.Black;
            }
        }

    }
}
