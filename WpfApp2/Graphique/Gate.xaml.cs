﻿using Noyau;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataObject = System.Windows.DataObject;
using DragDropEffects = System.Windows.DragDropEffects;
using System.ComponentModel;
using System.Windows.Data;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Gate.xaml
    /// </summary>
    public partial class Gate : UserControl
    {
        protected string data;
        public Outils outil;

        // Pour classifier les entrees selon la direction
        private List<ClasseEntree> E_Left = new List<ClasseEntree>();
        private List<ClasseEntree> E_Up = new List<ClasseEntree>();
        private List<ClasseEntree> E_Right = new List<ClasseEntree>();
        private List<ClasseEntree> E_Down = new List<ClasseEntree>();

        // Pour classifier les sorties selon la direction
        private List<Sortie> S_Left = new List<Sortie>();
        private List<Sortie> S_Up = new List<Sortie>();
        private List<Sortie> S_Right = new List<Sortie>();
        private List<Sortie> S_Down = new List<Sortie>();

        public Gate(Outils outils)
        {
            this.outil = outils;
            this.data = "M0.5,0.5 L38.611,0.5 L38.611,51.944 L0.5,51.944 z";
            path.Data = StreamGeometry.Parse(data);
            path.StrokeThickness = 1;
            path.Fill = Brushes.White;
        }

        public Gate(Outils outil, String ph)
        {
            InputOutputs = new List<InputOutput>();

            this.data = ph;
            this.outil = outil;
            this.InitializeComponent();
            ToolTip = outil.getLabel();
            path.Data = StreamGeometry.Parse(ph);
            path.StrokeThickness = 1;
            path.Fill = Brushes.White;

            Classification();

            Creation();

            MAJ_Path();

            //Fréquences de l'horloge
            if (outil is Horloge)
            {
                MenuItem mi = new MenuItem();
                BindingGroup bindingGroup = new BindingGroup();
                RadioButton propo1 = new RadioButton();

                propo1.BindingGroup = bindingGroup;
                propo1.Checked += (object s, RoutedEventArgs e) => { (outil as Horloge).setTUp(1000, 500); };
                propo1.Content = "T = 1s DutyCycle = 500 ms";

                RadioButton propo5 = new RadioButton();
                propo5.BindingGroup = bindingGroup;
                propo5.Checked += (object s, RoutedEventArgs e) => { (outil as Horloge).setTUp(1000, 200); };
                propo5.Content = "T = 1s DutyCycle = 200 ms";


                RadioButton propo2 = new RadioButton();
                propo2.IsChecked = true;
                propo2.BindingGroup = bindingGroup;
                propo2.Checked += (object s, RoutedEventArgs e) => { (outil as Horloge).setTUp(2000, 1000); };
                propo2.Content = "T = 2s DutyCycle = 1s";

                RadioButton propo6 = new RadioButton();
                propo6.BindingGroup = bindingGroup;
                propo6.Checked += (object s, RoutedEventArgs e) => { (outil as Horloge).setTUp(2000, 500); };
                propo6.Content = "T = 2s DutyCycle = 500 ms";


                RadioButton propo3 = new RadioButton();
                propo3.BindingGroup = bindingGroup;
                propo3.Checked += (object s, RoutedEventArgs e) => { (outil as Horloge).setTUp(3000, 1500); };
                propo3.Content = "T = 3s DutyCycle = 1,5s";

                RadioButton propo4 = new RadioButton();
                propo4.BindingGroup = bindingGroup;
                propo4.Checked += (object s, RoutedEventArgs e) => { (outil as Horloge).setTUp(4000, 2000); };
                propo4.Content = "T = 4s DutyCycle = 2s";

                mi.Items.Add(propo5);
                mi.Items.Add(propo1);
                mi.Items.Add(propo6);
                mi.Items.Add(propo2);
                mi.Items.Add(propo3);
                mi.Items.Add(propo4);

                mi.Header = "Fréquences";

                menu.Items.Add(mi);
            }

        }

        
        /// <summary>
        /// Fonction qui separe les entrees et sorties selon la disposition
        /// </summary>
        public void Classification()
        {
            int nE = 0;
            int nS = 0;

            while (nE < outil.getnbrentrees())
            {
                switch (outil.getListeentrees()[nE].GetDisposition())
                {
                    case Disposition.up:
                        E_Up.Add(outil.getListeentrees()[nE]);
                        break;

                    case Disposition.down:
                        E_Down.Add(outil.getListeentrees()[nE]);
                        break;

                    case Disposition.left:
                        E_Left.Add(outil.getListeentrees()[nE]);
                        break;

                    case Disposition.right:
                        E_Right.Add(outil.getListeentrees()[nE]);
                        break;
                }
                nE++;
            }

            while (nS < outil.getnbrsoryies())
            {
                switch (outil.getListesorties()[nS].GetDisposition())
                {
                    case Disposition.up:
                        S_Up.Add(outil.getListesorties()[nS]);
                        break;

                    case Disposition.down:
                        S_Down.Add(outil.getListesorties()[nS]);
                        break;

                    case Disposition.left:
                        S_Left.Add(outil.getListesorties()[nS]);
                        break;

                    case Disposition.right:
                        S_Right.Add(outil.getListesorties()[nS]);
                        break;
                }
                nS++;
            }

            E_Left.Reverse();
            S_Down.Reverse();
            E_Up.Reverse();
        }


        
        /// <summary>
        /// Pour la creaction de tt les IO du gate
        /// </summary>
        public void Creation()
        {
            // Les entrees :
            int i = 0;
            foreach (ClasseEntree Er in E_Right)
            {
                AjouterIO(RightGate, Er, i);
                i++;
            }

            i = 0;
            foreach (ClasseEntree El in E_Left)
            {
                AjouterIO(LeftGate, El, i);
                i++;
            }

            i = 0;
            foreach (ClasseEntree Ed in E_Down)
            {
                AjouterIO(BottomGate, Ed, i);
                i++;
            }

            i = 0;
            foreach (ClasseEntree Eu in E_Up)
            {
                AjouterIO(TopGate, Eu, i);
                i++;
            }

            // Les sorties :
            i = 0;
            foreach (Sortie Sr in S_Right)
            {
                AjouterIO(RightGate, Sr, i);
                i++;
            }

            i = 0;
            foreach (Sortie Sl in S_Left)
            {
                AjouterIO(LeftGate, Sl, i);
                i++;
            }

            i = 0;
            foreach (Sortie Sd in S_Down)
            {
                AjouterIO(BottomGate, Sd, i);
                i++;
            }

            i = 0;
            foreach (Sortie Su in S_Up)
            {
                AjouterIO(TopGate, Su, i);
                i++;
            }
        }


        /// <summary>
        /// ajouter et créer les i/o
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="myt"></param>
        /// <param name="n"></param>
        public void AjouterIO(Grid grid, InputOutput myt, int n)
        {
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(cd);
            grid.Children.Add(myt);
            Grid.SetColumn(myt, n);
            //Liaison
            InputOutputs.Add(myt);
        }

        /// <summary>
        /// Lors de l'ajout ou suppression, on refait tout le travail
        /// Donc il faut supprimer tous les children et columndefinitions du gate
        /// Pour qu'après on puisse ajouter ces derniers comme besoin
        /// </summary>
        public void MAJ()
        {
            InputOutputs = new List<InputOutput>();

            LeftGate.Children.Clear(); RightGate.Children.Clear(); BottomGate.Children.Clear(); TopGate.Children.Clear();
            LeftGate.ColumnDefinitions.Clear(); RightGate.ColumnDefinitions.Clear(); BottomGate.ColumnDefinitions.Clear(); TopGate.ColumnDefinitions.Clear();
        }

        
        /// <summary>
        /// Mise a jour du path apres ajout ou suppression d'une entrée ou sortie
        /// </summary>
        public void MAJ_Path()
        {
            int taille = 20;

            int ver = Math.Max(Math.Max(E_Left.Count, S_Left.Count), Math.Max(E_Right.Count, S_Right.Count));
            int hor = Math.Max(Math.Max(E_Up.Count, S_Up.Count), Math.Max(E_Down.Count, S_Down.Count));
            //mettre à jour la taille du path 
            if (ver > 0) { path.Height = ver * taille; }
            if (hor > 0) { path.Width = hor * taille; }

            OutilShape.Width = path.Width + 44;
            OutilShape.Height = path.Height + 44;
        }

        #region ContextMenu
        
        /// <summary>
        /// Ajout d'une entrée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterEntrée(object sender, RoutedEventArgs e)
        {
            Type type = outil.GetType();
            AddEntree(type);
        }
        public void AddEntree(Type type)
        {

            if ((type.Name == "ET") || (type.Name == "OU") || (type.Name == "NAND") || (type.Name == "OUX") || (type.Name == "NOR"))
            {
                if (outil.getnbrentrees() < 5)
                {
                    string i = (outil.getnbrentrees() + 1).ToString();
                    string etiq = ("Entrée " + i);
                    ClasseEntree classeEntree = new ClasseEntree(etiq, 1, Disposition.left, false, false);

                    outil.AjoutEntree(classeEntree);
                    E_Left.Insert(0, classeEntree);
                }
            }

            if (type.Name == "Encodeur")
            {
                int entree = 0;
                if (outil.getnbrentrees() == 2) { entree = 2; }
                if (outil.getnbrentrees() == 4) { entree = 4; }
                for (int i = 0; i < entree; i++)
                {
                    string ee = (outil.getnbrentrees() + 1).ToString();
                    string etiq_e = ("Entrée " + ee);
                    ClasseEntree classeEntree = new ClasseEntree(etiq_e, 1, Disposition.left, false, false);
                    outil.AjoutEntree(classeEntree);
                    E_Left.Insert(0, classeEntree);
                }

                string es = (outil.getnbrsoryies() + 1).ToString();
                string etiq_s = ("Sortie " + es);
                Sortie sortie = new Sortie(etiq_s, 1, Disposition.right, false, new List<OutStruct>());
                outil.AjoutSortie(sortie);
                S_Right.Add(sortie);
            }

            if (type.Name == "Decodeur")
            {
                int nbr_sortie = 0;
                if (outil.getnbrsoryies() == 2) { nbr_sortie = 2; }
                if (outil.getnbrsoryies() == 4) { nbr_sortie = 4; }
                for (int i = 0; i < nbr_sortie; i++)
                {
                    string es = (outil.getnbrsoryies() + 1).ToString();
                    string etiq_s = ("Sortie " + es);
                    Sortie sortie = new Sortie(etiq_s, 1, Disposition.right, false, new List<OutStruct>());
                    outil.AjoutSortie(sortie);
                    S_Right.Add(sortie);
                }
                string ee = (outil.getnbrentrees() + 1).ToString();
                string etiq_e = ("Entrée " + ee);
                ClasseEntree classeEntree = new ClasseEntree(etiq_e, 1, Disposition.left, false, false);
                outil.AjoutEntree(classeEntree);
                E_Left.Insert(0, classeEntree);
            }

            if (type.Name == "AddNbits")
            {
                if (outil.getnbrentrees() < 10)
                {

                    string ee = (outil.getnbrsoryies()).ToString();
                    string A = ("A" + ee);
                    ClasseEntree classeEntreeA = new ClasseEntree(A, 1, Disposition.up, false, false);
                    outil.AjoutEntreeSpe(classeEntreeA, outil.getnbrsoryies() - 1);
                    E_Up.Insert((outil.getnbrsoryies()) - 1, classeEntreeA);

                    string B = ("B" + ee);
                    ClasseEntree classeEntreeB = new ClasseEntree(B, 1, Disposition.up, false, false);
                    outil.AjoutEntreeSpe(classeEntreeB, 2 * (outil.getnbrsoryies()) - 1);
                    E_Up.Insert(0, classeEntreeB);

                    string es = (outil.getnbrsoryies()).ToString();
                    string etiq_s = ("Somme" + es);
                    Sortie sortie = new Sortie(etiq_s, 1, Disposition.down, false, new List<OutStruct>());
                    outil.AjoutSortieSpe(sortie, outil.getnbrsoryies() - 1);
                    S_Down.Insert(1, sortie);
                }
            }

            if (type.Name == "Multiplexeur")
            {
                if (outil.getnbrentrees() < 11)
                {
                    int entree = 0; string etiq = ""; int a = 0; int b = 0;
                    if (outil.getnbrentrees() == 3) { entree = 2; etiq = "Controle 2"; a = 1; }
                    if (outil.getnbrentrees() == 6) { entree = 4; etiq = "Controle 3"; a = 2; b = 1; }

                    for (int i = 0; i < entree; i++)
                    {
                        string ee = (outil.getnbrentrees() - b).ToString();
                        string etiq_e = ("Entrée " + ee);
                        ClasseEntree classeEntree = new ClasseEntree(etiq_e, 1, Disposition.left, false, false);
                        outil.AjoutEntree(classeEntree);
                        E_Left.Insert(0, classeEntree);
                    }

                    ClasseEntree Controle = new ClasseEntree(etiq, 1, Disposition.up, false, false);
                    outil.AjoutEntreeSpe(Controle, a);
                    E_Up.Insert(0, Controle);
                }
            }

            if (type.Name == "Demultiplexeur")
            {
                if (outil.getnbrsoryies() < 8)
                {
                    int nsortie = 0; string etiq = ""; int a = 0;
                    if (outil.getnbrentrees() == 2) { nsortie = 2; etiq = "Controle 2"; a = 1; }
                    if (outil.getnbrentrees() == 3) { nsortie = 4; etiq = "Controle 3"; a = 2; }

                    for (int i = 0; i < nsortie; i++)
                    {
                        string es = (outil.getnbrsoryies() + 1).ToString();
                        string etiq_s = ("Sortie " + es);
                        Sortie sortie = new Sortie(etiq_s, 1, Disposition.right, false, new List<OutStruct>());
                        outil.AjoutSortie(sortie);
                        S_Right.Add(sortie);
                    }

                    ClasseEntree Controle = new ClasseEntree(etiq, 1, Disposition.up, false, false);
                    outil.AjoutEntreeSpe(Controle, a);
                    E_Up.Insert(0, Controle);
                }
            }

            if (type.Name == "Compteur")
            {
                if (outil.getnbrsoryies() < 5)
                {
                    string es = (outil.getnbrsoryies() + 1).ToString();
                    string etiq_s = ("Sortie" + es);
                    Sortie sortie = new Sortie(etiq_s, 1, Disposition.down, false, new List<OutStruct>());
                    outil.AjoutSortie(sortie);
                    S_Down.Insert(0, sortie);
                }
            }

            if (type.Name == "Reg_Dec")
            {
                if (outil.getnbrsoryies() < 8)
                {
                    string ee = (outil.getnbrsoryies() + 1).ToString();
                    string Entree = ("Entrée" + ee);
                    ClasseEntree classeEntree = new ClasseEntree(Entree, 1, Disposition.up, false, false);
                    outil.AjoutEntree(classeEntree);
                    E_Up.Insert(0, classeEntree);

                    string es = (outil.getnbrsoryies() + 1).ToString();
                    string etiq_s = ("Sortie" + es);
                    Sortie sortie = new Sortie(etiq_s, 1, Disposition.down, false, new List<OutStruct>());
                    outil.AjoutSortie(sortie);
                    S_Down.Insert(0, sortie);
                }
            }

            MAJ();
            Creation();
            MAJ_Path();
        }

        /// <summary>
        /// Supprimer une entrée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerEntrée(object sender, RoutedEventArgs e)
        {

            Type type = outil.GetType();

            if ((type.Name == "ET") || (type.Name == "OU") || (type.Name == "NAND") || (type.Name == "OUX") || (type.Name == "NOR"))
            {
                if (outil.getnbrentrees() > 2)
                {
                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);
                }
            }

            if (type.Name == "Encodeur")
            {
                if (outil.getnbrentrees() == 4)
                {
                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);

                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);

                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);
                }
                if (outil.getnbrentrees() == 8)
                {
                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);

                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);

                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);

                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);

                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);
                }
            }

            if (type.Name == "Decodeur")
            {
                if (outil.getnbrentrees() == 2)
                {
                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);

                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);

                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);
                }
                if (outil.getnbrentrees() == 3)
                {
                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);

                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);

                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);

                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    S_Right.Remove(S_Right[S_Right.Count - 1]);

                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    E_Left.Remove(E_Left[0]);
                }
            }

            if (type.Name == "AddNbits")
            {
                if (outil.getnbrentrees() > 2)
                {
                    int entree = 0, sortie = 0;
                    if (outil.getnbrentrees() == 4) { entree = 1; sortie = 3; }
                    if (outil.getnbrentrees() == 6) { entree = 2; sortie = 5; }
                    if (outil.getnbrentrees() == 8) { entree = 3; sortie = 7; }
                    if (outil.getnbrentrees() == 10) { entree = 4; sortie = 9; }

                    if (outil.getListeentrees()[sortie].getRelated()) { outil.getListeentrees()[sortie].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[sortie]);

                    if (outil.getListeentrees()[entree].getRelated()) { outil.getListeentrees()[entree].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[entree]);

                    if (outil.getListesorties()[entree].getSortie() != null) { outil.getListesorties()[entree].Supprimer(); }
                    outil.SupprimerSortie(outil.getListesorties()[entree]);

                    E_Up.Remove(E_Up[outil.getnbrsoryies()]);
                    E_Up.Remove(E_Up[0]);
                    S_Down.Remove(S_Down[1]);
                }
            }

            if (type.Name == "Multiplexeur")
            {
                if (outil.getnbrentrees() > 3)
                {
                    int entree = 0, id = 0;
                    if (outil.getnbrentrees() == 11) { entree = 4; id = 2; }
                    if (outil.getnbrentrees() == 6) { entree = 2; id = 1; }

                    for (int i = 0; i < entree; i++)
                    {
                        E_Left.Remove(E_Left[0]);
                        if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                        outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                    }

                    E_Up.Remove(E_Up[0]);
                    if (outil.getListeentrees()[id].getRelated()) { outil.getListeentrees()[id].Supprimer(); }
                    outil.SupprimerEntree(outil.getListeentrees()[id]);
                }
            }

            if (type.Name == "Demultiplexeur")
            {
                if (outil.getnbrsoryies() > 2)
                {
                    int sortie = 0, id = 0;
                    if (outil.getnbrsoryies() == 8) { sortie = 4; id = 2; }
                    if (outil.getnbrsoryies() == 4) { sortie = 2; id = 1; }

                    for (int i = 0; i < sortie; i++)
                    {
                        if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                        S_Right.Remove(S_Right[S_Right.Count - 1]);
                        outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                    }

                    if (outil.getListeentrees()[id].getRelated()) { outil.getListeentrees()[id].Supprimer(); }
                    E_Up.Remove(E_Up[0]);
                    outil.SupprimerEntree(outil.getListeentrees()[id]);
                }

            }


            if (type.Name == "Compteur")
            {
                if (outil.getnbrsoryies() > 2)
                {
                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    S_Down.Remove(S_Down[0]);
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);
                }
            }

            if (type.Name == "Reg_Dec")
            {
                if (outil.getnbrsoryies() > 4)
                {
                    if (outil.getListesorties()[outil.getnbrsoryies() - 1].getSortie() != null) { outil.getListesorties()[outil.getnbrsoryies() - 1].Supprimer(); }
                    S_Down.Remove(S_Down[0]);
                    outil.SupprimerSortie(outil.getListesorties()[outil.getnbrsoryies() - 1]);

                    if (outil.getListeentrees()[outil.getnbrentrees() - 1].getRelated()) { outil.getListeentrees()[outil.getnbrentrees() - 1].Supprimer(); }
                    E_Up.Remove(E_Up[0]);
                    outil.SupprimerEntree(outil.getListeentrees()[outil.getnbrentrees() - 1]);
                }
            }

            MAJ_Path();
            MAJ();
            Creation();
        }

        /// <summary>
        /// Ajoyter une étiquette au composant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterLabel(object sender, MouseButtonEventArgs e)
        {
            AjoutLabel ajoutLabel = new AjoutLabel();
            ajoutLabel.InputChanged += OnDialogInputChanged;
            ajoutLabel.Show();
        }

        private void OnDialogInputChanged(object sender, DialogInputEventArgs e)
        {
            System.Windows.Controls.ToolTip tt = new System.Windows.Controls.ToolTip();
            tt.Content = e.Input;
            outil.setLabel(e.Input);
            path.ToolTip = tt;
        }

        //Suppression
        //**************************************************************

        /// <summary>
        /// Supprimer le composant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DeletingGateEvent));
        }

        public static readonly RoutedEvent DeletingGateEvent = EventManager.RegisterRoutedEvent(
            "DeletingGate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Gate));

        public event RoutedEventHandler DeletingGate
        {
            add { AddHandler(DeletingGateEvent, value); }
            remove { RemoveHandler(DeletingGateEvent, value); }
        }
        //Fin Suppression
        //*************************************************************

        #endregion

        
        #region Drag&Drop
        //Drag Drop 
        //les attributs
        public bool added;
        public Point currentPoint;
        public TranslateTransform transform = new TranslateTransform();
        public Point anchorPoint;

        protected override void OnGiveFeedback(System.Windows.GiveFeedbackEventArgs e)
        {

            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.
            if (e.Effects.HasFlag(System.Windows.DragDropEffects.Copy))
            {
                Mouse.SetCursor(System.Windows.Input.Cursors.Cross);
            }
            else if (e.Effects.HasFlag(System.Windows.DragDropEffects.Move))
            {
                Mouse.SetCursor(System.Windows.Input.Cursors.Pen);
            }
            else
            {
                Mouse.SetCursor(System.Windows.Input.Cursors.No);
            }
            e.Handled = true;
        }


        private void path_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Gate gate;
                Canvas a = new Canvas();
                if (this.Parent.GetType().IsInstanceOfType(a))//le cas de la grille 
                { gate = this; }
                else { gate = this.Copier(); }//le cas du menu  
                //transfert de l'information
                DataObject data = new DataObject();
                data.SetData("String", "Gate");
                data.SetData("Object", gate);//l'outils à copier                                            
                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.All);
            }
        }


        private Gate Copier()
        {
            Gate o = this;
            Gate outils = new Et();
            //porte logique
            if (o is Et) { outils = new Et(); }
            if (o is Ou) { outils = new Ou(); }
            if (o is Non) { outils = new Non(); }
            if (o is Nand) { outils = new Nand(); }
            if (o is Nor) { outils = new Nor(); }
            if (o is Oux) { outils = new Oux(); }
            if (o is Add_C) { outils = new Add_C(); }
            if (o is Add_N) { outils = new Add_N(); }
            if (o is Cpt) { outils = new Cpt(); }
            if (o is BasculeD) { outils = new BasculeD(); }
            if (o is Decod) { outils = new Decod(); }
            if (o is D_Add) { outils = new D_Add(); }
            if (o is Demux) { outils = new Demux(); }
            if (o is Encod) { outils = new Encod(); }
            if (o is horloge) { outils = new horloge(); }
            if (o is BasculeJk) { outils = new BasculeJk(); }
            if (o is Mux) { outils = new Mux(); }
            if (o is Reg) { outils = new Reg(); }
            if (o is BasculeRst) { outils = new BasculeRst(); }
            if (o is BasculeT) { outils = new BasculeT(); }
            //à ajouter less pins
            if (o is pin_entree) { outils = new pin_entree(); }
            if (o is pin_sortie) { outils = new pin_sortie(); }
            if (o is constantetrue) { outils = new constantetrue(); }
            if (o is constantefalse) { outils = new constantefalse(); }
            //le circuits personalisé aussi 
            return outils;
        }



        public List<ClasseEntree> getE_left() { return this.E_Left; }
        public List<Sortie> getS_right() { return this.S_Right; }

        public Outils GetOutil()
        {
            return outil;
        }

        //Liaison
        public List<InputOutput> InputOutputs;
        #endregion
    }

    #region Les paths de chaque outil spécifique
    //la partie portes logiques
    [Serializable]
    public class Et : Gate
    {
        public Et() : base(new ET(), "M 17,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15") { }

    }

    class Ou : Gate
    {
        public Ou() : base(new OU(), "M 15,17 h 10 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -10 c 5,-10 5,-20 0,-30") { }
    }

    [Serializable]
    public class Non : Gate
    {
        public Non() : base(new NON(), "M 15,17 v 30 l 30,-15 l -30,-15 M 46,33.5 a 3,3 1 1 1 0.1,0.1") { }
    }

    [Serializable]
    public class Nor : Gate
    {
        public Nor() : base(new NOR(), "M 15,17 h 5 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -5 c 5,-10 5,-20 0,-30 M 46,33.5 a 3,3 1 1 1 0.1,0.1") { }
    }

    [Serializable]
    public class Nand : Gate
    {
        public Nand() : base(new NAND(), "M 15,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15 M 46,33.5 a 3,3 1 1 1 0.1,0.1") { }
    }

    [Serializable]
    public class Oux : Gate
    {
        public Oux() : base(new OUX(), "M 13,47 c 5,-10 5,-20 0,-30 M 13,17 c 5,10 5,20 0,30 M 18,17 h 7 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -7 c 5,-10 5,-20 0,-30") { }
    }



    //la partie Combinatoires

    [Serializable]
    public class Add_C : Gate
    { //need to fix writting:too small
        public Add_C() : base(new AddComplet(), "M0.5,0.5 L78.833,0.5 L78.833,95.5 L0.5,95.5 z M14.163449,21.874763 L12.598018,26.673256 L15.735716,26.673256 z M13.363644,20.494 L15.031614,20.494 L18.463258,30.672 L17.034545,30.672 L16.111693,27.828449 L12.222041,27.828449 L11.299189,30.672 L9.932,30.672 z M20.779445,21.656028 L20.779445,29.509972 L21.811672,29.509972 C22.413235,29.509972 22.939603,29.462124 23.390775,29.366427 C23.841947,29.270731 24.254382,29.093009 24.628081,28.833261 C25.097482,28.514273 25.450672,28.089336 25.687652,27.558448 C25.924631,27.02756 26.043121,26.36566 26.043121,25.572747 C26.043121,24.788948 25.913238,24.120212 25.653472,23.566539 C25.393707,23.012868 25.008614,22.581094 24.498198,22.27122 C24.138171,22.052485 23.75308,21.895269 23.342924,21.799573 C22.932767,21.703876 22.422349,21.656028 21.811672,21.656028 z M19.425928,20.494 L21.743313,20.494 C22.700345,20.494 23.451159,20.565772 23.995756,20.709317 C24.540352,20.852862 25.004058,21.049951 25.386871,21.300584 C26.043121,21.733497 26.551258,22.308815 26.911286,23.026538 C27.271312,23.744261 27.451326,24.599833 27.451326,25.593253 C27.451326,26.522876 27.263337,27.361359 26.88736,28.108702 C26.511383,28.856046 26.006662,29.439339 25.373199,29.85858 C24.858224,30.182125 24.335274,30.398582 23.80435,30.507949 C23.273424,30.617316 22.595526,30.672 21.770657,30.672 L19.425928,30.672 z M30.276119,21.656028 L30.276119,29.509972 L31.308347,29.509972 C31.90991,29.509972 32.436277,29.462124 32.88745,29.366427 C33.338622,29.270731 33.751056,29.093009 34.124755,28.833261 C34.594156,28.514273 34.947346,28.089336 35.184327,27.558448 C35.421305,27.02756 35.539796,26.36566 35.539796,25.572747 C35.539796,24.788948 35.409913,24.120212 35.150147,23.566539 C34.890381,23.012868 34.505289,22.581094 33.994872,22.27122 C33.634845,22.052485 33.249755,21.895269 32.839598,21.799573 C32.429441,21.703876 31.919023,21.656028 31.308347,21.656028 z M28.922602,20.494 L31.239987,20.494 C32.197019,20.494 32.947832,20.565772 33.492431,20.709317 C34.037027,20.852862 34.500732,21.049951 34.883545,21.300584 C35.539796,21.733497 36.047933,22.308815 36.40796,23.026538 C36.767986,23.744261 36.948,24.599833 36.948,25.593253 C36.948,26.522876 36.760012,27.361359 36.384035,28.108702 C36.008058,28.856046 35.503337,29.439339 34.869873,29.85858 C34.354898,30.182125 33.831948,30.398582 33.301024,30.507949 C32.770098,30.617316 32.092201,30.672 31.267331,30.672 L28.922602,30.672 z M15.031084,42.209435 C15.372881,42.209435 15.693031,42.2345 15.991534,42.28463 C16.290037,42.33476 16.566892,42.398561 16.822102,42.476034 C17.040852,42.544393 17.265299,42.629841 17.495442,42.732379 C17.725586,42.834917 17.952311,42.947709 18.175619,43.070755 L18.175619,44.704529 L18.066244,44.704529 C17.947754,44.595155 17.795084,44.462995 17.608235,44.308048 C17.421386,44.153103 17.193521,44.000434 16.924641,43.850045 C16.664875,43.708771 16.382322,43.592561 16.076984,43.501415 C15.771644,43.410271 15.418454,43.364698 15.017412,43.364698 C14.584469,43.364698 14.174312,43.453564 13.786942,43.631297 C13.399571,43.80903 13.060053,44.071072 12.768386,44.417422 C12.481276,44.763774 12.257968,45.198991 12.098463,45.723074 C11.938958,46.247158 11.859205,46.837322 11.859205,47.493566 C11.859205,48.190825 11.943515,48.791242 12.112135,49.294819 C12.280755,49.798395 12.510898,50.223358 12.802565,50.569709 C13.085118,50.906946 13.417801,51.161013 13.800614,51.331909 C14.183427,51.502806 14.589026,51.588255 15.017412,51.588255 C15.409339,51.588255 15.771644,51.540404 16.104327,51.444701 C16.437009,51.348999 16.733234,51.228232 16.993,51.0824 C17.243652,50.941125 17.460123,50.796433 17.642415,50.648322 C17.824706,50.500211 17.968261,50.373747 18.07308,50.268931 L18.175619,50.268931 L18.175619,51.882197 C17.952311,51.987014 17.741536,52.087274 17.543294,52.182976 C17.345052,52.278678 17.104654,52.372102 16.822102,52.463247 C16.525877,52.558949 16.2513,52.631865 15.99837,52.681995 C15.74544,52.732124 15.416175,52.757189 15.010576,52.757189 C14.34521,52.757189 13.733393,52.645537 13.175124,52.422231 C12.616855,52.198926 12.134921,51.866247 11.729322,51.424194 C11.323722,50.98214 11.009269,50.430714 10.785961,49.769912 C10.562654,49.109111 10.451,48.350329 10.451,47.493566 C10.451,46.641361 10.559236,45.898529 10.775707,45.265071 C10.992179,44.631613 11.307772,44.080186 11.722486,43.610789 C12.128085,43.155065 12.60888,42.807574 13.164871,42.568318 C13.720861,42.329063 14.342932,42.209435 15.031084,42.209435 z M22.214127,45.832448 C21.544204,45.832448 21.02809,46.072844 20.665785,46.553633 C20.30348,47.034423 20.122327,47.769279 20.122327,48.758203 C20.122327,49.715225 20.30348,50.440967 20.665785,50.935429 C21.02809,51.42989 21.544204,51.677121 22.214127,51.677121 C22.874935,51.677121 23.386491,51.434448 23.748797,50.9491 C24.111102,50.463753 24.292254,49.733454 24.292254,48.758203 C24.292254,47.769279 24.112241,47.034423 23.752215,46.553633 C23.392188,46.072844 22.879492,45.832448 22.214127,45.832448 z M22.214127,44.725037 C23.244076,44.725037 24.068946,45.077084 24.688739,45.781179 C25.308532,46.485274 25.618428,47.477616 25.618428,48.758203 C25.618428,50.043347 25.308532,51.035689 24.688739,51.735226 C24.068946,52.434764 23.244076,52.784533 22.214127,52.784533 C21.152277,52.784533 20.317152,52.423371 19.708753,51.701047 C19.100353,50.978722 18.796154,49.997774 18.796154,48.758203 C18.796154,47.482173 19.109468,46.490971 19.736096,45.784597 C20.362725,45.078224 21.188735,44.725037 22.214127,44.725037 z M30.31316,44.725037 C30.764332,44.725037 31.164235,44.827575 31.512868,45.032651 C31.861502,45.237727 32.126965,45.574964 32.309256,46.044361 C32.696626,45.611422 33.074882,45.2833 33.444023,45.059995 C33.813164,44.83669 34.214206,44.725037 34.64715,44.725037 C34.975275,44.725037 35.273777,44.776306 35.542658,44.878844 C35.81154,44.981382 36.046239,45.146583 36.246761,45.374445 C36.451839,45.606865 36.610205,45.896251 36.721859,46.242601 C36.833514,46.588952 36.88934,47.024169 36.88934,47.548253 L36.88934,52.572621 L35.604182,52.572621 L35.604182,48.156646 C35.604182,47.805738 35.59051,47.481034 35.563166,47.182534 C35.535823,46.884033 35.478856,46.650475 35.392268,46.481857 C35.301121,46.299567 35.171238,46.165128 35.002619,46.07854 C34.833999,45.991953 34.606134,45.948659 34.319024,45.948658 C34.054701,45.948659 33.77101,46.02955 33.467949,46.191332 C33.164888,46.353115 32.851575,46.586673 32.528006,46.892009 C32.532563,46.969482 32.53826,47.061766 32.545096,47.168862 C32.551932,47.275958 32.55535,47.402421 32.55535,47.548253 L32.55535,52.572621 L31.270192,52.572621 L31.270192,48.156646 C31.270192,47.805738 31.25652,47.481034 31.229176,47.182534 C31.201833,46.884033 31.144866,46.650475 31.058278,46.481857 C30.967131,46.299567 30.837248,46.165128 30.668629,46.07854 C30.50001,45.991953 30.272144,45.948659 29.985034,45.948658 C29.70704,45.948659 29.415372,46.035246 29.110033,46.208422 C28.804695,46.381597 28.50847,46.602624 28.22136,46.871501 L28.22136,52.572621 L26.936202,52.572621 L26.936202,44.936949 L28.22136,44.936949 L28.22136,45.784597 C28.558601,45.447361 28.892422,45.186458 29.222826,45.00189 C29.553231,44.817321 29.916675,44.725037 30.31316,44.725037 z M42.001485,45.948658 C41.636899,45.948659 41.284847,46.02955 40.945329,46.191332 C40.605811,46.353115 40.283381,46.561609 39.978043,46.816814 L39.978043,51.143923 C40.315284,51.307984 40.603532,51.419636 40.84279,51.478881 C41.082048,51.538125 41.356625,51.567747 41.666522,51.567747 C42.331888,51.567747 42.849142,51.323934 43.218283,50.836309 C43.587421,50.348683 43.771991,49.635473 43.771995,48.69668 C43.771991,47.826245 43.632995,47.150633 43.355002,46.669843 C43.077006,46.189054 42.625833,45.948659 42.001485,45.948658 z M42.315938,44.725037 C43.19094,44.725037 43.873394,45.071388 44.363304,45.76409 C44.853211,46.456792 45.098169,47.38647 45.098169,48.553126 C45.098169,49.824599 44.793969,50.837448 44.18557,51.591673 C43.577171,52.345897 42.808127,52.72301 41.878436,52.72301 C41.50018,52.72301 41.165218,52.679716 40.873552,52.593128 C40.581886,52.50654 40.283381,52.372102 39.978043,52.189812 L39.978043,55.389 L38.692885,55.389 L38.692885,44.936949 L39.978043,44.936949 L39.978043,45.736746 C40.297053,45.449639 40.651384,45.209245 41.041033,45.015561 C41.430682,44.821879 41.855649,44.725037 42.315938,44.725037 z M46.456904,41.936 L47.742062,41.936 L47.742062,52.572621 L46.456904,52.572621 z M52.562186,45.784597 C52.220389,45.784598 51.929861,45.835867 51.690603,45.938405 C51.451345,46.040943 51.233735,46.190193 51.03777,46.386154 C50.846364,46.586673 50.700531,46.808839 50.60027,47.052652 C50.500008,47.296465 50.436207,47.580154 50.408863,47.903718 L54.448907,47.903718 C54.439794,47.557367 54.398778,47.258868 54.32586,47.008219 C54.252942,46.75757 54.148126,46.545658 54.011407,46.372483 C53.861016,46.181078 53.668469,46.035246 53.433769,45.934987 C53.19907,45.834728 52.908542,45.784598 52.562186,45.784597 z M52.637382,44.725037 C53.111339,44.725037 53.530613,44.793396 53.895196,44.930113 C54.259778,45.066831 54.57879,45.281021 54.852228,45.572685 C55.125666,45.86435 55.335303,46.220955 55.481135,46.6425 C55.626967,47.064045 55.699885,47.580154 55.699885,48.190825 L55.699885,48.888084 L50.408863,48.888084 C50.408863,49.776748 50.632172,50.455778 51.078786,50.925175 C51.5254,51.394572 52.142916,51.62927 52.931327,51.62927 C53.213878,51.62927 53.490734,51.597369 53.761895,51.533568 C54.033055,51.469767 54.278009,51.387736 54.496759,51.287476 C54.729181,51.18266 54.925146,51.08126 55.08465,50.98328 C55.244154,50.885299 55.376319,50.793015 55.481135,50.706427 L55.556331,50.706427 L55.556331,52.107781 C55.40594,52.167026 55.219092,52.241081 54.995783,52.329947 C54.772474,52.418813 54.571954,52.488312 54.39422,52.538441 C54.143567,52.6068 53.916844,52.659209 53.714043,52.695666 C53.511242,52.732124 53.254894,52.750353 52.944999,52.750353 C51.728201,52.750353 50.7837,52.40742 50.1115,51.721554 C49.439299,51.035689 49.103197,50.061576 49.103197,48.799218 C49.103197,47.555089 49.429046,46.565027 50.080738,45.829031 C50.73243,45.093035 51.584646,44.725037 52.637382,44.725037 z M57.081605,42.742633 L58.366763,42.742633 L58.366763,44.936949 L60.732,44.936949 L60.732,46.003345 L58.366763,46.003345 L58.366763,49.633195 C58.366763,50.020561 58.373599,50.31906 58.387271,50.528694 C58.400942,50.738328 58.453353,50.934289 58.544497,51.116579 C58.62197,51.276083 58.747298,51.395711 58.920474,51.475463 C59.09365,51.555215 59.328354,51.595091 59.624577,51.595091 C59.834214,51.595091 60.037011,51.564329 60.232976,51.502806 C60.428941,51.441283 60.570215,51.390014 60.656805,51.348999 L60.732,51.348999 L60.732,52.504262 C60.490465,52.572621 60.245507,52.626168 59.997136,52.664905 C59.748764,52.703642 59.51976,52.72301 59.310123,52.72301 C58.608298,52.72301 58.061422,52.52363 57.669496,52.124871 C57.27757,51.726112 57.081605,51.096072 57.081605,50.234752 L57.081605,46.003345 L56.21344,46.003345 L56.21344,44.936949 L57.081605,44.936949 z") { }
    }

    [Serializable]
    public class Add_N : Gate
    { //need to fix writting: too small
        public Add_N() : base(new AddNbits(), "M0.5, 0.5 L65.5, 0.5 L65.5, 85.5 L0.5, 85.5 z M20.28717, 19.56749 C20.248107, 19.805762 20.203185, 19.993254 20.152403, 20.129967 L18.845746, 23.709898 L21.757891, 23.709898 L20.439516, 20.129967 C20.396546, 20.012784 20.353577, 19.825292 20.310608, 19.56749 z M19.806696, 18.548 L20.826239, 18.548 L24.060654, 26.95 L22.970797, 26.95 L22.080161, 24.594628 L18.517617, 24.594628 L17.679717, 26.95 L16.584, 26.95 z M26.281149, 19.438589 L26.281149, 26.059411 L27.535071, 26.059411 C28.636647, 26.059411 29.494079, 25.764502 30.107368, 25.174682 C30.720657, 24.584862 31.027301, 23.748959 31.027301, 22.666972 C31.027301, 20.514717 29.882756, 19.438589 27.593665, 19.438589 z M25.296762, 18.548 L27.617103, 18.548 C30.578077, 18.548 32.058564, 19.913179 32.058564, 22.643536 C32.058564, 23.940358 31.647426, 24.982307 30.82515, 25.769384 C30.002874, 26.556461 28.902275, 26.95 27.523352, 26.95 L25.296762, 26.95 z M34.694586, 19.438589 L34.694586, 26.059411 L35.948507, 26.059411 C37.050083, 26.059411 37.907516, 25.764502 38.520804, 25.174682 C39.134093, 24.584862 39.440737, 23.748959 39.440737, 22.666972 C39.440737, 20.514717 38.296192, 19.438589 36.007102, 19.438589 z M33.710199, 18.548 L36.03054, 18.548 C38.991513, 18.548 40.472, 19.913179 40.472, 22.643536 C40.472, 23.940358 40.060862, 24.982307 39.238587, 25.769384 C38.416311, 26.556461 37.315712, 26.95 35.936788, 26.95 L33.710199, 26.95 z M20.3811, 41.769827 C21.037344, 41.769828 21.539293, 41.981739 21.886946, 42.405561 C22.2346, 42.829384 22.408426, 43.441682 22.408426, 44.242453 L22.408426, 47.910377 L21.447497, 47.910377 L21.447497, 44.488544 C21.447497, 43.215122 20.982657, 42.578411 20.052978, 42.578411 C19.572513, 42.578411 19.175055, 42.759073 18.860605, 43.120396 C18.546155, 43.48172 18.388929, 43.937769 18.388929, 44.488544 L18.388929, 47.910377 L17.428, 47.910377 L17.428, 41.91045 L18.388929, 41.91045 L18.388929, 42.906532 L18.412367, 42.906532 C18.865488, 42.148729 19.521732, 41.769828 20.3811, 41.769827 z M28.617853, 43.996362 L28.617853, 47.019763 L29.953779, 47.019763 C30.531899, 47.019763 30.980137, 46.883046 31.298494, 46.609611 C31.616851, 46.336177 31.776029, 45.961182 31.776029, 45.484625 C31.776029, 44.49245 31.100254, 43.996362 29.748703, 43.996362 z M28.617853, 40.39875 L28.617853, 43.111607 L29.625657, 43.111607 C30.164715, 43.111607 30.588539, 42.981726 30.89713, 42.721964 C31.205721, 42.462201 31.360017, 42.095995 31.360017, 41.623345 C31.360017, 40.806949 30.822912, 40.398751 29.748703, 40.39875 z M27.633486, 39.508136 L30.024091, 39.508136 C30.750647, 39.508136 31.326814, 39.685868 31.752592, 40.041333 C32.178369, 40.396798 32.391258, 40.859682 32.391258, 41.429988 C32.391258, 41.906544 32.262353, 42.320602 32.004543, 42.67216 C31.746732, 43.023718 31.391267, 43.273715 30.938146, 43.422151 L30.938146, 43.445588 C31.504547, 43.511993 31.957668, 43.725858 32.297509, 44.087181 C32.637349, 44.448505 32.807269, 44.918226 32.807271, 45.496344 C32.807269, 46.215085 32.549459, 46.797109 32.033839, 47.242416 C31.518219, 47.687723 30.867834, 47.910377 30.082684, 47.910377 L27.633486, 47.910377 z M34.384523, 41.91045 L35.345452, 41.91045 L35.345452, 47.910377 L34.384523, 47.910377 z M34.876706, 39.139 C35.052486, 39.139 35.201898, 39.19857 35.324944, 39.317709 C35.44799, 39.436848 35.509513, 39.58626 35.509513, 39.765945 C35.509513, 39.937819 35.44799, 40.084301 35.324944, 40.205393 C35.201898, 40.326486 35.052486, 40.387032 34.876706, 40.387032 C34.704832, 40.387032 34.558349, 40.328439 34.437256, 40.211252 C34.316164, 40.094067 34.255617, 39.945631 34.255617, 39.765945 C34.255617, 39.58626 34.316164, 39.436848 34.437256, 39.317709 C34.558349, 39.19857 34.704832, 39.139 34.876706, 39.139 z M38.562637, 40.135082 L38.562637, 41.91045 L40.074343, 41.91045 L40.074343, 42.730753 L38.562637, 42.730753 L38.562637, 46.111571 C38.562637, 46.513909 38.630996, 46.801015 38.767713, 46.972888 C38.904431, 47.144761 39.130992, 47.230698 39.447395, 47.230698 C39.689581, 47.230698 39.898563, 47.164292 40.074343, 47.031481 L40.074343, 47.851784 C39.847782, 47.976782 39.548957, 48.039281 39.177866, 48.039281 C38.127094, 48.039281 37.601708, 47.453351 37.601708, 46.28149 L37.601708, 42.730753 L36.570467, 42.730753 L36.570467, 41.91045 L37.601708, 41.91045 L37.601708, 40.445625 z M43.303092, 41.769827 C43.822619, 41.769828 44.287459, 41.85967 44.697612, 42.039355 L44.697612, 43.011999 C44.256209, 42.72294 43.748401, 42.578411 43.174187, 42.578411 C42.994501, 42.578411 42.832393, 42.598918 42.687863, 42.639934 C42.543333, 42.680949 42.419311, 42.738565 42.315796, 42.812783 C42.212281, 42.887001 42.132204, 42.975867 42.075564, 43.079381 C42.018924, 43.182896 41.990604, 43.297152 41.990604, 43.422151 C41.990604, 43.578399 42.018924, 43.709256 42.075564, 43.814724 C42.132204, 43.920191 42.215211, 44.01394 42.324585, 44.09597 C42.433959, 44.178001 42.566771, 44.252218 42.723019, 44.318624 C42.879268, 44.385029 43.057001, 44.457294 43.256218, 44.535418 C43.52184, 44.636979 43.76012, 44.74147 43.971055, 44.848891 C44.181991, 44.956311 44.361677, 45.077404 44.510113, 45.212168 C44.65855, 45.346932 44.772806, 45.502203 44.852884, 45.677982 C44.932961, 45.853761 44.973, 46.062743 44.973, 46.304928 C44.973, 46.601799 44.907571, 46.859608 44.776713, 47.078356 C44.645854, 47.297103 44.471051, 47.478741 44.252303, 47.623271 C44.033555, 47.7678 43.781604, 47.875221 43.49645, 47.945533 C43.211296, 48.015844 42.912471, 48.051 42.599973, 48.051 C41.982791, 48.051 41.447639, 47.931861 40.994518, 47.693583 L40.994518, 46.662345 C41.517951, 47.049059 42.094118, 47.242416 42.723019, 47.242416 C43.566762, 47.242416 43.988633, 46.96117 43.988633, 46.398677 C43.988633, 46.238522 43.952501, 46.102782 43.880236, 45.991455 C43.807971, 45.880128 43.710315, 45.781497 43.58727, 45.69556 C43.464224, 45.609624 43.319694, 45.532476 43.15368, 45.464118 C42.987665, 45.395759 42.808956, 45.324471 42.617551, 45.250253 C42.351929, 45.144786 42.118532, 45.038342 41.917362, 44.930921 C41.716192, 44.823501 41.548224, 44.702408 41.41346, 44.567644 C41.278696, 44.43288 41.177134, 44.279562 41.108775, 44.107689 C41.040416, 43.935816 41.006237, 43.734647 41.006237, 43.504181 C41.006237, 43.222934 41.07069, 42.973914 41.199595, 42.75712 C41.3285, 42.540325 41.500373, 42.358687 41.715215, 42.212204 C41.930057, 42.065722 42.175172, 41.955372 42.450561, 41.881154 C42.725949, 41.806936 43.010126, 41.769828 43.303092, 41.769827 z") { }
    }

    [Serializable]
    public class D_Add : Gate
    {
        public D_Add() : base(new DemiAdd(), "M0.5,0.5 L72.167,0.5 L72.167,89.5 L0.5,89.5 z M19.666508,26.263714 L19.666508,35.091978 L21.338393,35.091978 C22.807151,35.091978 23.950387,34.698743 24.768101,33.912272 C25.585814,33.125802 25.994671,32.011201 25.994671,30.568469 C25.994671,27.698633 24.46862,26.263714 21.416519,26.263714 z M18.354,25.076195 L21.447769,25.076195 C25.395709,25.076195 27.36968,26.896536 27.36968,30.537219 C27.36968,32.266413 26.821499,33.655758 25.725138,34.705253 C24.628777,35.754749 23.16132,36.279497 21.322768,36.279497 L18.354,36.279497 z M32.519399,29.17001 C31.915228,29.17001 31.402204,29.38616 30.980327,29.818458 C30.558449,30.250756 30.298031,30.81587 30.199072,31.513797 L34.519412,31.513797 C34.514203,30.774202 34.335817,30.198673 33.984252,29.787207 C33.632687,29.375743 33.144403,29.17001 32.519399,29.17001 z M32.542836,28.091868 C33.589718,28.091868 34.399618,28.430415 34.972539,29.107509 C35.545459,29.784604 35.83192,30.724722 35.83192,31.927866 L35.83192,32.599751 L30.183447,32.599751 C30.20428,33.490391 30.443865,34.177901 30.902201,34.662284 C31.360537,35.146667 31.99075,35.388858 32.792838,35.388858 C33.693885,35.388858 34.522015,35.091978 35.277229,34.498219 L35.277229,35.701363 C34.574098,36.211787 33.644404,36.467 32.488148,36.467 C31.357934,36.467 30.469907,36.103713 29.82407,35.377139 C29.178233,34.650565 28.855314,33.628414 28.855314,32.310684 C28.855314,31.065874 29.20818,30.051535 29.913914,29.267668 C30.619648,28.483802 31.495955,28.091868 32.542836,28.091868 z M41.565807,28.091868 C42.112685,28.091868 42.58925,28.244214 42.995503,28.548906 C43.401755,28.853599 43.680402,29.253345 43.831445,29.748144 C44.425199,28.643961 45.31062,28.091868 46.487711,28.091868 C48.248138,28.091868 49.128352,29.177822 49.128352,31.349732 L49.128352,36.279497 L47.847095,36.279497 L47.847095,31.685675 C47.847095,30.800245 47.710375,30.159609 47.436936,29.763769 C47.163497,29.36793 46.703857,29.17001 46.058021,29.17001 C45.511143,29.17001 45.046297,29.420014 44.663482,29.920022 C44.280667,30.42003 44.089259,31.018998 44.089259,31.716925 L44.089259,36.279497 L42.808002,36.279497 L42.808002,31.529422 C42.808002,29.956481 42.201227,29.17001 40.987678,29.17001 C40.425175,29.17001 39.961631,29.405691 39.597045,29.877052 C39.232459,30.348414 39.050166,30.961705 39.050166,31.716925 L39.050166,36.279497 L37.768909,36.279497 L37.768909,28.279371 L39.050166,28.279371 L39.050166,29.545016 L39.081417,29.545016 C39.649129,28.57625 40.477257,28.091868 41.565807,28.091868 z M51.548991,28.279371 L52.830249,28.279371 L52.830249,36.279497 L51.548991,36.279497 z M52.205245,24.584 C52.439621,24.584 52.638841,24.663429 52.802905,24.822285 C52.966968,24.981142 53.049,25.180364 53.049,25.419951 C53.049,25.649121 52.966968,25.844437 52.802905,26.005897 C52.638841,26.167359 52.439621,26.248089 52.205245,26.248089 C51.976076,26.248089 51.780762,26.169963 51.619304,26.01371 C51.457846,25.857458 51.377115,25.659538 51.377115,25.419951 C51.377115,25.180364 51.457846,24.981142 51.619304,24.822285 C51.780762,24.663429 51.976076,24.584 52.205245,24.584 z M21.994422,47.71636 C21.94234,48.034065 21.882445,48.284062 21.814738,48.466351 L20.072578,53.239736 L23.955329,53.239736 L22.197544,48.466351 C22.140253,48.310103 22.082963,48.060106 22.025672,47.71636 z M21.353807,46.357 L22.713161,46.357 L27.025593,57.56 L25.572491,57.56 L24.38501,54.41941 L19.635084,54.41941 L18.517915,57.56 L17.057,57.56 z M29.986171,47.544487 L29.986171,56.372513 L31.65802,56.372513 C33.126747,56.372513 34.269958,55.979288 35.087654,55.192839 C35.905348,54.40639 36.314197,53.291819 36.314197,51.849126 C36.314197,48.979367 34.788178,47.544487 31.736144,47.544487 z M28.673692,46.357 L31.767393,46.357 C35.715247,46.357 37.689175,48.177292 37.689175,51.817877 C37.689175,53.547024 37.141006,54.936332 36.04467,55.985799 C34.948332,57.035266 33.480907,57.56 31.642395,57.56 L28.673692,57.56 z M41.205996,47.544487 L41.205996,56.372513 L42.877845,56.372513 C44.346572,56.372513 45.489782,55.979288 46.307478,55.192839 C47.125173,54.40639 47.534022,53.291819 47.534022,51.849126 C47.534022,48.979367 46.008003,47.544487 42.955969,47.544487 z M39.893517,46.357 L42.987218,46.357 C46.935072,46.357 48.909,48.177292 48.909,51.817877 C48.909,53.547024 48.360831,54.936332 47.264495,55.985799 C46.168156,57.035266 44.700732,57.56 42.86222,57.56 L39.893517,57.56 z") { }
    }

    [Serializable]
    public class Mux : Gate
    {
        public Mux() : base(new Multiplexeur(), "M0.5,0.5 L78.5,0.5 L78.5,143.5 L0.5,143.5 z M17.475,55.684 L20.939329,55.684 L23.085308,61.909565 C23.256204,62.407611 23.380715,62.908097 23.45884,63.411025 L23.502785,63.411025 C23.634619,62.829973 23.773778,62.324603 23.920262,61.894917 L26.066243,55.684 L29.442681,55.684 L29.442681,66.186895 L27.106274,66.186895 L27.106274,59.902736 C27.106274,59.224027 27.13557,58.474518 27.194164,57.654208 L27.13557,57.654208 C27.013501,58.298737 26.903639,58.762603 26.805983,59.045805 L24.345065,66.186895 L22.411485,66.186895 L19.906622,59.119047 C19.838263,58.928618 19.7284,58.440339 19.577034,57.654208 L19.511117,57.654208 C19.574593,58.689361 19.606331,59.597561 19.606331,60.378809 L19.606331,66.186895 L17.475,66.186895 z M35.843678,55.684 L38.216707,55.684 L38.216707,61.77773 C38.216707,63.476943 38.880764,64.32655 40.208879,64.32655 C41.51258,64.32655 42.16443,63.50624 42.16443,61.86562 L42.16443,55.684 L44.530134,55.684 L44.530134,61.63857 C44.530134,64.792857 43.060419,66.37 40.120989,66.37 C37.269449,66.37 35.843678,64.829478 35.843678,61.748433 z M50.079831,55.684 L52.958227,55.684 L54.45968,58.833404 C54.576867,59.082426 54.681846,59.377836 54.774619,59.719631 L54.803916,59.719631 C54.862509,59.514554 54.972371,59.209379 55.133503,58.804107 L56.803412,55.684 L59.44011,55.684 L56.29072,60.891502 L59.528,66.186895 L56.722846,66.186895 L54.913778,62.766496 C54.845419,62.639544 54.774619,62.405169 54.701377,62.063374 L54.672081,62.063374 C54.637901,62.224506 54.557335,62.468646 54.430383,62.795793 L52.613991,66.186895 L49.794189,66.186895 L53.141331,60.935448 z") { }
        // public Mux() : base(new Multiplexeur(), " M1.1909967, 0.99999997 L1, 155.905 M3.1909926, 155.90501 L58.028002, 106.76201 M3.1910104, 1.0000038 L58.028007, 55.952005 M60.027998, 57.952004 L60.027998, 105.952 M7.1909979, 61.952003 L11.994512, 61.952003 L14.970052, 74.0897 C15.207012, 75.060715 15.379654, 76.036491 15.487979, 77.017026 L15.548911, 77.017026 C15.731709, 75.884175 15.924662, 74.898879 16.12777, 74.06114 L19.103309, 61.952003 L23.784958, 61.952003 L23.784958, 82.429011 L20.545379, 82.429011 L20.545379, 70.177077 C20.545376, 68.85383 20.585998, 67.392547 20.667244, 65.793227 L20.586, 65.793227 C20.416741, 67.049835 20.26441, 67.954213 20.129006, 68.506359 L16.716785, 82.429011 L14.035753, 82.429011 L10.562598, 68.649155 C10.467814, 68.277885 10.315483, 67.325908 10.105604, 65.793227 L10.014205, 65.793227 C10.102219, 67.811416 10.146226, 69.582092 10.146226, 71.105254 L10.146226, 82.429011 L7.1909979, 82.429011 z M26.923102, 61.952003 L30.213459, 61.952003 L30.213459, 73.832666 C30.213459, 77.145543 31.134217, 78.801982 32.975734, 78.801982 C34.783399, 78.801982 35.687231, 77.202662 35.687231, 74.004022 L35.687231, 61.952003 L38.967433, 61.952003 L38.967433, 73.561353 C38.967433, 79.711119 36.929578, 82.786003 32.853869, 82.786003 C28.900025, 82.786003 26.923102, 79.782517 26.923102, 73.775547 z M40.927504, 61.952003 L44.918585, 61.952003 L47.000446, 68.092249 C47.162933, 68.577757 47.308494, 69.153703 47.437129, 69.820086 L47.477751, 69.820086 C47.558994, 69.420256 47.711326, 68.825271 47.934745, 68.035131 L50.250181, 61.952003 L53.906133, 61.952003 L49.539302, 72.104829 L54.027998, 82.429011 L50.138471, 82.429011 L47.630082, 75.760418 C47.535298, 75.512904 47.437129, 75.055955 47.335575, 74.389572 L47.294953, 74.389572 C47.247561, 74.703724 47.135852, 75.179712 46.959824, 75.817536 L44.44128, 82.429011 L40.531442, 82.429011 L45.17247, 72.190507 z") { }
    }

    [Serializable]
    public class Demux : Gate
    {
        // public Demux() : base(new Demultiplexeur(), "M64.790005,2.1909962 L64.599009,157.096 M64.782013,157.09601 L0.99999998,103.19 M64.971992,1 L1.8099959,55.952004 M1.0000067,55.571 L1.0000067,103.571 M10.566748,75.514262 L10.566748,82.171955 L11.738633,82.171955 C12.764031,82.171955 13.568481,81.864339 14.151983,81.249106 C14.735483,80.633874 15.027234,79.796474 15.027234,78.736908 C15.027234,77.735934 14.737924,76.948583 14.159307,76.374855 C13.580688,75.801126 12.768914,75.514262 11.723984,75.514262 z M8.2010067,73.587999 L11.92174,73.587999 C15.652239,73.587999 17.517488,75.294536 17.517488,78.707611 C17.517488,80.343348 17.008451,81.649496 15.990376,82.626055 C14.972301,83.602614 13.616089,84.090894 11.92174,84.090894 L8.2010067,84.090894 z M22.322645,77.967867 C21.951548,77.967867 21.630501,78.121675 21.359502,78.429291 C21.088504,78.736908 20.923708,79.120207 20.865114,79.57919 L23.626367,79.57919 C23.626367,78.504975 23.191793,77.967867 22.322645,77.967867 z M22.337293,76.407814 C23.431053,76.407814 24.277007,76.73252 24.875157,77.381932 C25.473304,78.031344 25.772379,78.912688 25.772381,80.025966 L25.772381,81.000084 L20.879762,81.000084 C20.957888,82.088947 21.643928,82.633379 22.937884,82.633379 C23.763085,82.633379 24.488189,82.438067 25.113196,82.047444 L25.113196,83.71736 C24.419829,84.088452 23.518943,84.273999 22.410536,84.273999 C21.199589,84.273999 20.25964,83.938306 19.590689,83.266922 C18.921738,82.595537 18.587263,81.659261 18.587263,80.458093 C18.587263,79.21298 18.948594,78.226655 19.671256,77.499119 C20.393918,76.771582 21.282597,76.407814 22.337293,76.407814 z M31.918881,76.407814 C32.978459,76.407814 33.703563,76.874121 34.094191,77.806735 C34.665485,76.874121 35.505335,76.407814 36.613743,76.407814 C38.244615,76.407814 39.060052,77.41367 39.060052,79.425382 L39.060052,84.090894 L36.752904,84.090894 L36.752904,79.813564 C36.752904,78.724701 36.35251,78.180269 35.551722,78.180269 C35.17086,78.180269 34.860799,78.343842 34.621539,78.67099 C34.382279,78.998137 34.26265,79.405851 34.26265,79.89413 L34.26265,84.090894 L31.948178,84.090894 L31.948178,79.769619 C31.948178,78.710052 31.555108,78.180269 30.768969,78.180269 C30.373458,78.180269 30.057293,78.336518 29.820475,78.649017 C29.583657,78.961516 29.465247,79.38632 29.465247,79.923427 L29.465247,84.090894 L27.150776,84.090894 L27.150776,76.590919 L29.465247,76.590919 L29.465247,77.76279 L29.494545,77.76279 C29.733804,77.3624 30.0695,77.036474 30.501633,76.78501 C30.933765,76.533546 31.406181,76.407814 31.918881,76.407814 z M40.7737,76.590919 L43.080848,76.590919 L43.080848,80.912193 C43.080848,81.97176 43.500773,82.501544 44.340623,82.501544 C44.755666,82.501544 45.0877,82.35628 45.336725,82.065754 C45.585751,81.775228 45.710263,81.380942 45.710263,80.882897 L45.710263,76.590919 L48.017411,76.590919 L48.017411,84.090894 L45.710263,84.090894 L45.710263,82.94832 L45.673642,82.94832 C45.102348,83.832106 44.338182,84.273999 43.381143,84.273999 C41.642848,84.273999 40.7737,83.221756 40.7737,81.117271 z M49.202571,76.590919 L51.883257,76.590919 L52.952601,78.692962 C53.094204,78.971282 53.194302,79.205656 53.252897,79.396085 L53.282194,79.396085 C53.360319,79.166594 53.465301,78.927337 53.597138,78.678314 L54.688455,76.590919 L57.208007,76.590919 L54.732401,80.223719 L57.193358,84.090894 L54.527321,84.090894 L53.443328,82.069416 C53.355436,81.908284 53.250455,81.681234 53.128384,81.388266 L53.099087,81.388266 C53.025844,81.593344 52.925745,81.813069 52.798791,82.047444 L51.707474,84.090894 L49.048761,84.090894 L51.582961,80.355555 z") { }
        public Demux() : base(new Demultiplexeur(), "M0.5, 0.5 L99.5, 0.5 L99.5, 142.5 L0.5, 142.5 z M11.128565, 58.15589 L11.128565, 68.08718 L13.009429, 68.08718 C14.661775, 68.08718 15.947911, 67.644813 16.867835, 66.760078 C17.787759, 65.875344 18.24772, 64.621482 18.24772, 62.998492 C18.24772, 59.770091 16.53092, 58.15589 13.097319, 58.15589 z M9.652, 56.82 L13.132476, 56.82 C17.573891, 56.820001 19.794599, 58.86778 19.794599, 62.963337 C19.794599, 64.908581 19.177898, 66.471514 17.944497, 67.652136 C16.711096, 68.832759 15.060214, 69.42307 12.99185, 69.42307 L9.652, 69.42307 z M27.202035, 56.82 L33.600485, 56.82 L33.600485, 58.15589 L28.6786, 58.15589 L28.6786, 62.348125 L33.231344, 62.348125 L33.231344, 63.675226 L28.6786, 63.675226 L28.6786, 68.08718 L33.881736, 68.08718 L33.881736, 69.42307 L27.202035, 69.42307 z M41.23873, 56.82 L43.189906, 56.82 L47.057101, 65.608752 C47.35593, 66.282556 47.549289, 66.786444 47.63718, 67.120417 L47.689915, 67.120417 C47.941868, 66.429035 48.144017, 65.913428 48.296361, 65.573597 L52.242658, 56.82 L54.088365, 56.82 L54.088365, 69.42307 L52.620589, 69.42307 L52.620589, 60.968291 C52.620589, 60.300346 52.661604, 59.482992 52.743636, 58.516229 L52.70848, 58.516229 C52.567854, 59.084568 52.441877, 59.491781 52.330549, 59.737866 L48.0239, 69.42307 L47.303195, 69.42307 L43.005335, 59.808176 C42.882288, 59.526936 42.756311, 59.096287 42.627405, 58.516229 L42.592248, 58.516229 C42.639123, 59.020118 42.662561, 59.843331 42.662561, 60.985868 L42.662561, 69.42307 L41.23873, 69.42307 z M62.173903, 56.82 L63.650469, 56.82 L63.650469, 64.431059 C63.650469, 67.014952 64.740315, 68.306898 66.920007, 68.306898 C69.023526, 68.306898 70.075286, 67.058896 70.075286, 64.56289 L70.075286, 56.82 L71.551852, 56.82 L71.551852, 64.325594 C71.551852, 67.864531 69.955169, 69.634 66.761803, 69.634 C63.703203, 69.634 62.173903, 67.931912 62.173903, 64.527735 z M78.538965, 56.82 L80.349515, 56.82 L82.924716, 61.249531 C83.094638, 61.542489 83.244052, 61.835448 83.372959, 62.128406 L83.408115, 62.128406 C83.595615, 61.741701 83.759678, 61.437024 83.900304, 61.214376 L86.580973, 56.82 L88.277266, 56.82 L84.31339, 63.068802 L88.33, 69.42307 L86.528239, 69.42307 L83.627842, 64.598045 C83.539951, 64.451566 83.443271, 64.243566 83.337802, 63.974044 L83.302646, 63.974044 C83.244052, 64.108805 83.144443, 64.316805 83.003817, 64.598045 L80.01553, 69.42307 L78.20498, 69.42307 L82.423738, 63.08638 z") { }
    }

    [Serializable]
    public class Encod : Gate
    {
        public Encod() : base(new Encodeur(), "M0.5,0.5 L67.506996,0.5 67.506996,147.088 0.5,147.088 z M24.528501,24.494 L29.860726,24.494 L29.860726,25.607282 L25.759015,25.607282 L25.759015,29.100937 L29.553097,29.100937 L29.553097,30.206895 L25.759015,30.206895 L25.759015,33.883656 L30.095109,33.883656 L30.095109,34.996938 L24.528501,34.996938 z M24.528502,44.444015 L26.125239,44.444015 L31.384218,52.683768 C31.603953,53.025565 31.745559,53.25994 31.809038,53.386893 L31.838336,53.386893 C31.789506,53.084159 31.765091,52.569022 31.765091,51.841482 L31.765091,44.444015 L32.995605,44.444015 L32.995605,54.946953 L31.486761,54.946953 L26.081292,46.575364 C25.944568,46.365403 25.83226,46.145677 25.744366,45.916184 L25.700419,45.916184 C25.739483,46.140794 25.759015,46.621751 25.759015,47.359056 L25.759015,54.946953 L24.528502,54.946953 z M29.201522,64.21825 C30.197651,64.21825 31.022877,64.362294 31.677198,64.65038 L31.677198,65.961416 C30.925217,65.541494 30.095109,65.331533 29.186873,65.331533 C27.980775,65.331533 27.002956,65.734365 26.253417,66.54003 C25.503879,67.345694 25.129109,68.422355 25.129109,69.770013 C25.129109,71.049311 25.479464,72.068599 26.180173,72.827877 C26.880881,73.587155 27.800104,73.966794 28.937841,73.966794 C29.992566,73.966794 30.905685,73.732418 31.677198,73.263668 L31.677198,74.457517 C30.900802,74.867673 29.93397,75.072751 28.776702,75.072751 C27.282507,75.072751 26.086175,74.591794 25.187705,73.629879 C24.289235,72.667964 23.84,71.405756 23.84,69.843255 C23.84,68.163566 24.345389,66.806143 25.356168,65.770986 C26.366947,64.735829 27.648732,64.21825 29.201522,64.21825 z M28.849947,85.28155 C27.746391,85.28155 26.850363,85.679499 26.161861,86.475398 C25.47336,87.271297 25.129109,88.31622 25.129109,89.610166 C25.129109,90.904113 25.464815,91.945373 26.136226,92.733948 C26.807636,93.522523 27.682912,93.91681 28.762053,93.91681 C29.914438,93.91681 30.822674,93.540834 31.486761,92.78888 C32.150848,92.036926 32.482891,90.984679 32.482891,89.632139 C32.482891,88.245419 32.160614,87.173641 31.516059,86.416804 C30.871504,85.659968 29.9828,85.28155 28.849947,85.28155 z M28.937841,84.168267 C30.388088,84.168267 31.556344,84.656549 32.442606,85.633112 C33.328869,86.609676 33.772,87.886532 33.772,89.463682 C33.772,91.17755 33.317882,92.532532 32.409646,93.528626 C31.50141,94.524721 30.285545,95.022768 28.762053,95.022768 C27.272741,95.022768 26.080071,94.532045 25.184043,93.550599 C24.288014,92.569153 23.84,91.292297 23.84,89.72003 C23.84,88.030575 24.296559,86.682918 25.209678,85.677058 C26.122797,84.671197 27.365518,84.168267 28.937841,84.168267 z M25.759015,105.40734 L25.759015,113.68372 L27.326454,113.68372 C28.703457,113.68372 29.775273,113.31507 30.541903,112.57776 C31.308532,111.84045 31.691847,110.79553 31.691847,109.44299 C31.691847,106.75256 30.26113,105.40734 27.399699,105.40734 z M24.528501,104.29406 L27.428997,104.29406 C31.130302,104.29406 32.980956,106.00061 32.980956,109.41369 C32.980956,111.03479 32.467021,112.33728 31.439152,113.32117 C30.411282,114.30506 29.0355,114.797 27.311805,114.797 L24.528501,114.797 z"
 )
        { }
    }

    [Serializable]
    public class Decod : Gate
    {
        public Decod() : base(new Decodeur(), "M0.5,0.5 L67.506996,0.5 L67.506996,147.088 L0.5,147.088 z M23.825041,24.973271 L23.825041,31.630991 L24.99697,31.630991 C26.022407,31.630991 26.826888,31.323374 27.410411,30.708139 C27.993933,30.092904 28.285695,29.255501 28.285695,28.19593 C28.285695,27.194953 27.996375,26.407598 27.417736,25.833867 C26.839095,25.260137 26.02729,24.973271 24.982321,24.973271 z M21.45921,23.047 L25.180084,23.047 C28.910724,23.047 30.776044,24.753544 30.776044,28.166633 C30.776044,29.802377 30.266988,31.10853 29.248874,32.085093 C28.230761,33.061657 26.874497,33.549938 25.180084,33.549938 L21.45921,33.549938 z M21.45921,42.997015 L27.516617,42.997015 L27.516617,44.923286 L23.825041,44.923286 L23.825041,47.259714 L27.260258,47.259714 L27.260258,49.178661 L23.825041,49.178661 L23.825041,51.581006 L27.758327,51.581006 L27.758327,53.499953 L21.45921,53.499953 z M26.39596,62.77125 C27.421397,62.77125 28.285695,62.900645 28.988853,63.159434 L28.988853,65.437268 C28.285695,65.017346 27.484877,64.807385 26.586399,64.807385 C25.600025,64.807385 24.80409,65.117444 24.198594,65.737561 C23.593097,66.357679 23.290349,67.197524 23.290349,68.257095 C23.290349,69.272721 23.576006,70.082047 24.147322,70.685075 C24.718637,71.288103 25.487715,71.589617 26.454557,71.589617 C27.37745,71.589617 28.222215,71.365007 28.988853,70.915788 L28.988853,73.076435 C28.222215,73.447529 27.221193,73.633076 25.985785,73.633076 C24.374383,73.633076 23.107235,73.159442 22.184341,72.212176 C21.261447,71.26491 20.8,70.002702 20.8,68.425552 C20.8,66.745863 21.318823,65.383557 22.356468,64.338635 C23.394113,63.293712 24.740611,62.77125 26.39596,62.77125 z M26.000434,84.757402 C25.170318,84.757402 24.511108,85.068681 24.022804,85.69124 C23.5345,86.3138 23.290349,87.137775 23.290349,88.163166 C23.290349,89.203206 23.5345,90.025961 24.022804,90.63143 C24.511108,91.236899 25.150786,91.539634 25.941838,91.539634 C26.757304,91.539634 27.404307,91.245444 27.882845,90.657065 C28.361382,90.068685 28.600651,89.252034 28.600651,88.207112 C28.600651,87.118244 28.368707,86.271075 27.904819,85.665606 C27.44093,85.060136 26.806135,84.757402 26.000434,84.757402 z M26.066355,82.721267 C27.565447,82.721267 28.777661,83.21199 29.702997,84.193437 C30.628332,85.174883 31.091,86.468829 31.091,88.075276 C31.091,89.725668 30.611242,91.056235 29.651725,92.066978 C28.692208,93.077721 27.44093,93.583093 25.89789,93.583093 C24.393915,93.583093 23.168273,93.09359 22.220964,92.114585 C21.273655,91.135581 20.8,89.859945 20.8,88.287678 C20.8,86.627521 21.280979,85.284746 22.242938,84.259355 C23.204895,83.233963 24.479368,82.721267 26.066355,82.721267 z M23.825041,104.77333 L23.825041,111.43105 L24.99697,111.43105 C26.022407,111.43105 26.826888,111.12344 27.410411,110.5082 C27.993933,109.89297 28.285695,109.05556 28.285695,107.99599 C28.285695,106.99501 27.996375,106.20766 27.417736,105.63393 C26.839095,105.0602 26.02729,104.77333 24.982321,104.77333 z M21.45921,102.84706 L25.180084,102.84706 C28.910724,102.84706 30.776044,104.55361 30.776044,107.96669 C30.776044,109.60244 30.266988,110.90859 29.248874,111.88516 C28.230761,112.86172 26.874497,113.35 25.180084,113.35 L21.45921,113.35 z") { }
    }



    //la partie sequentielle

    [Serializable]
    public class Cpt : Gate
    {
        public Cpt() : base(new Compteur(), "M0.5,0.5 L56.167,0.5 L56.167,54.167 L0.5,54.167 z M22.455805,21.876 C23.518316,21.876 24.398532,22.029643 25.096456,22.336928 L25.096456,23.735336 C24.294364,23.28743 23.40894,23.063475 22.44018,23.063475 C21.15371,23.063475 20.11073,23.493154 19.311244,24.352511 C18.511757,25.211868 18.112013,26.360282 18.112013,27.797752 C18.112013,29.162308 18.485715,30.249525 19.233118,31.059403 C19.980521,31.869282 20.960999,32.274222 22.174553,32.274222 C23.299564,32.274222 24.273531,32.024227 25.096456,31.524237 L25.096456,32.797649 C24.268323,33.235139 23.237063,33.453885 22.002676,33.453885 C20.408911,33.453885 19.132856,32.940874 18.174514,31.914854 C17.216171,30.888835 16.737,29.542508 16.737,27.875876 C16.737,26.084247 17.276068,24.63636 18.354203,23.532216 C19.432339,22.428073 20.799539,21.876 22.455805,21.876 z M30.964117,26.157161 C30.219318,26.157161 29.620354,26.416271 29.167225,26.934489 C28.714096,27.452708 28.487531,28.102433 28.487531,28.883667 L28.487531,30.000832 C28.487531,30.662277 28.702377,31.223462 29.132068,31.68439 C29.56176,32.145318 30.107339,32.375782 30.768803,32.375782 C31.544852,32.375782 32.152931,32.078913 32.593038,31.485176 C33.033148,30.891438 33.253201,30.065935 33.253201,29.008665 C33.253201,28.118058 33.047471,27.420157 32.636008,26.914958 C32.224547,26.40976 31.667249,26.157161 30.964117,26.157161 z M31.284433,25.079059 C32.315692,25.079059 33.120387,25.437124 33.698518,26.153255 C34.276649,26.869387 34.565714,27.829002 34.565714,29.032102 C34.565714,30.370616 34.240191,31.442208 33.589142,32.246879 C32.938095,33.05155 32.047461,33.453885 30.917242,33.453885 C29.880774,33.453885 29.081287,33.005978 28.518781,32.110163 L28.487531,32.110163 L28.487531,36.946 L27.206268,36.946 L27.206268,25.266555 L28.487531,25.266555 L28.487531,26.672776 L28.518781,26.672776 C29.148995,25.610298 30.070879,25.079059 31.284433,25.079059 z M38.308355,22.899416 L38.308355,25.266555 L40.324,25.266555 L40.324,26.360282 L38.308355,26.360282 L38.308355,30.868001 C38.308355,31.404449 38.399503,31.787253 38.581796,32.016415 C38.764088,32.245576 39.066175,32.360158 39.488054,32.360158 C39.810974,32.360158 40.089623,32.271618 40.324,32.094538 L40.324,33.188265 C40.021913,33.354928 39.623472,33.43826 39.128676,33.43826 C37.727621,33.43826 37.027093,32.657026 37.027093,31.094559 L37.027093,26.360282 L35.65208,26.360282 L35.65208,25.266555 L37.027093,25.266555 L37.027093,23.31347 z") { }
    }

    [Serializable]
    public class BasculeJk : Gate
    {
        public BasculeJk() : base(new JK(), "M0.5,0.5 L66.167,0.5 L66.167,64.975999 L0.5,64.975999 z M22.627459,19.048 L27.658775,19.048 L27.658775,31.605025 C27.658774,34.392249 26.963453,36.526218 25.57281,38.006931 C24.182167,39.487644 22.169119,40.228 19.533668,40.228 C18.356569,40.228 17.268013,40.039282 16.268,39.661845 L16.268,35.263257 C17.132595,35.872962 18.101357,36.177815 19.174288,36.177815 C21.476402,36.177815 22.627459,34.590645 22.627459,31.416307 z M32.565756,19.048 L37.612698,19.048 L37.612698,28.890385 L37.690824,28.890385 C37.815825,28.619406 38.024161,28.232291 38.315832,27.729042 L44.503412,19.048 L50.519116,19.048 L42.706513,28.977486 L51.191,39.86508 L44.815916,39.86508 L38.284582,30.821119 C38.15958,30.646917 37.96166,30.264642 37.690824,29.674292 L37.612698,29.674292 L37.612698,39.86508 L32.565756,39.86508 z") { }
    }

    [Serializable]
    public class BasculeD : Gate
    {
        public BasculeD() : base(new D(), "M0.5,0.5 L66.167,0.5 66.167,64.975999 0.5,64.975999 z M28.365093,26.209089 L28.365093,36.86163 L30.240061,36.86163 C31.880659,36.86163 33.167747,36.369434 34.101325,35.38504 C35.034902,34.400647 35.501691,33.060778 35.501692,31.365434 C35.501691,29.763842 35.038808,28.504053 34.113043,27.586067 C33.187278,26.668082 31.888471,26.20909 30.216624,26.209089 z M24.58,23.127 L30.533025,23.127 C36.501674,23.127 39.486,25.85752 39.486,31.318559 C39.486,33.935795 38.671561,36.025677 37.042682,37.588206 C35.413803,39.150735 33.243917,39.932 30.533025,39.932 L24.58,39.932 z") { }
    }

    [Serializable]
    public class BasculeRst : Gate
    {
        public BasculeRst() : base(new RST(), "M0.5,0.5 L66.167,0.5 66.167,64.975999 0.5,64.975999 z M18.947176,27.153204 L18.947176,31.829011 L20.58781,31.829011 C21.400314,31.829011 22.052662,31.594635 22.544852,31.125882 C23.044854,30.649317 23.294856,30.05947 23.294856,29.356341 C23.294856,27.887584 22.415945,27.153205 20.658123,27.153204 z M15.162,24.317252 L21.162032,24.317252 C25.240177,24.317252 27.27925,25.840698 27.279252,28.887589 C27.27925,29.473529 27.189406,30.014548 27.009719,30.510644 C26.830029,31.006741 26.576121,31.454009 26.247997,31.852448 C25.919868,32.250888 25.523381,32.59464 25.058537,32.883704 C24.593691,33.172768 24.07611,33.399332 23.505794,33.563395 L23.505794,33.61027 C23.755796,33.688396 23.997985,33.81535 24.232361,33.991132 C24.466737,34.166914 24.693301,34.371993 24.912052,34.60637 C25.130803,34.840746 25.339789,35.0927 25.539008,35.362233 C25.738226,35.631766 25.919868,35.895439 26.083933,36.153253 L29.259731,41.12203 L24.912052,41.12203 L22.298757,36.797788 C22.103443,36.469661 21.915942,36.17669 21.736254,35.918877 C21.556565,35.661063 21.374924,35.440358 21.191329,35.256764 C21.007734,35.073169 20.816327,34.932543 20.617107,34.834886 C20.417887,34.73723 20.201089,34.688401 19.966713,34.688401 L18.947176,34.688401 L18.947176,41.12203 L15.162,41.12203 z M36.582686,24.036 C37.457691,24.036 38.233086,24.088735 38.908871,24.194204 C39.584655,24.299673 40.207706,24.461784 40.778021,24.680535 L40.778021,28.18446 C40.49677,27.989147 40.190127,27.817271 39.858094,27.668832 C39.526061,27.520395 39.184263,27.397347 38.832698,27.299689 C38.481134,27.202034 38.131523,27.129768 37.783865,27.082891 C37.436207,27.036017 37.106127,27.01258 36.793625,27.012579 C36.363935,27.01258 35.973308,27.053595 35.621744,27.135626 C35.270179,27.217659 34.973303,27.332894 34.731114,27.481331 C34.488925,27.62977 34.301424,27.807506 34.168611,28.014537 C34.035798,28.22157 33.969391,28.453994 33.969391,28.711806 C33.969391,28.993058 34.04361,29.245012 34.192049,29.46767 C34.340487,29.690327 34.551426,29.901266 34.824864,30.100486 C35.098303,30.299706 35.430336,30.495019 35.820964,30.686427 C36.211591,30.877834 36.652999,31.0751 37.145189,31.278227 C37.817068,31.559478 38.420587,31.858308 38.955746,32.174716 C39.490905,32.491124 39.949892,32.848548 40.332706,33.246987 C40.715521,33.645427 41.008491,34.100507 41.211617,34.612229 C41.414743,35.12395 41.516306,35.719657 41.516306,36.399348 C41.516306,37.336853 41.338571,38.123967 40.9831,38.760689 C40.62763,39.397411 40.145205,39.913039 39.535827,40.307572 C38.926449,40.702106 38.217461,40.98531 37.408863,41.157186 C36.600265,41.329062 35.746744,41.415 34.848302,41.415 C33.926422,41.415 33.049464,41.336875 32.217429,41.180624 C31.385393,41.024373 30.664686,40.789997 30.055308,40.477495 L30.055308,36.727475 C30.734999,37.29779 31.473284,37.725527 32.270163,38.010685 C33.067043,38.295843 33.871734,38.438421 34.684239,38.438421 C35.160804,38.438421 35.576822,38.395452 35.932292,38.309514 C36.287763,38.223577 36.584639,38.104435 36.822922,37.952091 C37.061205,37.799746 37.23894,37.620058 37.356128,37.413025 C37.473316,37.205993 37.53191,36.981382 37.53191,36.739194 C37.53191,36.411067 37.43816,36.118096 37.250659,35.860283 C37.063158,35.602469 36.807297,35.364186 36.483076,35.145435 C36.158856,34.926684 35.774088,34.715745 35.328773,34.512619 C34.883459,34.309493 34.402987,34.102461 33.887359,33.891522 C32.574853,33.344644 31.596332,32.676672 30.951797,31.887605 C30.307262,31.098538 29.984995,30.145408 29.984995,29.028214 C29.984995,28.153211 30.160777,27.401253 30.512342,26.772343 C30.863906,26.143434 31.342424,25.625853 31.947896,25.2196 C32.553368,24.813348 33.254544,24.514518 34.051423,24.323111 C34.848302,24.131704 35.692057,24.036 36.582686,24.036 z M42.723554,24.317252 L56.083,24.317252 L56.083,27.399299 L51.290006,27.399299 L51.290006,41.12203 L47.493111,41.12203 L47.493111,27.399299 L42.723554,27.399299 z") { }
    }

    [Serializable]
    public class BasculeT : Gate
    {
        public BasculeT() : base(new T(), "M0.5,0.5 L66.167,0.5 66.167,64.975999 0.5,64.975999 z M23.021,23.127 L36.381,23.127 L36.381,26.209089 L31.587807,26.209089 L31.587807,39.932 L27.790754,39.932 L27.790754,26.209089 L23.021,26.209089 z") { }
    }

    [Serializable]
    public class Reg : Gate
    {
        public Reg() : base(new Reg_Dec(), "M0.5,0.5 L77.5,0.5 L77.5,53.5 L0.5,53.5 z M12.114488,21.249441 L12.114488,25.311737 L13.895721,25.311737 C14.223843,25.311737 14.527226,25.262261 14.805869,25.163307 C15.084512,25.064354 15.325396,24.922434 15.528519,24.737547 C15.731642,24.552661 15.890494,24.32611 16.005077,24.057894 C16.119659,23.789678 16.17695,23.488912 16.17695,23.155595 C16.17695,22.556667 15.982941,22.089242 15.594924,21.753322 C15.206907,21.417401 14.645714,21.249441 13.911346,21.249441 z M10.802,20.062 L14.145719,20.062 C14.635298,20.062 15.087117,20.123195 15.501175,20.245585 C15.915234,20.367975 16.274606,20.554163 16.57929,20.80415 C16.883975,21.054138 17.122254,21.36532 17.294127,21.737697 C17.466001,22.110075 17.551938,22.546251 17.551938,23.046225 C17.551938,23.436831 17.493344,23.794886 17.376158,24.12039 C17.258972,24.445896 17.092306,24.736245 16.876163,24.991441 C16.660018,25.246637 16.399604,25.464073 16.09492,25.643752 C15.790235,25.82343 15.44779,25.962746 15.067586,26.061699 L15.067586,26.092948 C15.255084,26.176278 15.417843,26.271325 15.555862,26.37809 C15.693882,26.484856 15.825391,26.611151 15.95039,26.756977 C16.075389,26.902804 16.199085,27.06816 16.32148,27.253046 C16.443875,27.437933 16.580592,27.652766 16.731633,27.897545 L18.833176,31.264564 L17.27069,31.264564 L15.395708,28.124096 C15.223834,27.832445 15.057169,27.583759 14.895712,27.37804 C14.734255,27.172321 14.568892,27.004361 14.399623,26.874159 C14.230354,26.743957 14.048064,26.64891 13.852753,26.589017 C13.657442,26.529124 13.437392,26.499177 13.192603,26.499177 L12.114488,26.499177 L12.114488,31.264564 L10.802,31.264564 z M22.847202,24.155545 C22.24304,24.155545 21.730024,24.371681 21.308153,24.80395 C20.886282,25.23622 20.625868,25.801296 20.52691,26.499177 L24.847183,26.499177 C24.841974,25.759631 24.663591,25.18414 24.312032,24.772701 C23.960473,24.361264 23.472196,24.155545 22.847202,24.155545 z M22.870639,23.077474 C23.917504,23.077474 24.727392,23.415999 25.300304,24.093048 C25.873215,24.770098 26.159671,25.710154 26.159671,26.913219 L26.159671,27.585061 L20.511286,27.585061 C20.532118,28.475641 20.771699,29.163106 21.230029,29.647457 C21.688358,30.131808 22.31856,30.373983 23.120637,30.373983 C24.021669,30.373983 24.849787,30.077123 25.604989,29.483403 L25.604989,30.686468 C24.90187,31.196858 23.972191,31.452054 22.815952,31.452054 C21.685753,31.452054 20.797741,31.088791 20.151914,30.362265 C19.506087,29.635739 19.183173,28.613655 19.183173,27.296013 C19.183173,26.051284 19.536034,25.037011 20.241757,24.253196 C20.947479,23.469382 21.823774,23.077474 22.870639,23.077474 z M31.393477,24.155545 C30.601818,24.155545 29.982031,24.443291 29.534119,25.018783 C29.086207,25.594276 28.86225,26.400224 28.86225,27.43663 C28.86225,28.327211 29.077092,29.039415 29.506776,29.573242 C29.936459,30.107069 30.505464,30.373983 31.213791,30.373983 C31.932534,30.373983 32.517164,30.118787 32.967681,29.608397 C33.418196,29.098006 33.643456,28.444392 33.643456,27.647557 L33.643456,26.467929 C33.643456,25.832545 33.428614,25.288301 32.998931,24.835198 C32.569247,24.382096 32.034096,24.155545 31.393477,24.155545 z M31.213791,23.077474 C32.291906,23.077474 33.091377,23.509744 33.612206,24.374284 L33.643456,24.374284 L33.643456,23.264965 L34.924694,23.264965 L34.924694,30.623971 C34.924694,33.561324 33.518457,35.03 30.705983,35.03 C29.716408,35.03 28.851834,34.842509 28.112257,34.467528 L28.112257,33.186342 C29.01329,33.686317 29.872657,33.936305 30.690358,33.936305 C32.65909,33.936305 33.643456,32.889482 33.643456,30.795837 L33.643456,29.920881 L33.612206,29.920881 C33.002837,30.941664 32.086178,31.452054 30.862232,31.452054 C29.86745,31.452054 29.066676,31.096603 28.45991,30.385701 C27.853144,29.6748 27.549762,28.720421 27.549762,27.522564 C27.549762,26.163257 27.876582,25.082582 28.530222,24.280539 C29.183862,23.478496 30.078385,23.077474 31.213791,23.077474 z M36.223014,32.655119 L42.863578,32.655119 L42.863578,33.58476 L36.223014,33.58476 z M45.644177,21.249441 L45.644177,30.077123 L47.316036,30.077123 C48.784773,30.077123 49.927989,29.683913 50.745692,28.897495 C51.563392,28.111076 51.972243,26.996549 51.972243,25.553912 C51.972243,22.684265 50.446214,21.249441 47.394161,21.249441 z M44.331689,20.062 L47.42541,20.062 C51.373288,20.062 53.347231,21.882221 53.347231,25.522664 C53.347231,27.251744 52.799056,28.640998 51.702715,29.690424 C50.606369,30.73985 49.138934,31.264564 47.300411,31.264564 L44.331689,31.264564 z M58.496872,24.155545 C57.892709,24.155545 57.379695,24.371681 56.957823,24.80395 C56.535952,25.23622 56.275537,25.801296 56.176581,26.499177 L60.496853,26.499177 C60.491646,25.759631 60.313261,25.18414 59.961702,24.772701 C59.610143,24.361264 59.121866,24.155545 58.496872,24.155545 z M58.520309,23.077474 C59.567174,23.077474 60.377061,23.415999 60.949974,24.093048 C61.522887,24.770098 61.809341,25.710154 61.809341,26.913219 L61.809341,27.585061 L56.160956,27.585061 C56.181788,28.475641 56.421371,29.163106 56.879699,29.647457 C57.338027,30.131808 57.968232,30.373983 58.770307,30.373983 C59.671341,30.373983 60.499459,30.077123 61.254659,29.483403 L61.254659,30.686468 C60.55154,31.196858 59.621861,31.452054 58.465622,31.452054 C57.335425,31.452054 56.44741,31.088791 55.801584,30.362265 C55.155758,29.635739 54.832843,28.613655 54.832843,27.296013 C54.832843,26.051284 55.185703,25.037011 55.891427,24.253196 C56.597151,23.469382 57.473444,23.077474 58.520309,23.077474 z M67.355642,23.077474 C68.058761,23.077474 68.678548,23.207676 69.215,23.468079 L69.215,24.780514 C68.621255,24.363868 67.985843,24.155545 67.308768,24.155545 C66.491068,24.155545 65.8205,24.448499 65.297067,25.034407 C64.773635,25.620315 64.511918,26.389808 64.511918,27.342885 C64.511918,28.280338 64.75801,29.019885 65.250193,29.561524 C65.742376,30.103163 66.402526,30.373983 67.230643,30.373983 C67.928555,30.373983 68.584799,30.142223 69.199375,29.678706 L69.199375,30.897395 C68.584799,31.267167 67.855638,31.452054 67.011895,31.452054 C65.871281,31.452054 64.950715,31.080979 64.250202,30.338829 C63.549689,29.596678 63.199431,28.634487 63.199431,27.452255 C63.199431,26.134613 63.577033,25.076072 64.332233,24.276633 C65.087433,23.477194 66.095236,23.077474 67.355642,23.077474 z") { }
    }


    // Liaisons
    //Les pin et l'horloge 
    [Serializable]
    public class pin_entree : Gate
    {

        public pin_entree() : base(new PinIn(), "M57.5,29 C57.5,44.740115 44.740115,57.5 29,57.5 C13.259885,57.5 0.5,44.740115 0.5,29 C0.5,13.259885 13.259885,0.5 29,0.5 C44.740115,0.5 57.5,13.259885 57.5,29 z M35.5,28 C35.5,31.589851 32.365993,34.5 28.5,34.5 C24.634007,34.5 21.5,31.589851 21.5,28 C21.5,24.410149 24.634007,21.5 28.5,21.5 C32.365993,21.5 35.5,24.410149 35.5,28 z")
        {
            path.Height = 25;
            path.Width = 29;

            path.Fill = Brushes.Red;
            this.MouseDoubleClick += new MouseButtonEventHandler(OnClick);
        }

        public void OnClick(object sender, MouseEventArgs e)
        {

            if (this.outil.getListeentrees()[0].getEtat().Equals(true))
            {
                this.outil.getListeentrees()[0].setEtat(false);
                //le calcul automatique 
                path.Fill = Brushes.Red;
                ((PinIn)(this.outil)).Calcul();
            }
            else
            {
                this.outil.getListeentrees()[0].setEtat(true);
                path.Fill = Brushes.Green;
                ((PinIn)(this.outil)).Calcul();
            }

        }
    }

    [Serializable]
    public class pin_sortie : Gate
    {
        public pin_sortie() : base(new PinOut(), "M0.5, 0.5 L79.5, 0.5 L79.5, 79.5 L0.5, 79.5 z M71.166003, 39.285999 C71.166003, 54.131966 56.67928, 66.166999 38.809002, 66.166999 C20.938723, 66.166999 6.452, 54.131966 6.452, 39.285999 C6.452, 24.440033 20.938723, 12.405 38.809002, 12.405 C56.67928, 12.405 71.166003, 24.440033 71.166003, 39.285999 z")
        {
            path.Height = 25;
            path.Width = 29;

            (outil as PinOut).PropertyChanged += new PropertyChangedEventHandler((sender, e) =>

            {
                Application.Current.Dispatcher.Invoke(() => {
                    if (outil.getEntreeSpecifique(0).getEtat()) path.Fill = Brushes.Green;
                    else path.Fill = Brushes.Red;
                });
            });
        }

    }

    [Serializable]
    public class horloge : Gate
    {
        public horloge() : base(new Horloge(), "M1.0000047, 1.0000005 L83.333005, 1.0000005 L83.333005, 27.571001 L1.0000047, 27.571001 z M0.99999997, 28.570993 L13.095017, 1 M15.094999, 1 L27.380993, 28.570993 M29.381017, 28.570993 L40.476016, 1 M42.476016, 1 L54.761992, 28.570993 M56.762006, 28.570993 L69.047004, 1 M71.047003, 1 L84.332993, 28.570993")
        {
            path.Height = 25;
            path.Width = 29;
            this.MouseDoubleClick += new MouseButtonEventHandler(OnClick);
        }
        public void OnClick(object sender, MouseEventArgs e)
        {

            ((Horloge)this.outil).Demmarer();

        }
    }

    public class constantetrue : Gate
    {
        public constantetrue() : base(new ConstanteTrue(), "M0.5,0.5 L38.611,0.5 L38.611,51.944 L0.5,51.944 z")
        {
            path.Fill = Brushes.Green;
            path.Height = 25;
            path.Width = 29;
        }
    }

    public class constantefalse : Gate
    {
        public constantefalse() : base(new ConstanteFalse(), "M0.5,0.5 L38.611,0.5 L38.611,51.944 L0.5,51.944 z")
        {
            path.Fill = Brushes.Red;
            path.Height = 25;
            path.Width = 29;
        }
    }


    //le circuit perssonalisé 
    public class CircuitComplet : Gate
    {
        public CircuitComplet(CircuitPersonnalise c) : base(c, "M0.5,0.5 L38.611,0.5 L38.611,51.944 L0.5,51.944 z") { }
        public CircuitComplet() : base(new CircuitPersonnalise(), "M0.5,0.5 L38.611,0.5 L38.611,51.944 L0.5,51.944 z") { }
    }

    #endregion
}



