using Noyau;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Threading;
using System;
using System.ComponentModel;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour InputOutput.xaml
    /// </summary>
    /// 
    public partial class InputOutput : UserControl
    {

        public String etiquette { get; set; }
        protected int ID;
        protected Disposition dispo = Disposition.left;
        protected bool etat;
        protected bool IsInput;


        public InputOutput(String etiq, int ID, Disposition disposi)
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
        }

        public InputOutput() { InitializeComponent(); etat = false; }

        private void MouseOver(object sender, System.Windows.Input.MouseEventArgs e)
        {
            my_label.Content = this.GetEtiquette();
            this.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void MouseLeft(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }


        public void stopbutton() { elSelector.Fill = Brushes.Black; }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            if (IsInput)
                setEtat(!this.etat);

        }

        /***************************************************************/
        // Pour supprimer un wire lors de la suppression d'une entree
        /***************************************************************/

        public void Supprimer()
        {
            RaiseEvent(new RoutedEventArgs(SupprimerWireEvent));
        }

        // Pour supprimer un 
        public static readonly RoutedEvent SupprimerWireEvent = EventManager.RegisterRoutedEvent("SupprimerWire", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(InputOutput));

        public event RoutedEventHandler SupprimerWire
        {
            add { AddHandler(SupprimerWireEvent, value); }
            remove { RemoveHandler(SupprimerWireEvent, value); }
        }

        #region Getters/Setters

        virtual public void setEtat(bool etat)
        {
            this.etat = etat;

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (etat == true)
                    elSelector.Fill = Brushes.Green;
                else
                    elSelector.Fill = Brushes.Red;

            });

        }

        public bool GetIsInput() { return IsInput; }
        public String GetEtiquette() { return etiquette; }

        public void setDispo(Disposition dispo) { this.dispo = dispo; }


        public Disposition GetDisposition() { return dispo; }

                public bool getEtat()
        {
            return this.etat;
        }

        public bool isEtat() { return this.etat; }

        public String getEtiquette() { return etiquette; }


        #endregion

    }
}