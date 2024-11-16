using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Produto
{
    public partial class frm_cadastrar_editar_produto : Form
    {
        private int idProduto = 0;

        /// <summary>
        /// Construtor para cadastrar produto
        /// </summary>
        public frm_cadastrar_editar_produto()
        {
            InitializeComponent();

            FormatandoTelaCadastroProduto();
        }

        /// <summary>
        /// Construtor para editar produto
        /// </summary>
        /// <param name="_produto">Objeto contendo os dados do <see cref="tb_produtos"/> que foi selecionado na tela anterior.</param>
        public frm_cadastrar_editar_produto(tb_produtos _produto)
        {
            InitializeComponent();

            idProduto = _produto.id_produto;

            PreencherCamposComDadosProduto(_produto);

            FormatandoTelaEditarProduto();
        }

        /// <summary>
        /// Formatando a tela para cadastrar produto, alterando o texto do formulário e do botão
        /// </summary>
        private void FormatandoTelaCadastroProduto()
        {
            this.Text = "Cadastrar Produto";

            btnSalvar.Text = "Cadastrar Produto";
        }

        /// <summary>
        /// Formatando a tela para editar produto, alterando o texto do formulário e do botão
        /// </summary>
        private void FormatandoTelaEditarProduto()
        {
            this.Text = "Editar Produto";

            btnSalvar.Text = "Editar Produto";
        }

        /// <summary>
        /// Realiza a verificação se os campos estão preenchidos para cadastrar ou editar um produto
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se todos os campos de produto foram preenchidos.</returns>

        private bool IsCamposDadosProdutoPreenchidos()
        {
            if (string.IsNullOrEmpty(txtNomeProduto.Text) ||
                string.IsNullOrEmpty(txtDescricaoProduto.Text) ||
                string.IsNullOrEmpty(txtPrecoProduto.Text) ||
                string.IsNullOrEmpty(txtEstoqueProduto.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Pegando os dados do <see cref="tb_produtos"/> que foi selecionado para edição e preenchendo os campos do formulário.
        /// </summary>
        /// <param name="_produto">Objeto contendo os dados do <see cref="tb_produtos"/> a ser inserido nos campos.</param>
        private void PreencherCamposComDadosProduto(tb_produtos _produto)
        {
            txtNomeProduto.Text = _produto.pd_nome;
            txtDescricaoProduto.Text = _produto.pd_descricao;
            txtEstoqueProduto.Text = _produto.pd_estoque.ToString();
            txtPrecoProduto.Text = _produto.pd_preco.ToString("C2");
        }

        private void btnCadastrarProduto_Click(object sender, EventArgs e)
        {
            //Verifica se o botão é para cadastrar
            if (this.Text == "Cadastrar Produto")
            {
                AdicionarNovoProduto();
            }
            //Se não é para cadastrar é para produto
            else
            {
                EditarCadastroProduto();
            }
        }

        /// <summary>
        /// Realiza a interação com o serviço de produto para editar um cadastro de produto
        /// </summary>
        private void EditarCadastroProduto()
        {
            if (IsCamposDadosProdutoPreenchidos() == true)
            {
                var dadosProduto = PegarDadosProdutoDigitado();

                AcessoProdutoService().AtualizarProduto(dadosProduto);

                this.Close();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos para editar o cadastro do produto.", "Atenção", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="ProdutoService"/>, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="ProdutoService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private ProdutoService AcessoProdutoService()
        {
            var produtoRepository = new ProdutoRepository();
            var produtoService = new ProdutoService(produtoRepository);
            return produtoService;
        }

        /// <summary>
        /// Realiza a interação com o serviço de produto para adicionar um novo cadastro do produto
        /// </summary>
        private void AdicionarNovoProduto()
        {
            if (IsCamposDadosProdutoPreenchidos() == true)
            {
                var dadosProduto = PegarDadosProdutoDigitado();
                var isProdutoCadastrado = AcessoProdutoService().InserirProduto(dadosProduto);

                if (isProdutoCadastrado == true)
                {
                    LimparCampos();
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos para cadastrar o produto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Pega os dados digitados para o produto e retorna um objeto <see cref="tb_produtos"/> com os dados,
        /// para serem inseridos no banco de dados
        /// </summary>
        /// <returns>Retorna um objeto de <see cref="tb_produtos"/>, contendo os dados do produto informado</returns>
        private tb_produtos PegarDadosProdutoDigitado()
        {
            tb_produtos produto = new tb_produtos();

            try
            {
                produto.id_produto = idProduto;
                produto.pd_nome = txtNomeProduto.Text;
                produto.pd_descricao = txtDescricaoProduto.Text;
                produto.pd_preco = Convert.ToDecimal(txtPrecoProduto.Text.Replace("R$", "").Replace("$", ""));
                produto.pd_estoque = Convert.ToInt16(txtEstoqueProduto.Text);
                produto.pd_ativo = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao pegar os dados do produto digitado. Erro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return produto;
        }

        /// <summary>
        /// Realiza a limpeza dos campos do formulário, após o cadastro do produto
        /// </summary>
        private void LimparCampos()
        {
            txtNomeProduto.Text = string.Empty;
            txtDescricaoProduto.Text = string.Empty;
            txtPrecoProduto.Text = string.Empty;
            txtEstoqueProduto.Text = string.Empty;
        }

        private void txtPrecoProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatandoTextBoxDinheiro(sender, e);
        }

        /// <summary>
        /// Faz a formatação do textbox para aceitar apenas números e formatar para dinheiro, adiconando o R$ no inicio e a virgula para separar os centavos
        /// </summary>
        /// <param name="_sender">Objeto que disparou o evento.</param>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void FormatandoTextBoxDinheiro(object _sender, KeyPressEventArgs _event)
        {
            try
            {
                txtPrecoProduto.MaxLength = 10;

                if (Char.IsDigit(_event.KeyChar) || _event.KeyChar.Equals((char)Keys.Back))
                {
                    if (txtPrecoProduto.Text.Length <= 13 || _event.KeyChar.Equals((char)Keys.Back))
                    {
                        TextBox textbox = (TextBox)_sender;
                        string textoDoTextBox = Regex.Replace(textbox.Text, "[^0-9]", string.Empty);
                        if (textoDoTextBox == string.Empty)
                        {
                            textoDoTextBox = "00";
                        }

                        textoDoTextBox += _event.KeyChar;
                        textbox.Text = String.Format("R$ {0:#,##0.00}", double.Parse(textoDoTextBox) / 100);
                        textbox.Select(textbox.Text.Length, 0);
                    }
                }
                _event.Handled = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void txtEstoqueProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ImpedirDigitarLetras(e);
        }

        /// <summary>
        /// Faz a conversão do textbox para aceitar apenas números
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void ImpedirDigitarLetras(KeyPressEventArgs _event)
        {
            if (!Char.IsDigit(_event.KeyChar) && _event.KeyChar != (char)8)
            {
                _event.Handled = true;
            }
        }
    }
}