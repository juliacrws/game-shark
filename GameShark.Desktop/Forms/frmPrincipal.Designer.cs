namespace GameShark.Desktop.Forms
{
    partial class frmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel painelMenu;
        private System.Windows.Forms.Panel painelConteudo;
        private System.Windows.Forms.Button btnCaixa;
        private System.Windows.Forms.Button btnRetirada;
        private System.Windows.Forms.Button btnFechamento;
        private System.Windows.Forms.Button btnFornecedores; // 👈 BOTÃO DO NOVO CRUD
        private System.Windows.Forms.Label lblLogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.painelMenu = new System.Windows.Forms.Panel();
            this.btnFornecedores = new System.Windows.Forms.Button();
            this.btnFechamento = new System.Windows.Forms.Button();
            this.btnRetirada = new System.Windows.Forms.Button();
            this.btnCaixa = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.painelConteudo = new System.Windows.Forms.Panel();
            this.painelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelMenu
            // 
            this.painelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(24)))));
            this.painelMenu.Controls.Add(this.btnFornecedores);
            this.painelMenu.Controls.Add(this.btnFechamento);
            this.painelMenu.Controls.Add(this.btnRetirada);
            this.painelMenu.Controls.Add(this.btnCaixa);
            this.painelMenu.Controls.Add(this.lblLogo);
            this.painelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.painelMenu.Location = new System.Drawing.Point(0, 0);
            this.painelMenu.Name = "painelMenu";
            this.painelMenu.Size = new System.Drawing.Size(250, 768);
            this.painelMenu.TabIndex = 0;
            // 
            // btnFornecedores
            // 
            this.btnFornecedores.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFornecedores.FlatAppearance.BorderSize = 0;
            this.btnFornecedores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnFornecedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFornecedores.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFornecedores.ForeColor = System.Drawing.Color.White;
            this.btnFornecedores.Location = new System.Drawing.Point(0, 280);
            this.btnFornecedores.Name = "btnFornecedores";
            this.btnFornecedores.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnFornecedores.Size = new System.Drawing.Size(250, 60);
            this.btnFornecedores.TabIndex = 4;
            this.btnFornecedores.Text = "🚚 Fornecedores";
            this.btnFornecedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFornecedores.UseVisualStyleBackColor = true;
            // IMPORTANTE: Adicione o evento de click no code-behind (frmPrincipal.cs)
            // this.btnFornecedores.Click += new System.EventHandler(this.btnFornecedores_Click);
            // 
            // btnFechamento
            // 
            this.btnFechamento.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFechamento.FlatAppearance.BorderSize = 0;
            this.btnFechamento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnFechamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechamento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFechamento.ForeColor = System.Drawing.Color.White;
            this.btnFechamento.Location = new System.Drawing.Point(0, 220);
            this.btnFechamento.Name = "btnFechamento";
            this.btnFechamento.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnFechamento.Size = new System.Drawing.Size(250, 60);
            this.btnFechamento.TabIndex = 3;
            this.btnFechamento.Text = "💰 Fechamento";
            this.btnFechamento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechamento.UseVisualStyleBackColor = true;
            // 
            // btnRetirada
            // 
            this.btnRetirada.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRetirada.FlatAppearance.BorderSize = 0;
            this.btnRetirada.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnRetirada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetirada.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRetirada.ForeColor = System.Drawing.Color.White;
            this.btnRetirada.Location = new System.Drawing.Point(0, 160);
            this.btnRetirada.Name = "btnRetirada";
            this.btnRetirada.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRetirada.Size = new System.Drawing.Size(250, 60);
            this.btnRetirada.TabIndex = 2;
            this.btnRetirada.Text = "📦 Retirada em Loja";
            this.btnRetirada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetirada.UseVisualStyleBackColor = true;
            // 
            // btnCaixa
            // 
            this.btnCaixa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaixa.FlatAppearance.BorderSize = 0;
            this.btnCaixa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaixa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCaixa.ForeColor = System.Drawing.Color.White;
            this.btnCaixa.Location = new System.Drawing.Point(0, 100);
            this.btnCaixa.Name = "btnCaixa";
            this.btnCaixa.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCaixa.Size = new System.Drawing.Size(250, 60);
            this.btnCaixa.TabIndex = 1;
            this.btnCaixa.Text = "🎮 Frente de Caixa";
            this.btnCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaixa.UseVisualStyleBackColor = true;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogo.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(250, 100);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "GAMESHARK";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // painelConteudo
            // 
            this.painelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(8)))));
            this.painelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.painelConteudo.Location = new System.Drawing.Point(250, 0);
            this.painelConteudo.Name = "painelConteudo";
            this.painelConteudo.Size = new System.Drawing.Size(774, 768);
            this.painelConteudo.TabIndex = 1;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.painelConteudo);
            this.Controls.Add(this.painelMenu);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameShark PDV - Terminal Omnichannel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.painelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}