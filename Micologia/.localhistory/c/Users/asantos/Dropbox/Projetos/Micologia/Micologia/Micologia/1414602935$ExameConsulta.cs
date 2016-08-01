using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Micologia.Negocio;
using Micologia.Modelo;
using System.Transactions;
using Micologia.Relatorio;

namespace Micologia
{
    public partial class ExameConsulta : Form
    {
        IList<Modelo.MICOLOGIA_ExameResultado> lista;
        vwPedidoExameBL BLvwPedidoExame = new vwPedidoExameBL();

        public ExameConsulta()
        {
            InitializeComponent();

            txtExame.Select();
            popularCultura();
            popularResultado();
            popularMaterial();
            popularAno();

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Exame" && item.sIncluir.Trim() == "N" select item).Count() > 0)
                brnInserir.Visible = false;
        }
        private void dgvConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsulta.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                switch (dgvConsulta.Columns[e.ColumnIndex].Name)
                {
                    case "Excluir":
                        if (MessageBox.Show("Deseja realmente excluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            Excluir(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codigo"].Value));
                        break;

                    case "Alterar":
                        LoadDetalhe(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codigo"].Value));
                        break;

                    default:
                        ImprimirResultado(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codigo"].Value));
                        break;
                }
            }
        }
        private void txtProntuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 digito = Convert.ToInt32(txtProntuario.Text);
            }
            catch
            {
                txtProntuario.Text = String.Empty;
            }
        }
        private void txtExame_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 digito = Convert.ToInt32(txtExame.Text);
            }
            catch
            {
                txtExame.Text = String.Empty;
            }
        }
        private void txtExame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != ',')
                e.Handled = true;
        }
        private void txtExame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void txtData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void txtProntuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != ',')
                e.Handled = true;
        }
        private void txtProntuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void txtMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void txtLocal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void txtPaciente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }
        private void brnInserir_Click(object sender, EventArgs e)
        {
            LoadDetalhe(0);
        }

        private void Pesquisar()
        {
            if (!String.IsNullOrEmpty(txtData.Text) && txtData.Text != "  /  /")
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(txtData.Text);
                }
                catch
                {
                    MessageBox.Show("Data Inválida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ExameResultadoBL blExame = new ExameResultadoBL();

            Cursor.Current = Cursors.WaitCursor;

            Modelo.MICOLOGIA_Cultura cultura = null;
            Modelo.MICOLOGIA_Resultado resultado = null;

            if (cmbCultura.SelectedItem != "Selecione") cultura = (Modelo.MICOLOGIA_Cultura)cmbCultura.SelectedItem;
            if (cmbDireto.SelectedItem != "Selecione") resultado = (Modelo.MICOLOGIA_Resultado)cmbDireto.SelectedItem;

            lista = blExame.Pesquisar(txtExame.Text, txtData.Text, txtPaciente.Text, txtProntuario.Text, txtMedico.Text, (resultado != null) ? resultado.nIDResultado : 0,
                                     (cultura != null) ? cultura.nIDCultura : 0, txtLocal.Text.Trim(), cmbMaterial.SelectedItem.ToString(),
                                     (cmbAno.Text != "Selecione") ? int.Parse(cmbAno.Text.ToString()) : 0);
            CarregarGrid();

            Cursor.Current = Cursors.Default;
        }
        private void CarregarGrid()
        {
            dgvConsulta.ReadOnly = true;
            dgvConsulta.AllowUserToAddRows = false;

            gerarColunaDataGridView();

            lblTotal.Text = "0";

            if (lista.Count > 0)
            {
                IList<Modelo.MICOLOGIA_Exame> listaExame = new List<Modelo.MICOLOGIA_Exame>();
                lista.AsEnumerable().ToList().ForEach(x => listaExame.Add(x.Exame));

                listaExame = listaExame.Distinct().ToList();

                for (int i = 0; i < listaExame.Count; i++)
                {
                    dgvConsulta.Rows.Add("Excluir", "Alterar", "Relatório", listaExame[i].nNumero.ToString(), listaExame[i].Paciente.sNome, listaExame[i].dDataExame.ToString("dd/MM/yyyy"),
                                         listaExame[i].nProntuario, listaExame[i].sMedicoSolicitante);
                }

                lblTotal.Text = listaExame.Count.ToString();
            }
            else
                MessageBox.Show("Nenhum registro encontrado");
        }
        private void gerarColunaDataGridView()
        {
            dgvConsulta.Columns.Clear();

            DataGridViewButtonColumn columnExcluir = new DataGridViewButtonColumn();
            {
                columnExcluir.HeaderText = String.Empty;
                columnExcluir.Name = "Excluir";
                columnExcluir.Text = "Excluir";
                columnExcluir.Width = 65;
            }
            DataGridViewButtonColumn columnAlterar = new DataGridViewButtonColumn();
            {
                columnAlterar.HeaderText = String.Empty;
                columnAlterar.Name = "Alterar";
                columnAlterar.Text = "Alterar";
                columnAlterar.Width = 65;
            }
            DataGridViewButtonColumn columnRelatorio = new DataGridViewButtonColumn();
            {
                columnRelatorio.HeaderText = String.Empty;
                columnRelatorio.Name = "Relatorio";
                columnRelatorio.Text = "Relatório";
                columnRelatorio.Width = 65;
            }

            dgvConsulta.Columns.Insert(0, columnExcluir);
            dgvConsulta.Columns.Insert(1, columnAlterar);
            dgvConsulta.Columns.Insert(2, columnRelatorio);
            dgvConsulta.Columns.Add("codigo", "No Exame");
            dgvConsulta.Columns.Add("nome", "Paciente");
            dgvConsulta.Columns.Add("dataExame", "Dt Exame");
            dgvConsulta.Columns.Add("prontuario", "Prontuario");
            dgvConsulta.Columns.Add("medico", "Médico Solicitante");

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Exame" && item.sExcluir.Trim() == "N" select item).Count() > 0)
                dgvConsulta.Columns[0].Visible = false;

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Exame" && item.sAlterar.Trim() == "N" select item).Count() > 0)
                dgvConsulta.Columns[1].Visible = false;
        }
        private void LoadDetalhe(int id)
        {
            Cursor.Current = Cursors.WaitCursor;

            Exame form = new Exame(id);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            Pesquisar();

            Cursor.Current = Cursors.Default;
        }
        private void Excluir(int id)
        {
            Cursor.Current = Cursors.WaitCursor;

            Util util = new Util();
            ExameBL blExame = new ExameBL();

            Modelo.MICOLOGIA_Exame entity = blExame.ListarPorId(id);

            using (TransactionScope ts = new TransactionScope())
            {
                util.ExecutarSqlCrud("DELETE FROM MICOLOGIA_ExameResultado WHERE nNumeroExame = " + id.ToString());

                blExame.Excluir(entity);

                ts.Complete();
            }

            MessageBox.Show("Exclusão efetuada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Pesquisar();

            Cursor.Current = Cursors.WaitCursor;
        }
        private void ImprimirResultado(int NidNrExame)
        {
            Cursor.Current = Cursors.WaitCursor;

            PedidoItem Crpedido = new PedidoItem();

            IList<vwMICOLOGIA_PEDIDOEXAME> vwMICOLOGIAPEDIDOEXAME = BLvwPedidoExame.ListarPorNrExame(NidNrExame);

            foreach (var item in vwMICOLOGIAPEDIDOEXAME)
            {
                if (string.IsNullOrEmpty(item.sObsResultado)) item.sObsResultado = null;
                if (string.IsNullOrEmpty(item.sObsCultura)) item.sObsCultura = null;
            }

            frmRelatorio rel = new frmRelatorio(vwMICOLOGIAPEDIDOEXAME, Crpedido);
            rel.Show();

            Cursor.Current = Cursors.Default;
        }
        private void popularResultado()
        {
            cmbDireto.Items.Clear();

            ResultadoBL blResultado = new ResultadoBL();
            IList<MICOLOGIA_Resultado> list = blResultado.ListarTodos().OrderBy(x => x.sDescricao).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                this.cmbDireto.Items.Add(list[i]);
            }

            cmbDireto.ValueMember = "nIDResultado";
            cmbDireto.DisplayMember = "sDescricao";
            cmbDireto.Items.Insert(0, "Selecione");
            cmbDireto.SelectedIndex = 0;
        }
        private void popularCultura()
        {
            cmbCultura.Items.Clear();

            CulturaBL blCultura = new CulturaBL();
            IList<MICOLOGIA_Cultura> list = blCultura.ListarTodos().OrderBy(x => x.sDescricao).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                this.cmbCultura.Items.Add(list[i]);
            }

            cmbCultura.ValueMember = "nIDCultura";
            cmbCultura.DisplayMember = "sDescricao";
            cmbCultura.Items.Insert(0, "Selecione");
            cmbCultura.SelectedIndex = 0;
        }
        private void popularMaterial()
        {
            cmbMaterial.Items.Clear();

            cmbMaterial.Items.Insert(0, "Selecione");
            cmbMaterial.Items.Insert(1, "Aspirado");
            cmbMaterial.Items.Insert(2, "Biopsia");
            cmbMaterial.Items.Insert(3, "Exsudato");
            cmbMaterial.Items.Insert(4, "Pêlo");
            cmbMaterial.Items.Insert(5, "Raspado");
            cmbMaterial.Items.Insert(6, "Raspado e Pêlo");

            cmbMaterial.SelectedIndex = 0;
        }
        private void popularAno()
        {
            cmbAno.Items.Clear();

            cmbAno.Items.Insert(0, "Selecione");
            this.cmbAno.Items.Insert(1, "2012");
            this.cmbAno.Items.Insert(2, "2013");
            this.cmbAno.Items.Insert(3, "2014");

            if (DateTime.Now.Year >= 4) this.cmbAno.Items.Insert(2015, "2015");
            if (DateTime.Now.Year >= 5) this.cmbAno.Items.Insert(2016, "2016");
            if (DateTime.Now.Year >= 6) this.cmbAno.Items.Insert(2017, "2017");
            if (DateTime.Now.Year >= 7) this.cmbAno.Items.Insert(2018, "2018");
            if (DateTime.Now.Year >= 8) this.cmbAno.Items.Insert(2019, "2019");
            if (DateTime.Now.Year >= 9) this.cmbAno.Items.Insert(2020, "2020");
            
            cmbAno.SelectedIndex = 0;
        }

        #region Singleton
        private static ExameConsulta form = null;
        public static ExameConsulta getInstance()
        {
            if (form == null)
                form = new ExameConsulta();

            return form;
        }
        #endregion
    }
}
