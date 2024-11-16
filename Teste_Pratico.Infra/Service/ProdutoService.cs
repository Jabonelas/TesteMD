using System.Collections.Generic;
using Teste_Pratico.Domain.Interface;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Infra.Service
{
    public class ProdutoService
    {
        private readonly IProdutoRepository<tb_produtos> produtoRepository;

        /// <summary>
        /// Construtor da classe ProdutoService
        /// </summary>
        /// <param name="_produtoRepository"></param>
        public ProdutoService(IProdutoRepository<tb_produtos> _produtoRepository)
        {
            produtoRepository = _produtoRepository;
        }

        /// <summary>
        /// Buscar todos os produtos que contem o nome no sistema.
        /// </summary>
        /// <param name="_nomeProduto"></param>
        /// <returns>Retorna uma lista com todos os produtos que contem o nome informado</returns>
        public List<tb_produtos> BuscarProdutosContemNome(string _nomeProduto)
        {
            List<tb_produtos> produto = produtoRepository.BuscarProdutosContemNome(_nomeProduto);

            return produto;
        }

        /// <summary>
        /// Buscar todos os produtos que contem o nome no sistema.
        /// </summary>
        /// <param name="_nomeProduto">Nome do produto que sera realizado a busca</param>
        /// <returns>Retorna uma lista com todos os produtos com nome informado</returns>
        public tb_produtos BuscarProdutoPorNome(string _nomeProduto)
        {
            tb_produtos produto = produtoRepository.BuscarProdutoPorNome(_nomeProduto);

            return produto;
        }

        /// <summary>
        /// Buscar um produto por id no sistema.
        /// </summary>
        /// <param name="_idProduto">Id do produto a ser realizado a busca</param>
        /// <returns>Retorna um objeto de produtos contendo os dados do produto com o id informado</returns>
        public tb_produtos BuscarProdutoPorId(int _idProduto)
        {
            var produto = produtoRepository.BuscarProdutoPorId(_idProduto);

            return produto;
        }

        /// <summary>
        /// Buscar todos os produtos cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna lista com todos os produtos cadastrados</returns>
        public List<tb_produtos> BuscarTodosProdutos()
        {
            List<tb_produtos> produto = produtoRepository.BuscarTodosProdutos();

            return produto;
        }

        /// <summary>
        /// Buscar os ultimos 20 produtos cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna a lista com os ultimos 20 produtos cadastrados </returns>
        public List<tb_produtos> BuscarProdutosTop20()
        {
            List<tb_produtos> produto = produtoRepository.BuscarProdutosTop20();

            return produto;
        }

        /// <summary>
        /// Insere um produto no sistema, utilizando o objeto produto fornecido.
        /// </summary>
        /// <param name="_produto">Objeto contendo os dados do produto a ser inserido.</param>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se o produto foi cadastrado com sucesso.</returns>
        public bool InserirProduto(tb_produtos _produto)
        {
            bool isProdutoCadastrado = produtoRepository.InserirCadastroProduto(_produto);

            return isProdutoCadastrado;
        }

        /// <summary>
        /// Atualizar produto cadastrado
        /// </summary>
        /// <param name="_produto">Objeto contendo os dados do produto que sera utilizado</param>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se o produto foi atualizado com sucesso.</returns>
        public bool AtualizarProduto(tb_produtos _produto)
        {
            bool isProdutoCadastrado = produtoRepository.AtualizarCadastroProduto(_produto);

            return isProdutoCadastrado;
        }

        /// <summary>
        /// Deletar produto cadastrado
        /// </summary>
        /// <param name="_produto">ID do produto a ser deletado</param>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se o produto foi deletado com sucesso.</returns>
        public bool DeletarProduto(int _produto)
        {
            bool isProdutoCadastrado = produtoRepository.ExcluirCadastroProduto(_produto);

            return isProdutoCadastrado;
        }

        /// <summary>
        /// Atualizar estoque do produto após a venda
        /// </summary>
        /// <param name="_venda">Objeto contendo os dados da venda que sera utilizado para atualizar estoque</param>
        public void AtualizarEstoqueProduto(List<tb_vendas> _venda)
        {
            produtoRepository.AtualizarEstoqueProduto(_venda);
        }
    }
}