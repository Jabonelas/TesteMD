using System;
using System.IO;
using System.Windows.Forms;

namespace Teste_Pratico.Infra
{
    public class LogErros
    {
        /// <summary>
        /// Grave o log de erro em um arquivo txt na pasta do projeto Teste_Pratico na raiz do projeto.
        /// </summary>
        /// <param name="_mensagem">Mensagem contendo o erro</param>
        internal static void GravarLogErros(string _mensagem)
        {
            try
            {
                var diretorioRaiz = AppDomain.CurrentDomain.BaseDirectory;
                string diretorio = Path.Combine(diretorioRaiz, "Teste_Pratico");
                string path = Path.Combine(diretorio, "LogErros.txt");

                Directory.CreateDirectory(diretorio);

                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine($"Data: {DateTime.Now}\n" +
                                   $"Mensagem: {_mensagem}\n" +
                                   "-------------------------------------------\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar log de erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}