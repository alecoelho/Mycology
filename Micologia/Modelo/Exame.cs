using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class Exame
    {
        public Exame() { }
        public virtual int nNumeroExame { get; set; }
        public virtual System.DateTime dDataExame { get; set; }
        public virtual System.Nullable<int> nProntuario { get; set; }
        public virtual string sMedicoSolicitante { get; set; }
        public virtual string sNomePaciente { get; set; }
        public virtual System.DateTime dDataNascPaciente { get; set; }
        public virtual string sSexoPaciente { get; set; }
        public virtual bool bBiopsia { get; set; }
        public virtual bool bCorBranco { get; set; }
        public virtual string sJustificativaProcedencia { get; set; }
        public virtual string sObservacao { get; set; }
        public virtual int nIDProcedencia { get; set; }

        public virtual Procedencia Procedencia { get; set; }
    }
}
