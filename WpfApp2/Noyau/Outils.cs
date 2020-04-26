using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using System.Collections.Generic;
using WpfApp2;
using WpfApp2.Noyau;
namespace logisimConsole
{
    [Serializable]
    public abstract class Outils
    {
        public CircuitPersonnalise circuit;
        public bool added;
        protected int nb_entrees;
        protected int nb_sorties;
        protected string etiquette;
        //protected taille taille;
        protected Disposition disposition;
        protected List<ClasseEntree> liste_entrees;
        protected List<Sortie> liste_sorties;

        //Methodes:
        public abstract void calcul_sorties();

        public Outils(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo)
        {
            this.disposition = dispo;
            liste_sorties = new List<Sortie>();
            liste_entrees = new List<ClasseEntree>();
            this.nb_sorties = nb_sorties;
            this.nb_entrees = nb_entrees;
            this.etiquette = etiquette;

        }

        public Outils() { }


        //Constructeur pour les portes logiques (le nobre de sorties est fixee a 1)
        public Outils(int nb_entrees, string etiquette, List<ClasseEntree> liste_entrees, Disposition dispo)
        {
            this.disposition = dispo;
            this.nb_entrees = nb_entrees;
            this.etiquette = etiquette;
            this.liste_entrees = liste_entrees;
        }
        /*
                //Constructeur pour la porte non -cas speciale- car elle a une seule entree
                public Outils(string etiquette, List<ClasseEntree> liste_entrees)
                {
                    this.etiquette = etiquette;
                    this.liste_entrees = liste_entrees;
                }
        */

        //pour fixer le nombre de sorties a 1 pour les portes logique
        public void setnb_sorties(int i) { this.nb_sorties = i; }

        //pour fixer le nombre d'entrée à 1 pour la porte NON 
        public void setnb_entrees(int i) { this.nb_entrees = i; }

        //un getter et un setter pour la liste de sortie
        public List<Sortie> get_liste_sortie() { return liste_sorties; }
        public void set_liste_sortie(List<Sortie> list) { liste_sorties = list; }





        public List<Sortie> getListesorties() { return liste_sorties; }
        public List<ClasseEntree> getListeentrees() { return this.liste_entrees; }
        public int getnbrentrees() { return this.nb_entrees; }
        public int getnbrsoryies() { return this.nb_sorties; }

        public bool verifiRelie()
        {
            int i = 0;
            bool stop = false;
            while (i < this.nb_entrees && !stop)
            {
                stop = !!((this.liste_entrees[i]).getRelated());
            }
            if (stop) { Console.WriteLine("attention entrée non reliée"); }
            return (!stop);
        }
        public void appelCalcul()
        {

            int i = 0, i1 = 0;
            Sortie s;
            while (i < nb_entrees)
            {
                s = liste_sorties[i]; i1 = 0;
                while (i1 < s.getSortie().Count)
                    (s.getSortie())[i1].getOutils().calcul_sorties();
                i1++;
            }
            i++;
        }

        public void setEntree(List<ClasseEntree> l)
        {
            liste_entrees = l;
        }
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

        public virtual Sortie getSortieSpecifique(int i)
        {
            return liste_sorties[i];
        }

        //essai
        public Outils(int nb_entrees, int nb_sorties)
        {
            liste_sorties = new List<Sortie>(nb_sorties);
            liste_entrees = new List<ClasseEntree>(nb_entrees);
            this.nb_sorties = nb_sorties;
            this.nb_entrees = nb_entrees;

            ClasseEntree entree;
            for (int i = 0; i < nb_entrees; i++)
            {
                entree = new ClasseEntree("entree",i, Disposition.left, false, false);
                liste_entrees.Add(entree);
            }

            Sortie sortie;
            for (int i = 0; i < nb_sorties; i++)
            {
                sortie = new Sortie("sortie",0, Disposition.right, false, new List<OutStruct>());
                liste_sorties.Add(sortie);
            }
        }

        //construiction de la liste de fin d'un element
        public void EndCircuit(IN iN)
        {
            Outils o = this;
            Console.WriteLine("+++++++++entree end circuit ********");
            foreach (Edge<Outils> edge in iN.circuit.getCircuit().OutEdges(o))
                {
                    Console.WriteLine("bouce"+edge.Target);
                    if ((edge.Target is PinOut) || edge.Target.Empty())
                    {
                        Console.WriteLine("if");
                        iN.getEndListe().Add(edge.Target);


                    }
                    else { edge.Target.EndCircuit(iN); }

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
    }
    public class Outilspm
    {
        public void calcul_sorties()
        {
            throw new System.NotImplementedException();
        }
    }
 

}
