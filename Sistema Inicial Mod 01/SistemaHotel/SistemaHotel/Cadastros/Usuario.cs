﻿using MySql.Data.MySqlClient;
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
    public partial class FrmUsuarios : Form
    {

         Conexao con = new Conexao();
        String sql;
        MySqlCommand cmd;
        String id;
        String usuarioAntigo;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FormatarDG()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "cargo";
            grid.Columns[3].HeaderText = "Usuário";
            grid.Columns[4].HeaderText = "Senha";
            grid.Columns[5].HeaderText = "Data";

            grid.Columns[0].Visible = false;

            // grid.Columns[1].Width = 200;
        }

        private void Lista()
        {

            con.AbrirCon();
            sql = "SELECT * FROM usuarios order by nome asc";
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
            sql = "SELECT * FROM usuarios where nome LIKE @nome order by nome asc"; // LIKE: para buscar em aproximação
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

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            carregarComboox();
            Lista();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void habilitarCampos()
        {
            txtNome.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            cbCargo.Enabled = true;
            txtNome.Focus();

        }


        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            cbCargo.Enabled = false;
            
        }


        private void limparCampos()
        {
            txtNome.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";
           
        }

        private void btnNovo_Click(object sender, EventArgs e)
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                txtNome.Text = "";
                MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                return;
            }

           


            //CÓDIGO DO BOTÃO PARA SALVAR

            con.AbrirCon();
            sql = "INSERT INTO  usuarios (nome, usuario, senha, cargo, data) VALUES (@nome, @usuario, @senha, @cargo, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("@cargo", cbCargo.Text);

            // verificar se o usuario ja existe
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE usuario = @usuario", con.con);
            cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("Usuario ja Existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Text = "";
                txtUsuario.Focus();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                txtNome.Text = "";
                MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                return;
            }


            //CÓDIGO DO BOTÃO PARA EDITAR

            con.AbrirCon();
            sql = "UPDATE  usuarios SET nome = @nome, cargo = @cargo, usuario = @usuario, senha = @senha  where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("@cargo", cbCargo.Text);

            if (txtUsuario.Text != usuarioAntigo)
            {
                // verificar se o usuario ja existe
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE usuario = @usuario", con.con);
                cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Usuario ja Existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                //CÓDIGO DO BOTÃO PARA EXCLUIR


                con.AbrirCon();
                sql = "DELETE FROM  usuarios where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Registro Excluido com Sucesso!", "Registro Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                txtNome.Text = "";
                txtNome.Enabled = false;
                Lista();
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtUsuario.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtSenha.Text = grid.CurrentRow.Cells[4].Value.ToString();
            cbCargo.Text = grid.CurrentRow.Cells[2].Value.ToString();

            usuarioAntigo = Text = grid.CurrentRow.Cells[3].Value.ToString();
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();

        }
    }
}
