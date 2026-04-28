using GameShark.Desktop.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameShark.Desktop.Forms;

public partial class frmPrincipal : Form
{
    public frmPrincipal()
    {
        InitializeComponent();

        // Atrelando os cliques dos botões aos métodos
        btnCaixa.Click += BtnCaixa_Click;
        btnRetirada.Click += BtnRetirada_Click;
        btnFechamento.Click += BtnFechamento_Click; // 👈 NOVO: O botão agora tem vida!
    }

    // 🚀 O MOTOR DA ARQUITETURA
    public void NavegarPara(UserControl tela)
    {
        if (painelConteudo == null) return;

        painelConteudo.Controls.Clear();
        tela.Dock = DockStyle.Fill;
        painelConteudo.Controls.Add(tela);
        tela.BringToFront();
    }

    private void BtnCaixa_Click(object? sender, EventArgs e)
    {
        NavegarPara(new GameShark.Desktop.UserControls.ucPDV());
    }

    private void BtnRetirada_Click(object? sender, EventArgs e)
    {
        NavegarPara(new GameShark.Desktop.UserControls.ucRetirada());
    }

    // 💰 NOVO MÉTODO: Abre a nossa Dashboard de Fechamento de Caixa!
    private void BtnFechamento_Click(object? sender, EventArgs e)
    {
        NavegarPara(new GameShark.Desktop.UserControls.ucFechamento());
    }
    private void btnFornecedores_Click(object sender, EventArgs e)
    {
        painelConteudo.Controls.Clear();
        var uc = new GameShark.Desktop.UserControls.ucFornecedores();
        uc.Dock = DockStyle.Fill;
        painelConteudo.Controls.Add(uc);
    }
}