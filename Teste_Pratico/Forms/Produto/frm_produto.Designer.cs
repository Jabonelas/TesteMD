namespace Teste_Pratico.Forms.Produto
{
    partial class frm_produto
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeletarProduto = new System.Windows.Forms.Button();
            this.btnEditarProduto = new System.Windows.Forms.Button();
            this.btnCadastrarProduto = new System.Windows.Forms.Button();
            this.btnBuscarProdutoNome = new System.Windows.Forms.Button();
            this.txtBuscaProdutoNome = new System.Windows.Forms.TextBox();
            this.gdvProdutos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gdvProdutos)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Nome Produto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Os 20 cadastros de clientes mais recentes";
            // 
            // btnDeletarProduto
            // 
            this.btnDeletarProduto.Location = new System.Drawing.Point(174, 31);
            this.btnDeletarProduto.Name = "btnDeletarProduto";
            this.btnDeletarProduto.Size = new System.Drawing.Size(75, 23);
            this.btnDeletarProduto.TabIndex = 6;
            this.btnDeletarProduto.Text = "Deletar";
            this.btnDeletarProduto.UseVisualStyleBackColor = true;
            this.btnDeletarProduto.Click += new System.EventHandler(this.btnDeletarProduto_Click);
            // 
            // btnEditarProduto
            // 
            this.btnEditarProduto.Location = new System.Drawing.Point(93, 31);
            this.btnEditarProduto.Name = "btnEditarProduto";
            this.btnEditarProduto.Size = new System.Drawing.Size(75, 23);
            this.btnEditarProduto.TabIndex = 5;
            this.btnEditarProduto.Text = "Editar";
            this.btnEditarProduto.UseVisualStyleBackColor = true;
            this.btnEditarProduto.Click += new System.EventHandler(this.btnEditarProduto_Click);
            // 
            // btnCadastrarProduto
            // 
            this.btnCadastrarProduto.Location = new System.Drawing.Point(12, 31);
            this.btnCadastrarProduto.Name = "btnCadastrarProduto";
            this.btnCadastrarProduto.Size = new System.Drawing.Size(75, 23);
            this.btnCadastrarProduto.TabIndex = 4;
            this.btnCadastrarProduto.Text = "Cadastrar";
            this.btnCadastrarProduto.UseVisualStyleBackColor = true;
            this.btnCadastrarProduto.Click += new System.EventHandler(this.btnCadastrarProduto_Click);
            // 
            // btnBuscarProdutoNome
            // 
            this.btnBuscarProdutoNome.Location = new System.Drawing.Point(382, 94);
            this.btnBuscarProdutoNome.Name = "btnBuscarProdutoNome";
            this.btnBuscarProdutoNome.Size = new System.Drawing.Size(75, 20);
            this.btnBuscarProdutoNome.TabIndex = 3;
            this.btnBuscarProdutoNome.Text = "Buscar";
            this.btnBuscarProdutoNome.UseVisualStyleBackColor = true;
            this.btnBuscarProdutoNome.Click += new System.EventHandler(this.btnBuscarProdutoNome_Click);
            // 
            // txtBuscaProdutoNome
            // 
            this.txtBuscaProdutoNome.Location = new System.Drawing.Point(12, 94);
            this.txtBuscaProdutoNome.MaxLength = 200;
            this.txtBuscaProdutoNome.Name = "txtBuscaProdutoNome";
            this.txtBuscaProdutoNome.Size = new System.Drawing.Size(364, 20);
            this.txtBuscaProdutoNome.TabIndex = 2;
            // 
            // gdvProdutos
            // 
            this.gdvProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvProdutos.Location = new System.Drawing.Point(12, 147);
            this.gdvProdutos.Name = "gdvProdutos";
            this.gdvProdutos.ReadOnly = true;
            this.gdvProdutos.Size = new System.Drawing.Size(974, 338);
            this.gdvProdutos.TabIndex = 8;
            // 
            // frm_produto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 524);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeletarProduto);
            this.Controls.Add(this.btnEditarProduto);
            this.Controls.Add(this.btnCadastrarProduto);
            this.Controls.Add(this.btnBuscarProdutoNome);
            this.Controls.Add(this.txtBuscaProdutoNome);
            this.Controls.Add(this.gdvProdutos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_produto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Produto";
            ((System.ComponentModel.ISupportInitialize)(this.gdvProdutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeletarProduto;
        private System.Windows.Forms.Button btnEditarProduto;
        private System.Windows.Forms.Button btnCadastrarProduto;
        private System.Windows.Forms.Button btnBuscarProdutoNome;
        private System.Windows.Forms.TextBox txtBuscaProdutoNome;
        private System.Windows.Forms.DataGridView gdvProdutos;
    }
}