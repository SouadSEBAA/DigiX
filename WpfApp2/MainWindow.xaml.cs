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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double x, y;
        Point mousePos;
        int grid_gap = 20;
        int GRID_SIZE;
        Polyline Linelist = new Polyline();
        PointCollection pointcollection = new PointCollection();
        private bool isDrawing;
        List<UIElement> list = new List<UIElement>();
        Line line = null;

        RadioButton chkSnapToGrid = new RadioButton();
        public MainWindow()
        {
            InitializeComponent();
            chkSnapToGrid.IsChecked = true;


        }

        private void simuler_click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("hello");
           
        }

        private void aide_click(object sender, RoutedEventArgs e)
        {

        }

        private void sauvegarde_click(object sender, RoutedEventArgs e)
        {

        }

        private void SnapToGrid(ref double x, ref double y)
        {
            x = grid_gap * (double)Math.Round((double)x / grid_gap);
            y = grid_gap * (double)Math.Round((double)y / grid_gap);
        }
        private void StartDraw(object sender, MouseButtonEventArgs e)
        {
            mousePos = e.GetPosition(content);
            //            UIElement obj = e.Source as FrameworkElement;

            //            Point point = e.GetPosition(myWindow);
            //                //obj.TranslatePoint(new Point(0, 0), content);
            //            text.Text = point.X + "  " + point.Y; ;


            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDrawing = true;

                line = new Line();
                line.StrokeThickness = 3;
                line.Stroke = Brushes.Black;

                x = mousePos.X;
                y = mousePos.Y;
                SnapToGrid(ref x, ref y);
                //                /*
                line.X1 = x/* e.GetPosition(content).X*/;
                line.Y1 = y/*e.GetPosition(content).Y;*/;
                //                pointcollection.Add(point);
                //                Linelist.Points.Add(point);
                content.Children.Add(line);
            }
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && isDrawing)
            {
                mousePos = e.GetPosition(myWindow);
                //            //text.Text = mousePos.X + "  " + mousePos.Y;
                x = mousePos.X;
                y = mousePos.Y;
                SnapToGrid(ref x, ref y);

                var bind1 = new Binding();
                bind1.Source = x;

                var bind2 = new Binding();
                bind2.Source = y;

                line.SetBinding(Line.X2Property, bind1);
                line.SetBinding(Line.Y2Property, bind2);
            }
        }

        private void MouseReleased(object sender, MouseButtonEventArgs e)
        {
            mousePos = e.GetPosition(content);
            if (isDrawing)
            {
                isDrawing = false;
                FrameworkElement source = e.OriginalSource as FrameworkElement;
                Point point = source.TranslatePoint(new Point(), content);
                x = mousePos.X;
                y = mousePos.Y;
                SnapToGrid(ref x, ref y);

                //            text.Text = point.X + "  " + point.Y;
                //                //MessageBox.Show(point.ToString());
                //                Linelist.Points.Add(mousePos);
            }
        }

        private void Cursor(object sender, MouseEventArgs e)
        {
            //        e.MouseDevice.OverrideCursor = Cursors.Hand;
        }

        private void CursorInverse(object sender, MouseEventArgs e)
        {
            //        e.MouseDevice.OverrideCursor = Cursors.Arrow;
        }

        private void MouseRelaesed(object sender, MouseButtonEventArgs e)
        {
            //        if (isDrawing)
            //        {
            //            isDrawing = false;
            //            FrameworkElement source = e.Source as FrameworkElement;
            //            Point point = source.TranslatePoint(new Point(0, 0), myWindow);
            //            text.Text = point.X + "  " + point.Y;
            //        }
        }


    }
}
