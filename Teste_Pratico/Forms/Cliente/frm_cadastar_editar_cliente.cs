using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Teste_Pratico.Domain.Model;
using Teste_Pratico.Infra.API;
using Teste_Pratico.Infra.Repositorio;
using Teste_Pratico.Infra.Service;

namespace Teste_Pratico.Forms.Cliente
{
    public partial class frm_cadastar_editar_cliente : Form
    {
        private int idCliente = 0;

        /// <summary>
        /// Construtor para cadastrar cliente
        /// </summary>
        public frm_cadastar_editar_cliente()
        {
            InitializeComponent();

            FormatandoTelaCadastroCliente();
        }

        /// <summary>
        /// Construtor para editar cliente
        /// </summary>
        /// <param name="_cliente">Objeto contendo os dados do <see cref="tb_clientes"/> que foi selecionado na tela anterior.</param>

        public frm_cadastar_editar_cliente(tb_clientes _cliente)
        {
            InitializeComponent();

            idCliente = _cliente.id_cliente;

            PreencherCamposDadosCliente(_cliente);

            FormatandoTelaEditarCliente();
        }

        /// <summary>
        /// Formatando a tela para cadastrar cliente, alterando o texto do formulário e do botão
        /// </summary>
        private void FormatandoTelaCadastroCliente()
        {
            this.Text = "Cadastrar Cliente";

            btnSalvar.Text = "Cadastrar Cliente";
        }

        /// <summary>
        /// Formatando a tela para editar cliente, alterando o texto do formulário e do botão
        /// </summary>
        private void FormatandoTelaEditarCliente()
        {
            this.Text = "Editar Cliente";

            btnSalvar.Text = "Editar Cliente";
        }

        /// <summary>
        /// Pegando os dados do <see cref="tb_clientes"/> que foi selecionado para edição e preenchendo os campos do formulário
        /// </summary>
        /// <param name="_cliente">Objeto contendo os dados do <see cref="tb_clientes"/>.</param>
        private void PreencherCamposDadosCliente(tb_clientes _cliente)
        {
            txtNomeCliente.Text = _cliente.cl_nome;
            txtCPF.Text = _cliente.cl_cpf;
            txtCEP.Text = _cliente.cl_cep;
            txtEnderecoCliente.Text = _cliente.cl_endereco;
            txtBairro.Text = _cliente.cl_bairro;
            txtCidade.Text = _cliente.cl_cidade;
            cmbEstado.Text = _cliente.cl_estado;
            txtTelefoneCliente.Text = _cliente.cl_telefone;
            txtEmailCliente.Text = _cliente.cl_email;
        }

        /// <summary>
        /// Realiza a verificação se os campos estão preenchidos para cadastrar ou editar um cliente
        /// </summary>
        /// <returns>Retorna um valor <see cref="bool"/> indicando se todos os campos foram preenchidos.</returns>
        private bool IsCamposDadosClientePreenchidos()
        {
            if (string.IsNullOrEmpty(txtNomeCliente.Text) ||
                string.IsNullOrEmpty(txtCPF.Text) ||
                string.IsNullOrEmpty(txtCEP.Text) ||
                string.IsNullOrEmpty(txtEnderecoCliente.Text) ||
                string.IsNullOrEmpty(txtBairro.Text) ||
                string.IsNullOrEmpty(txtCidade.Text) ||
                string.IsNullOrEmpty(cmbEstado.Text) ||
                string.IsNullOrEmpty(txtTelefoneCliente.Text) ||
                string.IsNullOrEmpty(txtEmailCliente.Text))
            {
                return false;
            }

            return true;
        }

        private void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            //Verifica se o botão é para cadastrar
            if (this.Text == "Cadastrar Cliente")
            {
                AdicionarNovoCadastroCliente();
            }
            //Se não é para cadastrar é para editar
            else
            {
                EditarCadastroCliente();
            }
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
        /// Realiza a interação com o serviço de cliente para editar um cadastro de cliente
        /// </summary>
        private void EditarCadastroCliente()
        {
            if (IsCamposDadosClientePreenchidos() == true)
            {
                var dadosCliente = PegarDadosDigitadosPeloCliente();

                AcessoClienteService().AtualizarCliente(dadosCliente);

                this.Close();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos para editar o cadastro do cliente.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Realiza a interação com o serviço de cliente para adicionar um novo cadastro de cliente
        /// </summary>
        private void AdicionarNovoCadastroCliente()
        {
            if (IsCamposDadosClientePreenchidos() == true)
            {
                var dadosCliente = PegarDadosDigitadosPeloCliente();

                var isClienteCadastrado = AcessoClienteService().InserirCliente(dadosCliente);

                if (isClienteCadastrado == true)
                {
                    LimparCampos();
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos para cadastrar um cliente.", "Atenção", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Pega os dados digitados pelo cliente e retorna um objeto <see cref="tb_clientes"/> com os dados,
        /// para serem inseridos no banco de dados
        /// </summary>
        /// <returns>Retorno do dados do <see cref="tb_clientes"/> que foram digitados pelo usuario</returns>
        private tb_clientes PegarDadosDigitadosPeloCliente()
        {
            tb_clientes cliente = new tb_clientes();

            cliente.id_cliente = idCliente;
            cliente.cl_nome = txtNomeCliente.Text;
            cliente.cl_cpf = txtCPF.Text;
            cliente.cl_cep = txtCEP.Text;
            cliente.cl_endereco = txtEnderecoCliente.Text;
            cliente.cl_bairro = txtBairro.Text;
            cliente.cl_cidade = txtCidade.Text;
            cliente.cl_estado = cmbEstado.Text;
            cliente.cl_telefone = txtTelefoneCliente.Text;
            cliente.cl_email = txtEmailCliente.Text;
            cliente.cl_ativo = true;

            return cliente;
        }

        /// <summary>
        /// Realiza a limpeza dos campos do formulário, após o cadastro do cliente
        /// </summary>
        private void LimparCampos()
        {
            txtNomeCliente.Text = string.Empty;
            txtCPF.Text = string.Empty;
            txtCEP.Text = string.Empty;
            txtEnderecoCliente.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtTelefoneCliente.Text = string.Empty;
            txtEmailCliente.Text = string.Empty;
            cmbEstado.SelectedIndex = -1;
        }

        private async void btnBuscarCEP_Click(object sender, EventArgs e)
        {
            btnBuscarCEP.Enabled = false;
            await PreencherDadosEndereco();
            btnBuscarCEP.Enabled = true;
        }

        /// <summary>
        /// Preenche automaticamente os campos de endereço do cliente com os dados retornados pela API dos Correios.
        /// Caso o CEP não seja encontrado, exibe uma mensagem ao usuário perguntando se deseja continuar.
        /// Se o usuário optar por prosseguir, os campos podem ser preenchidos manualmente.
        /// </summary>
        private async Task PreencherDadosEndereco()
        {
            try
            {
                API_Correios apiCorreios = new API_Correios();

                //Tratando o CEP para enviar a requisição
                string cepValido = txtCEP.Text.Replace("-", "").Replace(".", "");

                //Pegando os dados que retornam da requisição
                var dadosCep = await apiCorreios.APICorreios(cepValido);

                txtEnderecoCliente.Text = dadosCep.logradouro;
                txtBairro.Text = dadosCep.bairro;
                txtCidade.Text = dadosCep.localidade;
                cmbEstado.Text = dadosCep.estado;

                //Se o CEP não for encontrado
                if (dadosCep.logradouro == null && dadosCep.bairro == null && dadosCep.localidade == null)
                {
                    var dialog = MessageBox.Show("C.E.P não encontrado deseja prosseguir?", "Atenção",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialog == DialogResult.Yes)
                    {
                        txtEnderecoCliente.Focus();
                    }
                    else
                    {
                        txtCEP.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar api correios: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnBuscarCEP.Enabled = true;
            }
        }

        private void txtCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatarTextBoxCEP(e);
        }

        /// <summary>
        /// Habilitando formatação do campo CEP
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void FormatarTextBoxCEP(KeyPressEventArgs _event)
        {
            ImpedirDigitarLetras(_event);

            FormatandoMascaraCEP(_event);
        }

        /// <summary>
        /// Faz a formatação do textbox do campo CEP, deixa o usuário digitar apenas números e coloca a mascara no campo formatando como CEP
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void FormatandoMascaraCEP(KeyPressEventArgs _event)
        {
            if (char.IsNumber(_event.KeyChar) == true)
            {
                switch (txtCEP.TextLength)
                {
                    case 0:
                        txtCEP.Text = "";
                        break;

                    case 5:
                        txtCEP.Text = txtCEP.Text + "-";
                        txtCEP.SelectionStart = 6;
                        break;
                }
            }
        }

        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatarTextBoxCPF(e);
        }

        /// <summary>
        /// Habilitando formatação do campo CPF
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void FormatarTextBoxCPF(KeyPressEventArgs _event)
        {
            ImpedirDigitarLetras(_event);

            FormatandoMascaraCPF(_event);
        }

        /// <summary>
        /// Faz a formatação do textbox do campo CPF, deixa o usuário digitar apenas números e coloca a mascara no campo formatando como CPF
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void FormatandoMascaraCPF(KeyPressEventArgs _event)
        {
            //Preencher o campo CPF com a mascara
            if (char.IsNumber(_event.KeyChar) == true)
            {
                switch (txtCPF.TextLength)
                {
                    case 0:
                        txtCPF.Text = "";
                        break;

                    case 3:
                        txtCPF.Text = txtCPF.Text + ".";
                        txtCPF.SelectionStart = 4;
                        break;

                    case 7:
                        txtCPF.Text = txtCPF.Text + ".";
                        txtCPF.SelectionStart = 8;
                        break;

                    case 11:
                        txtCPF.Text = txtCPF.Text + "-";
                        txtCPF.SelectionStart = 12;
                        break;
                }
            }
        }

        /// <summary>
        /// Faz a conversão do textbox para aceitar apenas números
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void ImpedirDigitarLetras(KeyPressEventArgs _event)
        {
            if (!Char.IsDigit(_event.KeyChar) && _event.KeyChar != (char)8)
            {
                _event.Handled = true;
            }
        }

        private void txtEmailCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatarTextBoxEmail(e);
        }

        /// <summary>
        /// Faz a formatação do campo de email, deixa o usuário digitar apenas letras, números e caracteres especiais permitidos
        /// </summary>
        /// <param name="_event">Tecla que está sendo pressionada</param>
        private void FormatarTextBoxEmail(KeyPressEventArgs _event)
        {
            if (!Char.IsLetter(_event.KeyChar) && _event.KeyChar != (char)8 && _event.KeyChar != (char)64
                && !Char.IsDigit(_event.KeyChar) && _event.KeyChar != (char)45 && _event.KeyChar != (char)46
                && _event.KeyChar != (char)95 && _event.KeyChar != (char)47)
            {
                _event.Handled = true;
            }
        }

        private void txtTelefoneCliente_TextChanged(object sender, EventArgs e)
        {
            string telefoneApenasNumeros = RemoverCaracteres(txtTelefoneCliente.Text);

            FormatandoMascaraTelefone(telefoneApenasNumeros);
        }

        /// <summary>
        /// Faz a formatação do textbox do campo telefone, coloca a mascara no campo formatando como telefone ou celular
        /// </summary>
        /// <param name="_numeroTelefone">Numero do telefone a ser formatado</param>
        private void FormatandoMascaraTelefone(string _numeroTelefone)
        {
            if (_numeroTelefone.Length <= 10)
            {
                // Telefone fixo: (XX) XXXX-XXXX
                if (_numeroTelefone.Length <= 2)
                    txtTelefoneCliente.Text = "(" + _numeroTelefone;
                else if (_numeroTelefone.Length <= 6)
                    txtTelefoneCliente.Text = "(" + _numeroTelefone.Substring(0, 2) + ") " + _numeroTelefone.Substring(2);
                else
                    txtTelefoneCliente.Text = "(" + _numeroTelefone.Substring(0, 2) + ") " + _numeroTelefone.Substring(2, 4) + "-" + _numeroTelefone.Substring(6);
            }
            else
            {
                // Celular: (XX) XXXXX-XXXX
                txtTelefoneCliente.Text = "(" + _numeroTelefone.Substring(0, 2) + ") " + _numeroTelefone.Substring(2, 5) + "-" + _numeroTelefone.Substring(7);
            }

            txtTelefoneCliente.SelectionStart = txtTelefoneCliente.Text.Length;
        }

        /// <summary>
        /// Removendo os caracteres do numero do telefone deixando apenas o numero
        /// </summary>
        /// <param name="_telefoneComCaracteres">Numero de telefone com formatacao incluindo caracteres</param>
        /// <returns>Retorna apenas os números do telefone, excluindo os caracteres especiais.</returns>
        private string RemoverCaracteres(string _telefoneComCaracteres)
        {
            string telefonepenasNumeros = _telefoneComCaracteres.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            return telefonepenasNumeros;
        }

        private void txtTelefoneCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            ImpedirDigitarLetras(e);
        }
    }
}