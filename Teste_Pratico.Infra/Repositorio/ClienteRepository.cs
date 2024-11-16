using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;
using Teste_Pratico.Domain.Interface;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Infra.Repositorio
{
    public class ClienteRepository : IClienteRepository<tb_clientes>
    {
        private ConexaoBanco conexaoBanco = new ConexaoBanco();

        public bool InserirCadastroCliente(tb_clientes _cliente)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "insert into tb_clientes " +
                                 "(cl_nome, cl_cpf, cl_cep, cl_endereco, cl_bairro, cl_cidade, cl_estado, cl_telefone, cl_email, cl_ativo) " +
                                 "values " +
                                 "(@nomeCliente, @cpfCliente, @cepCliente, @enderecoCliente, @bairroCliente, @cidadeCliente, @estadoCliente, @telefoneCliente, @emailCliente , @ativoCliente)";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@nomeCliente", _cliente.cl_nome);
                    cmd.Parameters.AddWithValue("@cpfCliente", _cliente.cl_cpf);
                    cmd.Parameters.AddWithValue("@cepCliente", _cliente.cl_cep);
                    cmd.Parameters.AddWithValue("@enderecoCliente", _cliente.cl_endereco);
                    cmd.Parameters.AddWithValue("@bairroCliente", _cliente.cl_bairro);
                    cmd.Parameters.AddWithValue("@cidadeCliente", _cliente.cl_cidade);
                    cmd.Parameters.AddWithValue("@estadoCliente", _cliente.cl_estado);
                    cmd.Parameters.AddWithValue("@telefoneCliente", _cliente.cl_telefone);
                    cmd.Parameters.AddWithValue("@emailCliente", _cliente.cl_email);
                    cmd.Parameters.AddWithValue("@ativoCliente", _cliente.cl_ativo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao inserir dados cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public void AtualizarCadastroCliente(tb_clientes _cliente)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "update tb_clientes set " +
                                 "cl_nome = @nomeCliente, cl_cpf = @cpfCliente, cl_cep = @cepCliente , cl_endereco = @enderecoCliente, cl_bairro = @bairroCliente, cl_cidade = @cidadeCliente, cl_estado = @estadoCliente , cl_telefone = @telefoneCliente, cl_email = @emailCliente " +
                                 "where id_cliente = @idCliente";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@idCliente", _cliente.id_cliente);
                    cmd.Parameters.AddWithValue("@nomeCliente", _cliente.cl_nome);
                    cmd.Parameters.AddWithValue("@cpfCliente", _cliente.cl_cpf);
                    cmd.Parameters.AddWithValue("@cepCliente", _cliente.cl_cep);
                    cmd.Parameters.AddWithValue("@enderecoCliente", _cliente.cl_endereco);
                    cmd.Parameters.AddWithValue("@bairroCliente", _cliente.cl_bairro);
                    cmd.Parameters.AddWithValue("@cidadeCliente", _cliente.cl_cidade);
                    cmd.Parameters.AddWithValue("@estadoCliente", _cliente.cl_estado);
                    cmd.Parameters.AddWithValue("@telefoneCliente", _cliente.cl_telefone);
                    cmd.Parameters.AddWithValue("@emailCliente", _cliente.cl_email);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cliente editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao editar dados cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExcluirCadastroCliente(int _idCliente)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "update tb_clientes set cl_ativo = false where id_cliente = @idCliente";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@idCliente", _idCliente);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cliente deletado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao deletar dados cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public tb_clientes BuscarClientePorId(int _idCliente)
        {
            try
            {
                tb_clientes cliente = new tb_clientes();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_cliente, cl_nome, cl_cpf, cl_cep, cl_endereco, cl_bairro, cl_cidade, cl_estado, cl_telefone, cl_email " +
                                 "from tb_clientes " +
                                 "where id_cliente = @idCliente";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@idCliente", _idCliente);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        cliente.id_cliente = Convert.ToInt32(dr["id_cliente"]);
                        cliente.cl_nome = dr["cl_nome"].ToString();
                        cliente.cl_cpf = dr["cl_cpf"].ToString();
                        cliente.cl_cep = dr["cl_cep"].ToString();
                        cliente.cl_endereco = dr["cl_endereco"].ToString();
                        cliente.cl_bairro = dr["cl_bairro"].ToString();
                        cliente.cl_cidade = dr["cl_cidade"].ToString();
                        cliente.cl_estado = dr["cl_estado"].ToString();
                        cliente.cl_telefone = dr["cl_telefone"].ToString();
                        cliente.cl_email = dr["cl_email"].ToString();
                    }

                    return cliente;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar dados cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public List<tb_clientes> BuscarClientePorNome(string _nomeCliente)
        {
            try
            {
                List<tb_clientes> listaClientes = new List<tb_clientes>();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_cliente, cl_nome, cl_cpf, cl_cep, cl_endereco, cl_bairro, cl_cidade, cl_estado, cl_telefone, cl_email " +
                                 "from tb_clientes " +
                                 "where cl_ativo = true and cl_nome like @nomeCliente";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@nomeCliente", $"%{_nomeCliente}%");

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var cliente = new tb_clientes()
                        {
                            id_cliente = dr.GetInt32(0),
                            cl_nome = dr.GetString(1),
                            cl_cpf = dr.GetString(2),
                            cl_cep = dr.GetString(3),
                            cl_endereco = dr.GetString(4),
                            cl_bairro = dr.GetString(5),
                            cl_cidade = dr.GetString(6),
                            cl_estado = dr.GetString(7),
                            cl_telefone = dr.GetString(8),
                            cl_email = dr.GetString(9)
                        };

                        listaClientes.Add(cliente);
                    }

                    if (listaClientes.Count == 0)
                    {
                        MessageBox.Show("Cliente não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    return listaClientes;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar dados cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public tb_clientes BuscarClientePorCPF(string _cpfCliente)
        {
            try
            {
                tb_clientes tb_clientes = new tb_clientes();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_cliente, cl_nome, cl_cpf, cl_cep, cl_endereco, cl_bairro, cl_cidade, cl_estado, cl_telefone, cl_email " +
                                 "from tb_clientes " +
                                 "where cl_ativo = true and cl_cpf = @cpfCliente";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@cpfCliente", _cpfCliente);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        tb_clientes = new tb_clientes()
                        {
                            id_cliente = dr.GetInt32(0),
                            cl_nome = dr.GetString(1),
                            cl_cpf = dr.GetString(2),
                            cl_cep = dr.GetString(3),
                            cl_endereco = dr.GetString(4),
                            cl_bairro = dr.GetString(5),
                            cl_cidade = dr.GetString(6),
                            cl_estado = dr.GetString(7),
                            cl_telefone = dr.GetString(8),
                            cl_email = dr.GetString(9)
                        };

                        return tb_clientes;
                    }
                    else
                    {
                        MessageBox.Show("Cliente não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar dados cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public List<tb_clientes> BuscarTodosClientes()
        {
            try
            {
                List<tb_clientes> listaClientes = new List<tb_clientes>();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_cliente, cl_nome, cl_cpf, cl_cep, cl_endereco, cl_bairro, cl_cidade, cl_estado, cl_telefone, cl_email " +
                                 "from tb_clientes " +
                                 "where cl_ativo = true";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var cliente = new tb_clientes()
                        {
                            id_cliente = dr.GetInt32(0),
                            cl_nome = dr.GetString(1),
                            cl_cpf = dr.GetString(2),
                            cl_cep = dr.GetString(3),
                            cl_endereco = dr.GetString(4),
                            cl_bairro = dr.GetString(5),
                            cl_cidade = dr.GetString(6),
                            cl_estado = dr.GetString(7),
                            cl_telefone = dr.GetString(8),
                            cl_email = dr.GetString(9)
                        };

                        listaClientes.Add(cliente);
                    }

                    return listaClientes;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar todos os dados dos Clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public List<tb_clientes> BuscarClientesTop20()
        {
            try
            {
                List<tb_clientes> listaClientes = new List<tb_clientes>();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_cliente, cl_nome, cl_cpf, cl_cep, cl_endereco, cl_bairro, cl_cidade, cl_estado, cl_telefone, cl_email " +
                                 "from tb_clientes " +
                                 "where cl_ativo = true " +
                                 "order by id_cliente desc limit 20";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var cliente = new tb_clientes()
                        {
                            id_cliente = dr.GetInt32(0),
                            cl_nome = dr.GetString(1),
                            cl_cpf = dr.GetString(2),
                            cl_cep = dr.GetString(3),
                            cl_endereco = dr.GetString(4),
                            cl_bairro = dr.GetString(5),
                            cl_cidade = dr.GetString(6),
                            cl_estado = dr.GetString(7),
                            cl_telefone = dr.GetString(8),
                            cl_email = dr.GetString(9)
                        };

                        listaClientes.Add(cliente);
                    }

                    return listaClientes;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar todos os dados dos Clientes top 20: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }
    }
}