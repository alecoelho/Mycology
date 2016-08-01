using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Micologia.Modelo
{
    [Serializable]
    public class MICOLOGIA_Tela
    {
        public MICOLOGIA_Tela() { }
        public virtual int nIDTela { get; set; }
        public virtual string sDescricao { get; set; }

        public virtual IList<MICOLOGIA_Seguranca> Segurancas { get; set; }
    }
}
