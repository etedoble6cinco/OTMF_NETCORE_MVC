using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [HttpPost]

        public async Task<IActionResult> ObtenerMaquinas()
        {
            return Json(new{data = await _context.Maquinas.ToListAsync()});
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerRelacionMaquinasUsuarios( int IdUsuarioFK)
        {
            var procedure = "ObtenerRelacionMaquinasUsuarios";
            using (var connection = new SqlConnection(con)) {
                var UsuarioMaquina = 
                    await connection.QueryAsync(procedure, 
                    new
                { 
                        IdUsuarioFK = IdUsuarioFK 
               }, commandType: CommandType.StoredProcedure);
                return Json(new { data = UsuarioMaquina });
            } 
        }
        [HttpPost]
        public async Task<IActionResult> InsertRelacionMaquinasUsuarios(
            int IdUsuarioFK , 
            int IdMaquinaFK , 
            string NombreMaquina)
        {
            UsuarioMaquina u = new UsuarioMaquina();
            Maquina maquina = new Maquina();    
            //u.IdUsuarioMaquina = 0;
            u.IdUsuarioFk = IdUsuarioFK;
            u.IdMaquinaFk = IdMaquinaFK;
            u.NombreUsuarioMaquina = "";

            _context.Add(u);
            await _context.SaveChangesAsync();
            try
            {
                maquina.IdMaquina = IdMaquinaFK;
                maquina.EstadoMaquina = false;
                maquina.NombreMaquina = NombreMaquina;
                //ACTUALIZACION DE ESTADO  DE LA MAQUINA  
                _context.Update(maquina);
                await _context.SaveChangesAsync();  
            }catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());   
            }
            
            return Json(new { data = "Insercion Realizada" });  
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRelacionMaquinasUsuarios (int IdUsuariosMaquinas ,
            int IdMaquinaFK, string NombreMaquina)
        {
            try
            {   //eliminando relacion maquina usuario
                if (_context.UsuarioMaquinas == null)
                {
                    return Json(new { data = "No existen elementos" });
                }
                var UsuariosMaquinas = await _context.UsuarioMaquinas.FindAsync(IdUsuariosMaquinas);
                if (UsuariosMaquinas != null)
                {
                _context.UsuarioMaquinas.Remove(UsuariosMaquinas);
                }
                await _context.SaveChangesAsync();
                //desbloqueando maquina tras eliminar relacion , regresando el estado de la maquina a disponible 
                Maquina maquina = new Maquina();
                maquina.IdMaquina = IdMaquinaFK;
                maquina.EstadoMaquina = true;
                maquina.NombreMaquina = NombreMaquina;
                _context.Update(maquina);
                await _context.SaveChangesAsync();
                
                return Json(new { data = "" }); 
            }catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return null; 
        }


    }
}
