namespace GameShark.Desktop.Forms
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Panel painelLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            painelLogin = new Panel();
            btnEntrar = new Button();
            txtSenha = new TextBox();
            txtEmail = new TextBox();
            lblLogo = new Label();
            painelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // painelLogin
            // 
            painelLogin.BackColor = Color.FromArgb(17, 17, 24);
            painelLogin.Controls.Add(btnEntrar);
            painelLogin.Controls.Add(txtSenha);
            painelLogin.Controls.Add(txtEmail);
            painelLogin.Controls.Add(lblLogo);
            painelLogin.Location = new Point(312, 184);
            painelLogin.Name = "painelLogin";
            painelLogin.Size = new Size(400, 400);
            painelLogin.TabIndex = 0;
            // 
            // btnEntrar
            // 
            btnEntrar.BackColor = Color.FromArgb(0, 243, 255);
            btnEntrar.FlatAppearance.BorderSize = 0;
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnEntrar.ForeColor = Color.Black;
            btnEntrar.Location = new Point(50, 260);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(300, 50);
            btnEntrar.TabIndex = 0;
            btnEntrar.Text = "ACESSAR TERMINAL";
            btnEntrar.UseVisualStyleBackColor = false;
            btnEntrar.Click += btnEntrar_Click_1;
            // 
            // txtSenha
            // 
            txtSenha.BackColor = Color.FromArgb(30, 30, 40);
            txtSenha.BorderStyle = BorderStyle.FixedSingle;
            txtSenha.Font = new Font("Segoe UI", 16F);
            txtSenha.ForeColor = Color.White;
            txtSenha.Location = new Point(50, 190);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '•';
            txtSenha.PlaceholderText = "Senha";
            txtSenha.Size = new Size(300, 36);
            txtSenha.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(30, 30, 40);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 16F);
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(50, 130);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail do Operador";
            txtEmail.Size = new Size(300, 36);
            txtEmail.TabIndex = 2;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Consolas", 28F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(0, 243, 255);
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(400, 100);
            lblLogo.TabIndex = 3;
            lblLogo.Text = "GAMESHARK";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(5, 5, 8);
            ClientSize = new Size(1024, 768);
            Controls.Add(painelLogin);
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameShark - Autenticação";
            painelLogin.ResumeLayout(false);
            painelLogin.PerformLayout();
            ResumeLayout(false);
        }
    }
}