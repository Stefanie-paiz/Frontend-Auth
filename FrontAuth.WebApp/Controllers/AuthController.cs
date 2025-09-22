using FrontAuth.WebApp.DTOs;
using FrontAuth.WebApp.DTOs.UsuarioDTOs;
using FrontAuth.WebApp.Helpers;
using FrontAuth.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontendAuth.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private const string AuthScheme = "AuthCookie";
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // GET: Mostrar Login (público)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login (público)
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View();
            }

            var principal = ClaimsHelper.CrearClaimsPrincipal(result);
            await HttpContext.SignInAsync(AuthScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        // GET: Mostrar Registro (público)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Registro (público)
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(UsuarioRegistroDTO dto)
        {
            var result = await _authService.RegistrarAsync(dto);
            if (result == null || result.Id <= 0)
            {
                ViewBag.Error = "Error al registrar";
                return View();
            }

            var principal = ClaimsHelper.CrearClaimsPrincipal(result);
            await HttpContext.SignInAsync(AuthScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        // Logout (requiere sesión)
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AuthScheme);
            return RedirectToAction("Login");
        }
    }
}