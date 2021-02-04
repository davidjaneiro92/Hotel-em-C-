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

namespace SistemaHotel.Cadastros
{
    public partial class FrmFuncionarios : Form
    {

        Conexao con = new Conexao();
        String sql;
        MySqlCommand cmd;
        String id;
        String cpfAntigo;

        public FrmFuncionarios()
        {
            InitializeComponent();
        }

        private void FormatarDG()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "CPF";
            grid.Columns[3].HeaderText = "Endereço";
            grid.Columns[4].HeaderText = "Telefone";
            grid.Columns[5].HeaderText = "Cargo";
            grid.Columns[6].HeaderText = "Data";

            grid.Columns[0].Visible = false;

           // grid.Columns[1].Width = 200;
        }

        private void Lista()
        {

            con.AbrirCon();
            sql = "SELECT * FROM funcionario order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT * FROM funcionario where nome LIKE @nome order by nome asc"; // LIKE: para buscar em aproximação
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscarNome.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void BuscarCpf()
        {
            con.AbrirCon();
            sql = "SELECT * FROM funcionario where cpf LIKE @cpf order by nome asc"; // LIKE: para buscar em aproximação
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cpf", txtBuscarCPF.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void carregarComboox()
        {

            con.AbrirCon();
            sql = "SELECT * FROM cargos order by cargo asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

                cbCargo.DataSource = dt;
                // cbCargo.ValueMember = "id";
                cbCargo.DisplayMember = "cargo";
           
            

            con.FecharCon();

        }

        private void habilitarCampos()
        {
            txtNome.Enabled = true;
            txtCPF.Enabled = true;
            txtEndereco.Enabled = true;
            cbCargo.Enabled = true;
            txtTelefone.Enabled = true;
            txtNome.Focus();

        }


        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtCPF.Enabled = false;
            txtEndereco.Enabled = false;
            cbCargo.Enabled = false;
            txtTelefone.Enabled = false;
        }


        private void limparCampos()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
        }




        private void FrmFuncionarios_Load(object sender, EventArgs e)
        {
            rbNome.Checked = true;
            carregarComboox();
            Lista();

        }

        private void RbNome_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscarNome.Visible = true;
            txtBuscarCPF.Visible = false;

            txtBuscarNome.Text = "";
            txtBuscarCPF.Text = "";

        }

        private void RbCPF_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscarNome.Visible = false;
            txtBuscarCPF.Visible = true;

            txtBuscarNome.Text = "";
            txtBuscarCPF.Text = "";
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            if (cbCargo.Text == "")
            {
                MessageBox.Show("Cadastre um Cargo");
                Close(); 
            }
            habilitarCampos();
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                txtNome.Text = "";
                MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                return;
            }

            if (txtCPF.Text == "   .   .   -")
            {
                MessageBox.Show("Preencha o CPF", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCPF.Focus();
                return;
            }


            //CÓDIGO DO BOTÃO PARA SALVAR

            con.AbrirCon();
            sql = "INSERT INTO  funcionario (nome, cpf, endereco, telefone, cargo, data) VALUES (@nome, @cpf, @endereco, @telefone, @cargo, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("@cargo", cbCargo.Text);


            // verificar se o usuario ja existe
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM funcionario WHERE cpf = @cpf", con.con);
            cmdVerificar.Parameters.AddWithValue("@cpf", txtCPF.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("CPF ja Existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCPF.Text = "";
                txtCPF.Focus();
                return;
            }


            cmd.ExecuteNonQuery();
            con.FecharCon();
            Lista();

            MessageBox.Show("Registro Salvo com Sucesso!", "Dados Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            limparCampos();
            desabilitarCampos();
        }

        private void Grid_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                txtNome.Text = "";
                MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                return;
            }

            if (txtCPF.Text == "   .   .   -")
            {
                MessageBox.Show("Preencha o CPF", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCPF.Focus();
                return;
            }


            //CÓDIGO DO BOTÃO PARA EDITAR

            con.AbrirCon();
            sql = "UPDATE  funcionario SET nome = @nome, cpf = @cpf, endereco = @endereco, telefone = @telefone, cargo = @cargo where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("@cargo", cbCargo.Text);

            // verificar se o usuario ja existe
            if(txtCPF.Text != cpfAntigo)
            {
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM funcionario WHERE cpf = @cpf", con.con);
                cmdVerificar.Parameters.AddWithValue("@cpf", txtCPF.Text);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("CPF ja Existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCPF.Text = "";
                    txtCPF.Focus();
                    return;
                }
            }

           


            cmd.ExecuteNonQuery();
            con.FecharCon();

            MessageBox.Show("Registro Editado com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            limparCampos();
            desabilitarCampos();
            Lista();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                //CÓDIGO DO BOTÃO PARA EXCLUIR


                con.AbrirCon();
                sql = "DELETE FROM  funcionario where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Registro Excluido com Sucesso!", "Registro Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Lista();
            }
        }

        private void cbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtCPF.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtTelefone.Text = grid.CurrentRow.Cells[4].Value.ToString();
            cbCargo.Text = grid.CurrentRow.Cells[5].Value.ToString();

            cpfAntigo = grid.CurrentRow.Cells[2].Value.ToString();
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarNome.Text == "")
            {
                Lista();
            }
            else
            {
                BuscarNome();
            }
           
        }

        private void txtBuscarCPF_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarCPF.Text == "   .   .   -")
            {
                Lista();
            }
            else
            {
                BuscarCpf();
            }
                
        }

        private void txtBuscarCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
