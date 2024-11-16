using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Aplicacao
{
    public class Aplicacao_produto
    {
        public bool InseririProduto()
        {
            tb_produtos produto = new tb_produtos();
            produto.pd_nome = "Produto 1";
            produto.pd_descricao = "Produto 1";
            produto.pd_preco = 10;
            produto.pd_estoque = 10;
            produto.pd_ativo = true;

            return AcessoProdutoService().InserirProduto(produto);
        }

        public List<tb_produtos> BuscarTodosProdutos()
        {
            return AcessoProdutoService().BuscarTodosProdutos();
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="ProdutoService"/>, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="ProdutoService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        public ProdutoService AcessoProdutoService()
        {
            var produtoRepository = new ProdutoRepository();
            var produtoService = new ProdutoService(produtoRepository);
            return produtoService;
        }
    }
}