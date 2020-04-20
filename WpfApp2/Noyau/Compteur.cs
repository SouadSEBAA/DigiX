using System.Collections.Generic;
namespace logisimConsole
{
    class Compteur : CircSequentielle
    {//liste des entrées : [clock ,raz]
     //liste des sorties : [les sories du compteur]
        Disposition dd = Disposition.down;
        public Compteur(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo)
        {
            this.nb_entrees = nb_entrees; this.nb_sorties = nb_sorties; int i = 0;
            while (i < nb_entrees) { this.liste_entrees.Add(new ClasseEntree(1, dd, false, false)); i++; }
            i = 0;
            while (i < nb_sorties) { this.liste_sorties.Add(new Sortie()); i++; }

        }
        public Compteur() 
        {
            this.nb_entrees = 2;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            this.liste_entrees.Add(new ClasseEntree(0, Disposition.left, false, false));
            this.liste_entrees.Add(new ClasseEntree(1, Disposition.right, false, false));
            this.liste_sorties.Add(new Sortie(0, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie(1, Disposition.down, false, new List<OutStruct>()));

        }
        public override void calcul_sorties()
        {


            if (((this.getListeentrees())[1]).isEtat() == true)
            {//remise à 0


                (this.getListeentrees())[1].setEtat(false);

                int i1 = 0;
                while (i1 < nb_sorties)
                {
                    (this.getListesorties())[i1].setEtat(false); i1++;

                }
            }
            else
            {
                if (((this.getListeentrees())[0]).isEtat() == true)// TO DO: on remplace (front) 
                {//clock activée
                 //if (this.verifiRelie())
                 //{



                    bool stop = false;
                    int i = 0;
                    while (!stop && i < this.nb_sorties)
                    {//incrementation du compteur +1


                        if ((this.getListesorties())[i].isEtat() == false/*0*/) { (this.getListesorties())[i].setEtat(true); stop = true; }
                        else { (this.getListesorties())[i].setEtat(false); i = i + 1; }
                        //}
                    }
                }
            }
            //parcours de lalistes 
            //this.appelCalcul();

        }

    }
}
