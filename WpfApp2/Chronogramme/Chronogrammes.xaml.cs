using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Diagnostics;
using logisimConsole;

namespace WpfApp2.Chronogramme
{
    /// <summary>
    /// Logique d'interaction pour Chronogrammes.xaml
    /// </summary>
    public partial class Chronogrammes : UserControl
    {
        Stopwatch watch = new Stopwatch();
        bool IsReading = true;

        public Chronogrammes()
        {
            InitializeComponent();
            watch.Start();
            DataContext = this;
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            if (IsReading)
                foreach (StepLine item in ChronoStack.Children)
                {
                    item.NextClick(5000); //avancer avec 5 secondes
                }
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            if (IsReading)
                foreach (StepLine item in ChronoStack.Children)
                {
                    item.PrevClick();//Retourner avec 5 secondes
                }
        }
        private void StopClick(object sender, RoutedEventArgs e)
        {
            IsReading = false;
            foreach (StepLine item in ChronoStack.Children)
            {
                item.InjectStopOnClick();
            }
            PreviousButton.IsEnabled = true;
            NextButton.IsEnabled = true;
            ContinueButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }
        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            foreach (StepLine item in ChronoStack.Children)
            {
                item.Quit();
            }
        }

        TaskFactory tf = new TaskFactory();
        private void Ajouter(object sender, RoutedEventArgs e)
        {
            //Ajouter un chronogramme
            //ChronoStack.Children.Add(new StepLine(watch, new Horloge(1000, 500, tf), tf));
        }

        private void AfficherAxe(object sender, RoutedEventArgs e)
        {
            foreach (StepLine item in ChronoStack.Children)
            {
                item.AfficherAxe();
            }
        }

        private void Continuer(object sender, RoutedEventArgs e)
        {
            IsReading = true;
            foreach (StepLine item in ChronoStack.Children)
            {
                item.InjectStopOnClick();
            }
            PreviousButton.IsEnabled = false;
            NextButton.IsEnabled = false;
            ContinueButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }
    }
}


