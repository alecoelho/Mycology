using System;
using System.Collections.Generic;
using Micologia.Repositorio;
using Micologia.Modelo;

namespace Micologia.Negocio
{
    public class ProcedenciaBL : GenericRepository<MICOLOGIA_Procedencia>, IMicologia<MICOLOGIA_Procedencia>
    {
        public ProcedenciaBL()
             { }

        public void Inserir(MICOLOGIA_Procedencia entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Procedencia entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Procedencia entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Procedencia ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Procedencia> ListarTodos()
        {
            return base.GetAll();
        }
    }
}
