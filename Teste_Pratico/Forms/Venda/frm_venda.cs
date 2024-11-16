using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Venda
{
    public partial class frm_venda : Form
    {
        private decimal valorTotal = 0;
        private tb_clientes clienteDados = new tb_clientes();
        private tb_produtos produtoDados = new tb_produtos();
        private DataTable dt = new DataTable();

        /// <summary>
        /// Lista de <see cref="tb_vendas"/> que vai ser usada para inserir no banco de dados
        /// </summary>
        private List<tb_vendas> listaVenda = new List<tb_vendas>();

        public frm_venda()
        {
            InitializeComponent();
            ConfigurarColunasGridView();
        }

        /// <summary>
        /// Pegando os dados do cliente que foi selecionado para preencher os campos do cliente
        /// </summary>
        /// <param name="_cliente">Objeto contendo os dados do <see cref="tb_clientes"/>.</param>
        private void PreencherCamposDadosCliente(tb_clientes _cliente)
        {
            txtNomeCliente.Text = _cliente.cl_nome;
            txtCEP.Text = _cliente.cl_cep;
            txtEndereco.Text = _cliente.cl_endereco;
            txtBairro.Text = _cliente.cl_bairro;
            txtCidade.Text = _cliente.cl_cidade;
            txtEstado.Text = _cliente.cl_estado;
            txtTelefone.Text = _cliente.cl_telefone;
            txtEmail.Text = _cliente.cl_email;
        }

        /// <summary>
        /// Realiza a limpeza dos campos de produto, após o produto ser adicionado na venda
        /// </summary>
        private void LimparCamposCliente()
        {
            txtEndereco.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        /// <summary>
        /// Realiza a verificação se o campo de CPF esta preenchido para realizar a busca do cliente
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se o campo CPF do cliente foi preenchido.</returns>
        private bool IsCampoClientePreenchido()
        {
            if (string.IsNullOrEmpty(txtCPF.Text))
            {
                return false;
            }

            return true;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }

        /// <summary>
        /// Realiza a busca do cliente pelo CPF, e exibe os dados do cliente nos campos de texto
        /// </summary>
        private void BuscarCliente()
        {
            if (IsCampoClientePreenchido())
            {
                clienteDados = BuscarClientePorCPF();

                if (clienteDados != null)
                {
                    PreencherCamposDadosCliente(clienteDados);
                }
                else
                {
                    LimparCamposCliente();
                }
            }
            else
            {
                MessageBox.Show("Preencha o campo nome para realizar a busca do cliente.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Acesso ao serviço de cliente, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="ClienteService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private ClienteService AcessoClienteService()
        {
            var clienteRepository = new ClienteRepository();
            var clienteService = new ClienteService(clienteRepository);
            return clienteService;
        }

        /// <summary>
        /// Busca o cliente pelo CPF
        /// </summary>
        /// <returns>Retorna um objeto contendo os dados do <see cref="tb_clientes"/>.</returns>
        private tb_clientes BuscarClientePorCPF()
        {
            try
            {
                clienteDados = AcessoClienteService().BuscarClientePorCPF(txtCPF.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar cliente por CPF: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return clienteDados;
        }

        /// <summary>
        /// Pegando os dados do <see cref="tb_produtos"/> que foi selecionado para preencher os campos do produto
        /// </summary>
        /// <param name="_produto">Objeto contendo os dados do produto a ser inserido nos campos.</param>
        private void PreencherCamposDadosProduto(tb_produtos _produto)
        {
            txtNomeProduto.Text = _produto.pd_nome;
            txtDescricaoProduto.Text = _produto.pd_descricao;
            txtPreco.Text = _produto.pd_preco.ToString("C2");
        }

        /// <summary>
        /// Realiza a limpeza dos campos de produto, após o produto ser adicionado na venda
        /// </summary>
        private void LimparCamposProduto()
        {
            txtNomeProduto.Text = string.Empty;
            txtQdtProduto.Text = string.Empty;
            txtDescricaoProduto.Text = string.Empty;
            txtPreco.Text = string.Empty;
        }

        /// <summary>
        /// Realiza a verificação se o campo de nome do produto esta preenchido para realizar buscar do produto
        /// </summary>
        private bool IsCampoNomeProdutoPreenchido()
        {
            if (string.IsNullOrEmpty(txtNomeProduto.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Acesso ao serviço de produto, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="ProdutoService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private ProdutoService AcessoProdutoService()
        {
            var produtoRepository = new ProdutoRepository();
            var produtoService = new ProdutoService(produtoRepository);
            return produtoService;
        }

        private void btnBuscarProduto_Click(object sender, EventArgs e)
        {
            BuscarProduto();
        }

        /// <summary>
        /// Realiza a busca do produto pelo nome, e exibe os dados do <see cref="tb_produtos"/> nos campos de texto
        /// </summary>
        private void BuscarProduto()
        {
            if (IsCampoNomeProdutoPreenchido())
            {
                produtoDados = AcessoProdutoService().BuscarProdutoPorNome(txtNomeProduto.Text);

                if (produtoDados != null)
                {
                    PreencherCamposDadosProduto(produtoDados);
                }
                else
                {
                    LimparCamposProduto();
                }
            }
            else
            {
                MessageBox.Show("Preencha o campo nome para realizar a busca do produto.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Configuração das colunas do DataGridView, para exibir os dados da venda.
        /// </summary>
        private void ConfigurarColunasGridView()
        {
            dt.Columns.Add("ID Produto", typeof(int));
            dt.Columns.Add("Nome Produto", typeof(string));
            dt.Columns.Add("Descrição Produto", typeof(string));
            dt.Columns.Add("Preço Produto", typeof(string));
            dt.Columns.Add("Quantidade", typeof(int));

            gdvVenda.DataSource = dt;

            gdvVenda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            gdvVenda.AllowUserToAddRows = false;

            gdvVenda.Columns[0].Visible = false; //coluna de ID
        }

        private void btnAddProduto_Click(object sender, EventArgs e)
        {
            if (IsCampoNomeProdutoPreenchido() == false)
            {
                MessageBox.Show("Por favor, informe um produto antes de continuar.", "Atenção", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (IsCampoQuantidadeProdutoPreenchido() == false)
            {
                MessageBox.Show("Por favor, informe a quantidade do produto antes de continuar.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsQuantidadeEstoqueDisponivel() == false)
            {
                return;
            }

            PreencherValorTotal();

            if (IsProdutoExistenteVenda() == false)
            {
                PreenchendoGridDadosProduto();
            }
        }

        /// <summary>
        /// Verifica se a quantidade de produto solicitada esta disponivel no estoque
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> que indica se a quantidade solicitada do produto está disponível em estoque.</returns>
        private bool IsQuantidadeEstoqueDisponivel()
        {
            try
            {
                int quantidadeSolicitada = Convert.ToInt32(txtQdtProduto.Text);

                bool isDisponivel = IsDisponibilidadeEstoque(quantidadeSolicitada);

                return isDisponivel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar disponibilidade de estoque: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        /// <summary>
        /// Verifica se o produto ja foi adicionado na venda, caso sim, chama o metodo que irar atualizar a quantidade do produto na venda
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se na venda ja existe o produto que esta tentando adicionar.</returns>
        private bool IsProdutoExistenteVenda()
        {
            int novaQuantidade = 0;

            foreach (tb_vendas itemVenda in listaVenda)
            {
                if (itemVenda.fk_tb_produto == produtoDados.id_produto)
                {
                    novaQuantidade = itemVenda.ve_quantidade + Convert.ToInt32(txtQdtProduto.Text);

                    if (IsDisponibilidadeEstoque(novaQuantidade) == true)
                    {
                        itemVenda.ve_quantidade = novaQuantidade;

                        AtualizarQuantidade(produtoDados.id_produto, novaQuantidade);

                        LimparCamposProduto();

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Caso o produto ja tenha sido adicionado, atualiza a quantidade do produto ja existente na venda.
        /// </summary>
        /// <param name="_idProduto">Id do produto que irar frer a alteração</param>
        /// <param name="_novaQuantidade">Valor da quantidade a ser atualizada</param>
        private void AtualizarQuantidade(int _idProduto, int _novaQuantidade)
        {
            DataTable dt = (DataTable)gdvVenda.DataSource;

            foreach (DataRow row in dt.Rows)
            {
                if (row["ID Produto"].ToString() == _idProduto.ToString())
                {
                    row["Quantidade"] = _novaQuantidade;
                    break;
                }
            }

            gdvVenda.Refresh();
        }

        /// <summary>
        /// Verifica se o produto tem estoque suficiente para realizar a venda
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> que indica se a quantidade solicitada do produto está disponível em estoque.</returns>
        private bool IsDisponibilidadeEstoque(int _quantidadeSolicitada)
        {
            try
            {
                tb_produtos produto = AcessoProdutoService().BuscarProdutoPorId(produtoDados.id_produto);

                if (produto.pd_estoque < _quantidadeSolicitada)
                {
                    MessageBox.Show($"Produto sem estoque suficiente.\nQuantidade total dispononivel: {produto.pd_estoque} und.",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar disponibilidade de estoque: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Realiza a verificação se o campo de quantidade esta preenchido para realizar a venda
        /// </summary>
        private bool IsCampoQuantidadeProdutoPreenchido()
        {
            if (string.IsNullOrEmpty(txtQdtProduto.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Preencher o valor total da venda, multiplicando o preço do produto pela quantidade informada
        /// </summary>
        private void PreencherValorTotal()
        {
            try
            {
                decimal precoProduto = Convert.ToDecimal(txtPreco.Text.Replace("R$", "").Replace("$", ""));
                int quantidadeProduto = Convert.ToInt16(txtQdtProduto.Text);

                valorTotal += precoProduto * quantidadeProduto;

                lblValorTotal.Text = valorTotal.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao preencher o valor total: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Preencher grid com os dados do <see cref="tb_produtos"/> que foi seleciondo
        /// </summary>
        private void PreenchendoGridDadosProduto()
        {
            try
            {
                string nomeProduto = produtoDados.pd_nome;
                string descricaoProduto = produtoDados.pd_descricao;
                decimal precoProduto = produtoDados.pd_preco;
                int quantidadeProduto = Convert.ToInt16(txtQdtProduto.Text);
                int idCliente = clienteDados.id_cliente;
                int idProduto = produtoDados.id_produto;

                dt.Rows.Add(idProduto, nomeProduto, descricaoProduto, precoProduto.ToString("C2"), quantidadeProduto);

                //preenchendo a lista de vendas que vai ser usando no momento de finalizar a venda
                listaVenda.Add(new tb_vendas(quantidadeProduto, idCliente, idProduto));

                LimparCamposProduto();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao preencher o grid de dados do produto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Você tem certeza de que deseja cancelar a venda?", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnFinalizarVenda_Click(object sender, EventArgs e)
        {
            if (IsVendaValida() == true)
            {
                RealizarVenda();
                DebitarEstoqueProduto();
            }
        }

        /// <summary>
        /// Verificar se a venda é válida, se o cliente foi informado e se há produtos na venda
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se a venda venda é valida pois todos os campos obrigatorios foram preenchidos.</returns>
        private bool IsVendaValida()
        {
            if (IsCampoClientePreenchido() == false)
            {
                MessageBox.Show("Por favor, informe o cliente antes de continuar.", "Atenção", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (gdvVenda.RowCount == 0)
            {
                MessageBox.Show("Por favor, informe um produto antes de continuar.", "Atenção", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Realiza a autualização do estoque do produto após a venda, debitando do estoque atual a quantidade vendida
        /// </summary>
        private void DebitarEstoqueProduto()
        {
            AcessoProdutoService().AtualizarEstoqueProduto(listaVenda);
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="VendaService"/>, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="VendaService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private VendaService AcessoVendaService()
        {
            var vendaRepository = new VendaRepository();
            var vendaService = new VendaService(vendaRepository);

            return vendaService;
        }

        /// <summary>
        /// Realiza a inserssão da <see cref="tb_vendas"/> no banco de dados
        /// </summary>
        private void RealizarVenda()
        {
            var isVendaRealizada = AcessoVendaService().InserirVendas(listaVenda);

            if (isVendaRealizada)
            {
                this.Close();
            }
        }

        private void txtQdtProduto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCPFCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatarTextBoxCPF(e);
        }

        private void FormatarTextBoxCPF(KeyPressEventArgs e)
        {
            ImpedirDigitarLetras(e);

            FormatandoMascaraCPF(e);
        }

        /// <summary>
        /// Faz a formatação do textbox do campo CPF, deixa o usuário digitar apenas números e coloca a mascara no campo formatando como CPF
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void FormatandoMascaraCPF(KeyPressEventArgs _event)
        {
            if (char.IsNumber(_event.KeyChar) == true)
            {
                switch (txtCPF.TextLength)
                {
                    case 0:
                        txtCPF.Text = "";
                        break;

                    case 3:
                        txtCPF.Text = txtCPF.Text + ".";
                        txtCPF.SelectionStart = 4;
                        break;

                    case 7:
                        txtCPF.Text = txtCPF.Text + ".";
                        txtCPF.SelectionStart = 8;
                        break;

                    case 11:
                        txtCPF.Text = txtCPF.Text + "-";
                        txtCPF.SelectionStart = 12;
                        break;
                }
            }
        }
    }
}