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

        [HttpPost]
        public async Task<IActionResult> UpsertCantidadMeta (int CantidadMeta)
        {
            var meta = await _context.Meta.FirstOrDefaultAsync(m => m.IdMeta == 1);
            meta.CantidadMeta = CantidadMeta;
            _context.Update(meta);
            await _context.SaveChangesAsync();
            return Json( new { data = ""});
        }

        [HttpGet]
        public async Task<int> ObtenerTotalPiezas()
        {
            var procedure = "[ObtenerSumTotalProduccion]";
            using (var connection = new SqlConnection(con))
            {
                int TotalPiezas = await connection.QueryFirstAsync<int>(procedure,  commandType: CommandType.StoredProcedure);
                return TotalPiezas;
            }
               
        }
        [HttpGet]
        public async Task<int> ObtenerCantidadMeta()
        {

            var meta = await _context.Meta.FirstOrDefaultAsync( m => m.IdMeta == 1);
            return (int)meta.CantidadMeta;
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