using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaHotel
{
    public partial class FrmLogin : Form
    {

        Conexao con = new Conexao();
        String sql;
        MySqlCommand cmd;
        String id;

        public FrmLogin()
        {
            
            InitializeComponent();
            pnlLogin.Visible = false;
            
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
           
            pnlLogin.Location = new Point(this.Width / 2 - 166, this.Height / 2 - 170);
            pnlLogin.Visible = true;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 114, 160);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(8, 72, 103);

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            ChamarLogin();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ChamarLogin();
            }
        }


        private void ChamarLogin()
        {
            if (txtUsuario.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o Usuário", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsuario.Text = "";
                txtUsuario.Focus();
                return;
            }

            if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha a Senha", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha.Text = "";
                txtSenha.Focus();
                return;
            }

            //AQUI VAI O CÓDIGO PARA O LOGIN
            // verificar se o usuario ja existe
            MySqlCommand cmdVerificar;

            MySqlDataReader reader;

            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE usuario = @usuario and senha = @senha", con.con);
            cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            cmdVerificar.Parameters.AddWithValue("@senha", txtSenha.Text);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                //Extraindo informações da consuta do meu Login
                while (reader.Read())
                {
                    Program.nomeUsuario = Convert.ToString(reader["nome"]);
                    Program.cargoUsuario = Convert.ToString(reader["cargo"]);

                    
                }
                MessageBox.Show("Bem Vindo", "Login Efetuado "+ Program.nomeUsuario, MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmMenu form = new FrmMenu();
                //this.Hide();
                Limpar();
                form.Show();
            }
            else
            {
                MessageBox.Show("Erro ao efetuar o login. Login ou Senha incorretos ", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Text = "";
                txtUsuario.Focus();
                txtSenha.Text = "";
               
            }
            con.FecharCon();
           
        }


        private void Limpar()
        {
            txtUsuario.Text = "";
            txtSenha.Text = "";
            txtUsuario.Focus();
        }

        private void FrmLogin_Resize(object sender, EventArgs e)
        {
            pnlLogin.Location = new Point(this.Width / 2 - 166, this.Height / 2 - 170);
        }
    }
}
