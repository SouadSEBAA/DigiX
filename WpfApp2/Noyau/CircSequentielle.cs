using System;

namespace logisimConsole
{
    [Serializable]
    abstract class CircSequentielle : Circuit
    {
        protected bool Trigger; //si Trigger = 1 alors le circuit se déclenche sur un FrontMontant

        protected bool front;  //Indique si front montant (si Trigger= 1), indique un front descendant sinon

        private System.Timers.Timer timer = new System.Timers.Timer(10); //Timer qui s'occupe de la gestion du front
        //TO DO changer 100

        public CircSequentielle(int nb_entrees, int nb_sorties, string etiquette, Disposition dispo) : base(nb_entrees, nb_sorties, etiquette, dispo)
        {
            Trigger = true; //Initialisé à etre déclenché sur un front montant (par défaut)
            timer.AutoReset = false; //Indique que le task du timer ne va s'exécuter qu'une seule fois
            timer.Elapsed += Maj; //Associe la methode a exécuter une fois l'intervalle de temps dépassé
        }
        public CircSequentielle() : base()
        {
            Trigger = true; //Initialisé à etre déclenché sur un front montant (par défaut)
            timer.AutoReset = false; //Indique que le task du timer ne va s'exécuter qu'une seule fois
            timer.Elapsed += Maj; //Associe la methode a exécuter une fois l'intervalle de temps dépassé
        }


        private void Maj(Object source, System.Timers.ElapsedEventArgs elapsedEventArgs)
        {
            front = false;
        }


        //Redéfinition de la méthode set pour controler le changeemt d'état de l'entrée Clock et générer un front
        public override void setEntreeSpe(int i, bool etat)
        {
            if (i == 0)
            {
                if (etat == true && !liste_entrees[0].isEtat() && Trigger == true)
                {
                    front = true;
                    timer.Start();
                }
                else if (etat == false && liste_entrees[0].isEtat() && Trigger == false)
                {
                    front = true;
                    timer.Start();
                }
            }
            liste_entrees[i].setEtat(etat);
        }

        /*
        private void watchClockEntry()
        {
            while (true)
            {
                Clock = liste_entrees[0].isEtat();
                /*if (liste_entrees[0].isEtat() && oldClockValue == false )
                {
                    if (Trigger == true)
                    {
                        frontDetector = true;
                        Thread.Sleep(2000); //TO DO: change 2000
                        frontDetector = false;
                    }
                    oldClockValue = true;
                }
                else if (!liste_entrees[0].isEtat() && oldClockValue == true)
                {
                    if (Trigger == false)
                    {
                        frontDetector = true;
                        Thread.Sleep(2000); //TO DO: change 2000
                        frontDetector = false;
                    }
                    oldClockValue = false;
                }*/
        /* frontMontant = liste_entrees[0].isEtat();
    }*/
    }

}
