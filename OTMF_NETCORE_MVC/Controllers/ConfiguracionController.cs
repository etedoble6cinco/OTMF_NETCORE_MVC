using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly OTMFContext _context;
        private readonly string con;
        public ConfiguracionController(OTMFContext context, IConfiguration configuration)
        {
            _context = context;
            con = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> Usuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            
            return View(usuarios);
        }
        public IActionResult EstandaresParametros()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerPorcentajeScrapPermitido()
        {

            return _context.ScrapPermitidos != null ?
                Json(new { data = await _context.ScrapPermitidos.ToListAsync() }) :
                Json(new { data = "Vacio" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePorcentajeScrapPermitido(decimal PorcentajeScrapPermitido)
        {
            ScrapPermitido scrapPermitido = new ScrapPermitido();
            scrapPermitido.IdScrapPermitido = 1;
            scrapPermitido.PorcentajeScrapPermitido = PorcentajeScrapPermitido;
            _context.Update(scrapPermitido);
            await _context.SaveChangesAsync();
            return Json(new { data = "ok" });
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerTurnoOt()
        {

            return _context.TurnoOts != null ?
                Json(new { data = await _context.TurnoOts.ToListAsync() }) :
                Json(new { data = "Vacio" });

        }
        [HttpPost]
        public async Task<IActionResult> UpdatePrimerTurnoOt( string NombreTurno,decimal HorasTrabajadas)
        {

            TurnoOt turnoOt = new TurnoOt();
            turnoOt.IdTurnoOt = 1;
            turnoOt.NombreTurno = NombreTurno;
            turnoOt.HorasTrabajadas = HorasTrabajadas;
            _context.Update(turnoOt);
             await _context.SaveChangesAsync();
            return Json(new { data = "ok" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSegundoTurnoOt( string NombreTurno, decimal HorasTrabajadas)
        {
            TurnoOt turnoOt = new TurnoOt();
            turnoOt.IdTurnoOt = 2;
            turnoOt.NombreTurno = NombreTurno;
            turnoOt.HorasTrabajadas = HorasTrabajadas;
            _context.Update(turnoOt);
            await _context.SaveChangesAsync();

            return Json(new { data = "ok" }); 
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerFraccionEstandarRelevo()
        {
            return _context.FraccionEstandarRelevos != null ?
                Json(new { data = await _context.FraccionEstandarRelevos.ToListAsync() }) :
                Json(new { data = "Vacio" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFraccionEstandarRelevo(decimal FracEstandarRelevo)
        {
            FraccionEstandarRelevo fraccionEstandarRelevo = new FraccionEstandarRelevo();
            fraccionEstandarRelevo.IdFraccionEstandarRelevo = 1;
            fraccionEstandarRelevo.FracEstandarRelevo = FracEstandarRelevo;
            _context.Update(fraccionEstandarRelevo);
            await _context.SaveChangesAsync();
            return Json(new { data = "ok" });
        }
        [HttpPost]
        public JsonResult ObtenerRolesUsuariosByIdUsuario(int IdUsuario){
           var procedure = "ObtenerRolesUsuarioByIdUsuario";
                  using (var connection = new SqlConnection(con))
            {
                var RolesUsuarios = connection.Query(procedure, new
                {
                    IdUsuario = IdUsuario

                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = RolesUsuarios });
            } 
        }
        [HttpPost]
        public JsonResult UpsertRolesUsuariosByIdUsuario(int IdUsuario, int Administrador ,
         int Supervisor , int Operador) {

            var procedure = "UpsertRolesUsuariosByIdUsuario";
            using(var connection = new SqlConnection(con)){
                var confirm = connection.Query(procedure, new{
                        IdUsuario     = IdUsuario,
                        Administrador = Administrador,
                        Supervisor    = Supervisor,
                        Operador      = Operador
                }, commandType: CommandType.StoredProcedure);
                return  Json(new {data = confirm});
            }
                
        }
    

    }
}
