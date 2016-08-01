using Micologia.Negocio;
using Micologia.Relatorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Micologia
{
    public partial class RelatorioGerenciais : Form
    {
        IList<Modelo.MICOLOGIA_ExameResultado> listaResultados;
        IList<Modelo.MICOLOGIA_Exame> listaExames;
        DalHelper Dal = new DalHelper();
        DataTable dt;

        public RelatorioGerenciais()
        {
            InitializeComponent();
        }

        #region Singleton
        private static RelatorioGerenciais form = null;
        public static RelatorioGerenciais getInstance()
        {
            if (form == null)
                form = new RelatorioGerenciais();

            return form;
        }
        #endregion

        private void btnTotalCultura_Click(object sender, EventArgs e)
        {
            gerarPlanilha();


            //Cursor.Current = Cursors.WaitCursor;

            //string SQL = String.Empty;

            //SQL += " SELECT ";
            //SQL += "   COUNT (dbo.MICOLOGIA_Exame.nNumeroExame) as total, ";
            //SQL += "   dbo.MICOLOGIA_Cultura.sDescricao AS cultura,  ";
            //SQL += "   dbo.MICOLOGIA_ExameResultado.sLocal AS local ";

            //SQL += "   FROM         ";
            //SQL += "   dbo.MICOLOGIA_Exame INNER JOIN ";
            //SQL += "   dbo.MICOLOGIA_ExameResultado ON dbo.MICOLOGIA_Exame.nNumeroExame = dbo.MICOLOGIA_ExameResultado.nNumeroExame INNER JOIN ";
            //SQL += "   dbo.MICOLOGIA_Cultura ON dbo.MICOLOGIA_ExameResultado.nIDCultura = dbo.MICOLOGIA_Cultura.nIDCultura  ";

            //SQL += "   where dbo.MICOLOGIA_Exame.dDataExame  >= '{0}' and dbo.MICOLOGIA_Exame.dDataExame  <= '{1}' ";

            //SQL += "   group by  ";
            //SQL += "   dbo.MICOLOGIA_Cultura.sDescricao,  ";
            //SQL += "   dbo.MICOLOGIA_ExameResultado.sLocal ";


            //SQL = String.Format(SQL, (Convert.ToDateTime(dtpDe.Text)).ToString("dd-MM-yyyy"), (Convert.ToDateTime(dtpAte.Text)).ToString("dd-MM-yyyy"));

            //DataTable Dados = Dal.ObterDados(SQL);

            //CulturaAgrupado Relatorio = new CulturaAgrupado();

            //frmRelatorio rel = new frmRelatorio(Dados, Relatorio);
            //rel.Show();

            //Cursor.Current = Cursors.Default;



        }

        private void btnTotalExameMicologicoDireto_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string SQL = String.Empty;

            SQL += " SELECT    ";
            SQL += " COUNT (dbo.MICOLOGIA_Exame.nNumeroExame) as total, ";
            SQL += " dbo.MICOLOGIA_Resultado.sDescricao AS ResultadoDireto ";

            SQL += " FROM          ";
            SQL += " dbo.MICOLOGIA_Exame INNER JOIN ";
            SQL += " dbo.MICOLOGIA_ExameResultado ON dbo.MICOLOGIA_Exame.nNumeroExame = dbo.MICOLOGIA_ExameResultado.nNumeroExame INNER JOIN ";
            SQL += " dbo.MICOLOGIA_Resultado ON dbo.MICOLOGIA_ExameResultado.nIDResultado = dbo.MICOLOGIA_Resultado.nIDResultado ";

            SQL += "   where dbo.MICOLOGIA_Exame.dDataExame  >= '{0}' and dbo.MICOLOGIA_Exame.dDataExame  <= '{1}' ";

            SQL += " group by  ";
            SQL += " dbo.MICOLOGIA_Resultado.sDescricao  ";

            SQL = String.Format(SQL, (Convert.ToDateTime(dtpDe.Text)).ToString("dd-MM-yyyy"), (Convert.ToDateTime(dtpAte.Text)).ToString("dd-MM-yyyy"));

            DataTable Dados = Dal.ObterDados(SQL);

            ExameMicologicoDiretoAgrupado Relatorio = new ExameMicologicoDiretoAgrupado();

            frmRelatorio rel = new frmRelatorio(Dados, Relatorio);
            rel.Show();

            Cursor.Current = Cursors.Default;


        }

        private void gerarPlanilha()
        {
            ExameResultadoBL blExame = new ExameResultadoBL();

            Cursor.Current = Cursors.WaitCursor;

            listaResultados = blExame.PesquisarDatas(dtpDe.Value, dtpAte.Value);

            if (listaResultados.Count > 0)
            {
                listaExames = new List<Modelo.MICOLOGIA_Exame>();

                listaResultados.AsEnumerable().ToList().ForEach(x => listaExames.Add(x.Exame));
                listaExames = listaExames.Distinct().ToList();

                var resultado = from linha in listaResultados
                                group linha by linha.nIDExame into grupo
                                orderby grupo.Count() descending
                                select new { qtde = grupo.Count(), exame = grupo.Key };

                dt = new DataTable();

                dt.Columns.Add("Número Exame");
                dt.Columns.Add("Data do Exame");
                dt.Columns.Add("Pontuário");
                dt.Columns.Add("Procedencia");
                dt.Columns.Add("Médico Solicitante");
                dt.Columns.Add("Paciente");
                dt.Columns.Add("Data Nascimento");
                dt.Columns.Add("Sexo");

                criarPlanilha(resultado.ToList()[0].qtde);
            }
            else
                MessageBox.Show("Nenhum registro encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Cursor.Current = Cursors.Default;
        }
        private void criarPlanilha(int colMax)
        {
            Microsoft.Office.Interop.Excel.Application wapp;
            Microsoft.Office.Interop.Excel.Worksheet wsheet;
            Microsoft.Office.Interop.Excel.Workbook wbook;
            Microsoft.Office.Interop.Excel.Range myCell;

            wapp = new Microsoft.Office.Interop.Excel.Application();

            wapp.Visible = false;
            wbook = wapp.Workbooks.Add(true);
            wsheet = (Microsoft.Office.Interop.Excel.Worksheet)wbook.ActiveSheet;
            wsheet.EnableAutoFilter = true;
            wsheet.Name = "Exames";

            try
            {
                int iExcel = 1;
                int index;
                for (index = 0; index < dt.Columns.Count; index++)
                {
                    wsheet.Cells[1, iExcel] = dt.Columns[index].ColumnName;
                    iExcel++;
                }

                for (int j = 0; j < colMax; j++)
                {
                    dt.Columns.Add("Material" + (j + 1).ToString());
                    wsheet.Cells[1, iExcel] = dt.Columns[index].ColumnName;
                    iExcel++;
                    index++;

                    dt.Columns.Add("Local" + (j + 1).ToString());
                    wsheet.Cells[1, iExcel] = dt.Columns[index].ColumnName;
                    iExcel++;
                    index++;

                    dt.Columns.Add("EMD" + (j + 1).ToString());
                    wsheet.Cells[1, iExcel] = dt.Columns[index].ColumnName;
                    iExcel++;
                    index++;

                    dt.Columns.Add("Cultura" + (j + 1).ToString());
                    wsheet.Cells[1, iExcel] = dt.Columns[index].ColumnName;
                    iExcel++;
                    index++;
                }


                for (int kIndex = 0; kIndex < listaExames.Count; kIndex++)
                {
                    dt.Rows.Add(new string[] { listaExames[kIndex].nNumero.ToString(), "'" + listaExames[kIndex].dDataExame.ToString("dd/MM/yyyy"), listaExames[kIndex].nProntuario.ToString(),
                                               (listaExames[kIndex].Procedencia.sDescricao.Equals("OUTROS")) ? "OUTROS / " + listaExames[kIndex].sJustificativaProcedencia : listaExames[kIndex].Procedencia.sDescricao, 
                                               listaExames[kIndex].sMedicoSolicitante, listaExames[kIndex].Paciente.sNome, (listaExames[kIndex].Paciente.dDataNascimento != null) ? "'" + listaExames[kIndex].Paciente.dDataNascimento.Value.ToString("dd/MM/yyyy") : String.Empty, 
                                               listaExames[kIndex].Paciente.sSexo});

                    var lista = from row in listaResultados
                                where row.nIDExame == listaExames[kIndex].nIDExame
                                select row;

                    for (int xIndex = 0; xIndex < lista.ToList().Count; xIndex++)
                    {
                        dt.Rows[kIndex]["Material" + (xIndex + 1).ToString()] = lista.ToList()[xIndex].sMaterial;
                        dt.Rows[kIndex]["Local" + (xIndex + 1).ToString()] = lista.ToList()[xIndex].sLocal;
                        dt.Rows[kIndex]["EMD" + (xIndex + 1).ToString()] = lista.ToList()[xIndex].Resultado.sDescricao;
                        dt.Rows[kIndex]["Cultura" + (xIndex + 1).ToString()] = lista.ToList()[xIndex].Cultura.sDescricao;
                    }
                }

                iExcel = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        wsheet.Cells[i + 2, iExcel + 1] = (String.IsNullOrEmpty(dt.Rows[i][j].ToString())) ? String.Empty : dt.Rows[i][j].ToString();
                        iExcel++;
                    }
                    iExcel = 0;
                }

                //wsheet.get_Range("C1", "G1").Merge(false);  ---- Mesclar Células
                wsheet.get_Range("A1", "Z1").AutoFilter(1, System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlAnd, System.Reflection.Missing.Value, true);
                myCell = (Microsoft.Office.Interop.Excel.Range)wsheet.get_Range("A1", "Z1");

                myCell.Font.Bold = true;
                myCell.Columns.AutoFit();
                myCell.Rows.AutoFit();               

                wapp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            wapp.Visible = true;
        }
    }
}
