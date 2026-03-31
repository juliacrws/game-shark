using GameShark.Domain.Entities; // 👈 Para reconhecer o LogAcesso
using GameShark.Infrastructure.Entities;
using GameShark.Infrastructure.Persistence; // 👈 Para reconhecer o AppDbContext
using GameShark.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameShark.Web.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context; // 👈 Adicionamos o banco de dados aqui

    // 👇 O construtor agora pede o banco de dados também
    public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Tenta fazer o login
        var result = await _signInManager.PasswordSignInAsync(
            userName: model.Email,
            password: model.Password,
            isPersistent: model.RememberMe,
            lockoutOnFailure: false
        );

        // 👇 INÍCIO DA AUDITORIA DE SEGURANÇA (O Espião)
        // Pega o IP do usuário (se estiver rodando local, vai aparecer "::1")
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP Desconhecido";

        var log = new LogAcesso
        {
            EmailTentativa = model.Email,
            DataHora = DateTime.Now,
            Sucesso = result.Succeeded, // Marca "true" se logou, "false" se errou a senha
            IpAddress = ip
        };

        // Salva a tentativa silenciosamente no banco
        _context.LogsAcesso.Add(log);
        await _context.SaveChangesAsync();
        // 👆 FIM DA AUDITORIA

        if (result.Succeeded)
        {
            // Login com sucesso, manda pra Vitrine!
            return RedirectToAction("Index", "Home");
        }

        // Se errar a senha ou e-mail, devolve o erro na tela (mas a falha já foi registrada no banco!)
        ModelState.AddModelError(string.Empty, "Game Over: E-mail ou senha inválidos. Tente novamente.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm model)
    {
        if (!ModelState.IsValid) return View(model);

        // Inicializamos o Apelido junto com o cadastro
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            NomeCompleto = model.Nome,
            Apelido = model.Nome // Por padrão, o Nick começa sendo o primeiro nome dele
        };

        var result = await _userManager.CreateAsync(user, model.Senha);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Player");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}