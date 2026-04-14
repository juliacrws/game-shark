using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameShark.Desktop.Services;

namespace GameShark.Desktop.UserControls;

public partial class ucRetirada : UserControl
{
    private readonly RetiradaApiService _apiService;
    private int _pedidoIdAtual = 0;

    public ucRetirada()
    {
        InitializeComponent();
        _apiService = new RetiradaApiService();

        // 🎨 Estilização Gamer
        ConfigurarGrid();

        btnBuscar.Click += BtnBuscar_Click;
        btnConfirmar.Click += BtnConfirmar_Click;
        btnCancelar.Click += BtnCancelar_Click; // 👈 GANCHO DO NOVO BOTÃO DE CANCELAR ADICIONADO AQUI
        this.Load += async (s, e) => await CarregarPedidos();
    }

    private void ConfigurarGrid()
    {
        // Apenas Estética e Comportamento
        dgvPedidos.BackgroundColor = System.Drawing.Color.FromArgb(10, 10, 15);
        dgvPedidos.ForeColor = System.Drawing.Color.White;
        dgvPedidos.GridColor = System.Drawing.Color.FromArgb(30, 30, 40);
        dgvPedidos.BorderStyle = BorderStyle.None;
        dgvPedidos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvPedidos.RowHeadersVisible = false;
        dgvPedidos.EnableHeadersVisualStyles = false;

        dgvPedidos.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(17, 17, 24);
        dgvPedidos.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 243, 255);
        dgvPedidos.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

        dgvPedidos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(25, 25, 35);
        dgvPedidos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 243, 255);
        dgvPedidos.ColumnHeadersHeight = 40;

        dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgvPedidos.CellClick += DgvPedidos_CellClick;
    }

    private async Task CarregarPedidos()
    {
        var pendentes = await _apiService.ObterPendentesAsync();
        dgvPedidos.DataSource = pendentes;

        // 🛡️ Usamos o operador "!" (null-forgiving) ou verificamos nulo com "?."
        if (dgvPedidos.Columns["Id"] != null)
            dgvPedidos.Columns["Id"]!.Visible = false;

        if (dgvPedidos.Columns["Codigo"] != null)
            dgvPedidos.Columns["Codigo"]!.HeaderText = "Nº PEDIDO";

        if (dgvPedidos.Columns["Cliente"] != null)
            dgvPedidos.Columns["Cliente"]!.HeaderText = "NOME DO CLIENTE";

        if (dgvPedidos.Columns["Data"] != null)
            dgvPedidos.Columns["Data"]!.HeaderText = "DATA COMPRA";
    }

    private async void BtnBuscar_Click(object? sender, EventArgs e)
    {
        string codigo = txtCodigo.Text.Trim();
        if (string.IsNullOrEmpty(codigo)) return;

        btnBuscar.Enabled = false;
        var pedido = await _apiService.BuscarPedidoAsync(codigo);

        if (pedido != null)
        {
            _pedidoIdAtual = pedido.Id;
            lblNomeCliente.Text = $"👤 CLIENTE: {pedido.ClienteNome}";
            lblValorTotal.Text = $"VALOR TOTAL: R$ {pedido.ValorTotal:F2}";

            lstItens.Items.Clear();
            foreach (var item in pedido.Itens)
            {
                lstItens.Items.Add(item);
            }

            dgvPedidos.Visible = false;
            painelPedido.Visible = true;
        }
        else
        {
            MessageBox.Show("Pedido não encontrado.", "Erro");
        }
        btnBuscar.Enabled = true;
    }

    private async void BtnConfirmar_Click(object? sender, EventArgs e)
    {
        if (_pedidoIdAtual == 0) return;

        if (MessageBox.Show("Confirmar entrega?", "GameShark", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false; // Trava o outro botão também

            if (await _apiService.ConfirmarEntregaAsync(_pedidoIdAtual))
            {
                MessageBox.Show("Loot entregue!");
                painelPedido.Visible = false;
                dgvPedidos.Visible = true;
                txtCodigo.Clear();
                await CarregarPedidos();
            }
            btnConfirmar.Enabled = true;
            btnCancelar.Enabled = true;
        }
    }

    // ❌ LÓGICA DO NOVO BOTÃO DE CANCELAR
    private async void BtnCancelar_Click(object? sender, EventArgs e)
    {
        if (_pedidoIdAtual == 0) return;

        var confirmacao = MessageBox.Show(
            "Atenção: Tem certeza que deseja CANCELAR este pedido e devolver os itens para a prateleira?",
            "Devolução de Estoque",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirmacao == DialogResult.Yes)
        {
            btnCancelar.Enabled = false;
            btnConfirmar.Enabled = false; // Trava os botões pra não dar duplo clique

            bool sucesso = await _apiService.CancelarPedidoAsync(_pedidoIdAtual);

            if (sucesso)
            {
                MessageBox.Show("Pedido cancelado e itens devolvidos ao estoque com sucesso!", "Estoque Atualizado");

                // Reseta a tela
                painelPedido.Visible = false;
                dgvPedidos.Visible = true;
                txtCodigo.Clear();
                await CarregarPedidos(); // Atualiza a lista para sumir o pedido cancelado
            }
            else
            {
                MessageBox.Show("Erro ao cancelar o pedido. Verifique a conexão com a matriz.", "Falha Crítica");
            }

            btnCancelar.Enabled = true;
            btnConfirmar.Enabled = true;
        }
    }

    private void DgvPedidos_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            var valor = dgvPedidos.Rows[e.RowIndex].Cells["Codigo"].Value;
            if (valor != null)
            {
                txtCodigo.Text = valor.ToString();
                btnBuscar.PerformClick();
            }
        }
    }
}