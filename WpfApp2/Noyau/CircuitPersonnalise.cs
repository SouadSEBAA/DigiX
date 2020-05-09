using QuickGraph;
using System.Collections.Generic;
using WpfApp2;
using WpfApp2.Noyau;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Windows;
namespace logisimConsole
{
    [Serializable]
    public class CircuitPersonnalise :Outils
    {
        private bool Sauvegardé;
        private bool simulation;
        private BidirectionalGraph<Outils, Edge<Outils>> Circuit;
        private List<Outils> CompFinaux;
        public List<Gate> gates;
        public List<Wire> wires;
        public List<Point> Entrée;
        public List<Point> Sortie;
        public List<Outils> GetCompFinaux() { return CompFinaux; }
        public void SetCompFinaux(List<Outils> l) { CompFinaux = l; }
        public BidirectionalGraph<Outils, Edge<Outils>> GetCircuit() { return Circuit; } //to iterate through vertices and edges of the graph created in the constructor


        public CircuitPersonnalise()
        {
            Circuit = new BidirectionalGraph<Outils, Edge<Outils>>();
            CompFinaux = new List<Outils>();
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            Entrée = new List<Point>();
            Sortie = new List<Point>();
            gates = new List<Gate>();
            wires = new List<Wire>();
        }
        public BidirectionalGraph<Outils, Edge<Outils>> GetGraph() { return Circuit; }

        public CircuitPersonnalise(int nbentree, int nbsortie)
        {
            liste_entrees = new List<ClasseEntree>(nb_entrees);
            liste_sorties = new List<Sortie>(nb_sorties);

            //à décommenter
            //this.nb_entrees = nbentree;
            //this.nb_sorties = nbsortie;
        }

        
        public CircuitPersonnalise(BidirectionalGraph<Outils, Edge<Outils>> grph)
        {
            this.Circuit = grph;
            CompFinaux = new List<Outils>();
        }


        //Relate for console
        /*public bool Relate(Outils component1, Outils component2, int num_sortie, int num_entree)
        { 
            if (!component2.getEntreeSpecifique(num_entree).getRelated() ) //Si l'entrée de component2 n'est pas reliée
            {
                OutStruct outstruct = new OutStruct(num_entree, component2);
                if (!component1.getSortieSpecifique(num_sortie).get_OutStruct().Contains(outstruct))
                {
                    component1.getSortieSpecifique(num_sortie).get_OutStruct().Add(outstruct);//Mise à jour des liaison
                    component2.getEntreeSpecifique(num_entree).setRelated(true);//Mise à jour de related
                }

                if (!Circuit.ContainsEdge(component1, component2))//Si il n'y a pas un edge déja présent liant component1 et component2
                {
                    Edge<Outils> edge = new Edge<Outils>(component1, component2);
                    Circuit.AddEdge(edge); //Ajouter edge entre component1 et component2
                }

                component2.getEntreeSpecifique(num_entree).setEtat(component1.getSortieSpecifique(num_sortie).getEtat());//Mise à jour de l'état d'entree de component2
                return true; // component1 et component2 liées avec succès
            }
            else
            {
                return false;
            }
        }
        */

        //Relate for graphique
        public bool Relate(Outils component1, Outils component2, Sortie sortie, ClasseEntree entree)
        {
            component1.circuit = this;
            component2.circuit = this;
            if (!entree.getRelated() && entree.getEtat() == sortie.getEtat() && component1.getListesorties().Contains(sortie) && component2.getListeentrees().Contains(entree) && Circuit.ContainsVertex(component2) && Circuit.ContainsVertex(component1)) //Si l'entrée de component2 n'est pas reliée
            {
                Console.WriteLine(component1.GetType() + "          ");

                Console.WriteLine(component2.GetType() + "          " );

                OutStruct outstruct = new OutStruct(entree, component2);//Mise à jour des liaison
                if (!sortie.getSortie().Contains(outstruct))
                {
                    sortie.getSortie().Add(outstruct);
                    entree.setRelated(true);//Mise à jour de related
                }

                if (!Circuit.ContainsEdge(component1, component2)) //Si il n'y a pas un edge déja présent liant component1 et component2
                {
                    Edge<Outils> edge = new Edge<Outils>(component1, component2);
                    Circuit.AddEdge(edge); //Ajouter edge entre component1 et component2
                }

                entree.setEtat(sortie.getEtat());//Mise à jour de l'état d'entree de component2
                return true; // component1 et component2 liées avec succès
            }
            else
            {
                return false;
            }
        }

        public bool AddComponent(Outils outil)
        {
            if (!Circuit.ContainsVertex(outil))
            {
                Circuit.AddVertex(outil);
                outil.circuit = this;
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Empty(Outils outil)  //to make sure an element is considered an ending element
        {
            bool empty = true;

            foreach (Sortie s in outil.get_liste_sortie()) 
            {
                if (s.get_OutStruct() != null)
                {
                    foreach (OutStruct o in s.get_OutStruct())
                    {
                        if (o.getOutils() != null) { empty = false; }
                    }
                }
                else empty = true;
            }
            return empty;
        }

        //pour trouver les elements dor sortie Fonction version  1
        public List<Outils> EndComponents()
        {
            foreach (var outil in Circuit.Vertices)
            {
                //foreach (var edge in Circuit.InEdges(outil))
                //{
                    if ((outil is PinOut) || Circuit.IsOutEdgesEmpty(outil))
                    //if ((outil is PinOut) || outil.SortieVide())
                    {
                        CompFinaux.Add(outil);
                    }
                //}
            }
            return CompFinaux;
        }

        public  void Evaluate(Outils outil)
        {
            if (!Circuit.IsInEdgesEmpty(outil))
            {
                IEnumerable<Edge<Outils>> inEdges = Circuit.InEdges(outil);
                foreach (Edge<Outils> edge in inEdges)
                {
                    Evaluate(edge.Source);
                }
            }

            
            outil.calcul_sorties();


        }

        public void EvaluateCircuit()
        {
            this.CompFinaux = new List<Outils>();
            this.EndComponents();
            foreach(Outils outil in this.CompFinaux)
            {
                Console.WriteLine("********Evaluate circuit *******");
                    this.Evaluate(outil);
            }
        }

        public void EvaluateCircuit(IN iN)
        {
            foreach (Outils outil in iN.getEndListe())
            {
               // new Thread(() => Evaluate(outil)).Start();
                Console.WriteLine("********Evaluate circuit *******"+ outil);
                    this.Evaluate(outil);
            }
        }

        public BidirectionalGraph<Outils, Edge<Outils>> getCircuit()
        {
            return Circuit;
        }


        public bool getSimulation() { return simulation; }
        public void setSimulation(bool s) { this.simulation = s; }

        public List<Outils> getUnrelatedGates()
        {
            List<Outils> UnrelatedList = new List<Outils>();
            foreach (var outil in Circuit.Vertices)
                if (outil.getnbrentrees() != 0 )
                {
                    List<ClasseEntree> listentree = outil.getListeentrees();
                    foreach (var entree in listentree)
                        if (!entree.getRelated())
                        {
                            UnrelatedList.Add(outil);
                            break;
                        }
                }
            return UnrelatedList;
        }

        
        //Suppression d'un outil
        public bool DeleteComponent(Outils outil)
        {
            if (Circuit.ContainsVertex(outil))
            {
                //Mettre à jour les entrées des outils auxquelles l'outil était connecté
                foreach(var sortie in outil.getListesorties())
                {
                    sortie.getSortie().ForEach((outstruct) => { outstruct.GetEntree().setRelated(false); });
                }
                //Mettre à jour les sorties des outils auxquelles l'outil était connecté
                foreach(var edge in Circuit.InEdges(outil))
                {
                   foreach(var sortie in edge.Source.getListesorties())
                    {
                        sortie.DeleteOustruct(outil);
                    }
                }
                Circuit.ClearEdges(outil);
                Circuit.RemoveVertex(outil);
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void calcul_sorties()
        {

            this.CompFinaux = new List<Outils>();
            this.EndComponents();
            foreach (Outils outil in this.CompFinaux)
            {
                //new Thread(() => Evaluate(outil)).Start();
                Console.WriteLine("********Evaluate circuit *******"+outil.GetType());
                this.EvaluatePerso(outil);
            }
        }

        //For chrnogramme
        /******************************************************/
        public List<Outils> StartComponents()
        {
            Console.WriteLine("Addind start components :");
            List<Outils> l = new List<Outils>();
            foreach (var outil in Circuit.Vertices)
            {
                if (Circuit.IsInEdgesEmpty(outil))
                {
                    Console.WriteLine(outil.GetType().Name);
                    l.Add(outil);
                }
            }
            return l;
        }


    
        //pour calculsortie()
        public void EvaluatePerso(Outils outil)
        {

            if (!outil.end)
            {
                IEnumerable<Edge<Outils>> inEdges = Circuit.InEdges(outil);
                foreach (Edge<Outils> edge in inEdges)
                {
                    EvaluatePerso(edge.Source);
                }
            }

            outil.calcul_sorties();
            Console.WriteLine("--------------------------");
            Console.WriteLine(outil.GetType());
            Console.WriteLine("apres calcul " + outil.getListeentrees()[0].getEtat());
            Console.WriteLine("apres calcul " + outil.getListesorties()[0].getEtat());
        }
        public void ConstructSortie()
        {
            
            foreach(Outils outils in this.Circuit.Vertices)
            {
                if(outils is PinOut )
                {
                    //on construit la liste des sorties
                   
                    foreach (Edge<Outils> edge in Circuit.InEdges(outils))
                    {
                        RecupSorti(edge.Source,(PinOut)outils);
                    }
                }
            }
            
            
        }
        //recuperation de la sortie
        public void RecupSorti(Outils outil,PinOut pin)
        {
            foreach(Sortie sorti in outil.getListesorties())
            {
                foreach(OutStruct outs in sorti.get_OutStruct())
                {
                    if (outs.getOutils().Equals(pin))
                    {
                        this.nb_sorties++;
                        sorti.set_Sorties(new List<OutStruct>());
                        sorti.setDispo(Disposition.right);
                        //creation de la liste pour la sauvegarde du circuit aprés  reutilisation 
                        this.Sortie.Add(new Point(outil.id, outil.getListesorties().IndexOf(sorti)));
                        this.liste_sorties.Add(sorti);
                        Console.WriteLine("SORTIE"+nb_sorties);
                        
                        //on supprime la sortie de gate 
                        ((Grid)(sorti.Parent)).Children.Remove(sorti);
                    }
                }
            }
        }
        //construction de la liste des entrées
        public void ConstructEntrée()
        {
            List<Outils> list = new List<Outils>();
            foreach (Outils outils in this.Circuit.Vertices)
            {
                if (outils is PinIn || outils is Horloge)
                {

                    list.Add(outils);

                    RecupEntré((IN)outils);

                }
            }
            List<Wire> listw = new List<Wire>();
            foreach (Outils outils1 in list)
            {
                InputOutput inputOutput = outils1.getListesorties()[0];
                foreach (Wire wire in wires)
                {
                    if (wire.io1.Equals(inputOutput) || wire.io2.Equals(inputOutput))
                    { listw.Add(wire); wire.Supprimer(); }

                }


            }
            foreach (Wire wire in listw)
            {
                wires.Remove(wire);

            }


        }
        //****
        public void RecupEntré(IN outils)
        {
            foreach (OutStruct outs in outils.getListesorties()[0].get_OutStruct())
            {
                this.nb_entrees++;
                ClasseEntree entree = outs.GetEntree();
                entree.setDispo(Disposition.left);
                entree.setRelated(false);
                outs.getOutils().end = true;
                this.liste_entrees.Add(entree);//on ajoute l'entrée 
                Console.WriteLine("ENTREE" + this.nb_entrees);
                //creation de la liste pour la sauvegarde aprés réutilisation 
                this.Entrée.Add(new Point(outs.getOutils().id, outs.getOutils().getListeentrees().IndexOf(outs.GetEntree())));

                ((Grid)(entree.Parent)).Children.Remove(entree);
            }
        }
        public void Clear()
        {
            this.Circuit.Clear();
            this.gates.Clear();
            this.wires.Clear();
            this.Entrée.Clear();
            this.Sortie.Clear();
            this.CompFinaux.Clear();
        }



    }
}



