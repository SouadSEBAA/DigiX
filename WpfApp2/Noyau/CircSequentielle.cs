using System;

namespace Noyau
{
    /// <summary>
    /// Classe mère des circuits séquentiels
    /// Les conventions :
    /// liste_entrees[0] -> Clock
    /// </summary>
    public abstract class CircSequentielle : Circuit
    {
        /// <summary>
        /// Si True alors ce circuit séquentiel focntionne sur front montant, sur front descendant sinon
        /// </summary>
        protected bool Trigger; 

        /// <summary>
        /// Se met à True quand un front est détécté sur l'entrée de l'horloge, False sinon
        /// </summary>
        protected bool front;  

        /// <summary>
        /// Un timer qui s'occupe de la gestion du front, il permet de remettre le booléen fornt à False après un certain intervalle de temps
        /// </summary>
        /// <returns></returns>
        private System.Timers.Timer timer = new System.Timers.Timer(100); 

        public CircSequentielle() : base()
        {
            Trigger = true; //Initialisé à etre déclenché sur un front montant (par défaut)
            timer.AutoReset = false; //Indique que le task du timer ne va s'exécuter qu'une seule fois
            timer.Elapsed += Maj; //Associe la methode a exécuter une fois l'intervalle de temps dépassé
        }

        /// <summary>
        /// Remet front à faux après l'intervalle de temps précisé dans la déclaration de timer
        /// </summary>
        /// <param name="source"></param>
        /// <param name="elapsedEventArgs"></param>
        private void Maj(Object source, System.Timers.ElapsedEventArgs elapsedEventArgs)
        {
            front = false;
        }

        /// <summary>
        /// Redéfinition de la méthode setEntreeSpe pour controler le changement d'état de l'entrée Clock et générer un front
        /// </summary>
        /// <param name="i"></param>
        /// <param name="etat"></param>
        public override void setEntreeSpe(int i, bool etat)
        {
            //On vérifie avant de changer l'état d'une entrée s'il s'agit de l'entrée Clock pour controler le front 
            if (i == 0) 
            {
                if (etat == true && !liste_entrees[0].isEtat() && Trigger == true)// Si le circuit fonctionne sur front montant, et l'état actuel est False, et l'état qui s'apprete à devenir est True
                {
                    front = true; //Un front montant detecté
                    timer.Start();//Déclencher le timer
                }
                else if (etat == false && liste_entrees[0].isEtat() && Trigger == false)// Si le circuit fonctionne sur front descendant, et l'état actuel est True, et l'état qui s'apprete à devenir est False
                {
                    front = true;//Un front descendant detecté
                    timer.Start();//Déclencher le timer
                }
            }
            //On change l'état de l'entrée spécifiée
            liste_entrees[i].setEtat(etat); // Changer l'état de l'entrée Clock
        }
    }
}
