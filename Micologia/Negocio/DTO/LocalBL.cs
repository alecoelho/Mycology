using System;
using System.Collections.Generic;
using Micologia.Repositorio;
using Micologia.Modelo;

namespace Micologia.Negocio
{
    public class LocalBL : GenericRepository<MICOLOGIA_Local>, IMicologia<MICOLOGIA_Local>
    {
        public LocalBL()
             { }

        public void Inserir(MICOLOGIA_Local entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Local entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Local entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Local ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Local> ListarTodos()
        {
            return base.GetAll();
        }
    }
}
