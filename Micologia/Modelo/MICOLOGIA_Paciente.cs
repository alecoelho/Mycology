using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Micologia.Modelo
{
    [Serializable]
    public class MICOLOGIA_Paciente
    {
        public MICOLOGIA_Paciente() { }
        public virtual int nIDPaciente { get; set; }
        public virtual string sNome { get; set; }
        public virtual string sTelefone { get; set; }
        public virtual string sEmail { get; set; }
        public virtual string sCelular { get; set; }
        public virtual System.Nullable<System.DateTime> dDataNascimento { get; set; }
        public virtual string sSexo { get; set; }
        public virtual bool bBranco { get; set; }
        public virtual string sObservacao { get; set; }
        public virtual bool bAtivo { get; set; }

        public virtual IList<MICOLOGIA_Exame> Exames { get; set; }
    }
}
