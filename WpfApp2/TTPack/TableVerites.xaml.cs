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
            InitialiseTable();
        }


        private void InitialiseTable()
        {
            
            this.Width = 700;
            FlowDocument oDoc = new FlowDocument();



            // Create the Table object instance

            Table oTable = new Table();

            // Append the table to the document

            oDoc.Blocks.Add(oTable);

            oTable.CellSpacing = 20;
            oTable.Background = Brushes.Red;



            

            //int numberOfColumns = 5;

            //get number of columns (calcul nbr des pin d'entree et sortie)
            IEnumerable<Outils> lst = circuit.Vertices;
            List<Outils> liste = lst.ToList();
            int nbrPinEntree = 0;
            int nbrDiodSortie = 0;
            string ch1 = "WpfApp2.Noyau.PinOut";
            string ch2 = "WpfApp2.Noyau.PinIn";
            List<Outils> PinEntreeLLC = new List<Outils>();
            List<Outils> DiodSortieLLC = new List<Outils>();
            foreach (Outils noeud in liste)
            {
                if (noeud.GetType().ToString().CompareTo(ch1) == 0)
                {
                    Console.WriteLine("il existe une sortie diode,il s'appelle : "+noeud.getname());
                    nbrDiodSortie++;
                    DiodSortieLLC.Add(noeud);
                }
                else
                {
                    if (noeud.GetType().ToString().CompareTo(ch2) == 0)
                    {
                        Console.WriteLine("il existe un Pin entreeil s'appelle : " + noeud.getname());
                        nbrPinEntree++;
                        PinEntreeLLC.Add(noeud);
                    }
                }
            }

            Console.WriteLine("il esxiste :  " + nbrDiodSortie + "Sorties   " + nbrPinEntree + "entrees ");
            int numberOfColumns = nbrPinEntree+nbrDiodSortie;
            //string max = Console.ReadLine();
            //string min = Console.ReadLine();
            //string equidistance = Console.ReadLine();


            // Create and add an empty TableRowGroup Rows.

            oTable.RowGroups.Add(new TableRowGroup());
            // Add the table head row.

            oTable.RowGroups[0].Rows.Add(new TableRow());

            //Configure the table head row

            TableRow currentRow = oTable.RowGroups[0].Rows[0];

            currentRow.Background = System.Windows.Media.Brushes.Navy;

            currentRow.Foreground = System.Windows.Media.Brushes.White;



            for (int x = 0; x < numberOfColumns; x++)

            {

                oTable.Columns.Add(new TableColumn());
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("sdf"))));

                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("sdfsdf"))));

                oTable.Columns[x].Width = new GridLength(130);

            }

            /*
            // Add the header row with content,

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Maximooom"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("pdfgp"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Min"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Equidistance"))));
            */




            //Add Libya data row

            oTable.RowGroups[0].Rows.Add(new TableRow());

            currentRow = oTable.RowGroups[0].Rows[1];

            //Configure the row layout

            currentRow.Background = System.Windows.Media.Brushes.White;

            currentRow.Foreground = System.Windows.Media.Brushes.Navy;

            //Add the country name in the first cell

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("200"))));

            //Add the country flag in the second cell


            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("100"))));

            //  Paragraph oParagraph0 = new Paragraph();



            // currentRow.Cells.Add(new TableCell(oParagraph0));

            //Add the calling code

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("50"))));

            //Add the internet TLD



            //Add the Time Zone




            //Add Tunisia data row

            oTable.RowGroups[0].Rows.Add(new TableRow());

            currentRow = oTable.RowGroups[0].Rows[2];

            //Configure the row layout

            currentRow.Background = System.Windows.Media.Brushes.Azure;

            currentRow.Foreground = System.Windows.Media.Brushes.Navy;

            //Add the country name in the first cell


            this.Content = oDoc;
        }
    }
}
