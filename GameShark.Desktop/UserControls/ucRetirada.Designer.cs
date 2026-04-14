namespace GameShark.Desktop.UserControls
{
    partial class ucRetirada
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel painelPedido;
        private System.Windows.Forms.Label lblNomeCliente;
        private System.Windows.Forms.ListBox lstItens;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Label lblValorTotal; // 👈 Declarado uma vez só
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.painelPedido = new System.Windows.Forms.Panel();
            this.lblNomeCliente = new System.Windows.Forms.Label();
            this.lstItens = new System.Windows.Forms.ListBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.lblValorTotal = new System.Windows.Forms.Label(); // 👈 Instanciado antes do painel
            this.painelPedido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(30, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(320, 45);
            this.lblTitulo.Text = "RETIRADA EM LOJA";

            // txtCodigo
            this.txtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.txtCodigo.ForeColor = System.Drawing.Color.White;
            this.txtCodigo.Location = new System.Drawing.Point(35, 100);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(300, 39);

            // btnBuscar
            this.btnBuscar.BackColor = System.Drawing.Color.Cyan;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Location = new System.Drawing.Point(350, 100);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 39);
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;

            // dgvPedidos
            this.dgvPedidos.Location = new System.Drawing.Point(35, 160);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.Size = new System.Drawing.Size(730, 400);
            this.dgvPedidos.TabIndex = 10;

            // painelPedido
            this.painelPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(24)))));
            this.painelPedido.Controls.Add(this.btnConfirmar);
            this.painelPedido.Controls.Add(this.lblValorTotal); // 👈 Adicionado UMA VEZ
            this.painelPedido.Controls.Add(this.lstItens);
            this.painelPedido.Controls.Add(this.lblNomeCliente);
            this.painelPedido.Controls.Add(this.btnCancelar);
            this.painelPedido.Location = new System.Drawing.Point(35, 160);
            this.painelPedido.Name = "painelPedido";
            this.painelPedido.Size = new System.Drawing.Size(730, 400);
            this.painelPedido.Visible = false;

            // lblNomeCliente
            this.lblNomeCliente.AutoSize = true;
            this.lblNomeCliente.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblNomeCliente.ForeColor = System.Drawing.Color.Yellow;
            this.lblNomeCliente.Location = new System.Drawing.Point(30, 20); // Respiro de borda
            this.lblNomeCliente.Name = "lblNomeCliente";
            this.lblNomeCliente.Size = new System.Drawing.Size(130, 25);
            this.lblNomeCliente.Text = "👤 CLIENTE:";

            // lstItens
            this.lstItens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.lstItens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstItens.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lstItens.ForeColor = System.Drawing.Color.White;
            this.lstItens.FormattingEnabled = true;
            this.lstItens.ItemHeight = 21;
            this.lstItens.Location = new System.Drawing.Point(30, 60); // Mais abaixo do nome
            this.lstItens.Name = "lstItens";
            this.lstItens.Size = new System.Drawing.Size(670, 191); // Mais larga

            // lblValorTotal
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblValorTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))); // Verde Neon
            this.lblValorTotal.Location = new System.Drawing.Point(30, 275); // Posicionado corretamente
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(183, 30);
            this.lblValorTotal.Text = "TOTAL: R$ 0,00";

            // btnConfirmar
            this.btnConfirmar.BackColor = System.Drawing.Color.Yellow;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.Black;
            this.btnConfirmar.Location = new System.Drawing.Point(30, 325); // Lá no rodapé do painel
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(670, 55); // Botão largão e confortável
            this.btnConfirmar.Text = "CONFIRMAR ENTREGA";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Location = new System.Drawing.Point(375, 325); // Empurrado pra direita
            this.btnConfirmar.Size = new System.Drawing.Size(325, 55); // Reduzido pela metade

            // 🔴 Configuração do btnCancelar (Metade Esquerda)
            this.btnCancelar.BackColor = System.Drawing.Color.Crimson; // Vermelho Gamer
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(30, 325);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(325, 55);
            this.btnCancelar.Text = "❌ CANCELAR E DEVOLVER";
            this.btnCancelar.UseVisualStyleBackColor = false;

            // ucRetirada
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(8)))));
            this.Controls.Add(this.dgvPedidos);
            this.Controls.Add(this.painelPedido);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblTitulo);
            this.Name = "ucRetirada";
            this.Size = new System.Drawing.Size(800, 600);
            this.painelPedido.ResumeLayout(false);
            this.painelPedido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}