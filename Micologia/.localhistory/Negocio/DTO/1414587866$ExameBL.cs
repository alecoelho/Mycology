using System;
using System.Collections.Generic;
using Micologia.Repositorio;
using Micologia.Modelo;
using System.Linq;

namespace Micologia.Negocio
{
    public class ExameBL : GenericRepository<MICOLOGIA_Exame>, IMicologia<MICOLOGIA_Exame>
    {
        public ExameBL()
             { }

        public void Inserir(MICOLOGIA_Exame entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_Exame entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_Exame entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_Exame ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_Exame> ListarTodos()
        {
            return base.GetAll();
        }

        public IList<MICOLOGIA_Exame> Pesquisar(string exame, string dtExame, string paciente, string prontuario, string medicoSolicitante)
        {
            var query = GetQuery();

            if (!String.IsNullOrEmpty(paciente))
                query = query.Where(x => x.Paciente.sNome.ToUpper().Contains(paciente.ToUpper()));

            if (!String.IsNullOrEmpty(medicoSolicitante))
                query = query.Where(x => x.sMedicoSolicitante.ToUpper().Contains(medicoSolicitante.ToUpper()));

            if (!String.IsNullOrEmpty(dtExame) && dtExame != "  /  /")
            {
                DateTime dt = Convert.ToDateTime(dtExame);
                query = query.Where(x => x.dDataExame == dt);
            }

            if (!String.IsNullOrEmpty(prontuario))
            {
                int num = int.Parse(prontuario);
                query = query.Where(x => x.nProntuario == num);
            }

            if (!String.IsNullOrEmpty(exame))
            {
                int num = int.Parse(exame);
                query = query.Where(x => x.nNumeroExame == num);
            }

            query.OrderBy(x => x.nNumeroExame);
            query.OrderBy(x => x.Paciente.sNome);

            return query.ToList();
        }

        public IList<MICOLOGIA_Exame> ListarPorChave(int codigo)
        {
            whereClausula = x => x.nNumeroExame == codigo;
            return base.Find(whereClausula);
        }
    }
}
