namespace Teste_Pratico.Forms.Cliente
{
    partial class frm_cliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gdvClientes = new System.Windows.Forms.DataGridView();
            this.txtBuscaClienteNome = new System.Windows.Forms.TextBox();
            this.btnBuscarClienteNome = new System.Windows.Forms.Button();
            this.btnCadastrarCliente = new System.Windows.Forms.Button();
            this.btnEditarCliente = new System.Windows.Forms.Button();
            this.btnDeletarCliente = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gdvClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // gdvClientes
            // 
            this.gdvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvClientes.Location = new System.Drawing.Point(12, 147);
            this.gdvClientes.Name = "gdvClientes";
            this.gdvClientes.ReadOnly = true;
            this.gdvClientes.Size = new System.Drawing.Size(974, 338);
            this.gdvClientes.TabIndex = 0;
            // 
            // txtBuscaClienteNome
            // 
            this.txtBuscaClienteNome.Location = new System.Drawing.Point(12, 94);
            this.txtBuscaClienteNome.MaxLength = 200;
            this.txtBuscaClienteNome.Name = "txtBuscaClienteNome";
            this.txtBuscaClienteNome.Size = new System.Drawing.Size(364, 20);
            this.txtBuscaClienteNome.TabIndex = 2;
            // 
            // btnBuscarClienteNome
            // 
            this.btnBuscarClienteNome.Location = new System.Drawing.Point(382, 94);
            this.btnBuscarClienteNome.Name = "btnBuscarClienteNome";
            this.btnBuscarClienteNome.Size = new System.Drawing.Size(75, 20);
            this.btnBuscarClienteNome.TabIndex = 3;
            this.btnBuscarClienteNome.Text = "Buscar";
            this.btnBuscarClienteNome.UseVisualStyleBackColor = true;
            this.btnBuscarClienteNome.Click += new System.EventHandler(this.btnBuscarClienteNome_Click);
            // 
            // btnCadastrarCliente
            // 
            this.btnCadastrarCliente.Location = new System.Drawing.Point(12, 31);
            this.btnCadastrarCliente.Name = "btnCadastrarCliente";
            this.btnCadastrarCliente.Size = new System.Drawing.Size(75, 23);
            this.btnCadastrarCliente.TabIndex = 4;
            this.btnCadastrarCliente.Text = "Cadastrar";
            this.btnCadastrarCliente.UseVisualStyleBackColor = true;
            this.btnCadastrarCliente.Click += new System.EventHandler(this.btnCadastrarCliente_Click);
            // 
            // btnEditarCliente
            // 
            this.btnEditarCliente.Location = new System.Drawing.Point(93, 31);
            this.btnEditarCliente.Name = "btnEditarCliente";
            this.btnEditarCliente.Size = new System.Drawing.Size(75, 23);
            this.btnEditarCliente.TabIndex = 5;
            this.btnEditarCliente.Text = "Editar";
            this.btnEditarCliente.UseVisualStyleBackColor = true;
            this.btnEditarCliente.Click += new System.EventHandler(this.btnEditarCliente_Click);
            // 
            // btnDeletarCliente
            // 
            this.btnDeletarCliente.Location = new System.Drawing.Point(174, 31);
            this.btnDeletarCliente.Name = "btnDeletarCliente";
            this.btnDeletarCliente.Size = new System.Drawing.Size(75, 23);
            this.btnDeletarCliente.TabIndex = 6;
            this.btnDeletarCliente.Text = "Deletar";
            this.btnDeletarCliente.UseVisualStyleBackColor = true;
            this.btnDeletarCliente.Click += new System.EventHandler(this.btnDeletarCliente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Os 20 cadastros de clientes mais recentes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nome Cliente";
            // 
            // frm_cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 524);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeletarCliente);
            this.Controls.Add(this.btnEditarCliente);
            this.Controls.Add(this.btnCadastrarCliente);
            this.Controls.Add(this.btnBuscarClienteNome);
            this.Controls.Add(this.txtBuscaClienteNome);
            this.Controls.Add(this.gdvClientes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente";
            ((System.ComponentModel.ISupportInitialize)(this.gdvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gdvClientes;
        private System.Windows.Forms.TextBox txtBuscaClienteNome;
        private System.Windows.Forms.Button btnBuscarClienteNome;
        private System.Windows.Forms.Button btnCadastrarCliente;
        private System.Windows.Forms.Button btnEditarCliente;
        private System.Windows.Forms.Button btnDeletarCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}