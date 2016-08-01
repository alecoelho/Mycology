using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Micologia.Modelo
{
    [Serializable]
    public class MICOLOGIA_Seguranca
    {
        public MICOLOGIA_Seguranca() { }

        public virtual int nIDSeguranca { get; set; }
        public virtual int nIDLogin { get; set; }
        public virtual int nIDTela { get; set; }
        public virtual string sIncluir { get; set; }
        public virtual string sAlterar { get; set; }
        public virtual string sExcluir { get; set; }
        public virtual string sConsultar { get; set; }

        public virtual MICOLOGIA_Usuario Usuario { get; set; }
        public virtual MICOLOGIA_Tela Tela { get; set; }
    }
}
