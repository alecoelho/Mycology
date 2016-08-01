using System;
using System.Collections.Generic;
using Micologia.Repositorio;
using Micologia.Modelo;
using System.Linq;

namespace Micologia.Negocio
{
    public class UsuarioBL : GenericRepository<MICOLOGIA_Usuario>, IMicologia<MICOLOGIA_Usuario>
    {
        public UsuarioBL()
        { }

        public void Inserir(MICOLOGIA_Usuario entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Usuario entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Usuario entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Usuario ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Usuario> ListarTodos()
        {
            return base.GetAll();
        }

        public bool ValidarUsuario(string login, string senha)
        {
            if (ListarTodos().Where(x => x.sLogin == login && x.sSenha == senha && x.bAtivo == true).Count() == 0)
                return false;
            return true;
        }

        public MICOLOGIA_Usuario GetLoginAndPassword(string login, string senha)
        {
            whereClausula = x => x.sLogin == login && x.sSenha == senha && x.bAtivo == true;
            IList<MICOLOGIA_Usuario> lista = base.Find(whereClausula);

            if (lista.Count == 0)
                return null;

            return lista[0];
        }
    }
}
