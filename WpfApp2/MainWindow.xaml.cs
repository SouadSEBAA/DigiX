using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point mousePos;
        int gridGap = 10;
        private bool isDrawing;
        Line line = null;
        public MainWindow()
        {
            InitializeComponent();
          /*   //pour essayer les gates creer
             
            List<ClasseEntree> list = new List<ClasseEntree>();
            list.Add(new ClasseEntree(1,Disposition.left,true, true));
            List<Sortie> list_s = new List<Sortie>();
            CircCombinatoire d = new Decodeur(2,2,"lol",Disposition.down);
            Gate g = new Decod(d);
            Grille.Children.Add(g);
            */
            
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

        private Point SnapToGrid( double x,  double y)
        {
            x = gridGap * (double)Math.Round((double)x / gridGap);
            y = gridGap * (double)Math.Round((double)y / gridGap);

            return new Point(x, y);
        }

        private void StartDraw(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDrawing = true;

                line = new Line();
                line.StrokeThickness = 1.5;
                line.Stroke = Brushes.Black;

                line.X1 = mousePos.X;
                line.Y1 = mousePos.Y;

                line.X2 = mousePos.X;
                line.Y2 = mousePos.Y;

                Grille.Children.Add(line);
            }
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            mousePos = e.GetPosition(Grille);
            mousePos = SnapToGrid(mousePos.X, mousePos.Y);
            
            if (e.LeftButton == MouseButtonState.Pressed && isDrawing)
            {
                var bind1 = new Binding();
                bind1.Source = mousePos.X;

                var bind2 = new Binding();
                bind2.Source = mousePos.Y;

                line.SetBinding(Line.X2Property, bind1);
                line.SetBinding(Line.Y2Property, bind2);
            }
        }
     
        private void MouseReleased(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                if (line.X1 == line.X2 && line.Y1 == line.Y2)
                    Grille.Children.Remove(line);
            }
        }

        private void CaptEcran_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("hello");
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            string chemin1 = @".\..\..\..\Capture\";
            string strRes1 = String.Concat(chemin1, finalString);
            string strRes = String.Concat(strRes1, ".png");
            CreateScreenShot(this, strRes);
        }

        private void CreateScreenShot(UIElement visual, string file)
        {
            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));
            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));
            if (double.IsNaN(width) || double.IsNaN(height))
            {
                throw new FormatException("You need to indicate the Width and Height values of the UIElement.");
            }
            RenderTargetBitmap render = new RenderTargetBitmap(
               Convert.ToInt32(width),
               Convert.ToInt32(visual.GetValue(FrameworkElement.HeightProperty)),
               96,
               96,
               PixelFormats.Pbgra32);
            // Indicate which control to render in the image
            render.Render(visual);
            using (FileStream stream = new FileStream(file, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(render));
                encoder.Save(stream);
            }
        }


        //the code below was added while we wanted to add zooming part, but now we will use the scrollviewer ..

        /*
        // Zoom
        private Double zoomMax = 5;
        private Double zoomMin = 0.5;
        private Double zoomSpeed = 0.001;
        private Double zoom = 1;

        // Zoom on Mouse wheel
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
            if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
            if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale

            Point mousePos = e.GetPosition(Grille);

            if (zoom > 1)
            {
                Grille.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
            }
            else
            {
                Grille.RenderTransform = new ScaleTransform(zoom, zoom); // transform Canvas size
            }
        }
        */

    }
}
