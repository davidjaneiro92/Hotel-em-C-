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
    public partial class FrmCargo : Form
    {
        Conexao con = new Conexao();
        String sql;
        MySqlCommand cmd;
        String id;

        public FrmCargo()
        {
            InitializeComponent();
        }

        private void FormatarDG()
        {
            grid.Columns[0].HeaderText ="ID";
            grid.Columns[1].HeaderText = "Cargo";

            grid.Columns[0].Visible = false;
           
            grid.Columns[1].Width = 200;
        }
        
        private void Lista()
        {
            
            con.AbrirCon();
            sql = "SELECT * FROM cargos order by cargo asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            txtNome.Focus();
        }

        //botao salvar
        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                txtNome.Text = "";
                MessageBox.Show("Preencha o Cargo", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                return;
            }

            //salvar no banco com SQL

            try
            {

                con.AbrirCon();
                sql = "INSERT INTO  cargos (cargo) VALUES (@cargo)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@cargo", txtNome.Text);
                cmd.ExecuteNonQuery();
                con.FecharCon();
            }
            catch (Exception)
            {

                throw;
            }
            

            MessageBox.Show("Registro Salvo com Sucesso", "Dados Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            txtNome.Text = "";
            txtNome.Enabled = false;
            Lista();
        }

        private void FrmCargo_Load(object sender, EventArgs e)
        {
            Lista();
        }

       

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                txtNome.Text = "";
                MessageBox.Show("Preencha o Cargo", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                return;
            }


            //CÓDIGO DO BOTÃO PARA EDITAR
            

            try
            {
                con.AbrirCon();
                sql = "UPDATE  cargos SET cargo = @cargo where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@cargo", txtNome.Text);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharCon();
            }
            catch (Exception)
            {

                throw;
            }

            MessageBox.Show("Registro Editado com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            txtNome.Text = "";
            txtNome.Enabled = false;
            Lista();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                //CÓDIGO DO BOTÃO PARA EXCLUIR

                try
                {
                    con.AbrirCon();
                    sql = "DELETE FROM  cargos where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharCon();
                }
                catch (Exception)
                {

                    throw;
                }

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
            txtNome.Enabled = true;

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();



            // MessageBox.Show(id);
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
