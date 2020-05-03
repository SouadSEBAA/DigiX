using System;
using System.Collections.Generic;
namespace logisimConsole
{
    [Serializable]
    class AddNbits : CircCombinatoire
    {
        public AddNbits(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo) { }
        public AddNbits() 
        {
            this.nb_entrees = 2;
            this.nb_sorties = 2;
            this.liste_entrees = new List<ClasseEntree>();
            this.liste_sorties = new List<Sortie>();
            //on crée less listes
            this.liste_entrees.Add(new ClasseEntree("A",1, Disposition.up, false, false));
            this.liste_entrees.Add(new ClasseEntree("B",2, Disposition.up, false, false));
            this.liste_sorties.Add(new Sortie("Somme",0, Disposition.down, false, new List<OutStruct>()));
            this.liste_sorties.Add(new Sortie("Retenue sortante",1, Disposition.down, false, new List<OutStruct>()));
        }
        public override void calcul_sorties()
        {
            if (nb_entrees == 2)
            {
                AddComplet add = new AddComplet();
                add.setEntree(0, liste_entrees[0].isEtat());
                add.setEntree(1, liste_entrees[1].isEtat());
                add.setEntree(2, false);

                add.calcul_sorties();
                liste_sorties[0].setEtat(add.getSortie());
                liste_sorties[1].setEtat(add.getRetenue());
            }
            if (nb_entrees == 4)
            {
                AddComplet add1 = new AddComplet();
                add1.setEntree(0, liste_entrees[0].isEtat());
                add1.setEntree(1, liste_entrees[2].isEtat());
                add1.setEntree(2, false);

                add1.calcul_sorties();
                liste_sorties[0].setEtat(add1.getSortie());
                bool retenue1 = add1.getRetenue();

                AddComplet add2 = new AddComplet();
                add1.setEntree(0, liste_entrees[1].isEtat());
                add1.setEntree(1, liste_entrees[3].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[1].setEtat(add1.getSortie());
                liste_sorties[2].setEtat(add1.getRetenue());
            }

            if (nb_entrees == 6)
            {
                AddComplet add1 = new AddComplet();
                add1.setEntree(0, liste_entrees[0].isEtat());
                add1.setEntree(1, liste_entrees[3].isEtat());
                add1.setEntree(2, false);

                add1.calcul_sorties();
                liste_sorties[0].setEtat(add1.getSortie());
                bool retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[1].isEtat());
                add1.setEntree(1, liste_entrees[4].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[1].setEtat(add1.getSortie());
                retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[2].isEtat());
                add1.setEntree(1, liste_entrees[5].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[2].setEtat(add1.getSortie());
                liste_sorties[3].setEtat(add1.getRetenue());
            }

            if (nb_entrees == 8)
            {
                AddComplet add1 = new AddComplet();
                add1.setEntree(0, liste_entrees[0].isEtat());
                add1.setEntree(1, liste_entrees[4].isEtat());
                add1.setEntree(2, false);

                add1.calcul_sorties();
                liste_sorties[0].setEtat(add1.getSortie());
                bool retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[1].isEtat());
                add1.setEntree(1, liste_entrees[5].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[1].setEtat(add1.getSortie());
                retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[2].isEtat());
                add1.setEntree(1, liste_entrees[6].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[2].setEtat(add1.getSortie());
                retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[3].isEtat());
                add1.setEntree(1, liste_entrees[7].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[3].setEtat(add1.getSortie());
                liste_sorties[4].setEtat(add1.getRetenue());
            }

            if (nb_entrees == 10)
            {
                AddComplet add1 = new AddComplet();
                add1.setEntree(0, liste_entrees[0].isEtat());
                add1.setEntree(1, liste_entrees[5].isEtat());
                add1.setEntree(2, false);

                add1.calcul_sorties();
                liste_sorties[0].setEtat(add1.getSortie());
                bool retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[1].isEtat());
                add1.setEntree(1, liste_entrees[6].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[1].setEtat(add1.getSortie());
                retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[2].isEtat());
                add1.setEntree(1, liste_entrees[7].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[2].setEtat(add1.getSortie());
                retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[3].isEtat());
                add1.setEntree(1, liste_entrees[8].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[3].setEtat(add1.getSortie());
                retenue1 = add1.getRetenue();

                add1.setEntree(0, liste_entrees[4].isEtat());
                add1.setEntree(1, liste_entrees[9].isEtat());
                add1.setEntree(2, retenue1);

                add1.calcul_sorties();
                liste_sorties[4].setEtat(add1.getSortie());
                liste_sorties[5].setEtat(add1.getRetenue());
            }
        }
    }
    //*just a test
}
