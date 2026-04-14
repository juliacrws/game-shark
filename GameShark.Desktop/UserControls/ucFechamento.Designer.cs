namespace GameShark.Desktop.UserControls
{
    partial class ucFechamento
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblReceita;
        private System.Windows.Forms.Label lblEntregues;
        private System.Windows.Forms.Label lblCancelados;
        private System.Windows.Forms.Button btnEncerrar;
        private System.Windows.Forms.Panel pnlReceita;
        private System.Windows.Forms.Panel pnlEntregues;
        private System.Windows.Forms.Panel pnlCancelados;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblReceita = new System.Windows.Forms.Label();
            this.lblEntregues = new System.Windows.Forms.Label();
            this.lblCancelados = new System.Windows.Forms.Label();
            this.btnEncerrar = new System.Windows.Forms.Button();
            this.pnlReceita = new System.Windows.Forms.Panel();
            this.pnlEntregues = new System.Windows.Forms.Panel();
            this.pnlCancelados = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // Título Principal
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(30, 30);
            this.lblTitulo.Text = "FECHAMENTO DE CAIXA (HOJE)";

            // Painel Receita (Verde Neon)
            this.pnlReceita.BackColor = System.Drawing.Color.FromArgb(17, 17, 24);
            this.pnlReceita.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReceita.Location = new System.Drawing.Point(35, 120);
            this.pnlReceita.Size = new System.Drawing.Size(730, 150);
            this.lblReceita.AutoSize = true;
            this.lblReceita.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold);
            this.lblReceita.ForeColor = System.Drawing.Color.FromArgb(57, 255, 20); // Verde Neon
            this.lblReceita.Location = new System.Drawing.Point(20, 30);
            this.lblReceita.Text = "R$ 0,00";
            this.pnlReceita.Controls.Add(this.lblReceita);

            // Painel Entregues (Ciano)
            this.pnlEntregues.BackColor = System.Drawing.Color.FromArgb(17, 17, 24);
            this.pnlEntregues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEntregues.Location = new System.Drawing.Point(35, 290);
            this.pnlEntregues.Size = new System.Drawing.Size(350, 120);
            this.lblEntregues.AutoSize = true;
            this.lblEntregues.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblEntregues.ForeColor = System.Drawing.Color.Cyan;
            this.lblEntregues.Location = new System.Drawing.Point(20, 35);
            this.lblEntregues.Text = "📦 0 Entregues";
            this.pnlEntregues.Controls.Add(this.lblEntregues);

            // Painel Cancelados (Vermelho)
            this.pnlCancelados.BackColor = System.Drawing.Color.FromArgb(17, 17, 24);
            this.pnlCancelados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCancelados.Location = new System.Drawing.Point(415, 290);
            this.pnlCancelados.Size = new System.Drawing.Size(350, 120);
            this.lblCancelados.AutoSize = true;
            this.lblCancelados.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblCancelados.ForeColor = System.Drawing.Color.Crimson;
            this.lblCancelados.Location = new System.Drawing.Point(20, 35);
            this.lblCancelados.Text = "❌ 0 Cancelados";
            this.pnlCancelados.Controls.Add(this.lblCancelados);

            // Botão Encerrar Expediente
            this.btnEncerrar.BackColor = System.Drawing.Color.Yellow;
            this.btnEncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEncerrar.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnEncerrar.Location = new System.Drawing.Point(35, 450);
            this.btnEncerrar.Size = new System.Drawing.Size(730, 60);
            this.btnEncerrar.Text = "🔒 ENCERRAR EXPEDIENTE";
            this.btnEncerrar.UseVisualStyleBackColor = false;

            // ucFechamento
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 5, 8);
            this.Size = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pnlReceita);
            this.Controls.Add(this.pnlEntregues);
            this.Controls.Add(this.pnlCancelados);
            this.Controls.Add(this.btnEncerrar);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}