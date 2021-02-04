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

namespace SistemaHotel.Produtos
{
    public partial class FrmEstoque : Form
    {

        Conexao con = new Conexao();
        String sql;
        MySqlCommand cmd;
        String id;

        public FrmEstoque()
        {
            InitializeComponent();
        }

        private void FrmEstoques_Load(object sender, EventArgs e)
        {
            carregarComboox();
            desabilitarCampos();
        }


        private void carregarComboox()
        {

            con.AbrirCon();
            sql = "SELECT * FROM fornecedores order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbFornecedor.DataSource = dt;
            cbFornecedor.ValueMember = "id";
            cbFornecedor.DisplayMember = "nome";



            con.FecharCon();
        }

        private void habilitarCampos()
        {
            //txtProduto.Enabled = true;
            txtValor.Enabled = true;
            // txtEstoque.Enabled = true;
            cbFornecedor.Enabled = true;
            txtQuantidade.Enabled = true;
            txtQuantidade.Focus();
            btnSalvar.Enabled = true;

        }


        private void desabilitarCampos()
        {
            txtProduto.Enabled = false;
            txtValor.Enabled = false;
            txtEstoque.Enabled = false;
            cbFornecedor.Enabled = false;
            txtQuantidade.Enabled = false;
            btnSalvar.Enabled = false;

        }


        private void limparCampos()
        {
            txtProduto.Text = "";
            txtValor.Text = "";
            txtEstoque.Text = "";
            txtQuantidade.Text = "";

        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            limparCampos();
            Program.ChamadaProdutos = "estoque";
            Produtos.FrmProdutos form = new Produtos.FrmProdutos();
            form.Show();
        }

        private void FrmEstoque_Activated(object sender, EventArgs e)
        {
            txtEstoque.Text = Program.estoqueProduto;
            txtProduto.Text = Program.nomeProduto;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtProduto.Text.ToString().Trim() == "")
            {
                txtProduto.Text = "";
                MessageBox.Show("Selecione um Produto", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProduto.Focus();
                return;
            }

            if (txtQuantidade.Text == "")
            {
                MessageBox.Show("Preencha A Quantidade", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuantidade.Focus();
                return;
            }

            //CÓDIGO DO BOTÃO PARA EDITAR os proditos

            con.AbrirCon();
            sql = "UPDATE produtos SET fornecedor = @fornecedor, valor_compra = @valor, estoque = @estoque where id = @id";
            cmd = new MySqlCommand(sql, con.con);

            cmd.Parameters.AddWithValue("@estoque", Convert.ToDouble(txtQuantidade.Text) + Convert.ToDouble(txtEstoque.Text));
            cmd.Parameters.AddWithValue("@valor", txtValor.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@fornecedor", cbFornecedor.SelectedValue);
            cmd.Parameters.AddWithValue("@id", Program.idProduto);

            cmd.ExecuteNonQuery();
            con.FecharCon();

            MessageBox.Show("Lançamento feito com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            limparCampos();
            desabilitarCampos();
            
        }
    }
}
