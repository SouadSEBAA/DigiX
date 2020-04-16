using logisimConsole;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Shapes;

using System;


namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour InputOutput.xaml
    /// </summary>
    public partial class InputOutput : UserControl
    {
        protected String etiquette;
        protected int ID;
        protected Disposition dispo = Disposition.left;
        protected bool etat;
        public InputOutput(int ID, Disposition disposi)
        {
            InitializeComponent();
            this.ID = ID;
            this.dispo = disposi;
        }
        public bool GetIsInput() { return IsInput; }
        protected bool IsInput;
        public InputOutput(bool isInput)
        {
            InitializeComponent();
            this.IsInput = isInput;
        }
        public InputOutput() { InitializeComponent(); }
        public void setDispo(Disposition dispo) { this.dispo = dispo; }
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

        public void setEtat(bool etat)
        {
            this.etat = etat;
        }
        public bool getEtat()
        {
            return this.etat;
        }

        public bool isEtat() { return this.etat; }
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