using System;
using System.Windows.Forms;
using Npgsql;

namespace Teste_Pratico.Infra
{
    public class ConexaoBanco
    {
        private NpgsqlConnection conexao = null;

        /// <summary>
        /// Estabelece uma conexão com o banco de dados PostgreSQL usando uma string de conexão pré-definida.
        /// Caso ocorra algum erro durante a tentativa de conexão, o erro é registrado em um log e uma mensagem é exibida ao usuário.
        /// </summary>
        /// <returns>Retorna uma instância da conexão aberta com o banco de dados <see cref="NpgsqlConnection"/>, ou null em caso de erro.</returns>

        public NpgsqlConnection AbrirConexao()
        {
            try
            {
                string conexaoString = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=testeMD";

                NpgsqlConnection conexao = new NpgsqlConnection(conexaoString);
                conexao.Open();
                return conexao;
            }
            catch (Exception ex)
            {
                LogErros.GravarLogErros(ex.Message);
                MessageBox.Show($"Erro ao conectar com o banco de dados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return conexao;
            }
        }
    }
}