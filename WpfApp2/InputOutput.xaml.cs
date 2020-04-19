using logisimConsole;
using System.Windows;
using System.Windows.Controls;
<<<<<<< HEAD
=======
using System.Windows.Media;
>>>>>>> 604e371295169631ba4b66d42fe46c4e08821ce7
using System.Windows.Input;
using System.Windows.Shapes;

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
<<<<<<< HEAD

        public void SetID(int n) { this.ID = n; }//i added this to set the input/outputs id as the specific index of classentree/sortie
        public int GetID() { return ID; }
=======
        protected bool IsInput;
>>>>>>> 604e371295169631ba4b66d42fe46c4e08821ce7

        public InputOutput(int ID, Disposition disposi)
        {
            InitializeComponent();
            this.ID = ID;
            this.dispo = disposi;
            this.MouseDoubleClick += MouseClick;
        }

        public bool GetIsInput() { return IsInput; }
<<<<<<< HEAD
        protected bool IsInput;

=======
>>>>>>> 604e371295169631ba4b66d42fe46c4e08821ce7
        public InputOutput(bool isInput)
        {
            InitializeComponent();
            this.IsInput = isInput;
        }
<<<<<<< HEAD

        public InputOutput() { InitializeComponent(); }
        public void setDispo(Disposition dispo) { this.dispo = dispo; }
=======
        public InputOutput() { InitializeComponent(); etat = false;  }
    public void setDispo(Disposition dispo) { this.dispo = dispo; }
>>>>>>> 604e371295169631ba4b66d42fe46c4e08821ce7
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
            this.etat = etat;
            if (etat == true)
                elSelector.Fill = Brushes.Red;
            else
                elSelector.Fill = Brushes.Black;
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


        /*
        private void elSelector_MouseMove(object sender, MouseEventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            But
            toolTip1.SetToolTip();
            // Force the ToolTip text to be displayed whether or not the form is active.
            /*toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(e.Source,this.etiquette);*


        }*/
    }
}