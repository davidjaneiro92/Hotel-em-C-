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
        public FrmFuncionarios()
        {
            InitializeComponent();
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

            MessageBox.Show("Registro Salvo com Sucesso!", "Dados Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            limparCampos();
            desabilitarCampos();
        }

        private void Grid_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
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
    }
}
