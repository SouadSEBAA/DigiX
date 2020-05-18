using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Linq;

namespace Noyau
{
	/// <summary>
	/// Un composant qui est à 0 ou à 1, pendant des intervals de temps réguliers
	/// </summary>
	[Serializable]
	public class Horloge : IN
	{
		//Constructeur 
		public Horloge()
		{
			this.nb_entrees = 0;
			this.nb_sorties = 1;
			this.etiquette = "horloge";
			this.liste_entrees = new List<ClasseEntree>();
			this.liste_sorties = new List<Sortie>();
			this.disposition = Disposition.right;
			liste_sorties.Add(new Sortie("Sortie Horloge", 1, Disposition.right, false, new List<OutStruct>()));
			liste_entrees.Add(new ClasseEntree("", 0, Disposition.left, false, false));
		}

		public void Demmarer()
		{
			this.mythread = new Thread(new ThreadStart(this.auto));
			mythread.SetApartmentState(ApartmentState.STA);
			mythread.Start();

		}

		
		/// <summary>
		/// Tour en millisecondes du signal d'horloge 
		/// </summary>
		int T = 2000;

		/// <summary>
		/// Temps en millisecondes ou l'horloge est True pendant un tour
		/// </summary>
		int UP = 1000;

		/// <summary>
		/// le Thread qui controle les intervals de temps
		/// </summary>
		Thread mythread;

		/// <summary>
		/// Si True alors l'horloge est arrétée
		/// </summary>
		bool stop = false;


		public void auto()
		{
			bool parti = false;
			while (!this.stop)
			{
				if (stop) { break; }
				else
				{
					if (parti)//etat haut 
					{
						(this.liste_sorties[0]).setEtat(true);
						this.Calcul();
						Thread.Sleep(UP);
					}
					else//etat bas 
					{
						(this.liste_sorties[0]).setEtat(false);
						this.Calcul();
						Thread.Sleep(T - UP);
					}
					parti = !parti;

				}
			}

		}

		public override void calcul_sorties() { }

		/// <summary>
		/// Permet de d'arreter l'horloge
		/// </summary>
		public void arreter()
		{
			Console.WriteLine("arreter");
			this.stop = true;
		}

		public void mini()
		{
			this.stop = false;
			//mythread.Start();
		}


		#region Getters/Setters
		/// <summary>
		/// Remet T et UP de l'horloge
		/// </summary>
		/// <param name="T">Le tour T en millisecondes</param>
		/// <param name="UP">Le temps à 1 de l'horloge pendant un tour en millisecondes</param>
		public void setTUp(int T, int UP)
		{
			this.UP = UP;
			this.T = T;
		}
		#endregion
	}
}
