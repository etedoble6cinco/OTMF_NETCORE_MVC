using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class OTEstadoController : Controller
    {
        private readonly OTMFContext _context;

        private readonly string connectionString;
        public OTEstadoController(OTMFContext context ,IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }
        // GET: OTEstadoController
        public async Task<IActionResult> Index()
        {
            return _context.Maquinas != null ?
                        View(await _context.Maquinas.ToListAsync()) :
                        Problem("Entity set 'OTMFContext.Insertos'  is null.");
        }
        public JsonResult ObtenerMaquinas()
        {


            var maquinas = _context.Maquinas.ToList();

                return Json(new { data = maquinas });
            
        }

        //METODO PARA OBTENER DETALLES DE LAS ORDENES DE TRABAJO  
        public ActionResult OrdenesDeTrabajoAsignadas(int? id)
        {
            using( var connection = new SqlConnection(connectionString))
            {

                var procedure = "[ObtenerOTAsignadas]";
                 var ot = connection.Query<ObtenerOTAsignadas>(procedure, new { idMaquina = id }, commandType: CommandType.StoredProcedure);
                return View(ot.ToList());
            }


           

       
        }
        [HttpPost]
        public JsonResult DetalleDeOT(int id)
        {
            using( var connection = new SqlConnection(connectionString))
            {
                var procedure = "[ObtenerOTbyId]";
                var ot = connection.Query<ObtenerOrdenesTrabajo>(procedure, new { idOrdenDeTrabajo = id }, commandType: CommandType.StoredProcedure).ToList();

                return Json(new { data = ot });
            }



          
        }

    }
}
