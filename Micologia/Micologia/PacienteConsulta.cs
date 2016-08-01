using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Micologia.Negocio;
using Micologia.Modelo;
using System.Linq;

namespace Micologia
{
    public partial class PacienteConsulta : Form
    {
        IList<Modelo.MICOLOGIA_Paciente> lista;

        public PacienteConsulta()
        {
            InitializeComponent();

            txtPesquisa.Select();

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Paciente" && item.sIncluir.Trim() == "N" select item).Count() > 0)
                brnNovo.Visible = false;
        }
        private void brnNovo_Click(object sender, EventArgs e)
        {
            LoadDetalhe(0);
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }
        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void dgvConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsulta.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (dgvConsulta.Columns[e.ColumnIndex].Name == "Excluir")
                {
                    if (MessageBox.Show("Deseja realmente excluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        Excluir(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codigo"].Value));
                }

                if (dgvConsulta.Columns[e.ColumnIndex].Name == "Alterar")
                {
                    LoadDetalhe(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codigo"].Value));
                }
            }
        }
        private void txtData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }

        private void LoadDetalhe(int id)
        {
            Cursor.Current = Cursors.WaitCursor;

            Paciente form = new Paciente(id);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            Cursor.Current = Cursors.Default;

            Pesquisar();
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

            Cursor.Current = Cursors.WaitCursor;

            PacienteBL blPaciente = new PacienteBL();

            lista = blPaciente.Pesquisar(txtPesquisa.Text.Trim(), txtData.Text, ckbAtivos.Checked);
            CarregarGrid();
            lblTotal.Text = lista.Count.ToString();

            Cursor.Current = Cursors.Default;
        }
        private void CarregarGrid()
        {
            dgvConsulta.ReadOnly = true;
            dgvConsulta.AllowUserToAddRows = false;

            gerarColunaDataGridView();

            if (lista.Count > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    dgvConsulta.Rows.Add("Excluir", "Alterar", lista[i].nIDPaciente.ToString(), lista[i].sNome, (lista[i].dDataNascimento != null) ? lista[i].dDataNascimento.Value.ToString("dd/MM/yyyy") : String.Empty, 
                        (lista[i].sSexo == "F") ? "Feminino" : "Masculino", lista[i].sEmail, lista[i].sTelefone);
                }
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

            dgvConsulta.Columns.Insert(0, columnExcluir);
            dgvConsulta.Columns.Insert(1, columnAlterar);
            dgvConsulta.Columns.Add("codigo", "codigo");
            dgvConsulta.Columns.Add("nome", "Paciente");
            dgvConsulta.Columns.Add("dataNasc", "Dt Nascimento");
            dgvConsulta.Columns.Add("sexo", "Sexo");
            dgvConsulta.Columns.Add("email", "E-mail");
            dgvConsulta.Columns.Add("telefone", "Telefone");

            dgvConsulta.Columns[2].Visible = false; //codigo


            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Paciente" && item.sExcluir.Trim() == "N" select item).Count() > 0)
                dgvConsulta.Columns[0].Visible = false;

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Paciente" && item.sAlterar.Trim() == "N" select item).Count() > 0)
                dgvConsulta.Columns[1].Visible = false;
        }
        private void Excluir(int id)
        {
            Cursor.Current = Cursors.WaitCursor;

            PacienteBL blPaciente = new PacienteBL();
            Modelo.MICOLOGIA_Paciente entity = blPaciente.ListarPorId(id);
            entity.bAtivo = false;
            blPaciente.Alterar(entity);

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Exclusão efetuada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Pesquisar();
        }

        #region Singleton
        private static PacienteConsulta form = null;
        public static PacienteConsulta getInstance()
        {
            if (form == null)
                form = new PacienteConsulta();

            return form;
        }
        #endregion
    }
}
