using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Teste_Pratico.Domain.Interface;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Infra.Repositorio
{
    public class VendaRepository : IVendaRepository<tb_vendas>
    {
        private ConexaoBanco conexaoBanco = new ConexaoBanco();

        public bool InserirCadastroVenda(List<tb_vendas> _itemsVenda)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    foreach (tb_vendas itemVenda in _itemsVenda)
                    {
                        string sql = "insert into tb_vendas " +
                                     "(ve_quantidade, fk_tb_cliente, fk_tb_produto) " +
                                     "values " +
                                     "(@quantidade, @fk_cliente, @fk_produto)";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao))
                        {
                            cmd.Parameters.AddWithValue("@quantidade", itemVenda.ve_quantidade);
                            cmd.Parameters.AddWithValue("@fk_cliente", itemVenda.fk_tb_cliente);
                            cmd.Parameters.AddWithValue("@fk_produto", itemVenda.fk_tb_produto);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Venda realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao inserir dados venda: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public List<tb_vendas> BuscarVendas()
        {
            try
            {
                List<tb_vendas> listaVendas = new List<tb_vendas>();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select tb_vendas.id_venda, tb_vendas.ve_quantidade, tb_vendas.fk_tb_cliente, tb_vendas.fk_tb_produto , tb_produtos.pd_nome , tb_clientes.cl_nome " +
                                 "from tb_vendas inner join " +
                                 "tb_clientes on tb_vendas.fk_tb_cliente = tb_clientes.id_cliente " +
                                 "inner join tb_produtos on tb_vendas.fk_tb_produto = tb_produtos.id_produto";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var itemVenda = new tb_vendas
                        {
                            ve_id = dr.GetInt32(0),
                            ve_quantidade = dr.GetInt32(1),
                            fk_tb_cliente = dr.GetInt32(2),
                            fk_tb_produto = dr.GetInt32(3),
                            pd_nome = dr.GetString(4),
                            cl_nome = dr.GetString(5)
                        };

                        listaVendas.Add(itemVenda);
                    }

                    return listaVendas;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar todos os dados das Vendas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public bool VerificarExistenteClienteVenda(int _idCliente)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_venda from tb_vendas where fk_tb_cliente = @cliente ";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@cliente", _idCliente);

                        NpgsqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao verificar se existe cliente vinculado a venda : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool VerificarExistenteProdutoVenda(int _idProduto)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_venda from tb_vendas where fk_tb_produto = @produto";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@produto", _idProduto);

                        NpgsqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao verificar se existe produto vinculado a venda : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }
    }
}