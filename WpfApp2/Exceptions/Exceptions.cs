using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Noyau;

namespace Noyau
{
/// <summary>
/// Classe statique qui englobe une liste générale pour les exceptions actuellement affichées
/// </summary>
    public static class Exceptions
    {
        public static List<ExceptionMessage> set = new List<ExceptionMessage>(1);//Liste des exceptions actuellemnt affichées
    }
}
