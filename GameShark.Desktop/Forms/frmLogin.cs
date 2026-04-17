using System;
using System.Windows.Forms;
using GameShark.Desktop.Services;

namespace GameShark.Desktop.Forms;

public partial class frmLogin : Form
{
    private readonly AuthApiService _apiService;

    // 🚀 O SEGREDO: O Program.cs vai ler essa variável para saber se abre o sistema ou não
    public bool LoginComSucesso { get; private set; } = false;

    public frmLogin()
    {
        InitializeComponent();
        _apiService = new AuthApiService();

        btnEntrar.Click += BtnEntrar_Click;
    }

    private async void BtnEntrar_Click(object? sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string senha = txtSenha.Text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        btnEntrar.Text = "AUTENTICANDO...";
        btnEntrar.Enabled = false;

        var usuario = await _apiService.FazerLoginAsync(email, senha);

        if (usuario != null)
        {
            // DEU CERTO! Avisamos que o login passou e fechamos a tela de login.
            LoginComSucesso = true;
            this.Close();
        }
        else
        {
            MessageBox.Show("Acesso Negado. E-mail ou senha incorretos.", "Erro de Segurança", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnEntrar.Text = "ACESSAR TERMINAL";
            btnEntrar.Enabled = true;
            txtSenha.Clear();
            txtSenha.Focus();
        }
    }

    private void btnEntrar_Click_1(object sender, EventArgs e)
    {

    }
}