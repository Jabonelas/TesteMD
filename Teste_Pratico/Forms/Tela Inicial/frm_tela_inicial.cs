using System;
using System.Windows.Forms;
using Teste_Pratico.Forms.Cliente;
using Teste_Pratico.Forms.Cliente.Relatorio;
using Teste_Pratico.Forms.Produto;
using Teste_Pratico.Forms.Produto.Relatorio;
using Teste_Pratico.Forms.Venda;
using Teste_Pratico.Forms.Venda.Relatorio;

namespace Teste_Pratico
{
    public partial class frm_tela_inicial : Form
    {
        public frm_tela_inicial()
        {
            InitializeComponent();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_cliente cliente = new frm_cliente();
            cliente.ShowDialog();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_produto produto = new frm_produto();
            produto.ShowDialog();
        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_venda venda = new frm_venda();
            venda.ShowDialog();
        }

        #region Relatorio

        private void btnRelatorioCliente_Click(object sender, EventArgs e)
        {
            frm_relatorio_cliente relatorioCliente = new frm_relatorio_cliente();
            relatorioCliente.ShowDialog();
        }

        private void btnRelatorioProduto_Click_1(object sender, EventArgs e)
        {
            frm_relatorio_produto relatorioProduto = new frm_relatorio_produto();
            relatorioProduto.ShowDialog();
        }

        private void btnRelatorioVenda_Click(object sender, EventArgs e)
        {
            frm_relatorio_venda relatorioVenda = new frm_relatorio_venda();
            relatorioVenda.ShowDialog();
        }

        #endregion Relatorio
    }
}