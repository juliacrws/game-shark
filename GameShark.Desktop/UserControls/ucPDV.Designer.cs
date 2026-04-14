namespace GameShark.Desktop.UserControls
{
    partial class ucPDV
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtBuscaProduto;
        private System.Windows.Forms.ListBox lstCatalogo;
        private System.Windows.Forms.Label lblSeparador;
        private System.Windows.Forms.ListBox lstCarrinho;
        private System.Windows.Forms.Label lblTotalTexto;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.Button btnFinalizar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtBuscaProduto = new System.Windows.Forms.TextBox();
            this.lstCatalogo = new System.Windows.Forms.ListBox();
            this.lblSeparador = new System.Windows.Forms.Label();
            this.lstCarrinho = new System.Windows.Forms.ListBox();
            this.lblTotalTexto = new System.Windows.Forms.Label();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // LADO ESQUERDO: CATÁLOGO
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(30, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(185, 45);
            this.lblTitulo.Text = "CATÁLOGO";

            this.txtBuscaProduto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.txtBuscaProduto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscaProduto.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txtBuscaProduto.ForeColor = System.Drawing.Color.White;
            this.txtBuscaProduto.Location = new System.Drawing.Point(35, 90);
            this.txtBuscaProduto.Name = "txtBuscaProduto";
            this.txtBuscaProduto.PlaceholderText = "🔍 Digite o nome para filtrar...";
            this.txtBuscaProduto.Size = new System.Drawing.Size(350, 36);

            this.lstCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(24)))));
            this.lstCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCatalogo.Font = new System.Drawing.Font("Consolas", 12F);
            this.lstCatalogo.ForeColor = System.Drawing.Color.LightGray;
            this.lstCatalogo.FormattingEnabled = true;
            this.lstCatalogo.ItemHeight = 19;
            this.lstCatalogo.Location = new System.Drawing.Point(35, 140);
            this.lstCatalogo.Name = "lstCatalogo";
            this.lstCatalogo.Size = new System.Drawing.Size(350, 418);

            // SEPARADOR VISUAL
            this.lblSeparador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.lblSeparador.Location = new System.Drawing.Point(405, 30);
            this.lblSeparador.Name = "lblSeparador";
            this.lblSeparador.Size = new System.Drawing.Size(2, 530);

            // LADO DIREITO: CARRINHO E TOTAL
            this.lstCarrinho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(24)))));
            this.lstCarrinho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCarrinho.Font = new System.Drawing.Font("Consolas", 14F);
            this.lstCarrinho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(243)))), ((int)(((byte)(255))))); // Ciano
            this.lstCarrinho.FormattingEnabled = true;
            this.lstCarrinho.ItemHeight = 22;
            this.lstCarrinho.Location = new System.Drawing.Point(430, 30);
            this.lstCarrinho.Name = "lstCarrinho";
            this.lstCarrinho.Size = new System.Drawing.Size(350, 352);

            this.lblTotalTexto.AutoSize = true;
            this.lblTotalTexto.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblTotalTexto.ForeColor = System.Drawing.Color.LightGray;
            this.lblTotalTexto.Location = new System.Drawing.Point(425, 410);
            this.lblTotalTexto.Name = "lblTotalTexto";
            this.lblTotalTexto.Size = new System.Drawing.Size(103, 30);
            this.lblTotalTexto.Text = "TOTAL R$";

            this.lblTotalValor.AutoSize = true;
            this.lblTotalValor.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))); // Verde Neon
            this.lblTotalValor.Location = new System.Drawing.Point(525, 395);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Size = new System.Drawing.Size(91, 47);
            this.lblTotalValor.Text = "0,00";

            this.btnFinalizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnFinalizar.FlatAppearance.BorderSize = 0;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.ForeColor = System.Drawing.Color.Black;
            this.btnFinalizar.Location = new System.Drawing.Point(430, 480);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(350, 50);
            this.btnFinalizar.Text = "FINALIZAR VENDA";
            this.btnFinalizar.UseVisualStyleBackColor = false;

            // ucPDV
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(8)))));
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.lblTotalValor);
            this.Controls.Add(this.lblTotalTexto);
            this.Controls.Add(this.lstCarrinho);
            this.Controls.Add(this.lblSeparador);
            this.Controls.Add(this.lstCatalogo);
            this.Controls.Add(this.txtBuscaProduto);
            this.Controls.Add(this.lblTitulo);
            this.Name = "ucPDV";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}