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
            //InitialiseTable();
            TT_start();
        }

        private void TT_start()
        {
            DataTable dt = new DataTable();
            int nbrPinEntree = 0;
            int nbrDiodSortie = 0;
            string ch1 = "WpfApp2.Noyau.PinOut";
            string ch2 = "WpfApp2.Noyau.PinIn";
            List<Outils> PinEntreeLLC = new List<Outils>();
            List<Outils> DiodSortieLLC = new List<Outils>();

            foreach (Outils noeud in circuit.Vertices)
            {
                if (noeud.GetType().ToString().CompareTo(ch1) == 0)
                {
                    Console.WriteLine("il existe une sortie diode,il s'appelle : " + noeud.getname());
                    nbrDiodSortie++;
                    //LLC.Add(noeud);
                    DiodSortieLLC.Add(noeud);
                }
                else
                {
                    if (noeud.GetType().ToString().CompareTo(ch2) == 0)
                    {
                        Console.WriteLine("il existe un Pin entreeil s'appelle : " + noeud.getname());
                        nbrPinEntree++;
                        //LLC.Add(noeud);
                        PinEntreeLLC.Add(noeud);
                    }
                }
            }

            int numberOfVariables = nbrPinEntree;
            int biggestvalue = Convert.ToInt32(Math.Pow(2, numberOfVariables))-1;
            int biggestDigitLength = Convert.ToString(biggestvalue, 2).Length;
            
           
            /*
            for (int i = 1;i<=numberOfVariables;i++ )
            {
                dt.Columns.Add(new DataColumn(i.ToString(), typeof(string)));
            }*/
            int cpt = 1;
            foreach(Outils elmnt in circuit.Vertices)
            {
                if (elmnt.GetType().ToString().CompareTo(ch2) == 0)
                { 
                    string nom = elmnt.getname() + cpt.ToString();
                    dt.Columns.Add(new DataColumn(nom, typeof(string)));
                    cpt++;
                }
                
            }
            
            cpt = 1;
            foreach (Outils elmnt in circuit.Vertices)
            {
                if (elmnt.GetType().ToString().CompareTo(ch1) == 0)
                {
                    DataColumn output = new DataColumn("output"+cpt.ToString(), typeof(string));
                    dt.Columns.Add(output);
                    cpt++;
                }

            }
            
                //DataColumn output = new DataColumn("output", typeof(string));

                //dt.Columns.Add(output);

            Console.WriteLine("rani kemlthaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            for (int i = 0; i < Math.Pow(2, numberOfVariables); i++)
            {
                string binary = Convert.ToString(i, 2);
                binary = binary.PadLeft(biggestDigitLength, '0');
                bool[] binaryExpression = binary.Select(c => c == '1').ToArray();
               // Console.WriteLine("sdfklefozekfozerkf^zeof           " + binaryExpression[i]);
                DataRow inputRow = dt.NewRow();

                //Add
                for (int j = 0; j < binaryExpression.Length; j++)
                {
                    if (binaryExpression[j] == true)
                    {
                        inputRow[j] = "True"+i.ToString();
                    }
                    else if (binaryExpression[j] == false)
                    {
                        inputRow[j] = "False";
                    }
                }
                Console.WriteLine("*****************************************************************");

                Console.WriteLine("la ligne numero : " + i);


                Console.WriteLine("*****************************************************************");
                int l = 0;
                foreach(Outils elmnt in circuit.Vertices)
                {
                    if (elmnt.GetType().ToString().CompareTo(ch2) == 0)
                    {
                        //Console.Write("fdofkd");
                        int mk = l + 1;
                        elmnt.setSortieSpe(0, binaryExpression[l]);
                        Console.WriteLine("elmnt : " + mk + "etat : " + elmnt.getSortie());
                        l++;
                    }
                }
                Console.WriteLine("*****************************************************************");

                Console.WriteLine("je vais afficher le graph seulement les inputs : ");
                l = 0;
                foreach (Outils elmnt in circuit.Vertices)
                {
                    if (elmnt.GetType().ToString().CompareTo(ch2) == 0)
                    {
                        //Console.Write("fdofkd");
                        int mk = l + 1;
                        //elmnt.setSortieSpe(0, binaryExpression[l]);
                        Console.WriteLine("elmnt : " + mk + "etat : " + elmnt.getSortie());
                        l++;
                    }
                }

                Console.WriteLine("*****************************************************************");

                Console.WriteLine("la valeur maximale du l est  :    " + l);

                //CircuitPersonnalise essaie = new CircuitPersonnalise(circuit);
                //simuler_click(essaie);

               // List<Outils> ll = essaie.GetCompFinaux();
                /*
                cpt = 1;
                foreach (Outils elmnt in ll)
                {
                    DataColumn output = new DataColumn("output" + cpt.ToString(), typeof(string));
                    dt.Columns.Add(output);
                    cpt++;
                }
                */



                foreach (Outils elmnt in circuit.Vertices)
                {
                    if (elmnt.GetType().ToString().CompareTo(ch1) == 0)
                    {
                        Console.WriteLine("kayena une sortieeeeee");
                    }
                }






                dt.Rows.Add(inputRow);
            }





            tVerite.ItemsSource = dt.DefaultView;
        }

        
        public void Last_Elements(CircuitPersonnalise circuit)
        {
            string ch1 = "WpfApp2.Noyau.PinOut";
            circuit.SetCompFinaux(new List<Outils>()); //so that each time it does the job all over again  for our circuit
            
            foreach (var vertex in circuit.GetCircuit().Vertices)
            {
                if (vertex.GetType().ToString().CompareTo(ch1) == 0 || circuit.GetCircuit().IsOutEdgesEmpty(vertex))
                {
                    //list_element_de_sortie.Add(vertex);
                    circuit.GetCompFinaux().Add(vertex);
                }

            }
            foreach (Outils o in circuit.GetCompFinaux()) Console.WriteLine(o);
        }



        private void simuler_click(CircuitPersonnalise circuit)
        {

            Console.WriteLine("--------------  Partie Simuler Click");
            Last_Elements(circuit);
            Console.WriteLine("--------------  Fin Simuler Click");

            //souad
            //circuit.Evaluate(circuit.getCircuit().Vertices.Last());
            foreach (var gate in circuit.GetCompFinaux())
                circuit.Evaluate(gate);
        }
        







        private void InitialiseTable()
        {
            
            this.Width = 800;
            FlowDocument oDoc = new FlowDocument();



            // Create the Table object instance

            Table oTable = new Table();

            // Append the table to the document

            oDoc.Blocks.Add(oTable);

            
            oTable.Background = Brushes.Red;



            

            //int numberOfColumns = 5;

            //get number of columns (calcul nbr des pin d'entree et sortie)
            IEnumerable<Outils> lst = circuit.Vertices;
            List<Outils> liste = lst.ToList();
            int nbrPinEntree = 0;
            int nbrDiodSortie = 0;
            string ch1 = "WpfApp2.Noyau.PinOut";
            string ch2 = "WpfApp2.Noyau.PinIn";
            List<Outils> LLC = new List<Outils>();
            List<Outils> PinEntreeLLC = new List<Outils>();
            List<Outils> DiodSortieLLC = new List<Outils>();
            foreach (Outils noeud in liste)
            {
                if (noeud.GetType().ToString().CompareTo(ch1) == 0)
                {
                    Console.WriteLine("il existe une sortie diode,il s'appelle : "+noeud.getname());
                    nbrDiodSortie++;
                    LLC.Add(noeud);
                    DiodSortieLLC.Add(noeud);
                }
                else
                {
                    if (noeud.GetType().ToString().CompareTo(ch2) == 0)
                    {
                        Console.WriteLine("il existe un Pin entreeil s'appelle : " + noeud.getname());
                        nbrPinEntree++;
                        LLC.Add(noeud);
                        PinEntreeLLC.Add(noeud);
                    }
                }
            }


            Console.WriteLine("il esxiste :  " + nbrDiodSortie + "Sorties   " + nbrPinEntree + "entrees ");
            int numberOfColumns = nbrPinEntree+nbrDiodSortie;
            oTable.CellSpacing = 10;
            // LLC = PinEntreeLLC.GetRange(0,nbrPinEntree);
            /*
             foreach(Outils ees in PinEntreeLLC) { LLC.Add(ees); }
             for (int i=0; i<nbrDiodSortie;i++)
             {
                 LLC[nbrPinEntree + i] = DiodSortieLLC[i];
             }*/
            //string max = Console.ReadLine();
            //string min = Console.ReadLine();
            //string equidistance = Console.ReadLine();


            // Create and add an empty TableRowGroup Rows.

            oTable.RowGroups.Add(new TableRowGroup());
            // Add the table head row.

            oTable.RowGroups[0].Rows.Add(new TableRow());

            //Configure the table head row

            TableRow currentRow = oTable.RowGroups[0].Rows[0];

            currentRow.Background = System.Windows.Media.Brushes.Green;

            currentRow.Foreground = System.Windows.Media.Brushes.White;
            int x = 0;
            /*
             
            foreach(Outils elmnt in PinEntreeLLC)
            {
                oTable.Columns.Add(new TableColumn());
               // currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname().ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname()))));

                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("sdfsdf"))));

                oTable.Columns[x].Width = new GridLength(130);
                x++;
            }
            
            foreach(Outils elmnt in DiodSortieLLC)
            {
                oTable.Columns.Add(new TableColumn());
                // currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname().ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname()))));

                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("sdfsdf"))));

                oTable.Columns[x].Width = new GridLength(130) ;
                x++;
            }
            */

            foreach (Outils elmnt in circuit.Vertices)
            {
                if (elmnt.GetType().ToString().CompareTo(ch2) == 0) { 
                oTable.Columns.Add(new TableColumn());
                // currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname().ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname()))));

                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("sdfsdf"))));

                oTable.Columns[x].Width = new GridLength(130);
                x++;
                }
            }

            foreach (Outils elmnt in circuit.Vertices)
            {
                if (elmnt.GetType().ToString().CompareTo(ch1) == 0) { 
                    oTable.Columns.Add(new TableColumn());
                // currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname().ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(elmnt.getname()))));

                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("sdfsdf"))));

                oTable.Columns[x].Width = new GridLength(130);
                x++;
                }
            }

            /*
             * 
            
            for (int x = 0; x < numberOfColumns; x++)
            {

                oTable.Columns.Add(new TableColumn());
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(ch1))));

                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("sdfsdf"))));

                oTable.Columns[x].Width = new GridLength(130);

            }
            */


            /*
            // Add the header row with content,

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Maximooom"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("pdfgp"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Min"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Equidistance"))));
            */




            //Add Libya data row
            /*
            oTable.RowGroups[0].Rows.Add(new TableRow());

            currentRow = oTable.RowGroups[0].Rows[1];

            //Configure the row layout

            currentRow.Background = System.Windows.Media.Brushes.White;

            currentRow.Foreground = System.Windows.Media.Brushes.Navy;

            //Add the country name in the first cell

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(true.ToString()))));

            //Add the country flag in the second cell


            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(true.ToString()))));

            //  Paragraph oParagraph0 = new Paragraph();



            // currentRow.Cells.Add(new TableCell(oParagraph0));

            //Add the calling code

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(true.ToString()))));

            //Add the internet TLD

            */

            //Add the Time Zone


            int iterr = 0;
            double totale = Math.Pow(2, nbrPinEntree);
            //Add Tunisia data row
            for (int k=1;k<=totale;k++)
            { 
                oTable.RowGroups[0].Rows.Add(new TableRow());

                currentRow = oTable.RowGroups[0].Rows[k];
                //int value = 3;
                //string binary = Convert.ToString(value, 2);
                //Console.WriteLine("dfdsfsdfsdfsdf" + binary);
                //elmnt.getSortie().ToString()

                int m = 0;
                foreach (Outils elmnt in circuit.Vertices)
                {
                    
                    if (elmnt.GetType().ToString().CompareTo(ch2) == 0) 
                    {
                        
                        string binary = Convert.ToString(iterr, 2);
                        int j = nbrPinEntree - m;
                        try { 
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(binary[j].ToString()))));
                        }
                        catch
                        {
                            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("err"))));
                        }
                        //Console.WriteLine("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk"+binary[m]);
                        m++;

                    }
                }
                iterr++;
                foreach (Outils elmnt in circuit.Vertices)
                {
                    if (elmnt.GetType().ToString().CompareTo(ch1) == 0) 
                    { 
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run("hadi sor"))));
                    }
                }
            }
            /*
        //Add the country flag in the second cell


        currentRow.Cells.Add(new TableCell(new Paragraph(new Run("100ERE"))));

        //  Paragraph oParagraph0 = new Paragraph();



        // currentRow.Cells.Add(new TableCell(oParagraph0));

        //Add the calling code

        currentRow.Cells.Add(new TableCell(new Paragraph(new Run("5ERER0"))));


        oTable.RowGroups[0].Rows.Add(new TableRow());
        //Configure the row layout

        currentRow.Background = System.Windows.Media.Brushes.Azure;

        currentRow.Foreground = System.Windows.Media.Brushes.Navy;

        //Add the country name in the first cell 
        */


            this.Content = oDoc;
        }
    }
}
