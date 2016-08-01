using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{
    [Serializable]
    public class Procedencia
    {
        public Procedencia() { }
        public virtual int nIDProcedencia { get; set; }
        public virtual IList<Exame> Exames { get; set; }
        public virtual string sDescrição { get; set; }
        public virtual bool bAtivo { get; set; }
    }
}
