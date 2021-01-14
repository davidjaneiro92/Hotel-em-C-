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
    public partial class FrmProdutos : Form
    {
        public FrmProdutos()
        {
            InitializeComponent();
        }


        private void habilitarCampos()
        {
            txtNome.Enabled = true;
            txtDescricao.Enabled = true;
            txtValor.Enabled = true;
            cbFornecedor.Enabled = true;
            txtEstoque.Enabled = true;
            btnImg.Enabled = true;
            txtNome.Focus();

        }


        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtDescricao.Enabled = false;
            txtValor.Enabled = false;
            cbFornecedor.Enabled = false;
            txtEstoque.Enabled = false;
            btnImg.Enabled = false;
        }


        private void limparCampos()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtValor.Text = "";
            txtEstoque.Text = "";
            LimparFoto();
        }



        private void LimparFoto()
        {
            img.Image = Properties.Resources.sem_foto;
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            LimparFoto();
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
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

            if (txtValor.Text == "")
            {
                MessageBox.Show("Preencha o Valor", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtValor.Focus();
                return;
            }


            //CÓDIGO DO BOTÃO PARA SALVAR

            MessageBox.Show("Registro Salvo com Sucesso!", "Dados Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            limparCampos();
            desabilitarCampos();
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

            if (txtValor.Text == "")
            {
                MessageBox.Show("Preencha o Valor", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtValor.Focus();
                return;
            }

            //CÓDIGO DO BOTÃO PARA EDITAR

            MessageBox.Show("Registro Editado com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            limparCampos();
            desabilitarCampos();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                //CÓDIGO DO BOTÃO PARA EXCLUIR


                MessageBox.Show("Registro Excluido com Sucesso!", "Registro Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                txtNome.Text = "";
                txtNome.Enabled = false;
            }
        }

        private void BtnImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Imagens(*.jpg;*.png)|*.jpg;*.png|Todos os Arquivos(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foto = dialog.FileName.ToString();
                img.ImageLocation = foto;
            }
        }
    }
}
