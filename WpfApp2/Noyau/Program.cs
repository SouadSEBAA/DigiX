using System;

namespace logisimConsole
{
    [Serializable]
    class Program
    {/*
        static void Main(string[] args)
        {
            Disposition dispo = new Disposition();
            dispo = Disposition.up;

            Console.WriteLine("Hello World!");
           /*
             //* Test Du circuit Combinatoire (Lyna/Oussama)
            Multiplexeur mux1 = new Multiplexeur(2, 1, "mux1",dispo);
            ClasseEntree cmd1 = new ClasseEntree(true, true);
            ClasseEntree ent1 = new ClasseEntree(true, false);
            ClasseEntree ent2 = new ClasseEntree(true, true);
            List<ClasseEntree> lli = new List<ClasseEntree>();
            lli.Add(cmd1); lli.Add(ent1); lli.Add(ent2);
            List<Sortie> list_s = new List<Sortie>(1);
            mux1.setListSorties(list_s);
            mux1.setListEntrees(lli);
            mux1.calcul_sorties();
             */
        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************


        // Test Du PorteLogique(Rania/Lyna)

        //une instance de la classe ET
        /*
List<ClasseEntree> list = new List<ClasseEntree>();
list.Add(new ClasseEntree(true, true));
list.Add(new ClasseEntree(false, false));
list.Add(new ClasseEntree(true, true));
list.Add(new ClasseEntree(true, true));
list.Add(new ClasseEntree(true, true));
List <Sortie> list_s = new List<Sortie>();
ET porte_et = new ET(4, "premiere porte et",list, dispo);
porte_et.set_liste_sortie(list_s);
try
{
porte_et.calcul_sorties();
}
catch (RelatedException e) { Console.WriteLine("Index out of boundaries.\nProbable error lies within the attribute 'related'.\nFailed to find the required number of entries."); }
catch (EmptyListException e) { Console.WriteLine("ERROR! Output list is empty."); }
//fin de l'instance de classe ET
/*
//une instance de la classe NAND
List<ClasseEntree> list3 = new List<ClasseEntree>();
list3.Add(new ClasseEntree(true, true));
list3.Add(new ClasseEntree(false, false));
list3.Add(new ClasseEntree(true, true));
list3.Add(new ClasseEntree(true, true));
list3.Add(new ClasseEntree(false, false));
List<Sortie> list_so = new List<Sortie>();
NAND porte_nand = new NAND(3, "premiere porte nand", list3, dispo);
porte_nand.set_liste_sortie(list_so);
try
{
porte_nand.calcul_sorties();
}
catch (RelatedException e) { Console.WriteLine("Index out of boundaries.\nProbable error lies within the attribute 'related'.\nFailed to find the required number of entries."); }
catch (EmptyListException e) { Console.WriteLine("ERROR! Output list is empty."); }
//fin de l'instance de classe NAND

//une instance de la classe NON
List<ClasseEntree> list2 = new List<ClasseEntree>();
list2.Add(new ClasseEntree(true, false));
List<Sortie> liste_sor = new List<Sortie>();
NON porte_non = new NON(1,"premiere porte non", list2, dispo);
porte_non.set_liste_sortie(liste_sor);
try
{
porte_non.calcul_sorties();
}
catch (RelatedException e) { Console.WriteLine("Index out of boundaries.\nProbable error lies within the attribute 'related'.\nFailed to find the required number of entries."); }
catch (EmptyListException e) { Console.WriteLine("ERROR! Output list is empty"); }
//fin de l'instance de classe NON

//une instance de la classe NOR
List<ClasseEntree> list5 = new List<ClasseEntree>();
list5.Add(new ClasseEntree(false, true));
list5.Add(new ClasseEntree(false, false));
list5.Add(new ClasseEntree(false, true));
list5.Add(new ClasseEntree(true, false));
list5.Add(new ClasseEntree(true, true));
List<Sortie> list_sort = new List<Sortie>();
NOR porte_nor = new NOR(2, "premiere porte nor", list5, dispo);
porte_nor.set_liste_sortie(list_sort);
try
{
porte_nor.calcul_sorties();
}
catch (RelatedException e) { Console.WriteLine("Index out of boundaries.\nProbable error lies within the attribute 'related'.\nFailed to find the required number of entries."); }
catch (EmptyListException e) { Console.WriteLine("ERROR! Output list is empty."); }
//fin de l'instance de classe NOR

//une instance de la classe OU
List<ClasseEntree> list1 = new List<ClasseEntree>();
list1.Add(new ClasseEntree(true, true));
list1.Add(new ClasseEntree(false, false));
list1.Add(new ClasseEntree(true, false));
list1.Add(new ClasseEntree(false, false));
list1.Add(new ClasseEntree(false, false));
List<Sortie> list_sorti = new List<Sortie>();
OU porte_ou = new OU(2, "premiere porte ou", list1, dispo);
porte_ou.set_liste_sortie(list_sorti);
try
{
porte_ou.calcul_sorties();
}
catch (RelatedException e) { Console.WriteLine("Index out of boundaries.\nProbable error lies within the attribute 'related'.\nFailed to find the required number of entries."); }
catch (EmptyListException e) { Console.WriteLine("ERROR! Output list is empty."); }
//fin de l'instance de classe OU

//une instance de la classe OUX
List<ClasseEntree> list4 = new List<ClasseEntree>();
list4.Add(new ClasseEntree(true, true));
list4.Add(new ClasseEntree(false, false));
list4.Add(new ClasseEntree(true, true));
list4.Add(new ClasseEntree(true, false));
list4.Add(new ClasseEntree(false, false));
List<Sortie> list_sortie = new List<Sortie>();
OUX porte_oux = new OUX(3, "premiere porte oux", list4, dispo);
porte_oux.set_liste_sortie(list_sortie);
try
{
porte_oux.calcul_sorties();
}
catch (RelatedException e) { Console.WriteLine("Index out of boundaries.\nProbable error lies within the attribute 'related'.\nFailed to find the required number of entries."); }
catch (EmptyListException e) { Console.WriteLine("ERROR! Output list is empty."); }
//fin de l'instance de classe OUX
*/

        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************
        //********************************************************************************************//********************************************************************************************

        /*
         Console.WriteLine("Hello World!");

        Horloge horloge = new Horloge(2000, 500);
        // Thread mythread = new Thread(new ThreadStart(horloge.arreter));
        horloge.auto();

       // mythread.Start();
       // horloge.arreter();
        Console.WriteLine("Hello World!222222");
        /*************essai compteur
        Compteur compteur = new Compteur(2, 5,"c1");
        int i = 0;
        Console.WriteLine("welcom here");
        while (true)
        {


            compteur.getListeentrees()[0].setEtat(true);
            compteur.calcul_sorties();Console.WriteLine("welcom here1");i = 0;
            while (i < 5) { Console.WriteLine("**"+compteur.getListesorties()[i].isEtat());i++; }
            Thread.Sleep(1000);
        }
        /***************essai regdec*/
        /*
        Reg_Dec reg = new Reg_Dec(5, 5, "registre");
        reg.getListeentrees()[0].setEtat(true);//clock
        reg.getListeentrees()[1].setEtat(false);//raz
        reg.getListeentrees()[2].setEtat(false);//chg
        reg.getListeentrees()[3].setEtat(false);//dd
        reg.getListeentrees()[4].setEtat(true);//dg
        reg.getListeentrees()[5].setEtat(true);//esd
        reg.getListeentrees()[6].setEtat(true);//esg
        reg.getListeentrees()[7].setEtat(false);//1
        reg.getListeentrees()[8].setEtat(true);//2
        reg.getListeentrees()[9].setEtat(false);//3
        reg.getListeentrees()[10].setEtat(false);//4
        reg.getListeentrees()[11].setEtat(true);//5



        int i = 0;
        Console.WriteLine("welcom here");
        i = 2;
        while (i < 07)
        { Console.WriteLine("**" + reg.getListesorties()[i].isEtat()); i++; }
        while (true)
        {


       reg.getListeentrees()[0].setEtat(true);
            reg.calcul_sorties(); Console.WriteLine("welcom here1"); i = 2;
            while (i < 07)
            { Console.WriteLine("**" + reg.getListesorties()[i].isEtat()); i++; }
            Thread.Sleep(1000);
        }








    }*/
    }
}
