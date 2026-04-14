using System;
using System.Windows.Forms;
using GameShark.Desktop.Forms;

namespace GameShark.Desktop;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // 1. Abre a tela de Login e congela a execução aqui até ela ser fechada
        var telaLogin = new frmLogin();
        System.Windows.Forms.Application.Run(telaLogin);

        // 2. A tela de login foi fechada. Foi porque logou ou porque clicou no X?
        if (telaLogin.LoginComSucesso)
        {
            // Se logou com sucesso, nós "startamos" o programa de novo, agora com a tela principal!
            System.Windows.Forms.Application.Run(new frmPrincipal());
        }
    }
}