using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Micologia.Modelo;

namespace Micologia.Negocio
{
    public class PermissaoUsuarioBL
    {
        private PermissaoUsuarioBL(MICOLOGIA_Usuario pLogin)
        {
            Login = pLogin;
            SegurancaBL segurancaBL = new SegurancaBL();
            ListaPermissao = segurancaBL.listByUsuario(pLogin.nIDLogin);
        }

        public static PermissaoUsuarioBL _permissao = null;
        public static PermissaoUsuarioBL getInstance(MICOLOGIA_Usuario pLogin)
        {
            if (_permissao == null)
                _permissao = new PermissaoUsuarioBL(pLogin);

            return _permissao;
        }

        static MICOLOGIA_Usuario _Login;
        public static MICOLOGIA_Usuario Login
        {
            get { return PermissaoUsuarioBL._Login; }
            set { PermissaoUsuarioBL._Login = value; }
        }
        private static IList<MICOLOGIA_Seguranca> _listaPermissao;
        public static IList<MICOLOGIA_Seguranca> ListaPermissao
        {
            get { return _listaPermissao; }
            set { _listaPermissao = value; }
        }
    }
}
