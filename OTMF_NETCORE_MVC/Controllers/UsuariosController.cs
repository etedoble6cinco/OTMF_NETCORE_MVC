using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        private SignInManager<Usuario> signInManager;   
        public UsuariosController(UserManager<Usuario> userManager,
            SignInManager<Usuario>signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Registro()
        {
           
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task <IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);

            }
            var resultado = await signInManager.PasswordSignInAsync(modelo.Email,
                modelo.Password, modelo.RememberMe, lockoutOnFailure:false);
            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto.");
                return View(modelo);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);    
            }
            var usuario  =  new Usuario() { Email = modelo.Email };

            var resultado = await userManager.CreateAsync(usuario , password :modelo.Password);

            if (resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(modelo);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");

        }
    }
}
