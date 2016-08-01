using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class MICOLOGIA_Exame
    {
        public MICOLOGIA_Exame() { }
        public virtual int nIDExame { get; set; }
        public virtual int nNumero { get; set; }
        public virtual System.DateTime dDataExame { get; set; }
        public virtual System.Nullable<int> nProntuario { get; set; }
        public virtual string sMedicoSolicitante { get; set; }
        public virtual bool bBiopsia { get; set; }
        public virtual string sJustificativaProcedencia { get; set; }
        public virtual string sObservacao { get; set; }
        public virtual int nIDProcedencia { get; set; }
        public virtual int nIDPaciente { get; set; }
        public virtual int nAno { get; set; }

        public virtual MICOLOGIA_Procedencia Procedencia { get; set; }
        public virtual MICOLOGIA_Paciente Paciente { get; set; }
        public virtual IList<MICOLOGIA_ExameResultado> ExameResultados { get; set; }
    }
}
