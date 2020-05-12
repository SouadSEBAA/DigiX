using logisimConsole;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2.Chronogramme
{
    /// <summary>
    /// Logique d'interaction pour EssaiChrono.xaml
    /// </summary>
    public partial class EssaiChrono : UserControl
    {
        public ObservableDataSource<MeasureModel> dataSource = new ObservableDataSource<MeasureModel>();
        TimeSpanAxis d = new TimeSpanAxis();
        
        int a = 16;
        double xMin;
        double startXMax;
        double startYMin = -0.06;
        double startYMax = 2;
        public InputOutput io { get; set; }

        bool Stop = true;

        public EssaiChrono(InputOutput io)
        {
            InitializeComponent();
            p.Children.Remove(p.MouseNavigation);
            p.Children.Remove(p.HorizontalAxisNavigation);
            p.Children.Remove(p.VerticalAxisNavigation);
            p.Children.Remove(p.KeyboardNavigation);

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
            if (io.Parent != null)
                Legend.SetDescription(chrono, io.getEtiquette() + "_" + (((io.Parent as Grid).Parent as Canvas).Parent as Gate).outil.getLabel());

            this.io = io;
            Stop = true;
            DataContext = this;

        }

        void timer_Tick()
        {
            while (Chronogrammes.IsReading && (Chronogrammes.watch.Elapsed.CompareTo(Chronogrammes.ts) < 0) && !Stop)
            {
                lock (Chronogrammes.watch)
                {
                    lock (io)
                    {

                        Thread.Sleep(1);
                        
                        dataSource.AppendAsync(Application.Current.Dispatcher, new MeasureModel
                        {
                            interval = Chronogrammes.watch.Elapsed,
                            Value = io.getEtat(),
                        });
                    }
                }
            }
        }

        public void NextClick()
        {
            xMin += d.ConvertToDouble(new TimeSpan(0, 0, a));
            startXMax += d.ConvertToDouble(new TimeSpan(0, 0, a));
            p.Visible = new Rect { X = xMin, Width = startXMax - xMin, Y = startYMin, Height = startYMax - startYMin };
        }
        public void PreviousClick()
        {
            xMin -= d.ConvertToDouble(new TimeSpan(0, 0, a));
            startXMax -= d.ConvertToDouble(new TimeSpan(0, 0, a));
            p.Visible = new Rect { X = xMin, Width = startXMax - xMin, Y = startYMin, Height = startYMax - startYMin };
        }

        public void StartClick()
        {
            Stop = false;

            Task.Factory.StartNew(timer_Tick);

            xMin = d.ConvertToDouble(Chronogrammes.watch.Elapsed);
            startXMax = d.ConvertToDouble(Chronogrammes.watch.Elapsed.Add(new TimeSpan(0, 0, a)));
            p.Visible = new Rect { X = xMin, Width = startXMax - xMin, Y = startYMin, Height = startYMax - startYMin };

        }


        public void stop()
        {
            Stop = true;
        }

        public void Continuer()
        {
            Stop = false;
            Task.Factory.StartNew(timer_Tick);
        }

        public void AfficherCacherAxes()
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

        public class MeasureModel
        {
            public TimeSpan interval { get; set; }
            public bool Value { get; set; }
        }

        public void Quit_Click(object sender, RoutedEventArgs e)
        {
            Stop = true;
        }
    }
}
