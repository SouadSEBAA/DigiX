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

        protected String etiquette;
        protected int ID;
        protected Disposition dispo = Disposition.left;
        protected bool etat;


        public void SetID(int n) { this.ID = n; }//i added this to set the input/outputs id as the specific index of classentree/sortie
        public int GetID() { return ID; }

        protected bool IsInput;


        public InputOutput(int ID, Disposition disposi)
        {
            InitializeComponent();
            this.ID = ID;
            this.dispo = disposi;
            this.MouseDoubleClick += MouseClick;
        }

        public bool GetIsInput() { return IsInput; }



        public InputOutput(bool isInput)
        {
            InitializeComponent();
            this.IsInput = isInput;
        }

     
        public void setDispo(Disposition dispo) { this.dispo = dispo; }

        public InputOutput() { InitializeComponent(); etat = false;  }

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
                data.SetData("Object",line);
                data.SetData("String", "inputoutput");
                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.All);

            }
        }


        private void MouseOver(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void MouseLeft(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        virtual public void setEtat(bool etat)
        {
            //exception aprés a fermeture dde la fenetre à regler(une tache annulée)
            //System.Threading.Tasks.TaskCanceledException : 'Une tâche a été annulée.'

            Application.Current.Dispatcher.Invoke(() =>
            {
                // Code causing the exception or requires UI thread access
                this.etat = etat;
                if (etat == true)
                    elSelector.Fill = Brushes.Red;
                else
                    elSelector.Fill = Brushes.Black;
            });


           
            
           
        }

        public bool getEtat()
        {
            return this.etat;
        }

        public bool isEtat() { return this.etat; }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            if (IsInput)
                setEtat(!this.etat);
        }

    }
}