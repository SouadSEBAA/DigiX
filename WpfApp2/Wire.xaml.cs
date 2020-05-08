using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

    [Serializable]
    public partial class Wire : UserControl
    {

        /******* En cas on pense à utiliser a grille pour la liaison (vertical et horizontal) ****
        public PathFigure fil;
        Dictionary<PolyLineSegment, Gate> existingWires;//Ts les fils dérivés
        ***************************************/
        private BezierSegment _ls;
        private PathFigure _fil;
        public Gate gateStart { get; set; }//début du fil
        public InputOutput io1 { get; set; }
        public InputOutput io2 { get; set; }
        public Gate gateEnd { get; set; }
        private bool _value;

        public Wire(Point start, Gate gatePrinciple, InputOutput io)
        {
            InitializeComponent();

            _fil = new PathFigure();
            _ls = new BezierSegment();
            _fil.IsClosed = false;
            StartPoint = start;
            EndPoint = start;
            _ls.Point3 = EndPoint;
            _ls.Point1 = new Point(_fil.StartPoint.X * 0.6 + EndPoint.X * 0.4, _fil.StartPoint.Y);
            _ls.Point2 = new Point(_fil.StartPoint.X * 0.4 + EndPoint.X * 0.6, _fil.StartPoint.Y);

            //PolyLineSegment pls = new PolyLineSegment(); pls.Points.Add(start); existingWires.Add(pls, null);
            //fil.Segments.Add(pls);

            _fil.Segments.Add(_ls);
            var d = new PathGeometry(new PathFigure[] { _fil });
            wire.Data = d;
            outer.Data = d;
            this.io1 = io;
            

            this.gateStart = gatePrinciple;

            Panel.SetZIndex(this, -2);

            DataContext = this;
        }


        public bool Connect(Point end, Gate gate, InputOutput io, CircuitPersonnalise circuit)
        {
            gateEnd = gate;
            Maj();
            EndPoint = end;
            this.io2 = io;

            io1.LayoutUpdated += Redraw;
            io2.LayoutUpdated += Redraw;

            if (end.Equals(_fil.StartPoint) == true || io1.GetIsInput() == io2.GetIsInput() || io1.getEtat() != io2.getEtat())
                return false;
            else
            {
                if (io1 is ClasseEntree)
                {

                    if (!circuit.Relate(gateEnd.GetOutil(), gateStart.GetOutil(), (Sortie)io2, (ClasseEntree)io1))
                        return false;
                    (io2 as Sortie).PropertyChanged += new PropertyChangedEventHandler((sender, e) => { Value = io2.getEtat(); });

                }
                else
                {
                    if (!circuit.Relate(gateStart.GetOutil(), gateEnd.GetOutil(), (Sortie)io1, (ClasseEntree)io2))
                        return false;
                    (io1 as Sortie).PropertyChanged += new PropertyChangedEventHandler((sender, e) => { Value = io1.getEtat(); });

                }
                return true;
            }
        }

        public void Redraw(Object sender, EventArgs e)
        {
            StartPoint = io1.TranslatePoint(new Point(5, 5), (UIElement)gateStart.Parent);
            EndPoint = io2.TranslatePoint(new Point(5, 5), (UIElement)gateStart.Parent);
            Maj();
        }


        void Maj()
        {
            _ls.Point1 = new Point(_fil.StartPoint.X * 0.6 + EndPoint.X * 0.4, _fil.StartPoint.Y);
            _ls.Point2 = new Point(_fil.StartPoint.X * 0.4 + EndPoint.X * 0.6, EndPoint.Y);
        }

        public Point StartPoint 
        { 
            get { return _fil.StartPoint; }
            set { _fil.StartPoint = value; }
        }


        public Point EndPoint
        {
            get { return _ls.Point3;}
            set {_ls.Point3 = value;   }
        }

        public void Supprimer()
        {
            // Supression in kernel
            RaiseEvent(new RoutedEventArgs(SuppwireEvent));

            if (io1 is ClasseEntree)
            {
                ClasseEntree i = (ClasseEntree)io1;
                i.setEtat(false);
                i.setRelated(false);


                Sortie o = (Sortie)io2;
                o.DeleteOustruct(gateStart.outil, gateStart.outil.getListeentrees().IndexOf(i));
            }
            else
            {
                ClasseEntree i = (ClasseEntree)io2;
                i.setEtat(false);
                i.setRelated(false);


                Sortie o = (Sortie)io1;
                o.DeleteOustruct(gateEnd.outil, gateEnd.outil.getListeentrees().IndexOf(i));
            }
        }

        private void Supprimer(object sender, MouseButtonEventArgs e)
        {
            Supprimer();
        }

        public static readonly RoutedEvent SuppwireEvent = EventManager.RegisterRoutedEvent("Suppwire", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Wire));

        public event RoutedEventHandler Suppwire
        {
            add { AddHandler(SuppwireEvent, value); }
            remove { RemoveHandler(SuppwireEvent, value); }
        }

        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (value == true)
                        wire.Stroke = Brushes.Green; //définir la couleur
                    else
                        wire.Stroke = Brushes.Red;
                });
            }
        }

        public void stopbutton() { Value = false; wire.Stroke = Brushes.Black; }
        

    }
}
