using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Noyau;
using QuickGraph;
using System.ComponentModel;

namespace TableVerite
{
    /// <summary>
    /// Logique d'interaction pour TableVerites.xaml
    /// </summary>
    public partial class TableVerites : Window
    {

        private BidirectionalGraph<Outils, Edge<Outils>> circuit;

        public TableVerites(BidirectionalGraph<Outils, Edge<Outils>> graph)
        {
            this.circuit = graph;
            InitializeComponent();

        }


        private void graph_Click(object sender, RoutedEventArgs e)
        {
            TT_start();
        }

        private void TT_start()
        {
            DataTable dt = new DataTable();
            int nbrPinEntree = 0;
            int nbrDiodSortie = 0;
            string ch1 = "Noyau.PinOut";
            string ch2 = "Noyau.PinIn";
            List<Outils> PinEntreeLLC = new List<Outils>();
            List<Outils> DiodSortieLLC = new List<Outils>();

            foreach (Outils noeud in circuit.Vertices)
            {
                if (noeud.GetType().ToString().CompareTo(ch1) == 0)
                {
                    nbrDiodSortie++;
                    DiodSortieLLC.Add(noeud);
                }
                else
                {
                    if (noeud.GetType().ToString().CompareTo(ch2) == 0)
                    {
                        nbrPinEntree++;
                        PinEntreeLLC.Add(noeud);
                    }
                }
            }

            int numberOfVariables = nbrPinEntree;
            int biggestvalue = Convert.ToInt32(Math.Pow(2, numberOfVariables)) - 1;
            int biggestDigitLength = Convert.ToString(biggestvalue, 2).Length;


            int cpt = 1;
            //Creating the columns des PinIns
            foreach (Outils elmnt in circuit.Vertices)
            {
                if (elmnt.GetType().ToString().CompareTo(ch2) == 0)
                {
                    string nom = elmnt.getname();
                    dt.Columns.Add(new DataColumn(nom, typeof(string)));
                    cpt++;
                }

            }

            cpt = 1;
            //Creating the columns des PinOut
            foreach (Outils elmnt in circuit.Vertices)
            {
                if (elmnt.GetType().ToString().CompareTo(ch1) == 0)
                {
                    DataColumn output = new DataColumn(elmnt.getname());
                    dt.Columns.Add(output);
                    cpt++;
                }

            }

            for (int i = 0; i < Math.Pow(2, numberOfVariables); i++)
            {
                string binary = Convert.ToString(i, 2);

                //Pour remplir les bits forts non atteints avec des 0 ,indiquants false
                binary = binary.PadLeft(biggestDigitLength, '0');

                bool[] binaryExpression = binary.Select(c => c == '1').ToArray();

                DataRow inputRow = dt.NewRow();

                //Add
                for (int j = 0; j < binaryExpression.Length; j++)
                {
                    if (binaryExpression[j] == true)
                    {
                        inputRow[j] = "True" ;
                    }
                    else if (binaryExpression[j] == false)
                    {
                        inputRow[j] = "False";
                    }
                }

                int l = 0;
                foreach (Outils elmnt in circuit.Vertices)
                {
                    if (elmnt.GetType().ToString().CompareTo(ch2) == 0)
                    {
                        elmnt.getEntreeSpecifique(0).setEtat(binaryExpression[l]);
                        l++;
                    }
                }
                
                CircuitPersonnalise essaie = new CircuitPersonnalise(circuit);
                essaie.EvaluateCircuit();
                l = 0;

                //La recuperation des etats des diodes apres la simulation
                foreach (Outils elmnt in circuit.Vertices)
                {
                    if (elmnt.GetType().ToString().CompareTo(ch1) == 0)
                    {
                        inputRow[binaryExpression.Length + l] = elmnt.getEntreeSpecifique(0).getEtat().ToString();
                        l++;
                    }
                }
                dt.Rows.Add(inputRow);

            }

            tVerite.ItemsSource = dt.DefaultView;
        }

        #region TopBar
        private void minimize_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void top_Bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
    }
}

