namespace Teste_Pratico.Domain.Model
{
    public class tb_clientes
    {
        public int id_cliente { get; set; }
        public string cl_nome { get; set; }
        public string cl_cpf { get; set; }
        public string cl_cep { get; set; }
        public string cl_endereco { get; set; }
        public string cl_bairro { get; set; }
        public string cl_cidade { get; set; }
        public string cl_estado { get; set; }
        public string cl_telefone { get; set; }
        public string cl_email { get; set; }
        public bool cl_ativo { get; set; }

        public tb_clientes()
        {
        }
    }
}