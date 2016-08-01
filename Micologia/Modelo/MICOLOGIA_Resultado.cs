using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class MICOLOGIA_Resultado
    {
        public MICOLOGIA_Resultado() { }
        public virtual int nIDResultado { get; set; }
        public virtual IList<MICOLOGIA_ExameResultado> ExameResultados { get; set; }
        public virtual string sDescricao { get; set; }
    }
}
