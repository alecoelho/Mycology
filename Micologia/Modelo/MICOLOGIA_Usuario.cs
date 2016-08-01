using System.Collections.Generic;
using System.Text;
using System;


namespace Micologia.Modelo
{

    [Serializable]
    public class MICOLOGIA_Usuario
    {
        public MICOLOGIA_Usuario() { }
        public virtual int nIDLogin { get; set; }
        public virtual string sNome { get; set; }
        public virtual string sLogin { get; set; }
        public virtual string sSenha { get; set; }
        public virtual string sEmail { get; set; }
        public virtual string sCargo { get; set; }
        public virtual string sSexo { get; set; }
        public virtual string sTelefone { get; set; }
        public virtual bool bAtivo { get; set; }

        public virtual IList<MICOLOGIA_Seguranca> Segurancas { get; set; }
    }
}
