using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameShark.Desktop.Services;

namespace GameShark.Desktop.UserControls;

public partial class ucFechamento : UserControl
{
    private readonly RetiradaApiService _apiService;
    private System.Windows.Forms.Timer _radarTimer;

    // 🎮 NOVOS ITENS GAMER: A Barra de XP e o Texto de Level!
    private ProgressBar pbXP;
    private Label lblLevel;
    private readonly decimal META_DIARIA = 1000.00m; // 👈 A meta do Boss!

    public ucFechamento()
    {
        InitializeComponent();
        _apiService = new RetiradaApiService();

        // 🎨 INJETANDO A BARRA DE XP NA TELA (Sem precisar do Designer)
        ConfigurarInterfaceGamer();

        _radarTimer = new System.Windows.Forms.Timer();
        _radarTimer.Interval = 5000;
        _radarTimer.Tick += RadarTimer_Tick;

        this.Load += async (s, e) =>
        {
            await CarregarResumoDoDia();
            _radarTimer.Start();
        };

        btnEncerrar.Click += BtnEncerrar_Click;
    }

    private void ConfigurarInterfaceGamer()
    {
        // Cria o texto de Level e coloca no final da tela
        lblLevel = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 40,
            ForeColor = Color.LimeGreen,
            Font = new Font("Consolas", 14, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Text = "Calculando XP..."
        };

        // Cria a Barra de Progresso e coloca embaixo do texto
        pbXP = new ProgressBar
        {
            Dock = DockStyle.Bottom,
            Height = 30,
            Style = ProgressBarStyle.Continuous
        };

        this.Controls.Add(lblLevel);
        this.Controls.Add(pbXP);
    }

    private async Task CarregarResumoDoDia()
    {
        btnEncerrar.Text = "CARREGANDO DADOS DA MATRIZ...";
        btnEncerrar.Enabled = false;

        var resumo = await _apiService.ObterResumoDoDiaAsync();
        AtualizarTela(resumo.Receita, resumo.Entregues, resumo.Cancelados);

        btnEncerrar.Enabled = true;
    }

    private async void RadarTimer_Tick(object? sender, EventArgs e)
    {
        var resumo = await _apiService.ObterResumoDoDiaAsync();
        AtualizarTela(resumo.Receita, resumo.Entregues, resumo.Cancelados);
    }

    // 🧠 MÁGICA REUNIDA: Atualiza os números e a Barra de XP juntos
    private void AtualizarTela(decimal receita, int entregues, int cancelados)
    {
        lblReceita.Text = $"R$ {receita:F2}";
        lblEntregues.Text = $"📦 {entregues} Entregues";
        lblCancelados.Text = $"❌ {cancelados} Cancelados";
        btnEncerrar.Text = $"🔒 ENCERRAR EXPEDIENTE (Sync: {DateTime.Now:HH:mm:ss})";

        // 🧮 Lógica da Barra de XP
        int porcentagem = (int)((receita / META_DIARIA) * 100);
        if (porcentagem > 100) porcentagem = 100; // Trava no 100% para não bugar a barra

        pbXP.Value = porcentagem;

        // 🏆 Efeito Boss Derrotado
        if (porcentagem >= 100)
        {
            lblLevel.Text = "🏆 LEVEL UP! META DIÁRIA BATIDA! 🏆";
            lblLevel.ForeColor = Color.Gold;
        }
        else
        {
            lblLevel.Text = $"XP Atual: {porcentagem}% (Faltam R$ {META_DIARIA - receita:F2})";
            lblLevel.ForeColor = Color.LimeGreen;
        }
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
            _radarTimer.Stop();
            MessageBox.Show("Expediente encerrado com sucesso. O relatório foi enviado para a matriz. Bom descanso, Operador!", "GameShark PDV");
            System.Windows.Forms.Application.Exit();
        }
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
        _radarTimer?.Stop();
        _radarTimer?.Dispose();
        base.OnHandleDestroyed(e);
    }
}