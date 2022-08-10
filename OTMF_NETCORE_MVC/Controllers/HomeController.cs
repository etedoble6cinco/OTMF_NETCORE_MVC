using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OTMFContext _context;
        private readonly string con;
        private readonly IServicioUsuarios _services;
        private readonly UserManager<Usuario> userManager;
        private readonly IClaimsTransformation claims;
    

        public HomeController(ILogger<HomeController> logger, OTMFContext context,
            IConfiguration configuration, IServicioUsuarios servicioUsuarios,
            UserManager<Usuario> userManager, IClaimsTransformation claims)
        {
            con = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
            _context = context;
            _services = servicioUsuarios;
            this.userManager = userManager;
            this.claims = claims;
        }


        public IActionResult Index()//obtener todos los usuarios para despues evaluarlos  
           
        {

          
            
            return View();
        }

        public async Task<IActionResult> CheckRole()
        {
            var estaAutenticado = User.Identity.IsAuthenticated;
            if (estaAutenticado)
            {
          
                var claims2 = User.Claims; //ver 
            }


        
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Privacy()
        {
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//agregar chk box para crear las ordenes de trabajo especiales y generar el prefijo 