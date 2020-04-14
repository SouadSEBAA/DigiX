using logisimConsole;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
//**********

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour InputOutput.xaml
    /// </summary>
    public partial class InputOutput : UserControl
    {
        protected int ID;
        protected Disposition dispo = Disposition.left;
        public InputOutput(int ID, Disposition disposi)
        {
            InitializeComponent();
            this.ID = ID;
            this.dispo = disposi;
        }
        public InputOutput(bool isInput)
        {
            InitializeComponent();

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

        private void cercle_Drop(object sender, DragEventArgs e)
        {


        }
    }
}