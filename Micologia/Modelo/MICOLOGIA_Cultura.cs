using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class MICOLOGIA_Cultura
    {
        public MICOLOGIA_Cultura() { }
        public virtual int nIDCultura { get; set; }
        public virtual IList<MICOLOGIA_ExameResultado> ExameResultados { get; set; }
        public virtual string sDescricao { get; set; }
    }
}
