using System;
using System.Collections.Generic;
using System.Threading;

namespace logisimConsole
{
    class Horloge : Outils
    {
        //  le Tour du signal d'horloge 

        int T = 1000;
        int UP = 500;
        Thread mythread;
        bool stop = false;
        //***********
        public void auto()
        {
            bool parti = false;
            while (!this.stop)
            {
                if (stop) {/* mythread.Interrupt();*/ break; }
                else
                {
                    if (parti)//etat haut 
                    {
                        Console.WriteLine("1111");
                        (this.liste_sorties[0]).setEtat(true); this.appelCalcul();
                        Thread.Sleep(UP);
                    }
                    else//etat bas 
                    {
                        Console.WriteLine("00000");
                        (this.liste_sorties[0]).setEtat(false); this.appelCalcul(); Thread.Sleep(T - UP);
                    }
                    parti = !parti;

                }
            }

        }
        //*********

        public void arreter()
        {
            Console.WriteLine("arreter");
            this.stop = true;
        }
        //**********
        public void mini()
        {
            this.stop = false;
            mythread.Start();
        }
        //**********/
        public Horloge(int T, int UP)
        {
            liste_sorties = new List<Sortie>();
            liste_sorties.Add(new Sortie());
            liste_sorties.Add(new Sortie());

            this.T = T; this.UP = UP;
            this.mythread = new Thread(new ThreadStart(this.auto));
            mythread.Start();

            //
            // TODO: Add constructor logic here
            //
        }

        public override void calcul_sorties() { }


    }
}
