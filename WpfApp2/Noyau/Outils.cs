using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;


namespace Noyau
{
    [Serializable]
    public abstract class Outils
    {
        public static int nbrOutils = 0;
        public int id = 0;
        public CircuitPersonnalise circuit;
        public bool end;
        protected int nb_entrees;
        protected int nb_sorties;
        protected string etiquette;
        protected Disposition disposition;
        protected List<ClasseEntree> liste_entrees;
        protected List<Sortie> liste_sorties;

        //Methodes:

        public Outils(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo)
        {
            this.disposition = dispo;
            liste_sorties = new List<Sortie>();
            liste_entrees = new List<ClasseEntree>();
            this.nb_sorties = nb_sorties;
            this.nb_entrees = nb_entrees;
            this.etiquette = etiquette;

        }

        public Outils()
        {
            id = nbrOutils;
            nbrOutils++;

            etiquette = "Label_" + (id - 24);
        }


        //Constructeur pour les portes logiques (le nobre de sorties est fixee a 1)
        public Outils(int nb_entrees, string etiquette, List<ClasseEntree> liste_entrees, Disposition dispo)
        {
            this.disposition = dispo;
            this.nb_entrees = nb_entrees;
            this.etiquette = etiquette;
            this.liste_entrees = liste_entrees;
        }

        /// <summary>
        /// Calculer les sorties de cet outil en focniton de ses entrées
        /// </summary>
        public abstract void calcul_sorties();


        public void AjoutEntree(ClasseEntree entree)
        {
            this.liste_entrees.Add(entree);
            this.nb_entrees++;
        }

        public void AjoutSortie(Sortie sortie)
        {
            this.liste_sorties.Add(sortie);
            this.nb_sorties++;
        }

        public void AjoutEntreeSpe(ClasseEntree entree, int i)
        {
            this.liste_entrees.Insert(i, entree);
            this.nb_entrees++;
        }

        public void AjoutSortieSpe(Sortie sortie, int i)
        {
            this.liste_sorties.Insert(i, sortie);
            this.nb_sorties++;
        }

        public void SupprimerEntree(ClasseEntree classeEntree)
        {
            this.nb_entrees--;
            this.liste_entrees.Remove(classeEntree);
        }

        public void SupprimerSortie(Sortie sortie)
        {
            this.nb_sorties--;
            this.liste_sorties.Remove(sortie);
        }



        /// <summary>
        /// Construire la liste de fin d'un element
        /// </summary>
        /// <param name="iN"></param>
        public void EndCircuit(IN iN, ICollection<Edge<Outils>> hs)
        {
            Outils o = this;

            if ((iN.circuit.getCircuit().OutEdges(o)).Any())
            {
                foreach (Edge<Outils> edge in iN.circuit.getCircuit().OutEdges(o))
                {
                    if (!hs.Contains(edge))
                    {
                        hs.Add(edge);
                        if ((edge.Target is PinOut) || edge.Target.Empty())
                        {
                            iN.getEndListe().Add(edge.Target);
                        }
                        else { edge.Target.EndCircuit(iN, hs); }
                    }
                }
            }

        }

        public bool Empty()  //to make sure an element is considered an ending element
        {
            bool empty = true;

            foreach (Sortie s in this.get_liste_sortie())
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


        #region Getters/Setters

        public List<Sortie> getListesorties() { return liste_sorties; }
        public List<ClasseEntree> getListeentrees() { return this.liste_entrees; }
        public int getnbrentrees() { return this.nb_entrees; }
        public int getnbrsoryies() { return this.nb_sorties; }

        public bool getSortie()
        {
            return liste_sorties[0].isEtat();
        }


        public virtual void setEntreeSpe(int i, bool etat)
        {
            liste_entrees[i].setEtat(etat);
        }

        public virtual ClasseEntree getEntreeSpecifique(int i)
        {
            return liste_entrees[i];
        }


        public String getLabel()
        {
            return etiquette;
        }

        public void setLabel(string label)
        {
            this.etiquette = label;
        }

        /// <summary>
        /// pour fixer le nombre de sorties a 1 pour les portes logique
        /// </summary>
        /// <param name="i"></param>
        public void setnb_sorties(int i) { this.nb_sorties = i; }

        /// <summary>
        /// pour fixer le nombre d'entrée à 1 pour la porte NON 
        /// </summary>
        /// <param name="i"></param>
        public void setnb_entrees(int i) { this.nb_entrees = i; }

        //un getter et un setter pour la liste de sortie
        public List<Sortie> get_liste_sortie() { return liste_sorties; }

        public string getname() { return this.etiquette; }

        #endregion

    }
}
