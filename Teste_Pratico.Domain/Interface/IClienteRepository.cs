using System.Collections.Generic;

namespace Teste_Pratico.Domain.Interface
{
    public interface IClienteRepository<tb_clientes>
    {
        bool InserirCadastroCliente(tb_clientes _clientes);

        void AtualizarCadastroCliente(tb_clientes _clientes);

        void ExcluirCadastroCliente(int _idClientes);

        List<tb_clientes> BuscarClientePorNome(string _nomeCliente);

        tb_clientes BuscarClientePorCPF(string _cpfliente);

        tb_clientes BuscarClientePorId(int _idCliente);

        List<tb_clientes> BuscarTodosClientes();

        List<tb_clientes> BuscarClientesTop20();
    }
}