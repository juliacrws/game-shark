namespace GameShark.Desktop.UserControls
{
    partial class ucCaixa
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblStatusCaixa;
        private System.Windows.Forms.Panel painelAbertura;
        private System.Windows.Forms.Label lblValorInicial;
        private System.Windows.Forms.TextBox txtValorInicial;
        private System.Windows.Forms.Button btnAbrirCaixa;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblStatusCaixa = new System.Windows.Forms.Label();
            this.painelAbertura = new System.Windows.Forms.Panel();
            this.lblValorInicial = new System.Windows.Forms.Label();
            this.txtValorInicial = new System.Windows.Forms.TextBox();
            this.btnAbrirCaixa = new System.Windows.Forms.Button();
            this.painelAbertura.SuspendLayout();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(30, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(262, 45);
            this.lblTitulo.Text = "FRENTE DE CAIXA";

            // lblStatusCaixa
            this.lblStatusCaixa.AutoSize = true;
            this.lblStatusCaixa.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatusCaixa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36))))); // Vermelho Neon
            this.lblStatusCaixa.Location = new System.Drawing.Point(35, 80);
            this.lblStatusCaixa.Name = "lblStatusCaixa";
            this.lblStatusCaixa.Size = new System.Drawing.Size(235, 25);
            this.lblStatusCaixa.Text = "🔴 STATUS: CAIXA FECHADO";

            // painelAbertura
            this.painelAbertura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(24)))));
            this.painelAbertura.Controls.Add(this.btnAbrirCaixa);
            this.painelAbertura.Controls.Add(this.txtValorInicial);
            this.painelAbertura.Controls.Add(this.lblValorInicial);
            this.painelAbertura.Location = new System.Drawing.Point(35, 130);
            this.painelAbertura.Name = "painelAbertura";
            this.painelAbertura.Size = new System.Drawing.Size(400, 200);

            // lblValorInicial
            this.lblValorInicial.AutoSize = true;
            this.lblValorInicial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblValorInicial.ForeColor = System.Drawing.Color.LightGray;
            this.lblValorInicial.Location = new System.Drawing.Point(25, 30);
            this.lblValorInicial.Name = "lblValorInicial";
            this.lblValorInicial.Size = new System.Drawing.Size(224, 21);
            this.lblValorInicial.Text = "Valor Inicial na Gaveta (Troco) R$";

            // txtValorInicial
            this.txtValorInicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.txtValorInicial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValorInicial.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtValorInicial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(243)))), ((int)(((byte)(255))))); // Ciano
            this.txtValorInicial.Location = new System.Drawing.Point(30, 60);
            this.txtValorInicial.Name = "txtValorInicial";
            this.txtValorInicial.Size = new System.Drawing.Size(340, 39);
            this.txtValorInicial.Text = "0,00";

            // btnAbrirCaixa
            this.btnAbrirCaixa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))); // Verde Neon
            this.btnAbrirCaixa.FlatAppearance.BorderSize = 0;
            this.btnAbrirCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirCaixa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAbrirCaixa.ForeColor = System.Drawing.Color.Black;
            this.btnAbrirCaixa.Location = new System.Drawing.Point(30, 120);
            this.btnAbrirCaixa.Name = "btnAbrirCaixa";
            this.btnAbrirCaixa.Size = new System.Drawing.Size(340, 50);
            this.btnAbrirCaixa.Text = "ABRIR CAIXA";
            this.btnAbrirCaixa.UseVisualStyleBackColor = false;

            // ucCaixa
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(8)))));
            this.Controls.Add(this.painelAbertura);
            this.Controls.Add(this.lblStatusCaixa);
            this.Controls.Add(this.lblTitulo);
            this.Name = "ucCaixa";
            this.Size = new System.Drawing.Size(800, 600);
            this.painelAbertura.ResumeLayout(false);
            this.painelAbertura.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}