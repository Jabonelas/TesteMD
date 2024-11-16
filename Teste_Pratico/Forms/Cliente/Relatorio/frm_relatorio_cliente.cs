using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Cliente.Relatorio
{
    public partial class frm_relatorio_cliente : Form
    {
        public frm_relatorio_cliente()
        {
            InitializeComponent();
        }

        private void frm_relatorio_cliente_Load(object sender, EventArgs e)
        {
            List<tb_clientes> listaClientes = AcessoClienteService().BuscarTodosClientes();

            PreencherDataSourceRelatorio(listaClientes);

            PreencherParametrosRelatorio();

            this.reportViewer.RefreshReport();
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="ClienteService"/>, para realizar as operações de CRUD.
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="ClienteService"/> , permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private ClienteService AcessoClienteService()
        {
            var clienteRepository = new ClienteRepository();

            var clienteService = new ClienteService(clienteRepository);

            return clienteService;
        }

        /// <summary>
        /// Preencher o DataSource do relatório com a lista completa de clientes.
        /// </summary>
        /// <param name="_listaClientes">Objeto contendo os dados dos <see cref="tb_clientes"/> a ser inserido no relatorio.</param>
        private void PreencherDataSourceRelatorio(List<tb_clientes> _listaClientes)
        {
            var datasou = new Microsoft.Reporting.WinForms.ReportDataSource("dsCliente", _listaClientes);
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(datasou);
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