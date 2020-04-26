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
using WpfApp2.Noyau;
using logisimConsole;
using LiveCharts;

namespace WpfApp2.Chronogramme
{
    /// <summary>
    /// Logique d'interaction pour Chronogrammes.xaml
    /// </summary>
    public partial class Chronogrammes : UserControl
    {
        //Stopwatch watch = new Stopwatch();
        bool IsReading = true;

        CircuitPersonnalise circuit;

        //public List<StepLine> SignauxList { get; set; }
        Dictionary<InputOutput, ChartValues<MeasureModel>> Pairs;
        Dictionary<int, InputOutput> ForMatrix;

        public List<Outils> OutilEntreeList;
        public List<Outils> OutilSortieList;
        List<InputOutput> IoOutList = new List<InputOutput>();
        List<InputOutput> IoInList = new List<InputOutput>();

        public int SignalsCount;
        public int StatesCount;
        private bool[,] matrice;


        public Chronogrammes(CircuitPersonnalise circ)
        {
            InitializeComponent();

            this.circuit = circ.Clone() as CircuitPersonnalise;
            Pairs = new Dictionary<InputOutput, ChartValues<MeasureModel>>();
            ForMatrix = new Dictionary<int, InputOutput>();

            //SignauxList = new List<StepLine>();
            OutilEntreeList = circuit.StartComponents();
            OutilSortieList = circuit.EndComponents();

            IoOutList = new List<InputOutput>();
            IoInList = new List<InputOutput>();

            /*
            foreach (var outil in OutilEntreeList)
            {
                int i = 0;
                if (!(outil is PinIn || outil is Horloge))
                    IoInList.AddRange(outil.getListeentrees()); 
                else
                    IoInList.AddRange(outil.getListesorties());
            }
            foreach (var IoIn in IoInList)
            {
                Pairs.Add(IoIn, new ChartValues<MeasureModel>());
            }

            //à supprimer
            foreach (var IoIn in IoInList)
            {
                if (IoIn.getEtiquette() == "Clock")
                {
                    IoInList.Remove(IoIn);
                    IoInList.Add(IoIn); //put it last
                    break;
                }
            }
            */


            foreach (var outil in OutilSortieList)
            {
                if (!(outil is PinOut))
                    IoOutList.AddRange(outil.getListesorties());
                else
                    IoOutList.AddRange(outil.getListeentrees());
            }
            foreach (var IoOut in IoOutList)
            {
                IoOut.setEtat(false);
                Pairs.Add(IoOut, new ChartValues<MeasureModel>());
            }

            AssignerForMatrix(0);

            SignalsCount = IoInList.Count;
            StatesCount = (int)Math.Pow(2, SignalsCount);

            //Déssiner la matrice
            matrice = Matice();

            Evaluate();

            
            foreach (var entry in Pairs.Reverse())
            {
                ChronoStack.Children.Add(new StepLine(entry.Value, entry.Key.getEtiquette()));
            }

            // watch.Start();
        }

        private void Evaluate()
        {
            InputOutput io; ChartValues<MeasureModel> chV;
            for(int j = 0; j<StatesCount+1; j++)
            {
                for(int i = 0; i< SignalsCount; i++)
                {
                    ForMatrix.TryGetValue(i, out io);
                    io.setEtat(matrice[j, i]);
                    Pairs.TryGetValue(io, out chV);
                    Console.WriteLine("tryget : " + io.getEtat() + "etiq : " + io.getEtiquette());
                    chV.Add(new MeasureModel { io = io.getEtat() , stateNum = j });
                }
                circuit.EvaluateCircuit();
                foreach(var IO in IoOutList)
                {
                    Pairs.TryGetValue(IO, out chV);
                    chV.Add(new MeasureModel { io = IO.getEtat(), stateNum = j });
                }
            }
        }

        private bool[,] Matice()
        {
            int a = StatesCount / 2; //celle qu'on va divisr sur
            bool[,] m = new bool[StatesCount + 1, SignalsCount];

            int j = 0;
            for (int i = 0; i< SignalsCount; i++)
            {
                int cpt = 0;
                for (j=0; j<StatesCount; j++)
                {
                    if (cpt < a)
                        m[j, i] = false;
                    else
                        m[j, i] = true;
                    cpt++;
                    if (cpt == 2 * a)
                        cpt = 0;
                }
                a = a / 2;
            }

            for (int k = 0; k<SignalsCount; k++)
            {
                m[StatesCount, k] = m[0, k];
            }
            return m;
        }

        private void AssignerForMatrix()
        {
            int i = 0; int k = 0; InputOutput io =null;
            bool HorlogeFound =false;
            while(i<OutilEntreeList.Count)
            {
                if (OutilEntreeList[i] is Horloge)
                {
                    HorlogeFound = true;
                    io = OutilEntreeList[i].getSortieSpecifique(0); //sortie de l'horloge
                    break;
                }
                i++;
            }
            if (HorlogeFound)
            {
                IoInList.Remove(io);
                IoInList.Add(io); //put it last
            }

            i = 0;
            while (i < SignalsCount)
            {
                ForMatrix.Add(i, IoInList[i]);
                Pairs.Add(IoInList[i], new ChartValues<MeasureModel>());
                i++;
            }
        }

        private void AssignerForMatrix(int w)
        {
            int i = 0; int k = 0; InputOutput io = null;

            SortedDictionary<int, List<InputOutput>> priorities = new SortedDictionary<int, List<InputOutput>>();
            priorities.Add(0, new List<InputOutput>());
            priorities.Add(1, new List<InputOutput>());
            priorities.Add(2, new List<InputOutput>());
            priorities.Add(3, new List<InputOutput>());

            //On donne priorité 0 à : Preset, Clear, Chg, Raz
            //On donne priorité 1 à : Clear, Chg
            //On donne priorité 2 à : Horloge
            //On donne priorité 3 aux autres

            List<InputOutput> l;
            //Assigner priorité
            foreach (var outil in OutilEntreeList)
            {
                foreach (var sortie in outil.getListesorties())
                {
                    if (outil is Horloge)
                    {
                        priorities.TryGetValue(3, out l);
                        l.Add(sortie); break;
                    }
                    else
                    {
                        foreach (var outstruct in sortie.get_OutStruct())
                        {
                            if (outstruct.getOutils() is CircSequentielle)
                            {
                                io = outstruct.getOutils().getEntreeSpecifique(outstruct.getNum_entree());
                                if (io.getEtiquette().Equals("Preset") || io.getEtiquette().Equals("Raz") )
                                {
                                    priorities.TryGetValue(0, out l);
                                    l.Add(sortie);
                                    break;
                                }
                                else if(io.getEtiquette().Equals("Clear") || io.getEtiquette().Equals("Chg"))
                                {
                                    priorities.TryGetValue(1, out l);
                                    l.Add(sortie);
                                    break;
                                }
                                else if(io.getEtiquette().Equals("Clock"))
                                {
                                    priorities.TryGetValue(3, out l);
                                    l.Add(sortie);
                                    break;
                                }
                                else
                                {
                                    priorities.TryGetValue(2, out l);
                                    l.Add(sortie);
                                    break;
                                }
                            }
                            else
                            {
                                priorities.TryGetValue(2, out l);
                                l.Add(sortie);
                                break;
                            }

                        }
                    }
                }
            }

            
            //Remplir les listes des io
            foreach(var entry in priorities)
            {
                IoInList.AddRange(entry.Value);
            }

            Console.WriteLine("nombre : " + IoInList.Count);
            i = 0;
            while (i < IoInList.Count)
            {
                Console.WriteLine(IoInList[i].getEtiquette() + "index in IoInList : "+ i);
                ForMatrix.Add(i, IoInList[i]);
                Pairs.Add(IoInList[i], new ChartValues<MeasureModel>());
                i++;
            }
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
            //ChronoStack.Children.Add(new StepLine(watch, new Horloge(1000, 500), tf));
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

    public class MeasureModel
    {
        public bool io { get; set; }
        public int stateNum { get; set; }
    }

}


