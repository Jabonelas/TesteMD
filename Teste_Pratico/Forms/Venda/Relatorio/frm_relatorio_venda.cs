using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Venda.Relatorio
{
    public partial class frm_relatorio_venda : Form
    {
        public frm_relatorio_venda()
        {
            InitializeComponent();
        }

        private void frm_relatorio_venda_Load(object sender, EventArgs e)
        {
            List<tb_vendas> listaVendas = AcessandoVendaService().BuscarVendas();

            PreencherDataSourceRelatorio(listaVendas);

            PreencherParametrosRelatorio();

            this.reportViewer.RefreshReport();
        }

        /// <summary>
        /// Preencher o DataSource do relatório com a lista completa de <see cref="tb_vendas"/>.
        /// </summary>
        /// <param name="_listaVendas">Objeto contendo os dados dos <see cref="tb_vendas"/> a ser inserido no relatorio.</param>
        private void PreencherDataSourceRelatorio(List<tb_vendas> _listaVendas)
        {
            var datasou = new Microsoft.Reporting.WinForms.ReportDataSource("dsVenda", _listaVendas);
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(datasou);
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="VendaService"/>, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="VendaService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private VendaService AcessandoVendaService()
        {
            var vendaRepository = new VendaRepository();
            var vendaService = new VendaService(vendaRepository);

            return vendaService;
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