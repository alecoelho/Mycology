using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Micologia.Repositorio;
using Micologia.Modelo;

namespace Micologia.Negocio
{
    public class vwPedidoExameBL: GenericRepository<vwMICOLOGIA_PEDIDOEXAME>, IMicologia<vwMICOLOGIA_PEDIDOEXAME>
    {

        public vwPedidoExameBL()
        { }

        public void Inserir(vwMICOLOGIA_PEDIDOEXAME entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(vwMICOLOGIA_PEDIDOEXAME entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(vwMICOLOGIA_PEDIDOEXAME entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public vwMICOLOGIA_PEDIDOEXAME ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }
        
        public IList<vwMICOLOGIA_PEDIDOEXAME> ListarTodos()
        {
            return base.GetAll();
        }
    }
}
