﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Noyau;
using System.Diagnostics;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Research.DynamicDataDisplay.Common.Auxiliary;

namespace WpfApp2.Chronogramme
{
    /// <summary>
    /// Logique d'interaction pour Chronogrammes.xaml
    /// </summary>
    public partial class Chronogrammes : Window
    {

        CircuitPersonnalise circuit;

        /// <summary>
        /// Indique si les chronogrammes sont entrain de se déssiner
        /// </summary>
        /// <value></value>
        public static bool IsReading { get; set; }

        /// <summary>
        /// Indique si les chronogrammes sont en pause
        /// </summary>
        private bool isPause;

        /// <summary>
        /// Indique s'il y a déjà une fenetre de chronogramme ouverte
        /// </summary>
        /// <value></value>
        public static bool isOneChrono { get; set; }

        /// <summary>
        /// Pour l'axe des x
        /// </summary>
        public static Stopwatch watch;

        DispatcherTimer timer;

        /// <summary>
        /// Indique le temps pendant lequel les chrongrammes vont se déssiner
        /// </summary>
        /// <returns></returns>
        public static TimeSpan ts = new TimeSpan();

        public List<TimeSpan> l_ts { get; set; }
        public List<InputOutput> list { get; set; }
        public TimeSpan STs { get; set; }
        public List<ViewModel> newl { get; set; }

        int ChronoNumber;//Le nombre de chronogrammes affichés

        public Chronogrammes(CircuitPersonnalise circ)
        {
            InitializeComponent();

            this.circuit = circ;
            IsReading = false;
            isPause = false;
            watch = new Stopwatch();
            timer = new DispatcherTimer(DispatcherPriority.Render);

            l_ts = new List<TimeSpan>();
            l_ts.Add(new TimeSpan(0, 0, 5));
            STs = new TimeSpan(0, 0, 16);
            l_ts.Add(STs);
            l_ts.Add(new TimeSpan(0, 0, 32));
            l_ts.Add(new TimeSpan(0, 0, 48));
            l_ts.Add(new TimeSpan(0, 0, 64));
            l_ts.Add(new TimeSpan(0, 0, 128));
            l_ts.Add(new TimeSpan(0, 0, 256));


            newl = new List<ViewModel>();
            foreach (var v in circ.getCircuit().Vertices)
            {
                list = new List<InputOutput>();
                list.AddRange(v.getListesorties());
                list.AddRange(v.getListeentrees());
                ViewModel vm = new ViewModel(v, list);
                newl.Add(vm);
            }

            timer.Tick += StopPlotting;
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(Supprimer));
            this.Closing += Quit;

            DataContext = this;
        }


        private void StopPlotting(object sender, EventArgs e)
        {
            StopClick(null, null);
        }

        /**********************************************************************/
        private void NextClick(object sender, RoutedEventArgs e)
        {
            foreach (EssaiChrono item in ChronoStack.Children)
            {
                item.NextClick(); //avancer avec 16 secondes
            }
        }
        /**********************************************************************/
        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            foreach (EssaiChrono item in ChronoStack.Children)
            {
                item.PreviousClick();//Retourner avec 16 secondes
            }
        }

        /**********************************************************************/
        private void StopClick(object sender, RoutedEventArgs e)
        {
            IsReading = false;
            isPause = false;
            watch.Stop();
            timer.Stop();
            StartContinuerButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Collapsed;
            StopButton.IsEnabled = false;
        }

        /**********************************************************************/
        private void PauseClick(object sender, RoutedEventArgs e)
        {
            IsReading = false;
            isPause = true;
            watch.Stop();
            timer.Stop();
            PauseButton.Visibility = Visibility.Collapsed;
            StartContinuerButton.Visibility = Visibility.Visible;
        }

        /**********************************************************************/
        private void StartContinuerClick(object sender, RoutedEventArgs e)
        {
            if (ChronoNumber > 0)
            {
                IsReading = true;
                ts = (TimeSpan)TimeChrono.SelectedItem;

                if (isPause)
                {
                    timer.Interval = ts.Subtract(watch.Elapsed);
                    watch.Start();
                    foreach (EssaiChrono item in ChronoStack.Children)
                    {
                        item.Continuer();
                    }
                    isPause = false;
                }
                else
                {
                    watch.Restart(); timer.Interval = ts; 
                    Parallel.ForEach(essaiChronos, (EssaiChrono item) =>
                    {
                        item.Dispatcher.BeginInvoke(() =>
                        {
                            method(item);
                        }, DispatcherPriority.Render);
 
                    } );
                   
                    /*foreach (EssaiChrono item in ChronoStack.Children)
                    {
                        item.dataSource.Collection.Clear();
                        item.StartClick();
                    }*/
                    
                }
                timer.Start();
                PauseButton.Visibility = Visibility.Visible;
                StartContinuerButton.Visibility = Visibility.Collapsed;
                StopButton.IsEnabled = true;
                NextButton.IsEnabled = true;
                PreviousButton.IsEnabled = true;
                AxesButton.IsEnabled = true;
            }
        }
        void method (EssaiChrono item)
        {
                item.dataSource.Collection.Clear(); item.data.Clear(); item.StartClick();
        }
        /**********************************************************************/
        private void Quit(object sender, CancelEventArgs e)
        {
            foreach (EssaiChrono item in ChronoStack.Children)
            {
                item.Quit_Click(null, null);
            }
            watch.Stop();
            IsReading = false;
            timer.Stop();
            isPause = false;
            ((MainWindow)this.Owner).ChronoButton.IsEnabled = true;
            isOneChrono = false;
        }
        /**********************************************************************/
        List<MenuItem> IoAdded = new List<MenuItem>();
        int ChronNumerAuthorised = 6;
        List<EssaiChrono> essaiChronos = new List<EssaiChrono>();

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            if (ChronoNumber < ChronNumerAuthorised)
            {
                if (!IsReading)
                {
                    MenuItem mi = e.OriginalSource as MenuItem;
                    InputOutput io = mi.DataContext as InputOutput;
                    mi.IsEnabled = false;
                    IoAdded.Add(mi); EssaiChrono ec = new EssaiChrono(io);
                    ChronoStack.Children.Add(ec); essaiChronos.Add(ec);
                    ChronoNumber++;
                }
                else
                {
                    try
                    {
                        throw new OneChronogrammeException(Errors);
                    }
                    catch (OneChronogrammeException exception)
                    {
                        Errors.Children.Clear();
                        exception.Gerer("Les chronogrammes sont en exécution !");
                    }
                }
            }
            else
            {
                try
                {
                    throw new OneChronogrammeException(Errors);
                }
                catch (OneChronogrammeException exception)
                {
                    Errors.Children.Clear();
                    exception.Gerer("Supprimez un de vos chronogrammes, puis rajoutez un autre !");
                }
            }
        }

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            if (e.Source is EssaiChrono)
            {
                ChronoStack.Children.Remove(e.Source as EssaiChrono);
                foreach (var mi in IoAdded)
                    if (mi.DataContext.Equals((e.Source as EssaiChrono).io))
                    {
                        mi.IsEnabled = true;
                        IoAdded.Remove(mi);
                        break;
                    }
                menu.IsEnabled = true;
            }
            if (IoAdded.Count == 0)
            {
                NextButton.IsEnabled = false;
                PreviousButton.IsEnabled = false;
                AxesButton.IsEnabled = false;
            }
        }

        /**********************************************************************/
        private void AfficherAxe(object sender, RoutedEventArgs e)
        {
            foreach (EssaiChrono item in ChronoStack.Children)
            {
                item.AfficherCacherAxes();
            }
        }
        /**********************************************************************/

        /// <summary>
        /// Pour le menu des signaux
        /// </summary>
        public class ViewModel : UserControl
        {
            public List<InputOutput> SubMenuItems { get; set; }

            public Outils outil { get; set; }

            public String header { get; set; }

            public ViewModel(Outils o, List<InputOutput> l)
            {
                header = o.getLabel();
                outil = o;
                SubMenuItems = l;
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Errors.Children.Clear();
        }

        #region TopBar
        private void top_Bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion
    }

}


