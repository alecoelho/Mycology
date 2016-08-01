using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Micologia.Modelo;
using Micologia.Negocio;

namespace Micologia
{
    public partial class frmUsuarioConsulta : Form
    {
        #region Variaveis
        MICOLOGIA_Usuario entity = new MICOLOGIA_Usuario();
        List<MICOLOGIA_Usuario> lista = new List<MICOLOGIA_Usuario>();
        #endregion

        public frmUsuarioConsulta()
        {
            InitializeComponent();
            txtPesquisa.Select();

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Usuário" && item.sIncluir.Trim() == "N" select item).Count() > 0)
                btnNovo.Visible = false; //Excluir
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisar();
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
             LoadDetalhe(0);
        }
        private void dgvConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsulta.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (dgvConsulta.Columns[e.ColumnIndex].Name == "Excluir")
                {
                    if (MessageBox.Show("Deseja realmente excluir esse usuário?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        excluir(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codLogin"].Value));
                }

                if (dgvConsulta.Columns[e.ColumnIndex].Name == "Alterar")
                {
                    LoadDetalhe(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codLogin"].Value));
                }
            }
        }
        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                pesquisar();
        }

        private void LoadDetalhe(int id)
        {
            Cursor.Current = Cursors.WaitCursor;

            frmUsuarioCadastro form = new frmUsuarioCadastro(id);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            Cursor.Current = Cursors.Default;

            pesquisar();
        }
        private void pesquisar()
        {
            Cursor.Current = Cursors.WaitCursor;

            UsuarioBL objBL = new UsuarioBL();
            IList<MICOLOGIA_Usuario> listaPesquisa = new List<MICOLOGIA_Usuario>();

            if (String.IsNullOrEmpty(txtPesquisa.Text.Trim()))
                listaPesquisa = objBL.ListarTodos().OrderBy(x => x.sNome).ToList();
            else
                listaPesquisa = objBL.ListarTodos().Where(x => x.sNome.ToUpper().Contains(txtPesquisa.Text.ToUpper().Trim())).ToList();


            CarregarGrid(listaPesquisa);

            Cursor.Current = Cursors.Default;
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
            dgvConsulta.Columns.Add("codLogin", "Código");
            dgvConsulta.Columns.Add("nomeusuario", "Nome");
            dgvConsulta.Columns.Add("nomelogin", "Login");
            dgvConsulta.Columns.Add("emailusuario", "E-Mail");
            dgvConsulta.Columns.Add("ativo", "Ativo");

            dgvConsulta.Columns[2].Visible = false;

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Usuário" && item.sExcluir.Trim() == "N" select item).Count() > 0)
                dgvConsulta.Columns[0].Visible = false; //Excluir

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Usuário" && item.sAlterar.Trim() == "N" select item).Count() > 0)
                dgvConsulta.Columns[1].Visible = false; //Alterar

        }
        private void CarregarGrid(IList<MICOLOGIA_Usuario> listaPesquisa)
        {
            dgvConsulta.ReadOnly = true;
            dgvConsulta.AllowUserToAddRows = false;

            gerarColunaDataGridView();

            if (listaPesquisa.Count > 0)
            {
                for (int i = 0; i < listaPesquisa.Count; i++)
                {
                    dgvConsulta.Rows.Add("Excluir", "Alterar", listaPesquisa[i].nIDLogin.ToString(), listaPesquisa[i].sNome, listaPesquisa[i].sLogin, listaPesquisa[i].sEmail, (listaPesquisa[i].bAtivo) ? "Sim" : "Não");
                }
            }
            else
                MessageBox.Show("Nenhum registro encontrado");
        }
        private void excluir(int codigo)
        {
            Cursor.Current = Cursors.WaitCursor;

            UsuarioBL objBL = new UsuarioBL();
            MICOLOGIA_Usuario entity = new MICOLOGIA_Usuario();

            entity = objBL.ListarPorId(codigo);

            entity.bAtivo = false;
            objBL.Alterar(entity);

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Exclusão efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pesquisar();
        }

        #region Singleton
        private static frmUsuarioConsulta form = null;
        public static frmUsuarioConsulta getInstance()
        {
            if (form == null)
                form = new frmUsuarioConsulta();

            return form;
        }
        #endregion

    }
}
