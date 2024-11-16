using System.Collections.Generic;
using Teste_Pratico.Domain.Interface;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Infra.Service
{
    public class ClienteService
    {
        private readonly IClienteRepository<tb_clientes> clienteRepository;

        /// <summary>
        /// Construtor da classe ClienteService
        /// </summary>
        /// <param name="_clienteRepository"></param>
        public ClienteService(IClienteRepository<tb_clientes> _clienteRepository)
        {
            clienteRepository = _clienteRepository;
        }

        /// <summary>
        /// Buscar todos os clientes que contem o nome no sistema.
        /// </summary>
        /// <param name="_nomeCliente">Nome do cliente que sera realizado a busca</param>
        /// <returns>Retorna uma lista com todos os clientes que contem o nome informado</returns>
        public List<tb_clientes> BuscarClientePorNome(string _nomeCliente)
        {
            var cliente = clienteRepository.BuscarClientePorNome(_nomeCliente);

            return cliente;
        }

        /// <summary>
        /// Buscar cliente por CPF no sistema.
        /// </summary>
        /// <param name="_cpfCliente">CPF do cliente a ser realizado a busca</param>
        /// <returns>Retorna um objeto de clientes contendo os dados do cliente com o cpf informado</returns>
        public tb_clientes BuscarClientePorCPF(string _cpfCliente)
        {
            var cliente = clienteRepository.BuscarClientePorCPF(_cpfCliente);

            return cliente;
        }

        /// <summary>
        /// Buscar um cliente por id no sistema.
        /// </summary>
        /// <param name="_idCliente">Id do cliente a ser realizado a busca</param>
        /// <returns>Retorna um objeto de clientes contendo os dados do cliente com o id informado</returns>
        public tb_clientes BuscarClientePorId(int _idCliente)
        {
            var cliente = clienteRepository.BuscarClientePorId(_idCliente);

            return cliente;
        }

        /// <summary>
        /// Buscar todos os clientes cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna lista com todos os clientes cadastrados</returns>
        public List<tb_clientes> BuscarTodosClientes()
        {
            List<tb_clientes> cliente = clienteRepository.BuscarTodosClientes();

            return cliente;
        }

        /// <summary>
        /// Buscar os ultimos 20 clientes cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna a lista com os ultimos 20 clientes cadastrados</returns>
        public List<tb_clientes> BuscarClientesTop20()
        {
            List<tb_clientes> cliente = clienteRepository.BuscarClientesTop20();

            return cliente;
        }

        /// <summary>
        /// Insere um cliente no sistema, utilizando o objeto cliente fornecido.
        /// </summary>
        /// <param name="_cliente">Objeto contendo os dados do cliente a ser inserido.</param>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se o cliente foi cadastrado com sucesso.</returns>
        public bool InserirCliente(tb_clientes _cliente)
        {
            bool isClienteCadastrado = clienteRepository.InserirCadastroCliente(_cliente);

            return isClienteCadastrado;
        }

        /// <summary>
        /// Atualizar um cliente no sistema, utilizando o objeto cliente fornecido.
        /// </summary>
        /// <param name="_cliente">Objeto contendo os dados do cliente a ser atualizado.</param>
        public void AtualizarCliente(tb_clientes _cliente)
        {
            clienteRepository.AtualizarCadastroCliente(_cliente);
        }

        /// <summary>
        /// Deletar um cliente no sistema, utilizando o id do cliente fornecido.
        /// </summary>
        /// <param name="_idCliente">Id do cliente que irar ser deletado</param>
        public void DeletarCliente(int _idCliente)
        {
            clienteRepository.ExcluirCadastroCliente(_idCliente);
        }
    }
}