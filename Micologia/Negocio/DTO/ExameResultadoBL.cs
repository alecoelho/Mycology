using System;
using System.Collections.Generic;
using Micologia.Repositorio;
using Micologia.Modelo;
using System.Linq;

namespace Micologia.Negocio
{
    public class ExameResultadoBL : GenericRepository<MICOLOGIA_ExameResultado>, IMicologia<MICOLOGIA_ExameResultado>
    {
        public ExameResultadoBL()
        { }

        public void Inserir(MICOLOGIA_ExameResultado entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        public void Alterar(MICOLOGIA_ExameResultado entity)
        {
            base.Update(entity);
            base.SaveChanges();
        }

        public void Excluir(MICOLOGIA_ExameResultado entity)
        {
            base.Delete(entity);
            base.SaveChanges();
        }

        public MICOLOGIA_ExameResultado ListarPorId(object id)
        {
            return base.SelectByKey(id);
        }

        public IList<MICOLOGIA_ExameResultado> ListarTodos()
        {
            return base.GetAll();
        }

        public IList<MICOLOGIA_ExameResultado> Pesquisar(string exame, string dtExame, string paciente, string prontuario, string medicoSolicitante, int idResultado,
                                                            int idCultura, string sLocal, string sMaterial, int ano)
        {
            var query = GetQuery();

            if (!String.IsNullOrEmpty(sLocal))
                query = query.Where(x => x.sLocal.Contains(sLocal));

            if (!String.IsNullOrEmpty(sMaterial) && sMaterial != "Selecione")
                query = query.Where(x => x.sMaterial.Contains(sMaterial));

            if (!String.IsNullOrEmpty(paciente))
                query = query.Where(x => x.Exame.Paciente.sNome.Contains(paciente));

            if (!String.IsNullOrEmpty(medicoSolicitante))
                query = query.Where(x => x.Exame.sMedicoSolicitante.Contains(medicoSolicitante));

            if (!String.IsNullOrEmpty(dtExame) && dtExame != "  /  /")
            {
                DateTime dt = Convert.ToDateTime(dtExame);
                query = query.Where(x => x.Exame.dDataExame == dt);
            }

            if (!String.IsNullOrEmpty(prontuario))
            {
                int num = int.Parse(prontuario);
                query = query.Where(x => x.Exame.nProntuario == num);
            }

            if (!String.IsNullOrEmpty(prontuario))
            {
                int num = int.Parse(prontuario);
                query = query.Where(x => x.Exame.nProntuario == num);
            }

            if (!String.IsNullOrEmpty(exame))
            {
                int num = int.Parse(exame);
                query = query.Where(x => x.Exame.nNumero == num);
            }

            if (idCultura != 0)
                query = query.Where(x => x.nIDCultura == idCultura);

            if (idResultado != 0)
                query = query.Where(x => x.nIDResultado == idResultado);

            if (ano != 0)
                query = query.Where(x => x.Exame.nAno == ano);

            return query.OrderBy(x => x.Exame.nNumero).ThenBy(x => x.Exame.Paciente.sNome).ToList();
        }

        public IList<MICOLOGIA_ExameResultado> PesquisarDatas(DateTime dtInicio, DateTime dtFim)
        {
            var query = GetQuery();

            dtInicio = Convert.ToDateTime(DateTime.Now.ToString(dtInicio.ToString("dd/MM/yyyy ") + "00:00:00"));
            dtFim = Convert.ToDateTime(DateTime.Now.ToString(dtFim.ToString("dd/MM/yyyy ") + "23:59:59"));

            query = query.Where(x => x.Exame.dDataExame >= dtInicio);
            query = query.Where(x => x.Exame.dDataExame <= dtFim);

            query.OrderBy(x => x.Exame.nNumero);

            return query.ToList();
        }
    }
}
