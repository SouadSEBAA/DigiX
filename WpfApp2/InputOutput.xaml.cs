using logisimConsole;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Threading;
using System;


namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour InputOutput.xaml
    /// </summary>
    /// 
    [Serializable]
    public partial class InputOutput : UserControl
    {

        public String etiquette { get; set; }
        protected int ID;
        protected Disposition dispo = Disposition.left;
        protected bool etat;
        protected bool IsInput;


        public InputOutput(String etiq,int ID, Disposition disposi)
        {
            InitializeComponent();
            this.etiquette = etiq;
            this.ID = ID;
            this.dispo = disposi;
            this.MouseDoubleClick += MouseClick;
        }

        public InputOutput(int ID, Disposition disposi)
        {
            InitializeComponent();
            this.ID = ID;
            this.dispo = disposi;
            this.MouseDoubleClick += MouseClick;
            // this.MouseEnter += MouseOver2; // for our labels
        }


        public bool GetIsInput() { return IsInput; }
        public String GetEtiquette() { return etiquette; }
        public void SetEtiquette(String e) { etiquette = e; }


        public InputOutput(bool isInput)
        {
            InitializeComponent();
            this.IsInput = isInput;
        }


        public void setDispo(Disposition dispo) { this.dispo = dispo; }

        public InputOutput() 
        { 
            InitializeComponent(); etat = false;
        }

        public Disposition GetDisposition() { return dispo; }

        //****************la liaison
        private void cercle_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                Line line = new Line();

                line.StrokeThickness = 2;
                line.Stroke = System.Windows.Media.Brushes.Black;

                //la ligne à copier 
                data.SetData("Object", line);
                data.SetData("String", "inputoutput");
                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.All);

            }
        }


        private void MouseOver(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //for the labels
            my_label.Content = this.GetEtiquette();
            this.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void MouseLeft(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }


        virtual public void setEtat(bool etat)
        {
            this.etat = etat;

            //exception aprés a fermeture dde la fenetre à regler(une tache annulée)
            //System.Threading.Tasks.TaskCanceledException : 'Une tâche a été annulée.'
            //this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,new Action( ()=>
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Code causing the exception or requires UI thread access
                if (etat == true)
                    elSelector.Fill = Brushes.Green;
                else
                    elSelector.Fill = Brushes.Red;  
            });       
        }

        public void stopbutton() { elSelector.Fill = Brushes.Black; }

        public bool getEtat()
        {
            return this.etat;
        }
        
        public bool isEtat() { return this.etat; }

        public String getEtiquette() { return etiquette; }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            if (IsInput)
                setEtat(!this.etat);
        }

        //Chronogrammes
        /**************************************************************************************/

        private void AfficherChronogrammeClick(Object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ChronogrammeEvent));
        }

        public static readonly RoutedEvent ChronogrammeEvent = EventManager.RegisterRoutedEvent(
            "AfficherChronogramme", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Gate));

        public event RoutedEventHandler AfficherChronogramme
        {
            add { AddHandler(ChronogrammeEvent, value); }
            remove { RemoveHandler(ChronogrammeEvent, value); }
        }


        /***************************************************************************************/
    }
}