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
    public partial class Exame : Form
    {
        int codigo = 0;
        ExameBL blExame = new ExameBL();
        ExameResultadoBL blExameResultado = new ExameResultadoBL();
        PacienteBL blPaciente = new PacienteBL();
        ProcedenciaBL blProcedencia = new ProcedenciaBL();
        CulturaBL blCultura = new CulturaBL();
        ResultadoBL blResultado = new ResultadoBL();
        Modelo.MICOLOGIA_Paciente paciente;
        MICOLOGIA_Procedencia procedencia;
        MICOLOGIA_Cultura cultura;
        MICOLOGIA_Resultado resultado;
        Util util = new Util();
        DataTable dt;
        Modelo.MICOLOGIA_Exame entity;
        vwPedidoExameBL BLvwPedidoExame = new vwPedidoExameBL();

        public Exame(int pCodigo)
        {
            InitializeComponent();

            popularPaciente();
            popularProcedencia();
            popularResultado();
            popularCultura();
            popularMaterial();

            if (pCodigo != 0)
                LoadForm(pCodigo);
            else
                txtNumeroExame.Text = BuscarProximoExame().ToString();

            txtNumeroExame.Select();
        }
        private void txtNumeroProntuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != ',')
                e.Handled = true;
        }
        private void txtNumeroExame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != ',')
                e.Handled = true;
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente salvar este Exame?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                Salvar();
        }
        private void txtNumeroExame_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 digito = Convert.ToInt32(txtNumeroExame.Text);
            }
            catch
            {
                txtNumeroExame.Text = String.Empty;
            }
        }
        private void txtNumeroProntuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 digito = Convert.ToInt32(txtNumeroProntuario.Text);
            }
            catch
            {
                txtNumeroProntuario.Text = String.Empty;
            }
        }
        private void cmbPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaciente.SelectedIndex != 0)
            {
                Modelo.MICOLOGIA_Paciente paciente = (Modelo.MICOLOGIA_Paciente)cmbPaciente.SelectedItem;

                if (paciente.dDataNascimento != null) dtpDataNasc.Text = paciente.dDataNascimento.Value.ToString("dd/MM/yyyy");
                else dtpDataNasc.Text = String.Empty;

                if (!paciente.bBranco)
                {
                    rdbBranco.Checked = false;
                    rdbNaoBranco.Checked = true;
                }
                else
                {
                    rdbBranco.Checked = true;
                    rdbNaoBranco.Checked = false;
                }

                if (paciente.sSexo == "M")
                {
                    rdbFeminino.Checked = false;
                    rdbMasculino.Checked = true;
                }
                else
                {
                    rdbFeminino.Checked = true;
                    rdbMasculino.Checked = false;
                }
            }
            else
            {
                dtpDataNasc.Text = String.Empty;
                rdbFeminino.Checked = false;
                rdbMasculino.Checked = false;
                rdbBranco.Checked = false;
                rdbNaoBranco.Checked = false;
            }
        }
        private void cmbProcedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProcedencia.SelectedIndex != 0)
            {
                MICOLOGIA_Procedencia procedencia = (MICOLOGIA_Procedencia)cmbProcedencia.SelectedItem;

                if (procedencia.sDescricao == "OUTROS")
                {
                    lblOutros.Visible = true;
                    txtOutrosProcedencia.Visible = true;
                }
                else
                {
                    lblOutros.Visible = false;
                    txtOutrosProcedencia.Visible = false;
                }
            }
            else
            {
                lblOutros.Visible = false;
                txtOutrosProcedencia.Visible = false;
            }
        }
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            IncluirResultado();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroExame.Text))
                ImprimirResultado(Convert.ToInt32(txtNumeroExame.Text));
        }
        private void dgvExame_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvExame.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (dgvExame.Columns[e.ColumnIndex].Name == "Excluir")
                {
                    if (MessageBox.Show("Deseja realmente excluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Excluir(dgvExame.Rows[e.RowIndex].Cells["material"].Value.ToString(), dgvExame.Rows[e.RowIndex].Cells["local"].Value.ToString(), dgvExame.Rows[e.RowIndex].Cells["resultado"].Value.ToString(), dgvExame.Rows[e.RowIndex].Cells["cultura"].Value.ToString());                       
                    }
                }
            }
        }
        private void btnResultado_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Resultado form = new Resultado(0);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            popularResultado();

            Cursor.Current = Cursors.Default;
        }
        private void btnCultura_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Cultura form = new Cultura(0);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            popularCultura();

            Cursor.Current = Cursors.Default;
        }

        private void Excluir(string material, string local, string resultado, string cultura)
        {
            for (int index = 0; index < dt.Rows.Count; index++)
            {
                if (dt.Rows[index]["material"] == material && dt.Rows[index]["local"] == local && dt.Rows[index]["resultado"] == resultado && dt.Rows[index]["cultura"] == cultura)
                {
                    dt.Rows.RemoveAt(index);
                    dgvExame.Rows.RemoveAt(index);
                    break;
                }
            }
        }
        private void IncluirResultado()
        {
            if (!CriticarGrid())
                return;

            if (dt == null || dt.Rows.Count == 0)
            {
                dt = new DataTable();

                dt.Columns.Add("obsCultura");
                dt.Columns.Add("obsResultado");
                dt.Columns.Add("local");
                dt.Columns.Add("cultura");
                dt.Columns.Add("resultado");
                dt.Columns.Add("nIDResultado");
                dt.Columns.Add("nIDCultura");
                dt.Columns.Add("material");
            }

            dt.Rows.Add(new string[] { txtOutrosCultura.Text, txtOutrosResultado.Text, txtLocal.Text.Trim(), cultura.sDescricao, resultado.sDescricao, 
                                        resultado.nIDResultado.ToString(), cultura.nIDCultura.ToString(), cmbMaterial.Text });

            CarregarGrid();

            cmbCultura.SelectedIndex = 0;
            cmbDireto.SelectedIndex = 0;
            cmbMaterial.SelectedIndex = 0;
            txtLocal.Text = String.Empty;
            txtOutrosCultura.Text = String.Empty;
            txtOutrosResultado.Text = String.Empty;
        }
        private void CarregarGrid()
        {
            dgvExame.ReadOnly = true;
            dgvExame.AllowUserToAddRows = false;

            gerarColunaDataGridView();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvExame.Rows.Add("Excluir", dt.Rows[i]["material"].ToString(), dt.Rows[i]["local"].ToString(), dt.Rows[i]["resultado"].ToString(),
                                        dt.Rows[i]["obsResultado"].ToString(), dt.Rows[i]["cultura"].ToString(), dt.Rows[i]["obsCultura"].ToString());
                }
            }
        }
        private void gerarColunaDataGridView()
        {
            dgvExame.Columns.Clear();

            DataGridViewButtonColumn columnExcluir = new DataGridViewButtonColumn();
            {
                columnExcluir.HeaderText = String.Empty;
                columnExcluir.Name = "Excluir";
                columnExcluir.Text = "Excluir";
                columnExcluir.Width = 65;
            }

            dgvExame.Columns.Insert(0, columnExcluir);
            dgvExame.Columns.Add("material", "Material");
            dgvExame.Columns.Add("local", "Local");
            dgvExame.Columns.Add("resultado", "Resul. Direto");
            dgvExame.Columns.Add("obsResultado", "Obs Resultado");
            dgvExame.Columns.Add("cultura", "Cultura");
            dgvExame.Columns.Add("obsCultura", "Obs Cultura");
        }
        private void LoadForm(int pCodigo)
        {
            codigo = pCodigo;

            entity = blExame.ListarPorId(codigo);

            if (entity != null)
            {
                txtNumeroExame.Enabled = false;

                paciente = new Modelo.MICOLOGIA_Paciente();
                paciente.nIDPaciente = entity.nIDPaciente;
                paciente.sNome = entity.Paciente.sNome;

                procedencia = new MICOLOGIA_Procedencia();
                procedencia.nIDProcedencia = entity.nIDProcedencia;
                procedencia.sDescricao = entity.Procedencia.sDescricao;

                dtpDataExame.Value = entity.dDataExame;
                txtNumeroExame.Text = entity.nNumero.ToString();
                if (entity.nProntuario != null) txtNumeroProntuario.Text = entity.nProntuario.ToString();
                txtMedico.Text = entity.sMedicoSolicitante;

                txtObs.Text = entity.sObservacao;
                ckbBiopsia.Checked = entity.bBiopsia;
                txtOutrosProcedencia.Text = entity.sJustificativaProcedencia;

                cmbPaciente.SelectedValue = paciente.nIDPaciente;
                cmbPaciente.Text = paciente.sNome;
                cmbProcedencia.SelectedValue = procedencia.nIDProcedencia;

                cmbProcedencia.Text = procedencia.sDescricao;
                if (entity.Paciente.dDataNascimento != null) dtpDataNasc.Text = entity.Paciente.dDataNascimento.Value.ToString("dd/MM/yyyy");

                if (!entity.Paciente.bBranco)
                {
                    rdbBranco.Checked = false;
                    rdbNaoBranco.Checked = true;
                }

                if (entity.Paciente.sSexo == "M")
                {
                    rdbFeminino.Checked = false;
                    rdbMasculino.Checked = true;
                }

                if (entity.ExameResultados.Count > 0)
                {
                    dt = new DataTable();

                    dt.Columns.Add("obsCultura");
                    dt.Columns.Add("obsResultado");
                    dt.Columns.Add("local");
                    dt.Columns.Add("cultura");
                    dt.Columns.Add("resultado");
                    dt.Columns.Add("nIDResultado");
                    dt.Columns.Add("nIDCultura");
                    dt.Columns.Add("material");

                    foreach (MICOLOGIA_ExameResultado item in entity.ExameResultados)
                    {
                        dt.Rows.Add(new string[] { item.sObsCultura, item.sObsResultado, item.sLocal, item.Cultura.sDescricao, item.Resultado.sDescricao, 
                                                        item.nIDResultado.ToString(), item.nIDCultura.ToString(), item.sMaterial });
                    }

                    CarregarGrid();

                    btnImprimir.Visible = true;
                }
            }
            else
                codigo = 0;
        }
        private void Salvar()
        {
            if (!Criticar())
                return;

            Cursor.Current = Cursors.WaitCursor;

            if (codigo == 0)
                entity = new Modelo.MICOLOGIA_Exame();

            procedencia = (MICOLOGIA_Procedencia)cmbProcedencia.SelectedItem;
            paciente = (Modelo.MICOLOGIA_Paciente)cmbPaciente.SelectedItem;

            entity.nNumero = int.Parse(txtNumeroExame.Text);
            entity.dDataExame = Convert.ToDateTime(dtpDataExame.Value.ToString("dd/MM/yyyy"));      
            entity.bBiopsia = ckbBiopsia.Checked;
            entity.nAno = entity.dDataExame.Year;

            entity.sJustificativaProcedencia = txtOutrosProcedencia.Text;
            entity.nIDPaciente = paciente.nIDPaciente;
            entity.nIDProcedencia = procedencia.nIDProcedencia;
            entity.sObservacao = txtObs.Text;
            entity.sMedicoSolicitante = txtMedico.Text;

            if (!String.IsNullOrEmpty(txtNumeroProntuario.Text)) entity.nProntuario = int.Parse(txtNumeroProntuario.Text);
            else entity.nProntuario = 0;

            using (TransactionScope ts = new TransactionScope())
            {
                if (codigo != 0)
                {
                    AtualizarResultado();
                    blExame.Alterar(entity);
                    MessageBox.Show("Alteração efetuada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    blExame.Inserir(entity);
                    AtualizarResultado();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Inclusão Efetuada. Imprimir Resultado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            ImprimirResultado(entity.nIDExame);
                    }
                    else
                        MessageBox.Show("Inclusão efetuada com sucesso", "Aviso", MessageBoxButtons.OK);

                    LimparCampos();
                    txtNumeroExame.Text = BuscarProximoExame().ToString();
                    txtNumeroExame.Select();
                }

                ts.Complete();
            }

            Cursor.Current = Cursors.Default;
        }
        private void AtualizarResultado()
        {
            util.ExecutarSqlCrud("DELETE FROM MICOLOGIA_ExameResultado WHERE nIDExame = " + entity.nIDExame.ToString());

            MICOLOGIA_ExameResultado exameResultado;

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    exameResultado = new MICOLOGIA_ExameResultado();
                    exameResultado.nIDExame = entity.nIDExame;

                    exameResultado.sMaterial = dt.Rows[i]["material"].ToString();
                    exameResultado.nIDResultado = int.Parse(dt.Rows[i]["nIDResultado"].ToString());
                    exameResultado.nIDCultura = int.Parse(dt.Rows[i]["nIDCultura"].ToString());

                    exameResultado.sLocal = dt.Rows[i]["local"].ToString();
                    exameResultado.sObsCultura = dt.Rows[i]["obsCultura"].ToString();
                    exameResultado.sObsResultado = dt.Rows[i]["obsResultado"].ToString();

                    blExameResultado.Inserir(exameResultado);
                }
            }
        }
        private bool Criticar()
        {
            if (String.IsNullOrEmpty(txtNumeroExame.Text.Trim()))
            {
                MessageBox.Show("Número Exame Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbProcedencia.SelectedIndex == 0)
            {
                MessageBox.Show("Procedência Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbPaciente.SelectedIndex == 0)
            {
                MessageBox.Show("Paciente Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (codigo == 0 && ExisteNumeroExame())
            {
                MessageBox.Show("Número de Exame já existente nesse ano", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dgvExame.Rows.Count == 0)
            {
                MessageBox.Show("Insira ao menos um Resultado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool CriticarGrid()
        {
            if (cmbMaterial.SelectedIndex == 0)
            {
                MessageBox.Show("Material Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbDireto.SelectedIndex == 0)
            {
                MessageBox.Show("Micol. Direto Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbCultura.SelectedIndex == 0)
            {
                MessageBox.Show("Cultura Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (String.IsNullOrEmpty(txtLocal.Text.Trim()))
            {
                MessageBox.Show("Local Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            resultado = (MICOLOGIA_Resultado)cmbDireto.SelectedItem;
            cultura = (MICOLOGIA_Cultura)cmbCultura.SelectedItem;

            if (dt != null)
            {
                var retorno = from linha in dt.AsEnumerable()
                              where linha.Field<string>("local").ToString() == txtLocal.Text.Trim() &&
                                    linha.Field<string>("cultura").ToString() == cultura.sDescricao &&
                                    linha.Field<string>("resultado").ToString() == resultado.sDescricao
                              select linha;

                if (retorno.Count() > 0)
                {
                    MessageBox.Show("Resultado já incluído", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }
        private void LimparCampos()
        {
            dtpDataNasc.Text = String.Empty;
            rdbFeminino.Checked = false;
            rdbMasculino.Checked = false;
            rdbBranco.Checked = false;
            rdbNaoBranco.Checked = false;

            dtpDataExame.Value = DateTime.Now;
            txtNumeroExame.Text = String.Empty;
            txtNumeroProntuario.Text = String.Empty;
            txtMedico.Text = String.Empty;
            txtObs.Text = String.Empty;

            ckbBiopsia.Checked = false;
            txtLocal.Text = String.Empty;
            txtOutrosCultura.Text = String.Empty;
            txtOutrosProcedencia.Text = String.Empty;
            txtOutrosResultado.Text = String.Empty;

            dgvExame.Rows.Clear();
            cmbCultura.SelectedIndex = 0;
            cmbDireto.SelectedIndex = 0;
            cmbMaterial.SelectedIndex = 0;
            cmbPaciente.SelectedIndex = 0;
            cmbProcedencia.SelectedIndex = 0;
        }
        private void popularProcedencia()
        {
            cmbProcedencia.Items.Clear();

            IList<MICOLOGIA_Procedencia> list = blProcedencia.ListarTodos().Where(x => x.bAtivo == true).OrderBy(x => x.sDescricao).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                this.cmbProcedencia.Items.Add(list[i]);
            }

            cmbProcedencia.ValueMember = "nIDProcedencia";
            cmbProcedencia.DisplayMember = "sDescricao";
            cmbProcedencia.Items.Insert(0, "Selecione");
            cmbProcedencia.SelectedIndex = 0;
        }
        private void popularPaciente()
        {
            cmbPaciente.Items.Clear();

            IList<Modelo.MICOLOGIA_Paciente> list;

            if (codigo == 0)
                list = blPaciente.ListarTodos().Where(x => x.bAtivo == true).OrderBy(x => x.sNome).ToList();
            else
                list = blPaciente.ListarTodos().OrderBy(x => x.sNome).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                this.cmbPaciente.Items.Add(list[i]);
            }

            cmbPaciente.ValueMember = "nIDPaciente";
            cmbPaciente.DisplayMember = "sNome";
            cmbPaciente.Items.Insert(0, "Selecione");
            cmbPaciente.SelectedIndex = 0;
        }
        private void popularResultado()
        {
            cmbDireto.Items.Clear();

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
        private bool ExisteNumeroExame()
        {
            if (blExame.ListarPorChave(int.Parse(txtNumeroExame.Text), dtpDataExame.Value.Year).Count == 0)
                return false;

            return true;
        }
        private int BuscarProximoExame()
        {
            IList<Modelo.MICOLOGIA_Exame> lista = blExame.ListarTodosPorAno(dtpDataExame.Value.Year);

            if (lista.Count != 0)
                return lista[lista.Count - 1].nNumero + 1;

            return 1;
        }
        private void ImprimirResultado(int codigo)
        {
            Cursor.Current = Cursors.WaitCursor;

            PedidoItem Crpedido = new PedidoItem();

            IList<vwMICOLOGIA_PEDIDOEXAME> vwMICOLOGIAPEDIDOEXAME = BLvwPedidoExame.ListarPorNrExame(codigo);

            foreach (var item in vwMICOLOGIAPEDIDOEXAME)
            {
                if (string.IsNullOrEmpty(item.sObsResultado)) item.sObsResultado = null;
                if (string.IsNullOrEmpty(item.sObsCultura)) item.sObsCultura = null;
            }

            frmRelatorio rel = new frmRelatorio(vwMICOLOGIAPEDIDOEXAME, Crpedido);
            rel.Show();

            Cursor.Current = Cursors.Default;
        }
    }
}

