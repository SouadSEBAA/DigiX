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
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    [Serializable]
    public partial class MainWindow : Window/*, System.Collections.IEnumerable*/
    {
        int gridGap = 20;
        CircuitPersonnalise circuit;
        List<Wire> Wires;

        public MainWindow()
        {
            InitializeComponent();
            circuit = new CircuitPersonnalise();
            Wires = new List<Wire>();

            //Suppression d'un Gate
            Grille.AddHandler(Gate.DeletingGateEvent, new RoutedEventHandler(Supprimer));

        }


        private Point SnapToGrid(double x, double y)
        {
            x = gridGap * (double)Math.Round((double)x / gridGap);
            y = gridGap * (double)Math.Round((double)y / gridGap);

            return new Point(x, y);
        }

        //Suppression d'un outil graphiquement et en noyau
        /***********************************************************/
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            
            circuit.DeleteComponent(((Gate)e.Source).outil);
            foreach(Wire wire in RecupererWires((Gate)e.Source))
            {
                Grille.Children.Remove(wire); 
            }
            Grille.Children.Remove(((Gate)e.Source));
            e.Handled = true;
        }

        private List<Wire> RecupererWires(Gate gate)
        {
            List<Wire> list = new List<Wire>();
            foreach(Wire wire in Wires)
            {
                if (wire.gateEnd.Equals(gate) || wire.gateStart.Equals(gate))
                    list.Add(wire);
            }
            return list;
        }
        /*************************************************************/


        //Liaison
        /*****************************************************************/
        private bool isDrawing;
        Wire line = null;
        Point mousePos;

        public void MouseLeftButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (!isDrawing && !circuit.getSimulation())
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
                    mousePosPrec = io.TranslatePoint(new Point(5, 5), Grille);
                    Panel.SetZIndex(line, -2);
                    Grille.Children.Add(line);
                }
            }
            e.Handled = true;
        }

        Point mousePosPrec;
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
                        }
                        else
                        {
                            Wires.Add(line);
                        }
                        break;
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


        //Chronogrammes
        /******************************************************************************/
        private void ChronogrammesClick(object sender, RoutedEventArgs e)
        {
            if (!Chronogrammes.isOneChrono)
            {
                Chronogrammes chronoPage = new Chronogrammes(circuit);
                chronoPage.Show();
                chronoPage.Topmost = true;
                chronoPage.Owner = this;
                ChronoButton.IsEnabled = false;
                Chronogrammes.isOneChrono = true;
            }

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
            if (!gate.added) {
                Grille.Children.Add(gate);
                gate.added = true;

                //foreach (var vertex in circuit.GetCircuit().Vertices) { Console.WriteLine(vertex); }
            }
            e.Handled = true;
        }


        private void Grille_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;

            //Console.WriteLine("Ecrit");
            Gate gate = (Gate)e.Data.GetData("Object");
            this.circuit.AddComponent(gate.outil); //to add our dragged and dropped component to our graph in order to manipulate its edges and vertices
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
                circuit.GetCompFinaux().Add(vertex);
                }
                
            }
            foreach (Outils o in circuit.GetCompFinaux()) Console.WriteLine(o);
        }


        
        private void simuler_click(object sender, RoutedEventArgs e)
        {

            //circuit.setSimulation(false);

            //For exceptions
            StackExceptions.Children.Clear();

             Last_Elements(); //idk if this is needed based on what has been done below
            //Vérifier si les éléments sont reliés
            if (circuit.getCircuit().VertexCount != 0)
            if (circuit.getUnrelatedGates().Count != 0 )
            {
                try
                {
                    throw new RelatedException(StackExceptions);
                }
                catch (RelatedException exception)
                {
                    StackExceptions.Children.Clear();
                    exception.Gerer();
                }
            }
            else
            {
                //In order to show the pause/stop buttons --------------------------------------------
                if (pause.Visibility == Visibility.Collapsed) { pause.Visibility = Visibility.Visible; }
                if (stop.Visibility == Visibility.Collapsed) { stop.Visibility = Visibility.Visible; }
                //-----------------------------------------------------------------------------------

                //melissa
                circuit.setSimulation(true);
                Tools.IsEnabled = false;
                circuit.EvaluateCircuit();
               //
            }
            
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
                string filename = dlg.FileName;
                DeSerializeXAML(filename);
            }
        }

        public void DeSerializeXAML(string filename)
        {
            Grille.Children.Clear();//a controler
            XElement root = XElement.Load(filename);
            //Gates
            foreach (XElement gate in root.Element("Gates").Elements())
            {
                Gate abgate = CreateGate(gate);
                Grille.Children.Add(abgate);
                this.circuit.AddComponent(abgate.outil);
                abgate.added = true;
                //gid.Add(int.Parse(gate.Attribute("ID").Value), abgate);
                //l'emplacement
                //anchorpoint
                double x = double.Parse(gate.Element("anchorPoint").Attribute("X").Value);
                double y = double.Parse(gate.Element("anchorPoint").Attribute("Y").Value);
                abgate.anchorPoint = new Point(x, y);
                //transform point 
                x = double.Parse(gate.Element("transform").Attribute("X").Value);
                y = double.Parse(gate.Element("transform").Attribute("Y").Value);
                abgate.transform.X += x;
                abgate.transform.Y += y;
                abgate.RenderTransform = abgate.transform;
                abgate.outil.id = int.Parse(gate.Attribute("ID").Value);

            }
            //Wires 
            foreach (XElement wire in root.Element("Wires").Elements())
            {
                //id
                int idStart = int.Parse(wire.Element("gatestart").Attribute("ID").Value);
                int idEnd = int.Parse(wire.Element("gateend").Attribute("ID").Value);
                //index
                int io1 = int.Parse(wire.Element("gatestart").Attribute("IO").Value);
                int io2 = int.Parse(wire.Element("gateend").Attribute("IO").Value);
                //points
                Console.WriteLine("x: " + wire.Element("endp").Attribute("X").Value);
                Console.WriteLine("y: " + wire.Element("endp").Attribute("Y").Value);
                Point startp = new Point(double.Parse(wire.Element("startp").Attribute("X").Value), double.Parse(wire.Element("startp").Attribute("Y").Value));
                Point endp = new Point(double.Parse(wire.Element("endp").Attribute("X").Value), double.Parse(wire.Element("endp").Attribute("Y").Value));
                InputOutput IN1, IN2;
                //on recupere les gate qui ont ces identifiants
                Gate gatestart = Recup(idStart), gateend = Recup(idEnd);

                if (wire.Element("gatestart").Attribute("Type").Value == "ClasseEntree")
                {
                    IN1 = gatestart.outil.getListeentrees()[io1];
                    IN2 = gateend.outil.getListesorties()[io2];
                }
                else
                {
                    IN1 = gatestart.outil.getListesorties()[io1];
                    IN2 = gateend.outil.getListeentrees()[io2];
                }
                Wire w = new Wire(startp, gatestart, IN1);
                w.Connect(endp, gateend, IN2, this.circuit);
                Grille.Children.Add(w);

            }
        }


        public Gate CreateGate(XElement gate)
        {
            switch (gate.Attribute("Type").Value)
            {
                case "Et":
                    return new Et();
                case "Ou":
                    return new Ou();
                case "Non":
                    return new Non();
                case "Oux":
                    return new Oux();
                case "Nand":
                    return new Nand();
                case "Nor":
                    return new Nor();
                case "Mux":
                    return new Mux();
                case "Demux":
                    return new Demux();
                case "Encod":
                    return new Encod();
                case "Decod":
                    return new Decod();
                case "Add_N":
                    return new Add_N();
                case "Add_C":
                    return new Add_C();
                case "D_Add":
                    return new D_Add();
                case "Cpt":
                    return new Cpt();
                case "horloge":
                    return new horloge();
                case "BasculeD":
                    return new BasculeD();
                case "BasculeJk":
                    return new BasculeJk();
                case "BasculeRst":
                    return new BasculeRst();
                case "BasculeT":
                    return new BasculeT();
                case "Reg":
                    return new Reg();
                case "pin_entree":
                    return new pin_entree();
                case "pin_sortie":
                    return new pin_sortie();

            }
            return null;
        }


        public Gate Recup(int id)
        {
            foreach (UserControl user in Grille.Children)
            {
                if (user is Gate)
                {
                    Gate gate = (Gate)user;
                    if (gate.outil.id == id) { return gate; }
                }
            }
            return null;
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
                string filename = dlg.FileName;
                SerializeToXAML(filename);
                //melissa 

            }
        }

        // Serializes any UIElement object to XAML using a given filename.
        public void SerializeToXAML(string filename)
        {

            XElement grille = new XElement("Grille");//pour contenir tt le circuit 
            XElement gates = new XElement("Gates");//pour contenir les composants
            XElement wires = new XElement("Wires");//pour les wires 
            //parcourire les gates
            foreach (UserControl user in Grille.Children)
            {//on ajoute le nbr d'entrées et sorties 
                if (user is Gate)
                {
                    Gate g = (Gate)user;
                    XElement gt = new XElement("Gate");
                    gt.SetAttributeValue("Type", g.GetType().Name);
                    //********
                    gt.Add(new XElement("anchorPoint"));//position 
                    gt.Element("anchorPoint").SetAttributeValue("X", g.anchorPoint.X);
                    gt.Element("anchorPoint").SetAttributeValue("Y", g.anchorPoint.Y);
                    //****transform 
                    gt.Add(new XElement("transform"));//position 
                    gt.Element("transform").SetAttributeValue("X", g.transform.X);
                    gt.Element("transform").SetAttributeValue("Y", g.transform.Y);
                    //*****nombre d'entrée et de sorties
                    gt.SetAttributeValue("Entree", g.outil.getnbrentrees());
                    gt.SetAttributeValue("Sortie", g.outil.getnbrsoryies());
                    //on ajoute le id pour recréer le circuit 
                    gt.SetAttributeValue("ID", g.outil.id);

                    gates.Add(gt);

                }
                else
                {
                    Wire wire = (Wire)user;
                    XElement w = new XElement("Wire");
                    //start point
                    w.Add(new XElement("startp"));//position 
                    w.Element("startp").SetAttributeValue("X", Convert.ToInt32(wire.StartPoint.X));
                    w.Element("startp").SetAttributeValue("Y", Convert.ToInt32(wire.StartPoint.Y));
                    //end point
                    w.Add(new XElement("endp"));//position 
                    w.Element("endp").SetAttributeValue("X", Convert.ToInt32(wire.EndPoint.X));
                    w.Element("endp").SetAttributeValue("Y", Convert.ToInt32(wire.EndPoint.Y));
                    //****
                    w.Add(new XElement("gatestart"));
                    w.Element("gatestart").SetAttributeValue("ID", wire.gateStart.outil.id);
                    w.Add(new XElement("gateend"));
                    w.Element("gateend").SetAttributeValue("ID", wire.gateEnd.outil.id);
                    if (wire.io1 is ClasseEntree)//io1 c une entrée donc io2 une sortie
                    {
                        if (wire.gateStart.outil.getListeentrees().Contains(wire.io1))//io1 est entrée dans gatestart..io2 sortie dans gateend
                        {
                            w.Element("gatestart").SetAttributeValue("Type", wire.io1.GetType().Name);//le name est entrée 
                            w.Element("gatestart").SetAttributeValue("IO", wire.gateStart.outil.getListeentrees().IndexOf((ClasseEntree)wire.io1));
                            w.Element("gateend").SetAttributeValue("Type", wire.io2.GetType().Name);//le name est entrée 
                            w.Element("gateend").SetAttributeValue("IO", wire.gateEnd.outil.getListesorties().IndexOf((Sortie)wire.io2));
                        }
                        else//io1 entrée dans gateend .. io2 sortie danss gatesstart
                        {
                            w.Element("gatestart").SetAttributeValue("Type", wire.io2.GetType().Name);//le name est entrée 
                            w.Element("gatestart").SetAttributeValue("IO", wire.gateStart.outil.getListesorties().IndexOf((Sortie)wire.io2));
                            w.Element("gateend").SetAttributeValue("Type", wire.io1.GetType().Name);//le name est entrée 
                            w.Element("gateend").SetAttributeValue("IO", wire.gateEnd.outil.getListeentrees().IndexOf((ClasseEntree)wire.io1));
                        }

                    }
                    else//io1 ssortie et io2 entrée
                    {
                        if (wire.gateStart.outil.getListesorties().Contains(wire.io1))//io1 est sortie dans gatestart... io2 entrée dans gateend
                        {
                            w.Element("gatestart").SetAttributeValue("Type", wire.io1.GetType().Name);//le name est sortie
                            w.Element("gatestart").SetAttributeValue("IO", wire.gateStart.outil.getListesorties().IndexOf((Sortie)wire.io1));
                            w.Element("gateend").SetAttributeValue("Type", wire.io2.GetType().Name);//le name est entrée 
                            w.Element("gateend").SetAttributeValue("IO", wire.gateEnd.outil.getListeentrees().IndexOf((ClasseEntree)wire.io2));
                        }
                        else//io1 sortie dans gateend ..io2 entrée dans gatestart
                        {
                            w.Element("gatestart").SetAttributeValue("Type", wire.io2.GetType().Name);//le name est sortie
                            w.Element("gatestart").SetAttributeValue("IO", wire.gateStart.outil.getListeentrees().IndexOf((ClasseEntree)wire.io2));
                            w.Element("gateend").SetAttributeValue("Type", wire.io1.GetType().Name);//le name est entrée 
                            w.Element("gateend").SetAttributeValue("IO", wire.gateEnd.outil.getListesorties().IndexOf((Sortie)wire.io1));
                        }
                    }
                    wires.Add(w);
                }
            }
            //on ajoute wires
            grille.Add(gates);
            grille.Add(wires);
            //on sauvegarde le tt 
            grille.Save(filename);
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
        /*
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }*/

        //For the top bar
        private void close_click(object sender, RoutedEventArgs e)
        {
                foreach (Outils o in circuit.getCircuit().Vertices)
                {
                    if (o is Horloge)
                    {
                        ((Horloge)o).arreter();
                    }
                }
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

        private void pause_click(object sender, RoutedEventArgs e)
        {
            circuit.setSimulation(false);
        }

       

        private void stop_click(object sender, RoutedEventArgs e)
        {
            int i = 0; int j = 0; //need this to make sure it works --on console only--
            Console.WriteLine("-----Stop Button--------");
            circuit.setSimulation(false);

            //Souad
            Tools.IsEnabled = true;

            foreach (Outils o in circuit.getCircuit().Vertices)
            {
                if (o is Horloge) { ((Horloge)o).arreter(); }
                Console.WriteLine("l'outil :"+o);
                foreach (ClasseEntree c_e in o.getListeentrees()) 
                {
                    i++;
                    Console.WriteLine("input number : "+i);
                    //I iterate through each vertice and set its "ClassEntree" anew as if its just being dragged and created again
                    Console.WriteLine("etat avant 'related,etat': " + c_e.getRelated() + "," + c_e.getEtat());
                    //c_e.setRelated(false);
                    c_e.setEtat(false);
                    c_e.stopbutton();
                    Console.WriteLine("etat apres 'related,etat': " + c_e.getRelated() + "," + c_e.getEtat());
                }

                foreach (Sortie s in o.getListesorties())
                {
                    j++;
                    Console.WriteLine("output number : " + j);
                    Console.WriteLine("etat avant"+s.getEtat());
                    s.setEtat(false);
                    s.stopbutton();
                    Console.WriteLine("etat avant:"+s.getEtat());
                }
            }
            foreach (Wire w in Wires)
            {
                w.stopbutton();
            }


            if (pause.Visibility == Visibility.Visible) { pause.Visibility = Visibility.Collapsed; }
            if (stop.Visibility == Visibility.Visible) { stop.Visibility = Visibility.Collapsed; }
            if (clock.Visibility == Visibility.Collapsed) { clock.Visibility = Visibility.Visible; }

        }

        public void reset_clock()
        {
            foreach (Outils o in circuit.getCircuit().Vertices)
            {
                if (o is Horloge)
                {
                    ((Horloge)o).mini();
                }
            }
        }

        
        private void circuit_click(object sender, MouseButtonEventArgs e)
        {
            //for the 'circuit personnalise' button 
            
        }

        private void clock_click(object sender, RoutedEventArgs e)
        {
            reset_clock();
        }

        //**************************************END OF MENU BUTTONS*******************************//

    }
}
