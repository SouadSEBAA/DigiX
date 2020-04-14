using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp2.Chronogramme;
using Microsoft.Win32;
using Path = System.IO.Path;



namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int gridGap = 10;
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


        private Point SnapToGrid(double x, double y)
        {
            x = gridGap * (double)Math.Round((double)x / gridGap);
            y = gridGap * (double)Math.Round((double)y / gridGap);

            return new Point(x, y);
        }


        //Liaison
        /*****************************************************************/
        private bool isDrawing;
        Line line = null;
        Point mousePos;
        //pour verifier le type des entrees/sorties
        bool entry1;
        bool entry2;

        public void MouseLeftButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (!isDrawing)
            {
                Gate gate = (Gate)e.Source;
                InputOutput io = null;

                foreach (InputOutput IO in gate.InputOutputs)
                {
                    if (IO.IsMouseOver)
                    {
                        io = IO;
                        break;
                    }
                }
                if (io != null)
                {

                    isDrawing = true;

                    line = new Line();
                    line.StrokeThickness = 2.5;
                    line.Stroke = Brushes.Black;

                    Point p = io.TranslatePoint(new Point(5, 5), Grille);
                    line.X1 = p.X;
                    line.Y1 = p.Y;

                    line.X2 = p.X;
                    line.Y2 = p.Y;
                    Panel.SetZIndex(line, -2);
                    //tells us if its an input or output
                    entry1 = io.GetIsInput();
                    Console.WriteLine("entry1:" + entry1); //to make sure it works *success*
                    Grille.Children.Add(line);
                }
            }
            e.Handled = true;
        }


        private void MouseMoved(object sender, MouseEventArgs e)
        {
            mousePos = e.GetPosition(Grille);
            //mousePos = SnapToGrid(mousePos.X, mousePos.Y);
            if (isDrawing && e.LeftButton == MouseButtonState.Pressed)
            {
                var bind1 = new Binding();
                bind1.Source = mousePos.X ;

                var bind2 = new Binding();
                bind2.Source = mousePos.Y ;

                line.SetBinding(Line.X2Property, bind1);
                line.SetBinding(Line.Y2Property, bind2);
            }

        }

        private void MouseLeftButtonReleased(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaa");
            if (isDrawing)
            {
                bool target = false;
                isDrawing = false;
                Gate gate = (Gate)sender;
                foreach (InputOutput IO in gate.InputOutputs)
                {
                    if (IO.IsMouseOver)
                    {
                        target = true;
                        Point p = IO.TranslatePoint(new Point(5, 5), Grille);
                        line.X2 = p.X;
                        line.Y2 = p.Y;
                        entry2 = IO.GetIsInput(); //gets the type of input
                        Console.WriteLine("entry2:" + entry2);
                        break;
                    }

                }

                if (target == false || entry1.Equals(entry2) == true)
                    Grille.Children.Remove(line);
            }
        
            e.Handled = true;
        }

    private void MouseReleased(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing )
            {
                isDrawing = false;
                Grille.Children.Remove(line);
            }
        }

        /******************************************************************************/

        //Chronogrammes
        /******************************************************************************/
        private void ChronogrammesClick(object sender, RoutedEventArgs e)
        {
            Chronogrammes chronoPage = new Chronogrammes();
            //Chronogrammes.Children.Add(chronoPage);
        }
        /******************************************************************************/



        /*****************/
        //drag and drop 

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e); Console.WriteLine("mouse4");
            e.Effects = DragDropEffects.All;


            e.Handled = true;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            e.Effects = DragDropEffects.All;


            //e.Effects = DragDropEffects.None;
            Console.WriteLine("mouse111");

            e.Handled = true;
        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            e.Effects = DragDropEffects.All;


        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);
            //Si la grille contient l'element in le supprime 
            // Undo the preview that was applied in OnDragEnter.

        }
        //nos controleurs de Drag &Drop 
        private void Grille_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            Gate gate = (Gate)e.Data.GetData("Object");
            gate.currentPoint = e.GetPosition(Grille);
            gate.transform.X += gate.currentPoint.X - gate.anchorPoint.X;
            gate.transform.Y += (gate.currentPoint.Y - gate.anchorPoint.Y);
            gate.RenderTransform = gate.transform;
            gate.anchorPoint = gate.currentPoint;

            //Liaison
            gate.MouseLeftButtonDown += new MouseButtonEventHandler( MouseLeftButtonPressed);
            gate.MouseLeftButtonUp += new MouseButtonEventHandler(MouseLeftButtonReleased);
            /*******/

            if (!gate.added) { Grille.Children.Add(gate); gate.added = true; }
            e.Handled = true;
        }


        private void Grille_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;

            Console.WriteLine("Ecrit");
            Gate gate = (Gate)e.Data.GetData("Object");

            //Set the dropped shape's X(Canvas.LeftProperty) and Y(Canvas.TopProperty) values.
            gate.currentPoint = e.GetPosition(Grille);
            gate.transform.X += (gate.currentPoint.X - gate.anchorPoint.X);
            gate.transform.Y += (gate.currentPoint.Y - gate.anchorPoint.Y);
            gate.RenderTransform = gate.transform;
            gate.anchorPoint = gate.currentPoint;
            // Grille.Children.Add(gate);
        }


        //FOR THE MENU BUTTONS 
        private void aide_click(object sender, RoutedEventArgs e)
        {
            //takes us to our main help site
            string path = @".\..\..\..\HelpSite\home.html"; // C:/Users/username/Documents (or whatever directory)
            System.Diagnostics.Process.Start(path);
        }

        private void simuler_click(object sender, RoutedEventArgs e)
        {
            //needs some work
        }

        private void open_tut(object sender, RoutedEventArgs e)
        {
            //takes us directly to the tutorial page
            string path = @".\..\..\..\HelpSite\tuto.html"; // C:/Users/username/Documents (or whatever directory)
            System.Diagnostics.Process.Start(path);
        }

        private void open_file(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                MessageBox.Show(openFileDlg.FileName, "Fichier Ouvert", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void sauvegarde_click(object sender, RoutedEventArgs e)
        {

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text documents (.txt) | *.txt |Binary files (.bin) | *.bin"  //for the time being we will keep it at .txt until we agree on the extension to be used
            };
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == true)
            {
                var fileName = saveFileDialog.FileName;
            }
            MessageBox.Show("Votre fichier a ete sauvegarder.", "Sauvegarder", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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

            //to inform theuser that the screeshot was created successfully
            MessageBox.Show("Votre Capture d'ecran a ete enregistree.", "Capture D'ecran", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        //END OF MENU BUTTONS



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
