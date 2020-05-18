using QuickGraph;
using System.Collections.Generic;
using WpfApp2;
using WpfApp2.Noyau;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Windows;
namespace Noyau
{
    public class CircuitPersonnalise : Outils
    {
        /// <summary>
        /// Si True indique que le circuit est en simulation
        /// </summary>
        private bool simulation;

        /// <summary>
        /// La vraie structure du circuit sous forme de graph
        /// </summary>
        private BidirectionalGraph<Outils, Edge<Outils>> Circuit;

        /// <summary>
        /// Liste des composants qui constituent la fin du circuit
        /// Ce sont les éléments qui n'ont pas de sortie reliée, ou qui sont des PinOut
        /// </summary>
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

        public CircuitPersonnalise(BidirectionalGraph<Outils, Edge<Outils>> grph)
        {
            this.Circuit = grph;
            CompFinaux = new List<Outils>();
        }


        #region Liaison
        /// <summary>
        /// Retourne True si une sortie d'un component1 à une sortie d'un component2 ont été reliées, et si c'est le cas mettre à jour les input/output
        /// </summary>
        /// <param name="component1">Le composant qui comporte le input/output sortie</param>
        /// <param name="component2">Le composant qui comporte le input/output entree</param>
        /// <param name="sortie">La sortie à relier</param>
        /// <param name="entree">L'entrée à relier</param>
        /// <returns></returns>
        public bool Relate(Outils component1, Outils component2, Sortie sortie, ClasseEntree entree)
        {
            component1.circuit = this;
            component2.circuit = this;
            //On vérifie si l'entrée entree n'est pas déjà reliée, 
            //et si sortie et entree ont le meme état booléen, 
            //et si sortie est contenue dans la liste_sorties de component1, 
            //et si entree est contenue dans la liste_sentrees de component2, 
            //et si component1 et component2 sont contenus dans le circuit 
            if (!entree.getRelated() && entree.getEtat() == sortie.getEtat() && component1.getListesorties().Contains(sortie) && component2.getListeentrees().Contains(entree) && Circuit.ContainsVertex(component2) && Circuit.ContainsVertex(component1)) //Si l'entrée de component2 n'est pas reliée
            {
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
        #endregion

        #region Ajout/Suppression d'un composant
        /// <summary>
        /// Retourne True si outil a été ajouté au circuit
        /// </summary>
        /// <param name="outil"></param>
        /// <returns></returns>
        public bool AddComponent(Outils outil)
        {
            if (!Circuit.ContainsVertex(outil)) //Si outil n'est aps déjà présent dans le circuit
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
        #endregion

        #region Ealuation du circuit
        /// <summary>
        /// Evalue le circuit en considérant outil sa fin
        /// </summary>
        /// <param name="outil">La fin du circuit considéré</param>
        /// <param name="hs">Collection des edges déjà passé par</param>
        public void Evaluate(Outils outil, ICollection<Edge<Outils>> hs)
        {
            if (Circuit.ContainsVertex(outil))
                if (!Circuit.IsInEdgesEmpty(outil))
                {
                    IEnumerable<Edge<Outils>> inEdges = Circuit.InEdges(outil);

                    foreach (Edge<Outils> edge in inEdges)
                    {
                        if (!hs.Contains(edge))
                        {
                            hs.Add(edge);
                            Evaluate(edge.Source, hs);
                        }
                    }
                }
            outil.calcul_sorties();
        }

        /// <summary>
        /// Evalue tout ce circuit
        /// </summary>
        public void EvaluateCircuit()
        {
            this.CompFinaux = new List<Outils>();
            this.EndComponents();

            ICollection<Edge<Outils>> hs = new HashSet<Edge<Outils>>();
            foreach (Outils outil in this.CompFinaux)
            {
                this.Evaluate(outil, hs);
            }
        }

        public void EvaluateCircuit(IN iN)
        {
            ICollection<Edge<Outils>> hs = new HashSet<Edge<Outils>>();

            foreach (Outils outil in iN.getEndListe())
            {
                this.Evaluate(outil, hs);
            }
        }

        /// <summary>
        /// Retourne CompFinaux (liste des composants qui constituent la fin du circuit)
        /// </summary>
        /// <returns></returns>
        public List<Outils> EndComponents()
        {
            foreach (var outil in Circuit.Vertices)
            {
                if ((outil is PinOut) || Circuit.IsOutEdgesEmpty(outil))
                {
                    CompFinaux.Add(outil);
                }
            }
            return CompFinaux;
        }


        #endregion

        /// <summary>
        /// Retourne liste des composants qui ont des entrées non reliées
        /// </summary>
        /// <returns></returns>
        public List<Outils> getUnrelatedGates()
        {
            List<Outils> UnrelatedList = new List<Outils>();
            foreach (var outil in Circuit.Vertices)
                if (outil.getnbrentrees() != 0)
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


        #region Suppression d'un composant
        /// <summary>
        /// Retourne True si outil est supprimé du circuit
        /// </summary>
        /// <param name="outil"></param>
        /// <returns></returns>
        public bool DeleteComponent(Outils outil)
        {
            if (Circuit.ContainsVertex(outil))// Si outil figure dans le circuit
            {
                //Mettre à jour les entrées des outils auxquelles l'outil était connecté
                foreach (var sortie in outil.getListesorties())
                {
                    sortie.getSortie().ForEach((outstruct) => { outstruct.GetEntree().setRelated(false); });
                }
                //Mettre à jour les sorties des outils auxquelles l'outil était connecté
                foreach (var edge in Circuit.InEdges(outil))
                {
                    foreach (var sortie in edge.Source.getListesorties())
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
        #endregion

        #region Réutilisation
        /// <summary>
        /// Pour la réutilisation du circuit
        /// </summary>
        public override void calcul_sorties()
        {
            ICollection<Edge<Outils>> hs = new HashSet<Edge<Outils>>();
            this.CompFinaux = new List<Outils>();
            this.EndComponents();
            foreach (Outils outil in this.CompFinaux)
            {
                this.EvaluatePerso(outil, hs);
            }
        }

        //pour calculsortie()
        public void EvaluatePerso(Outils outil, ICollection<Edge<Outils>> hs)
        {

            if (!outil.end || Circuit.InEdges(outil) != null)
            {
                IEnumerable<Edge<Outils>> inEdges = Circuit.InEdges(outil);
                foreach (Edge<Outils> edge in inEdges)
                {
                    if (!hs.Contains(edge))
                    {
                        hs.Add(edge);
                        EvaluatePerso(edge.Source, hs);
                    }
                }
            }

            outil.calcul_sorties();
        }

        public void ConstructSortie()
        {

            foreach (Outils outils in this.Circuit.Vertices)
            {
                if (outils is PinOut)
                {
                    //on construit la liste des sorties

                    foreach (Edge<Outils> edge in Circuit.InEdges(outils))
                    {
                        RecupSorti(edge.Source, (PinOut)outils);
                    }
                }
            }


        }
        //recuperation de la sortie
        public void RecupSorti(Outils outil, PinOut pin)
        {
            foreach (Sortie sorti in outil.getListesorties())
            {
                foreach (OutStruct outs in sorti.get_OutStruct())
                {
                    if (outs.getOutils().Equals(pin))
                    {
                        this.nb_sorties++;
                        sorti.set_Sorties(new List<OutStruct>());
                        sorti.setDispo(Disposition.right);
                        //creation de la liste pour la sauvegarde du circuit aprés  reutilisation 
                        this.Sortie.Add(new Point(outil.id, outil.getListesorties().IndexOf(sorti)));
                        this.liste_sorties.Add(sorti);

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

        /// Pour la réeutilisation
        public override void setEntreeSpe(int i, bool etat)
        {
            ClasseEntree io = liste_entrees[i];
            foreach (Outils v in Circuit.Vertices)
            {
                if (v.getListeentrees().Contains(io))
                {
                    v.setEntreeSpe(v.getListeentrees().IndexOf(io), etat);
                }
            }
        }

        #endregion

        #region Getter/Setters
                public BidirectionalGraph<Outils, Edge<Outils>> getCircuit()
        {
            return Circuit;
        }


        public bool getSimulation() { return simulation; }
        public void setSimulation(bool s) { this.simulation = s; }
        #endregion
    }
}



