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

namespace Micologia
{
    public partial class frmPermissaoUsuario : Form
    {
        private int codigo = 0;
        MICOLOGIA_Seguranca entity;
        MICOLOGIA_Usuario usuario;
        SegurancaBL segurancaBL;

        public frmPermissaoUsuario(int pCodigo)
        {
            InitializeComponent();

            Popularusuarios();

            if (pCodigo > 0)
                Loadform(pCodigo);
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }
        private void cmbUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedIndex != 0)
                PopularTelas();
            else
                ltbTelas.Items.Clear();
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
            ltbTelas.Items.Clear();

            TelaBL telaBL = new TelaBL();
            IList<MICOLOGIA_Tela> list = new List<MICOLOGIA_Tela>();

            if (codigo != 0)
            {
                MICOLOGIA_Tela tela = telaBL.ListarPorId(entity.nIDTela);
                list.Add(tela);
                ltbTelas.Enabled = false;
            }
            else
            {
                usuario = (MICOLOGIA_Usuario)cmbUsuario.SelectedItem;
                segurancaBL = new SegurancaBL();

                IList<MICOLOGIA_Seguranca> permissoes = segurancaBL.ListarTodos().Where(x => x.nIDLogin == usuario.nIDLogin).OrderBy(x => x.Tela.sDescricao).ToList();
                list = telaBL.ListarTodos().OrderBy(x => x.sDescricao).ToList();

                var res = from c in list
                          where !permissoes.Select(o => o.nIDTela).Contains(c.nIDTela)
                          select c;

                list = res.ToList<MICOLOGIA_Tela>();
            }

            for (int i = 0; i < list.Count; i++)
            {
                this.ltbTelas.Items.Add(list[i]);
            }

            ltbTelas.ValueMember = "nIDTela";
            ltbTelas.DisplayMember = "sDescricao";
        }
        private void Salvar()
        {
            if (!Critica())
                return;

            Cursor.Current = Cursors.WaitCursor;

            if (entity == null)
                entity = new MICOLOGIA_Seguranca();

            if (rbtConsultarSim.Checked) entity.sConsultar = "S"; else entity.sConsultar = "N";
            if (rbtIncluirSim.Checked) entity.sIncluir = "S"; else entity.sIncluir = "N";
            if (rbtAlterarSim.Checked) entity.sAlterar = "S"; else entity.sAlterar = "N";
            if (rbtExcluirSim.Checked) entity.sExcluir = "S"; else entity.sExcluir = "N";

            if (codigo > 0)
            {
                segurancaBL.Alterar(entity);
                MessageBox.Show("Alteração efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {

                usuario = (MICOLOGIA_Usuario)cmbUsuario.SelectedItem;
                entity.nIDLogin = usuario.nIDLogin;

                foreach (MICOLOGIA_Tela item in ltbTelas.SelectedItems)
                {
                    entity.nIDTela = item.nIDTela;
                    segurancaBL.Inserir(entity);
                }

                MessageBox.Show("Inclusão efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ltbTelas.Items.Clear();
                cmbUsuario.SelectedIndex = 0;
            }

            Cursor.Current = Cursors.Default;
        }
        private void Loadform(int pCodigo)
        {
            codigo = pCodigo;

            segurancaBL = new SegurancaBL();

            entity = segurancaBL.ListarPorId(codigo);

            if (entity != null)
            {
                cmbUsuario.SelectedValue = entity.nIDLogin;
                cmbUsuario.Text = entity.Usuario.sNome;

                cmbUsuario.Enabled = false;

                PopularTelas();
                ltbTelas.SetSelected(0, true);

                if (entity.sConsultar == "S") rbtConsultarSim.Checked = true; else rbtConsultarNao.Checked = true;
                if (entity.sIncluir == "S") rbtIncluirSim.Checked = true; else rbtIncluirNao.Checked = true;
                if (entity.sAlterar == "S") rbtAlterarSim.Checked = true; else rbtAlterarnao.Checked = true;
                if (entity.sExcluir == "S") rbtExcluirSim.Checked = true; else rbtExcluirNao.Checked = true;
            }
            else
                codigo = 0;
        }
        private bool Critica()
        {
            if (cmbUsuario.Text == "Selecione")
            {
                MessageBox.Show("Selecione um Usuário!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (ltbTelas.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma Tela!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
