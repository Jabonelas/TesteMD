using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Teste_Pratico.Domain.Interface;
using Teste_Pratico.Domain.Model;

namespace Teste_Pratico.Infra.Repositorio
{
    public class ProdutoRepository : IProdutoRepository<tb_produtos>
    {
        private ConexaoBanco conexaoBanco = new ConexaoBanco();

        public bool InserirCadastroProduto(tb_produtos _produto)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "insert into tb_produtos " +
                                 "(pd_nome, pd_descricao, pd_preco, pd_estoque, pd_ativo) " +
                                 "values " +
                                 "(@nomeProduto, @descricaoProduto, @precoProduto, @estoqueProduto , @ativoProduto)";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@nomeProduto", _produto.pd_nome);
                    cmd.Parameters.AddWithValue("@descricaoProduto", _produto.pd_descricao);
                    cmd.Parameters.AddWithValue("@precoProduto", _produto.pd_preco);
                    cmd.Parameters.AddWithValue("@estoqueProduto", _produto.pd_estoque);
                    cmd.Parameters.AddWithValue("@ativoProduto", _produto.pd_ativo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao inserir dados produto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool AtualizarCadastroProduto(tb_produtos _produto)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "update tb_produtos set " +
                                 "pd_nome = @nomeProduto, pd_descricao = @descricaoProduto, pd_preco = @precoProduto, pd_estoque = @estoqueProduto " +
                                 "where id_produto = @idProduto";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@idProduto", _produto.id_produto);
                    cmd.Parameters.AddWithValue("@nomeProduto", _produto.pd_nome);
                    cmd.Parameters.AddWithValue("@descricaoProduto", _produto.pd_descricao);
                    cmd.Parameters.AddWithValue("@precoProduto", _produto.pd_preco);
                    cmd.Parameters.AddWithValue("@estoqueProduto", _produto.pd_estoque);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao editar dados produto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool ExcluirCadastroProduto(int _idProduto)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "update tb_produtos set pd_ativo = false where id_produto = @idProduto";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@idProduto", _idProduto);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto deletado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao deletar dados produto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public List<tb_produtos> BuscarProdutosContemNome(string _nomeProduto)
        {
            try
            {
                List<tb_produtos> listaProdutos = new List<tb_produtos>();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_produto, pd_nome, pd_descricao, pd_preco, pd_estoque " +
                                 "from tb_produtos " +
                                 "where pd_nome like @nomeProduto and pd_ativo = true";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@nomeProduto", $"%{_nomeProduto}%");

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var produto = new tb_produtos
                        {
                            id_produto = dr.GetInt32(0),
                            pd_nome = dr.GetString(1),
                            pd_descricao = dr.GetString(2),
                            pd_preco = dr.GetDecimal(3),
                            pd_estoque = dr.GetInt32(4)
                        };

                        listaProdutos.Add(produto);
                    }

                    return listaProdutos;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar Produtos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public tb_produtos BuscarProdutoPorNome(string _nomeProduto)
        {
            try
            {
                tb_produtos produto = new tb_produtos();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_produto, pd_nome, pd_descricao, pd_preco, pd_estoque, pd_ativo " +
                                 "from tb_produtos " +
                                 "where pd_nome = @nomeProduto and pd_ativo = true";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@nomeProduto", _nomeProduto);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        produto = new tb_produtos
                        {
                            id_produto = dr.GetInt32(0),
                            pd_nome = dr.GetString(1),
                            pd_descricao = dr.GetString(2),
                            pd_preco = dr.GetDecimal(3),
                            pd_estoque = dr.GetInt32(4),
                        };
                    }

                    return produto;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar dados Produto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public tb_produtos BuscarProdutoPorId(int _idProduto)
        {
            try
            {
                tb_produtos produto = new tb_produtos();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_produto, pd_nome, pd_descricao, pd_preco, pd_estoque " +
                                 "from tb_produtos " +
                                 "where id_produto = @idProduto";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@idProduto", _idProduto);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        produto = new tb_produtos
                        {
                            id_produto = dr.GetInt32(0),
                            pd_nome = dr.GetString(1),
                            pd_descricao = dr.GetString(2),
                            pd_preco = dr.GetDecimal(3),
                            pd_estoque = dr.GetInt32(4)
                        };
                    }

                    return produto;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar dados produto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public List<tb_produtos> BuscarProdutosTop20()
        {
            try
            {
                List<tb_produtos> listaProdutos = new List<tb_produtos>();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_produto, pd_nome, pd_descricao, pd_preco, pd_estoque " +
                                 "from tb_produtos " +
                                 "where pd_ativo = true " +
                                 "order by id_produto desc limit 20";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var produto = new tb_produtos
                        {
                            id_produto = dr.GetInt32(0),
                            pd_nome = dr.GetString(1),
                            pd_descricao = dr.GetString(2),
                            pd_preco = dr.GetDecimal(3),
                            pd_estoque = dr.GetInt32(4),
                        };

                        listaProdutos.Add(produto);
                    }

                    return listaProdutos;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);

                MessageBox.Show($"Erro ao buscar todos os dados dos Produtos top 20: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public List<tb_produtos> BuscarTodosProdutos()
        {
            try
            {
                List<tb_produtos> listaProdutos = new List<tb_produtos>();

                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    string sql = "select id_produto,pd_nome,pd_descricao,pd_preco,pd_estoque from tb_produtos where pd_ativo = true";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var produto = new tb_produtos
                        {
                            id_produto = dr.GetInt32(0),
                            pd_nome = dr.GetString(1),
                            pd_descricao = dr.GetString(2),
                            pd_preco = dr.GetDecimal(3),
                            pd_estoque = dr.GetInt32(4),
                        };

                        listaProdutos.Add(produto);
                    }

                    return listaProdutos;
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao buscar todos os dados dos Produtos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        public void AtualizarEstoqueProduto(List<tb_vendas> _itemsVenda)
        {
            try
            {
                using (NpgsqlConnection conexao = conexaoBanco.AbrirConexao())
                {
                    foreach (var itemVenda in _itemsVenda)
                    {
                        string sql = "update tb_produtos set pd_estoque = pd_estoque - @quantidade where id_produto = @idProduto";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao))
                        {
                            cmd.Parameters.AddWithValue("@idProduto", itemVenda.fk_tb_produto);
                            cmd.Parameters.AddWithValue("@quantidade", itemVenda.ve_quantidade);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao atualizar estoque do Produto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}