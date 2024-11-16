using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Cliente
{
    public partial class frm_cliente : Form
    {
        private DataTable dt = new DataTable();

        public frm_cliente()
        {
            InitializeComponent();
            ConfigurarColunasGridView();
            AtualizarTabelaClientes();
        }

        /// <summary>
        /// Configuração das colunas do DataGridView, para exibir os dados dos clientes.
        /// </summary>
        private void ConfigurarColunasGridView()
        {
            gdvClientes.AutoGenerateColumns = false;
            gdvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gdvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            gdvClientes.Columns.Clear();

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "id_cliente"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nome Cliente",
                DataPropertyName = "cl_nome"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "C.P.F",
                DataPropertyName = "cl_cpf"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "C.E.P",
                DataPropertyName = "cl_cep"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Endereço",
                DataPropertyName = "cl_endereco"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Bairro",
                DataPropertyName = "cl_bairro"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cidade",
                DataPropertyName = "cl_cidade"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "cl_estado"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Telefone",
                DataPropertyName = "cl_telefone"
            });

            gdvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Email",
                DataPropertyName = "cl_email"
            });

            gdvClientes.Columns[0].Width = 40;//coluna de ID
            gdvClientes.Columns[2].Width = 90;//coluna do cpf
            gdvClientes.Columns[3].Width = 70;//coluna do cep

            gdvClientes.Columns[0].Visible = false;//coluna de ID
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="ClienteService"/>, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="ClienteService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private ClienteService AcessoClienteService()
        {
            var clienteRepository = new ClienteRepository();
            var clienteService = new ClienteService(clienteRepository);
            return clienteService;
        }

        /// <summary>
        /// Inserindo lista dos ultimos 20 clientes adiconados no DataGridView
        /// </summary>
        private void AtualizarTabelaClientes()
        {
            List<tb_clientes> clienteDados = AcessoClienteService().BuscarClientesTop20();
            gdvClientes.DataSource = clienteDados;
        }

        private void btnBuscarClienteNome_Click(object sender, EventArgs e)
        {
            BuscarClientePorNome();
        }

        /// <summary>
        /// Realiza a busca do cliente pelo nome, e exibe no DataGridView
        /// </summary>
        private void BuscarClientePorNome()
        {
            if (string.IsNullOrEmpty(txtBuscaClienteNome.Text) == true)
            {
                MessageBox.Show("Preencha o campo Nome Cliente para realizar a busca do cliente.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            string nomeCliente = txtBuscaClienteNome.Text;

            var clienteDados = AcessoClienteService().BuscarClientePorNome(nomeCliente);

            gdvClientes.DataSource = clienteDados;
        }

        private void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            frm_cadastar_editar_cliente cadastrarCliente = new frm_cadastar_editar_cliente();
            cadastrarCliente.ShowDialog();

            AtualizarTabelaClientes();
        }

        private void btnEditarCliente_Click(object sender, EventArgs e)
        {
            EditarCliente();
        }

        /// <summary>
        /// Realiza a edição do cliente selecionado no DataGridView,
        /// </summary>
        private void EditarCliente()
        {
            try
            {
                if (IsExistemLinhasNoGrid() == true)
                {
                    var idClienteSelecionado = gdvClientes.SelectedRows[0].Cells[0].Value;
                    int idCliente = Convert.ToInt32(idClienteSelecionado);

                    var clienteDados = AcessoClienteService().BuscarClientePorId(idCliente);

                    frm_cadastar_editar_cliente editarCliente = new frm_cadastar_editar_cliente(clienteDados);
                    editarCliente.ShowDialog();

                    AtualizarTabelaClientes();
                }
                else
                {
                    MessageBox.Show("Selecione um cliente para editar.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao solicitar edição do cadastro do cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletarCliente_Click(object sender, EventArgs e)
        {
            if (IsExisteClienteVinculadoVenda() == true)
            {
                MessageBox.Show($"Não é possível excluir o cadastro deste cliente, pois ele está vinculado a algumas vendas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            var result = MessageBox.Show("Deseja realmente deletar o cliente selecionado?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeletarClienteSelecionado();
            }
        }

        /// <summary>
        /// Verifica se o cliente selecionado no DataGridView está vinculado a alguma venda,
        /// impedindo a exclusão de clientes que tenham realizado compras.
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se existe vinculado a alguma venda realizada.</returns>
        private bool IsExisteClienteVinculadoVenda()
        {
            try
            {
                var idClienteSelecionado = gdvClientes.SelectedRows[0].Cells[0].Value;
                int idCliente = Convert.ToInt32(idClienteSelecionado);

                bool IsExisteClienteVinculadoVenda = AcessoVendaService().VerificarExistenteClienteVenda(idCliente);

                return IsExisteClienteVinculadoVenda;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao solicitar verificação de vinculo de cliente com venda : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Acesso ao serviço de <see cref="VendaService"/>, para realizar as operações de CRUD
        /// </summary>
        /// <returns>Retorna o acesso ao <see cref="VendaService"/>, permitindo a utilização dos métodos que interagem com o banco de dados.</returns>
        private VendaService AcessoVendaService()
        {
            var vendaRepository = new VendaRepository();
            var vendaService = new VendaService(vendaRepository);

            return vendaService;
        }

        /// <summary>
        /// Veirifaca se existe linhas no DataGridView, para saber se tem algum cliente selecionado.
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se já existe algum cliente selecionado</returns>
        private bool IsExistemLinhasNoGrid()
        {
            return gdvClientes.SelectedRows.Count > 0;
        }

        /// <summary>
        /// Realiza a exclusão do cliente selecionado no DataGridView
        /// </summary>
        private void DeletarClienteSelecionado()
        {
            try
            {
                if (IsExistemLinhasNoGrid() == true)
                {
                    var idClienteSelecionado = gdvClientes.SelectedRows[0].Cells[0].Value;
                    int idCliente = Convert.ToInt32(idClienteSelecionado);

                    AcessoClienteService().DeletarCliente(idCliente);

                    AtualizarTabelaClientes();
                }
                else
                {
                    MessageBox.Show("Selecione um cliente para editar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao solicitar exclusão de cliente : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}