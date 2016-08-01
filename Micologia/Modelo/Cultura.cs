using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class Cultura
    {
        public Cultura() { }
        public virtual int nIDCultura { get; set; }
        public virtual IList<ExameResultado> ExameResultados { get; set; }
        public virtual string sDescricao { get; set; }
    }
}
