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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using logisimConsole;
using QuickGraph;
using System.ComponentModel;


namespace WpfApp2.TTPack
{
    /// <summary>
    /// Logique d'interaction pour TableVerites.xaml
    /// </summary>
    public partial class TableVerites : Window
    {

        private BidirectionalGraph<Outils, Edge<Outils>> circuit;

        public TableVerites(BidirectionalGraph<Outils, Edge<Outils>> graph)
        {
            Console.WriteLine("rani hna");
            this.circuit = graph;
            InitializeComponent();
        }


        private void graph_Click(object sender, RoutedEventArgs e)
        {
            //int n = circuit.VertexCount{ get;}

            /*if (circuit.Vertices.Count() == 0) Console.WriteLine("graph is empty");
            else Console.WriteLine("it contains : " + circuit.Vertices.Count());*/
            IEnumerable<Outils> lst = circuit.Vertices;
            List<Outils> liste = lst.ToList();
            foreach (Outils noeud in liste)
            {
                Console.WriteLine(noeud.GetType().ToString());
                //Console.WriteLine(lst.);
                //circuit.

            }
        }
    }
}
