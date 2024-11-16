namespace Teste_Pratico.Domain.Model
{
    public class tb_produtos
    {
        public int id_produto { get; set; }
        public string pd_nome { get; set; }
        public string pd_descricao { get; set; }
        public decimal pd_preco { get; set; }
        public int pd_estoque { get; set; }
        public bool pd_ativo { get; set; }

        public tb_produtos()
        {
        }
    }
}