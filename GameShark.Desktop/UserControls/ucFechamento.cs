using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameShark.Desktop.Services;

namespace GameShark.Desktop.UserControls;

public partial class ucFechamento : UserControl
{
    private readonly RetiradaApiService _apiService;
    private System.Windows.Forms.Timer _radarTimer;
    public ucFechamento()
    {
        InitializeComponent();
        _apiService = new RetiradaApiService();

        // ⚙️ Configurando o Radar
        _radarTimer = new System.Windows.Forms.Timer();
        _radarTimer.Interval = 5000; // 5000 ms = 5 segundos (Pode mudar se quiser mais rápido)
        _radarTimer.Tick += RadarTimer_Tick; // A cada 5s, ele dispara essa função

        // Eventos
        this.Load += async (s, e) =>
        {
            await CarregarResumoDoDia(); // Carrega forte a primeira vez
            _radarTimer.Start();         // Liga o radar silencioso
        };

        btnEncerrar.Click += BtnEncerrar_Click;
    }

    // 🚀 Carregamento Principal (Quando abre a tela)
    private async Task CarregarResumoDoDia()
    {
        btnEncerrar.Text = "CARREGANDO DADOS DA MATRIZ...";
        btnEncerrar.Enabled = false;

        var resumo = await _apiService.ObterResumoDoDiaAsync();

        lblReceita.Text = $"R$ {resumo.Receita:F2}";
        lblEntregues.Text = $"📦 {resumo.Entregues} Entregues";
        lblCancelados.Text = $"❌ {resumo.Cancelados} Cancelados";

        btnEncerrar.Text = "🔒 ENCERRAR EXPEDIENTE";
        btnEncerrar.Enabled = true;
    }

    // 📡 O Ping Silencioso (Atualiza em tempo real sem travar a tela)
    private async void RadarTimer_Tick(object? sender, EventArgs e)
    {
        var resumo = await _apiService.ObterResumoDoDiaAsync();

        // Atualiza os textos de forma fluida
        lblReceita.Text = $"R$ {resumo.Receita:F2}";
        lblEntregues.Text = $"📦 {resumo.Entregues} Entregues";
        lblCancelados.Text = $"❌ {resumo.Cancelados} Cancelados";
        // 👇 ADICIONA ESTA LINHA AQUI:
        btnEncerrar.Text = $"🔒 ENCERRAR EXPEDIENTE (Atualizado às {DateTime.Now:HH:mm:ss})";
    }

    private void BtnEncerrar_Click(object? sender, EventArgs e)
    {
        var confirmacao = MessageBox.Show(
            "Tem certeza que deseja encerrar o expediente? Isso fará logoff do sistema.",
            "Fechamento de Caixa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirmacao == DialogResult.Yes)
        {
            _radarTimer.Stop(); // Desliga o radar antes de fechar
            MessageBox.Show("Expediente encerrado com sucesso. O relatório foi enviado para a matriz. Bom descanso, Operador!", "GameShark PDV");
            System.Windows.Forms.Application.Exit();
        }
    }

    // 🛡️ Prevenção de vazamento de memória (Se você trocar de tela, desliga o radar)
    protected override void OnHandleDestroyed(EventArgs e)
    {
        _radarTimer?.Stop();
        _radarTimer?.Dispose();
        base.OnHandleDestroyed(e);
    }
}