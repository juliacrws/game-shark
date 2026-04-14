namespace GameShark.Desktop.Forms
{
    partial class frmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel painelMenu;
        private System.Windows.Forms.Panel painelConteudo;
        private System.Windows.Forms.Button btnCaixa;
        private System.Windows.Forms.Button btnRetirada;
        private System.Windows.Forms.Button btnFechamento; // 👈 DECLARADO AQUI
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
            painelMenu = new Panel();
            btnFechamento = new Button();
            btnRetirada = new Button();
            btnCaixa = new Button();
            lblLogo = new Label();
            painelConteudo = new Panel();
            painelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // painelMenu
            // 
            painelMenu.BackColor = Color.FromArgb(17, 17, 24);
            painelMenu.Controls.Add(btnFechamento);
            painelMenu.Controls.Add(btnRetirada);
            painelMenu.Controls.Add(btnCaixa);
            painelMenu.Controls.Add(lblLogo);
            painelMenu.Dock = DockStyle.Left;
            painelMenu.Location = new Point(0, 0);
            painelMenu.Name = "painelMenu";
            painelMenu.Size = new Size(250, 768);
            painelMenu.TabIndex = 0;
            // 
            // btnFechamento
            // 
            btnFechamento.Dock = DockStyle.Top;
            btnFechamento.FlatAppearance.BorderSize = 0;
            btnFechamento.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 40, 50);
            btnFechamento.FlatStyle = FlatStyle.Flat;
            btnFechamento.Font = new Font("Segoe UI", 12F);
            btnFechamento.ForeColor = Color.White;
            btnFechamento.Location = new Point(0, 220);
            btnFechamento.Name = "btnFechamento";
            btnFechamento.Padding = new Padding(20, 0, 0, 0);
            btnFechamento.Size = new Size(250, 60);
            btnFechamento.TabIndex = 3;
            btnFechamento.Text = "💰 Fechamento";
            btnFechamento.TextAlign = ContentAlignment.MiddleLeft;
            btnFechamento.UseVisualStyleBackColor = true;
            // 
            // btnRetirada
            // 
            btnRetirada.Dock = DockStyle.Top;
            btnRetirada.FlatAppearance.BorderSize = 0;
            btnRetirada.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 40, 50);
            btnRetirada.FlatStyle = FlatStyle.Flat;
            btnRetirada.Font = new Font("Segoe UI", 12F);
            btnRetirada.ForeColor = Color.White;
            btnRetirada.Location = new Point(0, 160);
            btnRetirada.Name = "btnRetirada";
            btnRetirada.Padding = new Padding(20, 0, 0, 0);
            btnRetirada.Size = new Size(250, 60);
            btnRetirada.TabIndex = 2;
            btnRetirada.Text = "📦 Retirada em Loja";
            btnRetirada.TextAlign = ContentAlignment.MiddleLeft;
            btnRetirada.UseVisualStyleBackColor = true;
            // 
            // btnCaixa
            // 
            btnCaixa.Dock = DockStyle.Top;
            btnCaixa.FlatAppearance.BorderSize = 0;
            btnCaixa.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 40, 50);
            btnCaixa.FlatStyle = FlatStyle.Flat;
            btnCaixa.Font = new Font("Segoe UI", 12F);
            btnCaixa.ForeColor = Color.White;
            btnCaixa.Location = new Point(0, 100);
            btnCaixa.Name = "btnCaixa";
            btnCaixa.Padding = new Padding(20, 0, 0, 0);
            btnCaixa.Size = new Size(250, 60);
            btnCaixa.TabIndex = 1;
            btnCaixa.Text = "🎮 Frente de Caixa";
            btnCaixa.TextAlign = ContentAlignment.MiddleLeft;
            btnCaixa.UseVisualStyleBackColor = true;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Consolas", 20.25F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(0, 243, 255);
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(250, 100);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "GAMESHARK";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // painelConteudo
            // 
            painelConteudo.BackColor = Color.FromArgb(5, 5, 8);
            painelConteudo.Dock = DockStyle.Fill;
            painelConteudo.Location = new Point(250, 0);
            painelConteudo.Name = "painelConteudo";
            painelConteudo.Size = new Size(774, 768);
            painelConteudo.TabIndex = 1;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 768);
            ControlBox = false;
            Controls.Add(painelConteudo);
            Controls.Add(painelMenu);
            Name = "frmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameShark PDV - Terminal Omnichannel";
            WindowState = FormWindowState.Maximized;
            painelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}