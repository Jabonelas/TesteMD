using System.Collections.Generic;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Domain.Interface
{
    public interface IVendaRepository<tb_venda>
    {
        bool InserirCadastroVenda(List<tb_vendas> _itemsVenda);

        bool VerificarExistenteClienteVenda(int _idCliente);

        bool VerificarExistenteProdutoVenda(int _idProduto);

        List<tb_vendas> BuscarVendas();
    }
}