using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Linq;

namespace logisimConsole
{
	[Serializable]
	public class Horloge : IN
	{
		public Outils fin;
		//Constructeur 
		public Horloge()
		{
			this.nb_entrees = 0;
			this.nb_sorties = 1;
			this.etiquette = "horloge";
			this.liste_entrees = new List<ClasseEntree>();
			this.liste_sorties = new List<Sortie>();
			this.disposition = Disposition.right;
			//liste_sorties.Add(new Sortie("Sortie",1, Disposition.down, false, null));
			//liste_entrees.Add(new ClasseEntree(0, Disposition.left, false, false));
			//on demare le thread
	        /*this.mythread = new Thread(new ThreadStart(this.auto));
			mythread.Start();*/
			liste_sorties.Add(new Sortie("Sortie Horloge",1, Disposition.right, false, new List<OutStruct>()));
			liste_entrees.Add(new ClasseEntree("",0, Disposition.left, false, false));
		}

		public void Demmarer()
		{
			this.mythread = new Thread(new ThreadStart(this.auto));
				mythread.Start();

		}

		//  le Tour du signal d'horloge 

		int T = 1000;
		int UP = 500;
		Thread mythread;
		//Task task;
		bool stop = false;


		//***********
		public void auto()
		{
			//fin = circuit.getCircuit().Vertices.Last();
			bool parti = false;
			while (!this.stop)
			{
				if (stop) {/* mythread.Interrupt();*/ break; }
				else
				{
					if (parti)//etat haut 
					{
						(this.liste_sorties[0]).setEtat(true);/*this.circuit.Evaluate(circuit.getCircuit().Vertices.Last()); *///this.Calcul();
							this.Calcul();

						Thread.Sleep(UP);
					}
					else//etat bas 
					{
						(this.liste_sorties[0]).setEtat(false); /*this.circuit.Evaluate(circuit.getCircuit().Vertices.Last()); *///this.Calcul(); //this.circuit.EvaluateCircuit();
							this.Calcul();
						Thread.Sleep(T - UP);
					}
					parti = !parti;

				}
			}

		}

		//Chronogramme
		public void auto(object sender, EventArgs e )
		{
			bool parti = false;
			while (!this.stop)
			{
					if (parti)//etat haut 
					{
						(this.liste_sorties[0]).setEtat(true); this.appelCalcul();
						Thread.Sleep(UP);
					}
					else//etat bas 
					{
						(this.liste_sorties[0]).setEtat(false); this.appelCalcul();
						Thread.Sleep(T - UP);
					}
					parti = !parti;
			}

		}

		//*********
		
		public override void calcul_sorties() { }

		public void arreter()
		{
			Console.WriteLine("arreter");
			this.stop = true;
		}
		//**********
		public void mini()
		{
			this.stop = false;
			//mythread.Start();
		}
		//**********/
		public Horloge(int T, int UP)
		{

			this.nb_entrees = 0;
			this.nb_sorties = 1;
			this.etiquette = "horloge";
			this.liste_entrees = new List<ClasseEntree>();
			this.liste_sorties = new List<Sortie>();
			this.disposition = Disposition.right;
			liste_sorties.Add(new Sortie(1, Disposition.down, false, new List<OutStruct>()));
			liste_entrees.Add(new ClasseEntree(0, Disposition.left, false, false));

			this.T = T; this.UP = UP;
			this.mythread = new Thread(new ThreadStart(this.auto));
			

			//task = Task.Run(new Action(auto));
			mythread.Start();
			//tf.StartNew(new Action(auto));

			// TODO: Add constructor logic here
			//
		}

		public int getUP()
		{
			return UP;
		}

		public void setUP(int UP)
		{
			this.UP =UP;
		}

		public void setT(int T)
		{
			this.T = T;
		}

		public void setTUp(int T, int UP)
		{
			this.UP = UP;
			this.T = T;
		}
	}
}
