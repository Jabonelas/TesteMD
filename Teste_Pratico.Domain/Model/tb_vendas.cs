namespace Teste_Pratico.Domain.Model
{
    public class tb_vendas
    {
        public int ve_id { get; set; }
        public int ve_quantidade { get; set; }
        public int fk_tb_cliente { get; set; }
        public int fk_tb_produto { get; set; }

        public string pd_nome { get; set; }
        public string cl_nome { get; set; }

        public tb_vendas()
        {
        }

        public tb_vendas(int _ve_quantidade, int _fk_tb_cliente, int _fk_tb_produto)
        {
            ve_quantidade = _ve_quantidade;
            fk_tb_cliente = _fk_tb_cliente;
            fk_tb_produto = _fk_tb_produto;
        }
    }
}