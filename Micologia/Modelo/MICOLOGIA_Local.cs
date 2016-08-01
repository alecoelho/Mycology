using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class MICOLOGIA_Local
    {
        public MICOLOGIA_Local() { }
        public virtual int nIDLocal { get; set; }
        public virtual IList<MICOLOGIA_ExameResultado> ExameResultados { get; set; }
        public virtual string sDescricao { get; set; }
    }
}
