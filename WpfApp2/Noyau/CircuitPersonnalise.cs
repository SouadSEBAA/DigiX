using QuickGraph;
using WpfApp2;
using System.Collections.Generic;
using System;

namespace logisimConsole
{
    public class CircuitPersonnalise
    {
        private bool Sauvegardé;
        private BidirectionalGraph<Outils, Edge<Outils>> Circuit;

        public CircuitPersonnalise()
        {
            Circuit = new BidirectionalGraph<Outils, Edge<Outils>>();
        }

        public bool Relate(Outils component1, Outils component2, int num_sortie, int num_entree)
        {
            
            if (!component2.getEntreeSpecifique(num_entree).getRelated()) //Si l'entrée de component2 n'est pas reliée
            {
                OutStruct outstruct = new OutStruct(num_entree, component2);

                component1.getSortieSpecifique(num_sortie).get_OutStruct().Add(outstruct);//Mise à jour des liaison
                component2.getEntreeSpecifique(num_entree).setRelated(true);//Mise à jour de related

                if (!Circuit.ContainsEdge(component1, component2))//Si il n'y a pas un edge déja présent liant component1 et component2
                {
                    Edge<Outils> edge = new Edge<Outils>(component1, component2);
                    Circuit.AddEdge(edge); //Ajouter edge entre component1 et component2
                    //component1.getSortieSpecifique(num_sortie).getSortie().Add(outstruct);
                }

                component2.getEntreeSpecifique(num_entree).setEtat(component1.getSortieSpecifique(num_sortie).getEtat());//Mise à jour de l'état d'entree de component2
                Console.WriteLine("Relate :" + component2.getEntreeSpecifique(num_entree).getEtat() + component1.getSortieSpecifique(num_sortie).getEtat());
                return true; // component1 et component2 liées avec succès
            }
            else
            {
                return false;
            }
        }

        public bool Relate(Outils component1, Outils component2, InputOutput sortie, InputOutput entree)
        {
            if (!((ClasseEntree) entree).getRelated() || sortie.GetIsInput() == entree.GetIsInput()) //Si l'entrée de component2 n'est pas reliée
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

        public void Evaluate(Outils outil)
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


    }


}
