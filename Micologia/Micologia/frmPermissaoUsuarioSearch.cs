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
    public partial class frmPermissaoUsuarioSearch : Form
    {
        MICOLOGIA_Seguranca entity = new MICOLOGIA_Seguranca();
        List<MICOLOGIA_Seguranca> lista = new List<MICOLOGIA_Seguranca>();

        public frmPermissaoUsuarioSearch()
        {
            InitializeComponent();

            Popularusuarios();
            PopularTelas();

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Permissões De Usuários" && item.sIncluir.Trim() == "N" select item).Count() > 0)
                btnNovo.Visible = false;
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            loadDetalhe(0);
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisar();
        }
        private void dgvPermissao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPermissao.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (dgvPermissao.Columns[e.ColumnIndex].Name == "Excluir")
                {
                    if (MessageBox.Show("Deseja realmente excluir esse acesso?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        excluir(Convert.ToInt32(dgvPermissao.Rows[e.RowIndex].Cells["nIDSeguranca"].Value));
                }

                if (dgvPermissao.Columns[e.ColumnIndex].Name == "Alterar")
                    loadDetalhe(Convert.ToInt32(dgvPermissao.Rows[e.RowIndex].Cells["nIDSeguranca"].Value));
            }
        }

        private void Popularusuarios()
        {
            cmbUsuario.Items.Clear();

            UsuarioBL loginBL = new UsuarioBL();
            IList<MICOLOGIA_Usuario> list = new List<MICOLOGIA_Usuario>();

            list = loginBL.ListarTodos().Where(x => x.bAtivo == true).OrderBy(x => x.sNome).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                this.cmbUsuario.Items.Add(list[i]);
            }

            cmbUsuario.ValueMember = "nIDLogin";
            cmbUsuario.DisplayMember = "sNome";
            cmbUsuario.Items.Insert(0, "Selecione");
            cmbUsuario.SelectedIndex = 0;
        }
        private void PopularTelas()
        {
            cmbTela.Items.Clear();

            TelaBL telaBL = new TelaBL();
            IList<MICOLOGIA_Tela> list = new List<MICOLOGIA_Tela>();

            list = telaBL.ListarTodos().OrderBy(x => x.sDescricao).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                this.cmbTela.Items.Add(list[i]);
            }

            cmbTela.ValueMember = "nIDTela";
            cmbTela.DisplayMember = "sDescricao";
            cmbTela.Items.Insert(0, "Selecione");
            cmbTela.SelectedIndex = 0;
        }
        private void pesquisar()
        {
            Cursor.Current = Cursors.WaitCursor;

            SegurancaBL objBL = new SegurancaBL();
            lista = new List<MICOLOGIA_Seguranca>();

            if (cmbTela.SelectedIndex == 0 && cmbUsuario.SelectedIndex == 0)
                lista = objBL.ListarTodos().OrderBy(x => x.Usuario.sNome).ToList();
            else
            {
                if (cmbTela.SelectedIndex != 0 && cmbUsuario.SelectedIndex != 0)
                {
                    MICOLOGIA_Tela tela = (MICOLOGIA_Tela)cmbTela.SelectedItem;
                    MICOLOGIA_Usuario login = (MICOLOGIA_Usuario)cmbUsuario.SelectedItem;

                    entity.nIDTela = tela.nIDTela;
                    entity.nIDLogin = login.nIDLogin;

                    lista = objBL.ListarTodos().Where(x => x.nIDLogin == login.nIDLogin && x.nIDTela == tela.nIDTela).ToList();
                }
                else
                {
                    if (cmbTela.SelectedIndex != 0)
                    {
                        MICOLOGIA_Tela tela = (MICOLOGIA_Tela)cmbTela.SelectedItem;
                        lista = objBL.ListarTodos().Where(x => x.nIDTela == tela.nIDTela).ToList();
                    }
                    else
                    {
                        MICOLOGIA_Usuario login = (MICOLOGIA_Usuario)cmbUsuario.SelectedItem;
                        lista = objBL.ListarTodos().Where(x => x.nIDLogin == login.nIDLogin).ToList();
                    }
                }
            }

            CarregarGrid();

            Cursor.Current = Cursors.Default;
        }
        private void loadDetalhe(int codigo)
        {
            Cursor.Current = Cursors.WaitCursor;

            frmPermissaoUsuario frmPermissao = new frmPermissaoUsuario(codigo);
            frmPermissao.StartPosition = FormStartPosition.CenterScreen;
            frmPermissao.ShowDialog();

            Cursor.Current = Cursors.Default;

            pesquisar();
        }
        private void excluir(int codigo)
        {
            Cursor.Current = Cursors.WaitCursor;

            SegurancaBL objBL = new SegurancaBL();

            entity = objBL.ListarPorId(codigo);
            objBL.Excluir(entity);

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Exclusão efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pesquisar();            
        }
        private void CarregarGrid()
        {
            dgvPermissao.ReadOnly = true;
            dgvPermissao.AllowUserToAddRows = false;

            gerarColunaDataGridView();

            if (lista.Count > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    dgvPermissao.Rows.Add("Excluir", "Alterar", lista[i].Usuario.sNome.ToString(), lista[i].Tela.sDescricao, lista[i].sConsultar, lista[i].sIncluir,
                                    lista[i].sAlterar, lista[i].sExcluir, lista[i].nIDSeguranca.ToString());
                }
            }
            else
                MessageBox.Show("Nenhum registro encontrado");
        }
        private void gerarColunaDataGridView()
        {
            dgvPermissao.Columns.Clear();

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

            dgvPermissao.Columns.Insert(0, columnExcluir);
            dgvPermissao.Columns.Insert(1, columnAlterar);
            dgvPermissao.Columns.Add("nomeusuario", "Usuário");
            dgvPermissao.Columns.Add("nometela", "Tela");
            dgvPermissao.Columns.Add("consultar", "Consultar");
            dgvPermissao.Columns.Add("incluir", "Incluir");
            dgvPermissao.Columns.Add("alterar", "Alterar");
            dgvPermissao.Columns.Add("excluir", "Excluir");
            dgvPermissao.Columns.Add("nIDSeguranca", "nIDSeguranca");

            dgvPermissao.Columns[8].Visible = false; //idUsuario

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Permissões De Usuários" && item.sExcluir.Trim() == "N" select item).Count() > 0)
                dgvPermissao.Columns[0].Visible = false; //Excluir

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Permissões De Usuários" && item.sAlterar.Trim() == "N" select item).Count() > 0)
                dgvPermissao.Columns[1].Visible = false; //Excluir

        }

        #region Singleton
        private static frmPermissaoUsuarioSearch form = null;
        public static frmPermissaoUsuarioSearch getInstance()
        {
            if (form == null)
                form = new frmPermissaoUsuarioSearch();

            return form;
        }
        #endregion

    }
}
