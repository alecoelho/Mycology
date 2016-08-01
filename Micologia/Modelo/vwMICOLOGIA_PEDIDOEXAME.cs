using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Micologia.Modelo
{
    [Serializable]
    public class vwMICOLOGIA_PEDIDOEXAME
    {
        public vwMICOLOGIA_PEDIDOEXAME() { }

        public virtual int ID { get; set; }
        public virtual int nrExame { get; set; }
        public virtual DateTime data { get; set; }
        public virtual string paciente { get; set; }
        private  String idade;
        public virtual String Idade
        {
            get 
            { 
                return idade; 
            }
            set 
            {
                idade = value;//(String.IsNullOrEmpty(value) ? value : Convert.ToDateTime(value).ToString("dd/mm/yyyy")); 
            }
        } 

        public virtual int nrProntuario { get; set; }
        public virtual string medicoSolicitante { get; set; }
        public virtual string cultura { get; set; }
        public virtual string local { get; set; }
        public virtual string exame { get; set; }
        public virtual string sJustificativaProcedencia { get; set; }
        public virtual string sObsResultado { get; set; }
        public virtual string sObsCultura { get; set; }
        public virtual string ResultadoDireto { get; set; }
        public virtual string sprocedencia { get; set; }
        public virtual string sbiopsia { get; set; }
        public virtual string sMaterial { get; set; }
    }
}
