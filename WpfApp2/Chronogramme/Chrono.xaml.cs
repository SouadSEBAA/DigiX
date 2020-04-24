using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Chronogramme.xaml
    /// </summary>
    public partial class Chrono : Window
    {
        private ObservableDataSource<MeasureModel> dataSource = new ObservableDataSource<MeasureModel>();
        TimeSpanAxis d = new TimeSpanAxis();

        private DispatcherTimer timer = new DispatcherTimer();
        System.Threading.Thread thread;
        Stopwatch watch;
        int a = 10;
        System.Timers.Timer t;
        Task task;
        double xMin;
        double startXMax;
        double startYMin = 0;
        double startYMax = 2;
        InputOutput io;


        public Chrono( InputOutput io, String etiquette)
        {
            InitializeComponent();
            p.Children.Remove(p.MouseNavigation);
            p.Children.Remove(p.HorizontalAxisNavigation);
            p.Children.Remove(p.VerticalAxisNavigation);
            p.Children.Remove(p.KeyboardNavigation);


            // p.Viewport.PanningState = Viewport2DPanningState.NotPanning;

            //Controler les axes
            //d.AddToPlotter(p);
            xMin = d.ConvertToDouble(new TimeSpan(0));
            startXMax = d.ConvertToDouble(new TimeSpan(0, 0, a));
            p.Visible = new Rect { X = xMin, Width = startXMax - xMin, Y = startYMin, Height = startYMax - startYMin };
            p.AxisGrid.Visibility = Visibility.Hidden;
            p.MainHorizontalAxis.Visibility = Visibility.Hidden;
            p.MainVerticalAxis.Visibility = Visibility.Hidden;
            dataSource.SetXMapping(model => d.ConvertToDouble(model.interval));
            dataSource.SetYMapping((model) => { if (model.Value == true) return 1; else return 0; });

            //Bind the data with the graph
            chrono.DataSource = dataSource;

            //Labels
            Legend.SetDescription(chrono, etiquette);

            //thread = new Thread(new ThreadStart(timer_Tick));
            //thread.Start();
            //timer.Tick += timer_Tick;
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            //timer.Start();

            //TaskFactory tf = new TaskFactory();
            //Task.Factory.StartNew(timer_Tick);
            //t = new System.Timers.Timer(1);
            //t.Elapsed += timer_Tick;
            //t.Start();
            //task = new Task(new Action(timer_Tick));
            //task.Start();
            this.io = io;
            this.watch = new Stopwatch();
            DataContext = this;

            Closing += Quit;

        }

        void timer_Tick(object sender, EventArgs e)
        {
            lock (watch)
            {
                lock (io)
                {
                    // Application.Current.Dispatcher.Invoke(() =>
                    // {
                    dataSource.AppendAsync(Dispatcher, new MeasureModel
                    {
                        interval = watch.Elapsed,
                        Value = io.getEtat(),
                    });
                    //});
                }
            }
        }

        void timer_Tick(object sender)
        {
            while (true)
            {
                Thread.Sleep(1);

                dataSource.AppendAsync(p.Dispatcher, new MeasureModel
                {
                    interval = watch.Elapsed,
                    Value = io.getEtat()
                }); ;

            }

        }

        bool Stop = false;
        void timer_Tick()
        {
            while (MainWindow.isChrono && !Stop)
            {
                Thread.Sleep(1);

                dataSource.AppendAsync(p.Dispatcher, new MeasureModel
                {
                    interval = watch.Elapsed,
                    Value = io.getEtat(),
                });
            }
        }

        public void NextClick(Object sender, RoutedEventArgs e)
        {
            xMin += d.ConvertToDouble(new TimeSpan(0, 0, a));
            startXMax += d.ConvertToDouble(new TimeSpan(0, 0, a));
            p.Visible = new Rect { X = xMin, Width = startXMax - xMin, Y = startYMin, Height = startYMax - startYMin };
        }
        public void PreviousClick(Object sender, RoutedEventArgs e)
        {
            xMin -= d.ConvertToDouble(new TimeSpan(0, 0, a));
            startXMax -= d.ConvertToDouble(new TimeSpan(0, 0, a));
            p.Visible = new Rect { X = xMin, Width = startXMax - xMin, Y = startYMin, Height = startYMax - startYMin };
        }

        public void StartClick(Object sender, RoutedEventArgs e)
        {
            Stop = false;
            watch.Start();
            Task.Factory.StartNew(timer_Tick);
            ContinueButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            StartButton.IsEnabled = false;

            xMin = d.ConvertToDouble(watch.Elapsed);
            startXMax = d.ConvertToDouble(watch.Elapsed.Add(new TimeSpan(0,0,a)));
            p.Visible = new Rect { X = xMin, Width = startXMax - xMin, Y = startYMin, Height = startYMax - startYMin };

        }


        public void StopClick(Object sender, RoutedEventArgs e)
        {
            Stop = true;
            watch.Stop();
            StopButton.IsEnabled = false;
            ContinueButton.IsEnabled = true;
        }

        public void Continuer(Object sender, RoutedEventArgs e)
        {
            Stop = false;
            watch.Start();
            Task.Factory.StartNew(timer_Tick);
            ContinueButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        public void AfficherCacherAxes(Object sender, RoutedEventArgs e)
        {
            if (!p.AxisGrid.Visibility.Equals(Visibility.Visible))
            {
                p.AxisGrid.Visibility = Visibility.Visible;
                p.MainHorizontalAxis.Visibility = Visibility.Visible;
                p.MainVerticalAxis.Visibility = Visibility.Visible;
            }
            else
            {
                p.AxisGrid.Visibility = Visibility.Hidden;
                p.MainHorizontalAxis.Visibility = Visibility.Hidden;
                p.MainVerticalAxis.Visibility = Visibility.Hidden;
            }

        }

        public void Quit(Object sender, CancelEventArgs e)
        {
            MainWindow.isChrono = false;
            Stop = true;
            watch.Stop();
        }

        public class MeasureModel
        {
            public TimeSpan interval { get; set; }
            public bool Value { get; set; }
        }

    }
}
