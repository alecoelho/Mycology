using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class MICOLOGIA_ExameResultado
    {
        public MICOLOGIA_ExameResultado() { }
        public virtual int nIDExameResultado { get; set; }
        public virtual int nIDResultado { get; set; }
        public virtual int nIDCultura { get; set; }
        public virtual int nNumeroExame { get; set; }
        public virtual string sLocal { get; set; }
        public virtual string sObsResultado { get; set; }
        public virtual string sObsCultura { get; set; }
        public virtual string sMaterial { get; set; }

        public virtual MICOLOGIA_Exame Exame { get; set; }
        public virtual MICOLOGIA_Resultado Resultado { get; set; }
        public virtual MICOLOGIA_Cultura Cultura { get; set; }
    }
}
