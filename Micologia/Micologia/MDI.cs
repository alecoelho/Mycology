using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Micologia.Negocio;

namespace Micologia
{
    public partial class MDI : Form
    {
        private ResultadoConsulta frmResultado = null;
        private CulturaConsulta frmCultura = null;
        private ExameConsulta frmExame = null;
        private PacienteConsulta frmPaciente = null;
        private frmUsuarioConsulta frmUsuarioConsulta = null;
        private frmPermissaoUsuarioSearch frmPermissaoUsuarioSearch = null;
        private RelatorioGerenciais RelatorioGerenciais = null;

        public MDI()
        {
            InitializeComponent();

            LoadLogin();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fecharTodasToolStripMenuItem_Click(null, null);
            PermissaoUsuarioBL._permissao = null;
            LoadLogin();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
                
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void pacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Paciente" && item.sConsultar.Trim() == "S" select item).Count() > 0)
            {
                frmPaciente = PacienteConsulta.getInstance();
                frmPaciente.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmPaciente.MdiParent = this;
                frmPaciente.Show();
            }
            else
                MessageBox.Show("Permissão Negada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Exame" && item.sConsultar.Trim() == "S" select item).Count() > 0)
            {
                frmExame = ExameConsulta.getInstance();
                frmExame.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmExame.MdiParent = this;
                frmExame.Show();
            }
            else
                MessageBox.Show("Permissão Negada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void fecharTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void LoadLogin()
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.menuStrip = statusBar;
            frmLogin.StartPosition = FormStartPosition.CenterScreen;
            frmLogin.ShowDialog();
        }

        private void permissõesDeUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Permissões de Usuários" && item.sConsultar.Trim() == "S" select item).Count() > 0)
            {
                frmPermissaoUsuarioSearch = frmPermissaoUsuarioSearch.getInstance();
                frmPermissaoUsuarioSearch.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmPermissaoUsuarioSearch.MdiParent = this;
                frmPermissaoUsuarioSearch.Show();
            }
            else
                MessageBox.Show("Permissão Negada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Usuário" && item.sConsultar.Trim() == "S" select item).Count() > 0)
            {
                frmUsuarioConsulta = frmUsuarioConsulta.getInstance();
                frmUsuarioConsulta.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmUsuarioConsulta.MdiParent = this;
                frmUsuarioConsulta.Show();
            }
            else
                MessageBox.Show("Permissão Negada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exameMicológicoDiretoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Resultado" && item.sConsultar.Trim() == "S" select item).Count() > 0)
            {
                frmResultado = ResultadoConsulta.getInstance();
                frmResultado.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmResultado.MdiParent = this;
                frmResultado.Show();
            }
            else
                MessageBox.Show("Permissão Negada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void culturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Cultura" && item.sConsultar.Trim() == "S" select item).Count() > 0)
            {
                frmCultura = CulturaConsulta.getInstance();
                frmCultura.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmCultura.MdiParent = this;
                frmCultura.Show();
            }
            else
                MessageBox.Show("Permissão Negada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gerenciaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Cultura" && item.sConsultar.Trim() == "S" select item).Count() > 0)
            //{
                RelatorioGerenciais = RelatorioGerenciais.getInstance();
                RelatorioGerenciais.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                RelatorioGerenciais.MdiParent = this;
                RelatorioGerenciais.Show();
            //}
            //else
            //    MessageBox.Show("Permissão Negada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            

        }

    }
}
