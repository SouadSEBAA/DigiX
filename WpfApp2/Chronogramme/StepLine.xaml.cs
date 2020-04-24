using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.ComponentModel;
using LiveCharts;
using LiveCharts.Configurations;
using logisimConsole;
using System.Windows.Threading;

namespace WpfApp2.Chronogramme
{
    /// <summary>
    /// Logique d'interaction pour StepLine.xaml
    /// </summary>
    public partial class StepLine : UserControl, INotifyPropertyChanged
    {
        Horloge horloge; //à remplacer par Object qui définit  a field de type Boolean
        private double _from;
        private double _to;

        public double diff;

        //DispatcherTimer timer = new DispatcherTimer();
        Thread timer;
        Task task;
        Stopwatch watch;

        //public GearedValues<MeasureModel> ChartValues { get; set; }
        public ChartValues<MeasureModel> ChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }
        public bool IsReading { get; set; }

        public StepLine(Stopwatch watch, Horloge h, TaskFactory tf)
        {
            InitializeComponent();

            horloge = h;
            this.watch = watch;
            diff = 8000; //8 seconds
            param = h.getUP();

            //timer = new Thread(new ThreadStart(this.Read));
            //timer.Interval = TimeSpan.FromMilliseconds(100);
            //timer.Tick += Read;
            //timer.Start();

            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");//lets set how to display the X Labels
            AxisStep = TimeSpan.FromMilliseconds(100).Ticks;//AxisStep forces the distance between each separator in the X axis
            AxisUnit = TimeSpan.TicksPerMillisecond;//AxisUnit forces lets the axis know that we are plotting seconds

            var mapper = Mappers.Xy<MeasureModel>()
            .X(model => model.interval.Ticks)   //use DateTime.Ticks as X
            .Y((model) => { if (model.Value == true) return 1; else return 0; });           //use the value property as Y

            Charting.For<MeasureModel>(mapper);//lets save the mapper globally.
            ChartValues = new ChartValues<MeasureModel>();
            //ChartValues = new GearedValues<MeasureModel>();
            line.AlternativeStroke = Brushes.Black;
            line.PointGeometry = null;

            From = 0;
            To = TimeSpan.FromMilliseconds(diff).Ticks;

            IsReading = true;
            DataContext = this;
            timer = new Thread(new ThreadStart(Read));
            
            tf.StartNew(new Action(Read));
            
        }
        double d;
        public int param;
        public void Read(/*object sender, EventArgs eobject state*/)
        {
            while (/*IsReading*/IsReading)
            {
            Thread.Sleep(100);
             d = 0;
           lock (horloge)
            {
            }
            lock (watch)
            {

                    ChartValues.Add(new MeasureModel
                    {
                        interval = watch.Elapsed,
                        Value = horloge.getSortieSpecifique(0).getEtat()
                    }) ;


                //if ((double)watch.ElapsedTicks > To)
                  //NextClick(diff);
            }
            }
        }

        public void NextClick(double d)
        {
            From += TimeSpan.FromMilliseconds(d).Ticks;
            To += TimeSpan.FromMilliseconds(d).Ticks;
            //if (ChartValues.Count > 150) ChartValues.RemoveAt(0);
            //ChartValues.AsEnumerable<MeasureModel>().ToList().RemoveRange(0, ChartValues.Count);
        }

        public void PrevClick()
        {
            From -= TimeSpan.FromMilliseconds(5000).Ticks;
            To -= TimeSpan.FromMilliseconds(5000).Ticks;
        }
        public void InjectStopOnClick()
        {
            IsReading = !IsReading;
            //if (IsReading)
                //timer.Start();
                //Task.Factory.StartNew(Read);
                //ThreadPool.QueueUserWorkItem(Read);
                //else
                //timer.Stop();
                
            
        }
        public void Quit()
        {
            //Dispose the thread ressources
            //Thread.CurrentThread.Interrupt();
            
        }

        public void AfficherAxe()
        {
            XAxes.ShowLabels = true;
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        public class MeasureModel
        {
            public TimeSpan interval { get; set; }
            public bool Value { get; set; }
        }
    }
}


