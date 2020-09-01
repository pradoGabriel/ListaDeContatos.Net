using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaContatos.Gabriel
{
    public partial class frmAgendaContatos : Form
    {
        private OperacaoEnum acao;
        public frmAgendaContatos()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbTelefone.Text = contato.NumeroTelefone;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesIncluirAlterarEExcluir(false);
            AlterarCaixasTexto(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Contato contato = new Contato(txbNome.Text, txbEmail.Text, txbTelefone.Text); ;
            List<Contato> contatosList = new List<Contato>();
            foreach (Contato contatoDaLista in lbxContatos.Items)
            {
                contatosList.Add(contatoDaLista);
            }
            if (acao == OperacaoEnum.INCLUIR)
            {
                contatosList.Add(contato);
            }
            else 
            {
                int indice = lbxContatos.SelectedIndex;
                contatosList.RemoveAt(indice);
                contatosList.Insert(indice, contato);
            }
            ManipuladorArquivos.EscreverArquivo(contatosList);
            CarregarListaContatos();
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarEExcluir(true);
            LimparCampos();
            AlterarCaixasTexto(false);
        }

        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorArquivos.LerArquivo().ToArray());
            lbxContatos.SelectedIndex = 0;
        }

        private void LimparCampos()
        {
            txbEmail.Text = null;
            txbNome.Text = null;
            txbTelefone.Text = null;
        }
        private void frmAgendaContatos_Load(object sender, EventArgs e)
        {

        }

        private void frmAgendaContatos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarEExcluir(true);
            CarregarListaContatos();
            AlterarCaixasTexto(false);
        }

        private void AlterarBotoesSalvarECancelar (bool estado)
        {
            btnSalvar.Enabled = estado;
            btnCancelar.Enabled = estado;
        }

        private void AlterarBotoesIncluirAlterarEExcluir (bool estado)
        {
            btnIncluir.Enabled = estado;
            btnAlterar.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void AlterarCaixasTexto(bool estado)
        {
            txbEmail.Enabled = estado;
            txbNome.Enabled = estado;
            txbTelefone.Enabled = estado;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesIncluirAlterarEExcluir(false);
            AlterarCaixasTexto(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarEExcluir(true);
            AlterarCaixasTexto(false);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza?", "Deseja mesmo excluir?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int indiceExcluido = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indiceExcluido);
                List<Contato> contatosList = new List<Contato>();
                foreach (Contato contato in lbxContatos.Items)
                {
                    contatosList.Add(contato);
                }
                ManipuladorArquivos.EscreverArquivo(contatosList);
                CarregarListaContatos();
                LimparCampos();
            }
        }
    }
}
