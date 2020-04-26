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
<<<<<<< HEAD
			liste_sorties.Add(new Sortie("Sortie",1, Disposition.down, false, null));
			//liste_entrees.Add(new ClasseEntree(0, Disposition.left, false, false));
			//on demare le thread
	/*		this.mythread = new Thread(new ThreadStart(this.auto));
			mythread.Start();*/
=======
			liste_sorties.Add(new Sortie("sortie horloge",1, Disposition.down, false, new List<OutStruct>()));
			liste_entrees.Add(new ClasseEntree("",0, Disposition.left, false, false));


		}
		public void Demmarer()
		{
			this.mythread = new Thread(new ThreadStart(this.auto));
			mythread.Start();
>>>>>>> 29608c115190de007502b017e75fc7ccae1826ad

		}

		//  le Tour du signal d'horloge 

		int T = 2000;
		int UP = 1000;
		Thread mythread;
		//Task task;
		bool stop = false;
		//***********
		public void auto()
		{
			fin = circuit.getCircuit().Vertices.Last();
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
		public Horloge(int T, int UP/*, TaskFactory tf*/)
		{
			liste_sorties = new List<Sortie>();
			liste_sorties.Add(new Sortie());
			liste_sorties.Add(new Sortie());

			this.T = T; this.UP = UP;
			this.mythread = new Thread(new ThreadStart(this.auto));
			//task = Task.Run(new Action(auto));
			mythread.Start();
			//tf.StartNew(new Action(auto));
			//
			// TODO: Add constructor logic here
			//
		}

		

		public int getUP()
		{
			return UP;
		}


	}
}
