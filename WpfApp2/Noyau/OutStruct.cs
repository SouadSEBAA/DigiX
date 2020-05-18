using System;

namespace Noyau
{
    /// <summary>
    /// Cette structure englobe une entree et son outil
    /// </summary>
    public class OutStruct
    {
        private ClasseEntree entree;
        private Outils outils;

        public OutStruct(ClasseEntree entree, Outils outils)
        {
            this.entree = entree;
            this.outils = outils;

        }

        public override bool Equals(Object obj)
        {
            return ((OutStruct)obj).entree.Equals(this.entree) && ((OutStruct)obj).outils.Equals(this.outils);
        }


        public Outils getOutils() { return this.outils; }
        public ClasseEntree GetEntree() { return this.entree; }

        public int getNumEntree()
        {
            return outils.getListeentrees().IndexOf(entree);
        }
    }
}
