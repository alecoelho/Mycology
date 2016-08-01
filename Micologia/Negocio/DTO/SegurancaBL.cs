using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Micologia.Modelo;
using Micologia.Repositorio;

namespace Micologia.Negocio
{
    public class SegurancaBL : GenericRepository<MICOLOGIA_Seguranca>, IMicologia<MICOLOGIA_Seguranca>
    {
        public SegurancaBL()
             { }

        public void Inserir(MICOLOGIA_Seguranca entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Seguranca entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Seguranca entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Seguranca ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Seguranca> ListarTodos()
        {
            return base.GetAll();
        }

        public IList<MICOLOGIA_Seguranca> listByUsuario(int idUsuario)
        {
            whereClausula = x => x.nIDLogin == idUsuario;
            return base.Find(whereClausula);
        }
    }
}

