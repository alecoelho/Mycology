using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Micologia.Repositorio;
using Micologia.Modelo;

namespace Micologia.Negocio
{
    public class PacienteBL : GenericRepository<MICOLOGIA_Paciente>, IMicologia<MICOLOGIA_Paciente>
    {
        public PacienteBL()
        { }

        public void Inserir(MICOLOGIA_Paciente entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Paciente entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Paciente entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Paciente ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Paciente> ListarTodos()
        {
            return base.GetAll();
        }

        public IList<MICOLOGIA_Paciente> Pesquisar(string nome, string dtNasc, bool ativo)
        {
            whereClausula = x => x.bAtivo == ativo;

            if (!String.IsNullOrEmpty(nome))
                whereClausula = x => x.sNome.ToUpper().Contains(nome.ToUpper());

            if (!String.IsNullOrEmpty(dtNasc) && dtNasc != "  /  /")
            {
                DateTime dt = Convert.ToDateTime(dtNasc);
                whereClausula = x => x.dDataNascimento == dt;
            }
            
            return Find(whereClausula).OrderBy(x => x.sNome).ToList();
        }
    }
}
