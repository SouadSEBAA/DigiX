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
using WpfApp2.Noyau;
using WpfApp2.Chronogramme;
using Microsoft.Win32;
using Path = System.IO.Path;
using System.Xml.Serialization;
using System.Collections;
using System.Windows.Markup;
using System.Xml;
using WpfApp2.TTPack;
using System.Windows.Controls.Primitives;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    [Serializable]
    public partial class MainWindow : Window, System.Collections.IEnumerable
    {
        int gridGap = 10;
        CircuitPersonnalise circuit;

        public MainWindow()
        {
            InitializeComponent();
            circuit = new CircuitPersonnalise();
            ///test sequentiel
            //T
            /*
             T basculeT = new T(); circuit.AddComponent(basculeT);
             basculeT.getEntreeSpecifique(3).setEtat(true);//T
             basculeT.getEntreeSpecifique(2).setEtat(true);//Clr
             basculeT.getEntreeSpecifique(1).setEtat(true);//Pr
             //D
             D basculeD = new D(); circuit.AddComponent(basculeD);
             basculeD.getEntreeSpecifique(3).setEtat(true);//T
             basculeD.getEntreeSpecifique(2).setEtat(true);//Clr
             basculeD.getEntreeSpecifique(1).setEtat(true);//Pr
             //Et
             ET et = new ET();circuit.AddComponent(et);
             //horloge
             Horloge horloge = new Horloge();circuit.AddComponent(horloge);
             horloge.circuit = circuit;
             //relation
             circuit.Relate(horloge, basculeT, 0, 0);
             circuit.Relate(horloge, basculeD, 0, 0);
             circuit.Relate(basculeT, et, 0, 0);
             circuit.Relate(basculeD, et, 0, 1);
             horloge.fin = et;
             horloge.Demmarer();*/
            //seriaisation 

            Grille.AddHandler(Gate.MAJwiresEvent, new RoutedEventHandler(Redraw2));
            Grille.AddHandler(Wire.SuppwireEvent, new RoutedEventHandler(Supp_Wire));
            Grille.AddHandler(InputOutput.SupprimerWireEvent, new RoutedEventHandler(SupprimerWire));
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
        Wire line = null;
        Point mousePos;
        protected List<Wire> Wires = new List<Wire>();
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
                    line = new Wire(io.TranslatePoint(new Point(5, 5), Grille), gate, io);
                    Panel.SetZIndex(line, -2);
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
                line.EndPoint = mousePos;
            }

        }

        private void MouseLeftButtonReleased(object sender, MouseButtonEventArgs e)
        {
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
                        if (!line.Connect(IO.TranslatePoint(new Point(5, 5), Grille), gate, IO, circuit))
                        {
                            Grille.Children.Remove(line);
                            break;
                        }
                        else
                        {
                            Wires.Add(line);
                        }
                    }

                }
                if (target == false)
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
        // Pour redessiner les wires
        /*****************************************************************************/

        public void Redraw()
        {
            if (Wires != null)
            {
                foreach (Wire wire in Wires)
                {
                    wire.StartPoint = wire.io1.TranslatePoint(new Point(5, 5), Grille);
                    wire.EndPoint = wire.io2.TranslatePoint(new Point(5, 5), Grille);
                }
            }
        }

        public void Redraw2(object sender, RoutedEventArgs e)
        {
            Redraw();
            e.Handled = true;
        }

        /******************************************************************************/
        // Pour 
        /*****************************************************************************/


        private void SupprimerWire(object sender, RoutedEventArgs e)
        {
            InputOutput inputOutput = (InputOutput)e.OriginalSource;
            foreach (Wire wire in Wires)
            {
                if (wire.io1.Equals(inputOutput) || wire.io2.Equals(inputOutput))
                    wire.Supprimer();
            }
            e.Handled = true;
        }

        /******************************************************************************/
        //Pour supprimer un wire
        /******************************************************************************/
        public void Supp_Wire(object sender, RoutedEventArgs e)
        {
            Grille.Children.Remove((Wire)e.Source);
            e.Handled = true;
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
        private void TVClick(object sender, RoutedEventArgs e)
        {
            //Chronogrammes chronoPage = new Chronogrammes();
            //Chronogrammes.Children.Add(chronoPage);
            TableVerites tv = new TableVerites(circuit.GetCircuit());
            tv.Show();

        }

        /*****************/
        //drag and drop 

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e); Console.WriteLine("mouse4");
            e.Effects = DragDropEffects.All;
            e.Handled = true;

            Redraw();
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
            if (!gate.added) {
                Grille.Children.Add(gate);
                gate.added = true;

                circuit.AddComponent(gate.GetOutil()); //to add our dragged and dropped component to our graph in order to manipulate its edges and vertices
            }
            e.Handled = true;
        }


        private void Grille_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;

            //Console.WriteLine("Ecrit");
            Gate gate = (Gate)e.Data.GetData("Object");
            this.circuit.AddComponent(gate.outil);
            //Set the dropped shape's X(Canvas.LeftProperty) and Y(Canvas.TopProperty) values.
            gate.currentPoint = e.GetPosition(Grille);
            gate.transform.X += (gate.currentPoint.X - gate.anchorPoint.X);
            gate.transform.Y += (gate.currentPoint.Y - gate.anchorPoint.Y);
            gate.RenderTransform = gate.transform;
            gate.anchorPoint = gate.currentPoint;
            // Grille.Children.Add(gate);
        }




        //************************************FOR THE MENU BUTTONS**********************************************// 

        private void aide_click(object sender, RoutedEventArgs e)
        {
            //takes us to our main help site
            string path = @".\..\..\..\HelpSite\home.html"; // C:/Users/username/Documents (or whatever directory)
            System.Diagnostics.Process.Start(path);
        }


       //Fonction elements de sortie version 2
        public void Last_Elements() 
        {
            circuit.SetCompFinaux(new List<Outils>()); //so that each time it does the job all over again  for our circuit

            foreach (var vertex in circuit.GetCircuit().Vertices)
            {
                if (vertex is PinOut || circuit.GetCircuit().IsOutEdgesEmpty(vertex))
                { 
                //list_element_de_sortie.Add(vertex);
                circuit.GetCompFinaux().Add(vertex);
                }
                
            }
            foreach (Outils o in circuit.GetCompFinaux()) Console.WriteLine(o);
        }



        private void simuler_click(object sender, RoutedEventArgs e)
        {
            circuit.setSimulation(false);
            //jimin
            Last_Elements(); //idk if this is needed based on what has been done below
            //souad
            //circuit.Evaluate(circuit.getCircuit().Vertices.Last());
            //melissa
            circuit.EvaluateCircuit();
            circuit.setSimulation(true);
        }


        private void open_tut(object sender, RoutedEventArgs e)
        {
            //takes us directly to the tutorial page
            string path = @".\..\..\..\HelpSite\tuto.html"; // C:/Users/username/Documents (or whatever directory)
            System.Diagnostics.Process.Start(path);
        }


        private void open_file(object sender, RoutedEventArgs e)
        {      
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension
            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            
            if (result == true)
            {
                Grille.Children.Clear();

                string filename = dlg.FileName;
                Canvas canvas = DeSerializeXAML(filename) as Canvas; 
                // Add all child elements (lines, rectangles etc) to canvas
                while (canvas.Children.Count > 0)
                {
                    UIElement obj = canvas.Children[0]; // Get next child
                    canvas.Children.Remove(obj); // Have to disconnect it from result before we can add it
                    Grille.Children.Add(obj); // Add to canvas
                }
            }
        }


        /* public static void DeSerializeXAML(UIElementCollection elements, string filename)
         {
             var context = System.Windows.Markup.XamlReader.GetWpfSchemaContext();
             var settings = new System.Xaml.XamlObjectWriterSettings
             {
                 RootObjectInstance = elements
             };

             using (var reader = new System.Xaml.XamlXmlReader(filename))
             using (var writer = new System.Xaml.XamlObjectWriter(context, settings))
             {
                 System.Xaml.XamlServices.Transform(reader, writer);
             }
         }*/


        public static Canvas DeSerializeXAML(string filename)
        {
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                // return System.Windows.Markup.XamlReader.Load(fs) as UIElementCollection;
                StringReader strreader = new StringReader(mystrXAML);
                XmlReader xmlReader = XmlReader.Create(strreader);
                Canvas readerLoadChildren = (Canvas)XamlReader.Load(xmlReader);
                return (readerLoadChildren);
            }
        }

        private void sauvegarde_click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "UIElement File"; // Default file name
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                //string filename = dlg.FileName;
                //SerializeToXAML(Grille.Children, filename);
                SerializeToXML(Grille, dlg.FileName);
            }
        }


        public static string mystrXAML;
        public static void SetStr(string s) { mystrXAML = s; }

        // Serializes any UIElement object to XAML using a given filename. // function version 2
        public static void SerializeToXML(Canvas canvas, string filename)
        {
            mystrXAML = XamlWriter.Save(canvas);
            SetStr(mystrXAML);
            FileStream filestream = File.Create(filename);
            StreamWriter streamwriter = new StreamWriter(filestream);
            streamwriter.Write(mystrXAML);
            streamwriter.Close();
            filestream.Close();
        }


       
        // Serializes any UIElement object to XAML using a given filename. //Function version 1
        public static void SerializeToXAML(UIElementCollection elements, string filename) //UIElementCollection elements
        {
            // Use XamlWriter object to serialize element
            string strXAML = System.Windows.Markup.XamlWriter.Save(elements);

            // Write XAML to file. Use 'using' so objects are disposed of properly.
            using (System.IO.FileStream fs = System.IO.File.Create(filename))
            {
                using (System.IO.StreamWriter streamwriter = new System.IO.StreamWriter(fs))
                {
                    streamwriter.Write(strXAML);
                }
            }
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
            //MessageBox.Show("Votre Capture d'ecran a ete enregistree.", "Capture D'ecran", MessageBoxButton.OK, MessageBoxImage.Asterisk);
           
            
           
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //For the top bar
        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void normal_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;  
        }

        private void maximize_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        //**************************************END OF MENU BUTTONS*******************************//



    }
}
