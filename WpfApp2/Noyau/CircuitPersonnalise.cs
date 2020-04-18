using QuickGraph;
using System.Collections.Generic;
using WpfApp2;
using WpfApp2.Noyau;

namespace logisimConsole
{
    public class CircuitPersonnalise
    {
        private bool Sauvegardé;
        private BidirectionalGraph<Outils, Edge<Outils>> Circuit;
        private List<Outils> CompFinaux;

        public BidirectionalGraph<Outils, Edge<Outils>> GetCircuit() { return Circuit; } //to iterate through vertices and edges of the graph created in the constructor
        public List<Outils> GetCompFinaux() { return CompFinaux; }
        public void SetCompFinaux(List<Outils> l) { CompFinaux = l; }
        

        public CircuitPersonnalise()
        {
            Circuit = new BidirectionalGraph<Outils, Edge<Outils>>();
            CompFinaux = new List<Outils>();
        }

        public bool Relate(Outils component1, Outils component2, int num_sortie, int num_entree)
        {
            if (!component2.getEntreeSpecifique(num_entree).getRelated()) //Si l'entrée de component2 n'est pas reliée
            {
                Edge<Outils> edge = new Edge<Outils>(component1, component2);
                OutStruct outstruct = new OutStruct(num_entree, component2);//Mise à jour des liaison

                if (!Circuit.ContainsEdge(edge)) //Si il n'y a pas un edge déja présent liant component1 et component2
                {
                    Circuit.AddEdge(edge); //Ajouter edge entre component1 et component2
                    component1.getSortieSpecifique(num_sortie).getSortie().Add(outstruct);
                }

                component2.getEntreeSpecifique(num_entree).setEtat(component1.getSortieSpecifique(num_sortie).getEtat());//Mise à jour de l'état d'entree de component2

                return true; // component1 et component2 liées avec succès
            }
            else
            {
                return false;
            }
        }

        public bool Relate(Outils component1, Outils component2, InputOutput sortie, InputOutput entree)
        {
            //the original procedure
            if (!((ClasseEntree)entree).getRelated() || sortie.GetIsInput() == entree.GetIsInput()) //Si l'entrée de component2 n'est pas reliée
            {
                Edge<Outils> edge = new Edge<Outils>(component1, component2);
                OutStruct outstruct = new OutStruct(component2.getListeentrees().BinarySearch((ClasseEntree)entree), component2);//Mise à jour des liaison
                ((Sortie)sortie).getSortie().Add(outstruct);

                if (!Circuit.ContainsEdge(component1, component2)) //Si il n'y a pas un edge déja présent liant component1 et component2
                {
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

        public void AddComponent(Outils outil)
        {
            Circuit.AddVertex(outil);
        }

        public bool Empty(Outils outil)  //to make sure an element is considered an ending element
        {
            bool empty = true;

            foreach (Sortie s in outil.get_liste_sortie())
            {
                //Console.WriteLine("hello1");
                //if (s.get_OutStruct() == null) { finish = false; break; }
                if (s.get_OutStruct() != null)
                {
                    foreach (OutStruct o in s.get_OutStruct())
                    {
                        //Console.WriteLine("hello2");
                        if (o.getOutils() != null) { empty = false; }
                    }
                }
                else empty = true;
            }
            return empty;
        }

        //pour trouver les eements dor sortie Fonction 1
        public void EndComponents()
        {
            foreach (var outil in Circuit.Vertices)
            {
                foreach (var edge in Circuit.InEdges(outil))
                {
                    if ((outil is PinOut) || Empty(outil))
                    {
                        CompFinaux.Add(outil);
                    }
                }
            }
        }

    }


}
