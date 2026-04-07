using Microsoft.AspNetCore.Http;

namespace GameShark.Web.Helpers;

public static class ImageSecurity
{
    public static bool IsValidImage(IFormFile file)
    {
        // 1. Verifica se o arquivo existe e se não é um arquivo vazio
        if (file == null || file.Length == 0) return false;

        // 2. Limite de Tamanho: Máximo de 5 Megabytes (para não lotar seu servidor)
        const long maxFileSize = 5 * 1024 * 1024;
        if (file.Length > maxFileSize) return false;

        // 3. Extensões Permitidas (Whitelist)
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension)) return false;

        // 4. Assinatura Real do Arquivo (MIME Type)
        // Isso impede que alguém renomeie um "virus.exe" para "virus.jpg"
        var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };
        if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant())) return false;

        return true;
    }
}