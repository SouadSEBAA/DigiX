namespace logisimConsole
{
    public class OutStruct
    {
        private int num_entree;
        private Outils outils;

        public OutStruct(int num_entree, Outils outils)
        {
            this.num_entree = num_entree;
            this.outils = outils;
        }

        public int getNum_entree() { return this.num_entree; }
        public void setNum_entree(int entree) { num_entree = entree; }
        public void setOutil(Outils outil) { outils = outil; }
        public Outils getOutils() { return this.outils; }

        public override bool Equals(object obj)
        {
            return ((OutStruct)obj).num_entree.Equals(this.num_entree) && ((OutStruct)obj).outils.Equals(this.outils);
        }

    }
}
