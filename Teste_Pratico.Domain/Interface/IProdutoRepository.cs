using System.Collections.Generic;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Domain.Interface
{
    public interface IProdutoRepository<tb_produtos>
    {
        bool InserirCadastroProduto(tb_produtos _produto);

        bool AtualizarCadastroProduto(tb_produtos _produto);

        bool ExcluirCadastroProduto(int _produto);

        List<tb_produtos> BuscarProdutosContemNome(string _nomeProduto);

        tb_produtos BuscarProdutoPorNome(string _nomeProduto);

        List<tb_produtos> BuscarProdutosTop20();

        tb_produtos BuscarProdutoPorId(int _idProduto);

        List<tb_produtos> BuscarTodosProdutos();

        void AtualizarEstoqueProduto(List<tb_vendas> _itemsVenda);
    }
}