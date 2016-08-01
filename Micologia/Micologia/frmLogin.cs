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
    public partial class frmLogin : Form
    {
        StatusStrip _menuStrip;
        public StatusStrip menuStrip
        {
            get { return _menuStrip; }
            set { _menuStrip = value; }
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            UsuarioBL objBL = new UsuarioBL();
            try
            {
                if (String.IsNullOrEmpty(txtLogin.Text))
                    return;

                if (objBL.ValidarUsuario(txtLogin.Text, txtSenha.Text))
                {
                    PermissaoUsuarioBL.getInstance(objBL.GetLoginAndPassword(txtLogin.Text, txtSenha.Text));

                    if ((PermissaoUsuarioBL.Login != null))
                    {
                        menuStrip.Items["statusUsuario"].Text = "Usuário: " + PermissaoUsuarioBL.Login.sNome;
                        menuStrip.Items["statusDataLogin"].Text = "Hora Login: " + DateTime.Now.ToString("HH:mm");
                        menuStrip.Items["statusDataVersao"].Text = "Data Versão: 06/05/2013";
                    }

                    this.Close();
                }
                else
                    MessageBox.Show("Login e/ou senha inválidos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEntrar.Focus();
            }
        }
    }
}
