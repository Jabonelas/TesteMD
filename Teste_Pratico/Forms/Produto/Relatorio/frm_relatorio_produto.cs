using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Produto.Relatorio
{
    public partial class frm_relatorio_produto : Form
    {
        public frm_relatorio_produto()
        {
            InitializeComponent();
        }

        private void frm_relatorio_produto_Load(object sender, EventArgs e)
        {
            List<tb_produtos> listaProdutos = AcessoProdutoService().BuscarTodosProdutos();

            PreencherDataSourceRelatorio(listaProdutos);

            PreencherParametrosRelatorio();

            this.reportViewer.RefreshReport();
        }

        /// <summary>
        /// Preencher o DataSource do relatório com a lista completa de <see cref="tb_produtos"/>.
        /// </summary>
        /// <param name="_listaProdutos">Objeto contendo os dados dos <see cref="tb_produtos"/> a ser inserido no relatorio.</param>
        private void PreencherDataSourceRelatorio(List<tb_produtos> _listaProdutos)
        {
            var datasou = new Microsoft.Reporting.WinForms.ReportDataSource("dsProduto", _listaProdutos);
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(datasou);
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="ProdutoService"/>, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="ProdutoService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private ProdutoService AcessoProdutoService()
        {
            var produtoRepository = new ProdutoRepository();
            var produtoService = new ProdutoService(produtoRepository);
            return produtoService;
        }

        /// <summary>
        /// Preencher os parametros informados dentro do relatório
        /// </summary>
        private void PreencherParametrosRelatorio()
        {
            this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Empresa", "Empresa: Teste Prático - Desenvolvedor C#"));
            this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Data", DateTime.Now.ToString()));
        }
    }
}