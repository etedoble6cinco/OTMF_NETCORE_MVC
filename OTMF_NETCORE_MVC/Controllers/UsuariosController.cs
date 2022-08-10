using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Services;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        private SignInManager<Usuario> signInManager;
        private readonly string con;
        private readonly IServicioUsuarios servicio;
        public UsuariosController(UserManager<Usuario> userManager,
            SignInManager<Usuario>signInManager ,
            IConfiguration configuration,
           IServicioUsuarios _services)
        {
            con = configuration.GetConnectionString("DefaultConnection");
            this.signInManager = signInManager;
            this.userManager = userManager;
            servicio = _services;
      

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
                var user =  await userManager.FindByEmailAsync(modelo.Email);   
                var procedure = "[ObtenerRolUsuarioByIdUsuario]";
                using (var connection = new SqlConnection(con))
                {
                    var usuario = connection.Query<RolesUsuarios>(procedure, new
                    {
                        IdUsuario = user.IdUsuarios
                    }, commandType: CommandType.StoredProcedure).ToList();

                  

                    var claims = new List<Claim>();

                    foreach (var role in usuario)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.NombreRolUsuario));
                    }

                    await signInManager.SignInWithClaimsAsync(user, modelo.RememberMe, claims);
                }
             
                return RedirectToAction("CheckRole", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto.");
                return View(modelo);
            }
        }
        [AllowAnonymous]
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
