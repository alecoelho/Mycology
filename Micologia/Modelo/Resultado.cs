using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class Resultado
    {
        public Resultado() { }
        public virtual int nIDResultado { get; set; }
        public virtual IList<ExameResultado> ExameResultados { get; set; }
        public virtual string sDescricao { get; set; }
    }
}
