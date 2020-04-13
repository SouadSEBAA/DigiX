using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp2
{
    public class Wire : UserControl
    {

        public Line line;
        Gate gatePrinciple;
        List<Gate> gates;
        Point startPoint;
        Point endPoint;
        bool Value;

        public Wire(Point start, Gate gatePrinciple, Point end )
        {
            line = new Line();
            line.StrokeThickness = 2.5;
            line.Stroke = Brushes.Black;
            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;
            endPoint = end;

             var bind1 = new Binding();
            bind1.Source = endPoint.X;

            var bind2 = new Binding();
            bind2.Source = endPoint.Y;

            line.SetBinding(Line.X2Property, bind1);
            line.SetBinding(Line.Y2Property, bind2);


            this.gatePrinciple = gatePrinciple;
            Value = false;

        }

        
    }
}
