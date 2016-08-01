using System;
using System.Collections.Generic;
using Micologia.Repositorio;
using Micologia.Modelo;

namespace Micologia.Negocio
{
    public class ResultadoBL : GenericRepository<MICOLOGIA_Resultado>, IMicologia<MICOLOGIA_Resultado>
    {
        public ResultadoBL()
             { }

        public void Inserir(MICOLOGIA_Resultado entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Resultado entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Resultado entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Resultado ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Resultado> ListarTodos()
        {
            return base.GetAll();
        }
    }
}
