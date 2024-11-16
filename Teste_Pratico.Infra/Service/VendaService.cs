using System.Collections.Generic;
using Teste_Pratico.Domain.Interface;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Infra.Service
{
    public class VendaService
    {
        private readonly IVendaRepository<tb_vendas> vendaRepository;

        /// <summary>
        /// Construtor da classe VendaService
        /// </summary>
        /// <param name="_vendaRepository"></param>
        public VendaService(IVendaRepository<tb_vendas> _vendaRepository)
        {
            vendaRepository = _vendaRepository;
        }

        /// <summary>
        /// Insere uma venda no sistema, utilizando o objeto venda fornecido.
        /// </summary>
        /// <param name="_vendas">Objeto contendo os dados do venda a ser inserido.</param>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se a venda foi cadastrada com sucesso.</returns>
        public bool InserirVendas(List<tb_vendas> _vendas)
        {
            bool isVendaCadastrada = vendaRepository.InserirCadastroVenda(_vendas);

            return isVendaCadastrada;
        }

        /// <summary>
        /// Verificar se existe cliente vinculado a uma venda.
        /// </summary>
        /// <param name="_idCliente">Id cliente para verificar.</param>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se o cliente esta vinculado a alguma venda.</returns>
        public bool VerificarExistenteClienteVenda(int _idCliente)
        {
            bool isVendaCadastrada = vendaRepository.VerificarExistenteClienteVenda(_idCliente);

            return isVendaCadastrada;
        }

        /// <summary>
        /// Verificar se existe produto vinculado a uma venda.
        /// </summary>
        /// <param name="_idProduto">Id produto para verificar.</param>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se o produto esta vinculado a alguma venda .</returns>
        public bool VerificarExistenteProdutoVenda(int _idProduto)
        {
            bool isVendaCadastrada = vendaRepository.VerificarExistenteProdutoVenda(_idProduto);

            return isVendaCadastrada;
        }

        /// <summary>
        /// Buscar todos os vendas cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna lista com todos os vendas cadastrados</returns>
        public List<tb_vendas> BuscarVendas()
        {
            var venda = vendaRepository.BuscarVendas();

            return venda;
        }
    }
}