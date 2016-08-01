using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{
    [Serializable]
    public class MICOLOGIA_Procedencia
    {
        public MICOLOGIA_Procedencia() { }
        public virtual int nIDProcedencia { get; set; }
        public virtual IList<MICOLOGIA_Exame> Exames { get; set; }
        public virtual string sDescricao { get; set; }
        public virtual bool bAtivo { get; set; }
    }
}
