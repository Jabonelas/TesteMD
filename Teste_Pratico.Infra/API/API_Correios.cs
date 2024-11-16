using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Teste_Pratico.Infra.API
{
    public class API_Correios
    {
        public DadosCep temperatures = new DadosCep();

        /// <summary>
        /// Realiza uma requisição para a API dos Correios para obter os dados de um CEP.
        /// Retorna um objeto <see cref="DadosCep"/> com as informações do endereço associado ao CEP informado.
        /// </summary>
        /// <param name="_cep">CEP para consulta na API dos Correios.</param>
        /// <returns>Um objeto <see cref="DadosCep"/> contendo os dados do endereço do CEP.</returns>
        public async Task<DadosCep> APICorreios(string _cep)
        {
            HttpClient cliente = new HttpClient { BaseAddress = new Uri($"https://viacep.com.br/ws/{_cep}/json/") };
            var response = await cliente.GetAsync(string.Empty);
            var content = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<DadosCep>(content);

            temperatures = users;

            return temperatures;
        }
    }
}