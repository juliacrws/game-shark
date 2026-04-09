using System.Net;
using System.Net.Mail;

namespace GameShark.Web.Services; // 👈 Atualizado para a sua pasta Services!

public static class HackerMailService
{
    public static async Task EnviarAlertaEstoqueAsync(string emailDestino, string nomeJogo)
    {
        // ⚠️ ATENÇÃO: Para enviar de verdade, você precisará colocar um e-mail e senha reais aqui no futuro.
        var remetente = "seu_email_aqui@gmail.com";
        var senhaApp = "sua_senha_de_aplicativo_aqui";
        var smtpHost = "smtp.gmail.com";
        var smtpPort = 587;

        try
        {
            var mailMessage = new MailMessage(remetente, emailDestino)
            {
                Subject = "🎮 RADAR DE LOOT: O Estoque Chegou!",
                Body = $@"
                    <div style='font-family: Arial, sans-serif; background-color: #050508; color: #fff; padding: 30px; border: 2px solid #ffc107; border-radius: 10px; max-width: 600px; margin: 0 auto;'>
                        <h1 style='color: #00f3ff; text-transform: uppercase;'>Atenção, Player!</h1>
                        <p style='font-size: 16px;'>O nosso algoritmo detectou que o item <strong style='color: #39ff14;'>{nomeJogo}</strong> acabou de pousar no nosso armazém.</p>
                        <p style='font-size: 16px;'>Corra para a GameShark antes que os outros players roubem o seu loot!</p>
                        <a href='https://localhost:5001' style='display: inline-block; background-color: #ffc107; color: #000; padding: 12px 24px; text-decoration: none; font-weight: bold; border-radius: 5px; margin-top: 20px;'>ACESSAR LOJA</a>
                    </div>",
                IsBodyHtml = true
            };

            using var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(remetente, senhaApp),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar email hacker: {ex.Message}");
        }
    }
}