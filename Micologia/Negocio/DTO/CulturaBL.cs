using System;
using System.Collections.Generic;
using Micologia.Repositorio;
using Micologia.Modelo;

namespace Micologia.Negocio
{
    public class CulturaBL : GenericRepository<MICOLOGIA_Cultura>, IMicologia<MICOLOGIA_Cultura>
    {
        public CulturaBL()
             { }

        public void Inserir(MICOLOGIA_Cultura entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Cultura entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Cultura entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Cultura ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Cultura> ListarTodos()
        {
            return base.GetAll();
        }
    }
}
