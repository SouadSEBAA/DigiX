using System;

namespace logisimConsole
{
    [Serializable]
    public class OutStruct
    {
        private ClasseEntree entree;
        private Outils outils;

        public OutStruct(ClasseEntree entree, Outils outils)
        {
            this.entree = entree;
            this.outils = outils;

        }

        //public Outstruct(){ }
        public void setOutil(Outils outil) { outils = outil; }
        public Outils getOutils() { return this.outils; }
        public ClasseEntree GetEntree() { return this.entree; }

        public override bool Equals(Object obj)
        {
            return ((OutStruct)obj).entree.Equals(this.entree) && ((OutStruct)obj).outils.Equals(this.outils);
        }




        /*essai
        public ClasseEntree getEntree()
        {
            return outils.getEntreeSpecifique(num_entree);
        }*/
    }
}
