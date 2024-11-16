using System;
using System.Windows.Forms;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Produto
{
    public partial class frm_produto : Form
    {
        public frm_produto()
        {
            InitializeComponent();
            ConfigurarColunasGridView();
            AtualizarTabelaProdutos();
        }

        /// <summary>
        /// Configuração das colunas do DataGridView, para exibir os dados dos produto.
        /// </summary>
        private void ConfigurarColunasGridView()
        {
            gdvProdutos.AutoGenerateColumns = false;
            gdvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gdvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            gdvProdutos.Columns.Clear();

            gdvProdutos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "id_produto",
            });

            gdvProdutos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nome Produto",
                DataPropertyName = "pd_nome",
            });

            gdvProdutos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Descrição Produto",
                DataPropertyName = "pd_descricao",
            });

            gdvProdutos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estoque",
                DataPropertyName = "pd_estoque",
            });

            gdvProdutos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Preço",
                DataPropertyName = "pd_preco",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            gdvProdutos.Columns[0].Width = 50;//coluna ID
            gdvProdutos.Columns[1].Width = 200;//coluna Nome Produto
            gdvProdutos.Columns[2].Width = 200;//coluna Descricao Produto
            gdvProdutos.Columns[3].Width = 100;//coluna Estoque
            gdvProdutos.Columns[4].Width = 100;//coluna Preço

            gdvProdutos.Columns[0].Visible = false;//coluna ID
        }

        private void btnCadastrarProduto_Click(object sender, EventArgs e)
        {
            frm_cadastrar_editar_produto cadastrarProduto = new frm_cadastrar_editar_produto();
            cadastrarProduto.ShowDialog();

            AtualizarTabelaProdutos();
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
        /// Inserindo lista dos ultimos 20 produtos adiconados no DataGridView
        /// </summary>
        private void AtualizarTabelaProdutos()
        {
            var produtoDados = AcessoProdutoService().BuscarProdutosTop20();
            gdvProdutos.DataSource = produtoDados;
        }

        private void btnBuscarProdutoNome_Click(object sender, EventArgs e)
        {
            BuscarProdutoPorNome();
        }

        /// <summary>
        /// Realiza a busca do produto pelo nome, e exibe no DataGridView
        /// </summary>
        private void BuscarProdutoPorNome()
        {
            if (string.IsNullOrEmpty(txtBuscaProdutoNome.Text))
            {
                MessageBox.Show("Preencha o campo Nome Produto para realizar a busca do produto.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            string nomeProduto = txtBuscaProdutoNome.Text;

            var produtoDados = AcessoProdutoService().BuscarProdutosContemNome(nomeProduto);

            gdvProdutos.DataSource = produtoDados;
        }

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
            EditarProduto();
        }

        /// <summary>
        /// Veirifaca se existe linhas no DataGridView, para saber se tem algum produto selecionado.
        /// </summary>
        /// <returns></returns>
        private bool IsExistemLinhasNoGrid()
        {
            return gdvProdutos.SelectedRows.Count > 0;
        }

        /// <summary>
        /// Realiza a edição do produto selecionado no DataGridView,
        /// </summary>
        private void EditarProduto()
        {
            try
            {
                if (IsExistemLinhasNoGrid() == true)
                {
                    var idProdutoSelecionado = gdvProdutos.SelectedRows[0].Cells[0].Value;
                    int idProduto = Convert.ToInt32(idProdutoSelecionado);

                    var produtoDados = AcessoProdutoService().BuscarProdutoPorId(idProduto);

                    frm_cadastrar_editar_produto editarProduto = new frm_cadastrar_editar_produto(produtoDados);
                    editarProduto.ShowDialog();

                    AtualizarTabelaProdutos();
                }
                else
                {
                    MessageBox.Show("Selecione um produto para editar.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao solicitar edição do cadastro do produto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Realiza a exclusão do produto selecionado no DataGridView
        /// </summary>
        private void DeletarProdutoSelecionado()
        {
            try
            {
                if (IsExistemLinhasNoGrid() == true)
                {
                    var idProdutoSelecionado = gdvProdutos.SelectedRows[0].Cells[0].Value;
                    int idProduto = Convert.ToInt32(idProdutoSelecionado);

                    AcessoProdutoService().DeletarProduto(idProduto);

                    AtualizarTabelaProdutos();
                }
                else
                {
                    MessageBox.Show("Selecione um produto para editar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao solicitar exclusão de produto : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletarProduto_Click(object sender, EventArgs e)
        {
            if (IsExisteProdutoVinculadoVenda() == true)
            {
                MessageBox.Show($"Não é possível excluir o cadastro deste produto, pois ele está vinculado a algumas vendas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            var result = MessageBox.Show("Deseja realmente deletar o produto selecionado?", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeletarProdutoSelecionado();
            }
        }

        /// <summary>
        /// Verifica se o produto selecionado no DataGridView está vinculado a alguma venda,
        /// impedindo a exclusão de produtos que tenham realizado vendas.
        /// </summary>
        /// <returns></returns>
        private bool IsExisteProdutoVinculadoVenda()
        {
            try
            {
                var idProdutoSelecionado = gdvProdutos.SelectedRows[0].Cells[0].Value;
                int idProduto = Convert.ToInt32(idProdutoSelecionado);

                bool IsExisteProdutoVinculadoVenda = AcessoVendaService().VerificarExistenteProdutoVenda(idProduto);

                return IsExisteProdutoVinculadoVenda;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao solicitar verificação de vinculo de produto com venda : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
    }
}