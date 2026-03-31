using GameShark.Infrastructure.Entities;
using GameShark.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameShark.Web.Controllers; // 👈 Adicionamos o namespace para manter a organização

[Authorize]
public class PerfilController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _env;

    public PerfilController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(user);
    }

    // 👇 NOVO: Método que recebe o Nick da View e salva no banco
    [HttpPost]
    public async Task<IActionResult> AtualizarPerfil(string nuevoApelido)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound("Player não encontrado no banco de dados.");

        user.Apelido = nuevoApelido;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            // Dispara o Toast Neon de sucesso!
            TempData["MensagemSucesso"] = "Codinome atualizado com sucesso no HUD!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarFoto(IFormFile foto)
    {
        if (foto == null) return RedirectToAction("Index");

        var user = await _userManager.GetUserAsync(User);

        if (user == null) return NotFound("Player não encontrado no banco de dados.");

        var nomeArquivo = Guid.NewGuid() + "_" + foto.FileName;
        var caminho = Path.Combine(_env.WebRootPath, "images/perfis", nomeArquivo);

        if (!Directory.Exists(Path.GetDirectoryName(caminho)))
            Directory.CreateDirectory(Path.GetDirectoryName(caminho)!);

        using (var stream = new FileStream(caminho, FileMode.Create))
        {
            await foto.CopyToAsync(stream);
        }

        user.FotoPerfilUrl = "/images/perfis/" + nomeArquivo;
        await _userManager.UpdateAsync(user);

        // 👇 BÔNUS: Dispara o Toast ao mudar o avatar também!
        TempData["MensagemSucesso"] = "Avatar atualizado com sucesso!";

        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Configuracoes()
    {
        return View(new MudarSenhaVm()); // Manda o modelo vazio pra tela
    }

    [HttpPost]
    public async Task<IActionResult> Configuracoes(MudarSenhaVm model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound("Player não encontrado.");

        // O Identity faz o trabalho duro de criptografar e validar
        var result = await _userManager.ChangePasswordAsync(user, model.SenhaAtual, model.NovaSenha);

        if (result.Succeeded)
        {
            TempData["MensagemSucesso"] = "Senha atualizada com sucesso! Conta 100% segura.";
            return RedirectToAction("Index"); // Volta pro HUD do Perfil
        }

        // Se errar a senha atual ou não cumprir os requisitos de segurança
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
}