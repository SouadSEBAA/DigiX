using logisimConsole;
using System.Windows.Controls;
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

    }
}