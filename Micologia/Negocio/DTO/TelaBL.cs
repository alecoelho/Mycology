using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Micologia.Modelo;
using Micologia.Repositorio;

namespace Micologia.Negocio
{
    public class TelaBL : GenericRepository<MICOLOGIA_Tela>, IMicologia<MICOLOGIA_Tela>
    {

        public TelaBL()
        { }

        public void Inserir(MICOLOGIA_Tela entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Tela entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Tela entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Tela ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Tela> ListarTodos()
        {
            return base.GetAll();
        }
    }
}
