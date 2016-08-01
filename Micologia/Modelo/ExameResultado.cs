using System.Collections.Generic; 
using System.Text; 
using System; 


namespace Micologia.Modelo {

    [Serializable]
    public class ExameResultado {
        public ExameResultado() { }
        public virtual int nIDExameResultado { get; set; }
        public virtual int nIDResultado { get; set; }
        public virtual int nIDCultura { get; set; }
        public virtual int nNumeroExame { get; set; }
        public virtual string sLocal { get; set; }

        public virtual Resultado Resultado { get; set; }
        public virtual Cultura Cultura { get; set; }
    }
}
