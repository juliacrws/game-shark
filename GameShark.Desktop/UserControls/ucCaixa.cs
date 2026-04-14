using System;
using System.Drawing;
using System.Windows.Forms;
using GameShark.Desktop.Services;

namespace GameShark.Desktop.UserControls;

public partial class ucCaixa : UserControl
{
    private readonly CaixaApiService _caixaApiService;

    public ucCaixa()
    {
        InitializeComponent();
        _caixaApiService = new CaixaApiService();

        // Atrela o evento de clique do botão ao nosso método assíncrono
        btnAbrirCaixa.Click += BtnAbrirCaixa_Click;
    }

    private async void BtnAbrirCaixa_Click(object? sender, EventArgs e)
    {
        // 1. Valida se o que foi digitado é um número
        if (!decimal.TryParse(txtValorInicial.Text, out decimal valorInicial))
        {
            MessageBox.Show("Por favor, digite um valor numérico válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // 2. Muda o visual do botão para mostrar que está processando
        btnAbrirCaixa.Text = "CONECTANDO À MATRIZ...";
        btnAbrirCaixa.Enabled = false;

        try
        {
            // 3. 🚀 O disparo para a API!
            bool sucesso = await _caixaApiService.AbrirCaixaAsync(valorInicial);

            if (sucesso)
            {
                lblStatusCaixa.Text = "🟢 STATUS: CAIXA ABERTO E OPERANTE";
                lblStatusCaixa.ForeColor = Color.FromArgb(57, 255, 20); // Verde
                painelAbertura.Visible = false; // Esconde o painel de abrir caixa

                MessageBox.Show("Caixa aberto com sucesso na Matriz!", "GameShark PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("A API recusou a abertura do caixa. Verifique os dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha de comunicação com o servidor.\n\nDetalhe: {ex.Message}", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            // Devolve o botão ao normal
            btnAbrirCaixa.Text = "ABRIR CAIXA";
            btnAbrirCaixa.Enabled = true;
        }
    }
}