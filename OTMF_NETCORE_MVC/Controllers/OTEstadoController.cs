using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public OTEstadoController(OTMFContext context, IConfiguration configuration)
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


            using (var connection = new SqlConnection(connectionString))
            {
                var OE = "[ObtenerEmpleadoConcatType]";

                var empleados = connection.Query<ObtenerEmpleadoConcatType>(OE,
                    commandType: CommandType.StoredProcedure);
                var procedure = "[ObtenerOTAsignadas]";
                var ot = connection.Query<ObtenerOTAsignadas>(procedure, new { idMaquina = id }, commandType: CommandType.StoredProcedure);
                ViewData["Empleado"] = new SelectList(empleados, "IdEmpleado", "NombreEmpleado");
                ViewData["EstadoOrden"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "NombreEstadoOrden");
                // ViewData["TipoEmpleado"] = new SelectList(_context.TipoEmpleados, "IdTipoEmpleado", "NombreTipoEmpleado");
                return View(ot.ToList());
            }





        }

        //METODO PARA OBTENER EL DETALLE DE LA ORDEN DE TRABAJO 
        [HttpPost]
        public JsonResult DetalleDeOT(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "[ObtenerOTbyId]";
                var ot = connection.Query<ObtenerOrdenesTrabajo>(procedure, new { idOrdenDeTrabajo = id }, commandType: CommandType.StoredProcedure).ToList();

                return Json(new { data = ot });
            }




        }
        //METODO PARA OBTENER LA PARTE DE LA ORDEN DE TRABAJO
        [HttpPost]
        public JsonResult ObtenerParteByOTId(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "[ObtenerParteByOTId]";
                var parte = connection.Query<ObtenerParteByOTId>
                    (procedure, new { idOrdenTrabajo = id }, commandType: CommandType.StoredProcedure);
                return Json(new { data = parte });
            }
        }
        [HttpPost]
        public JsonResult ObtenerEmpleadosByOTId(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "[ObtenerEmpleadoByOTId]";
                var empleados = connection.Query<ObtenerEmpleadoByOTId>
                    (procedure, new { idOrdenTrabajo = id }, commandType: CommandType.StoredProcedure);

                return Json(new { data = empleados });
            }
        }

        [HttpPost]
        public JsonResult UpsertAsignacionEmpleadoOTById(UpsertAsignacionEmpleadoOTById obj)
        {
            var procedure = "[UpsertAsignacionEmpleadoOTById]";
            using (var connection = new SqlConnection(connectionString))
            {
                var empleados = connection.Query(procedure, new
                {
                    idOrdenTrabajo = obj.idOrdenTrabajo,
                    idEmpleado = obj.idEmpleado,
                    idRelacion = obj.idRelacion,

                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = empleados });
            }

        }

        [HttpPost]
        public JsonResult UpdateOTEstado(int idOrdenTrabajo, int idEstadoOT)
        {
            var procedure = "[UpdateOTEstado]";
            using (var connection = new SqlConnection(connectionString))
            {
                var estado = connection.Query(procedure, new
                {
                    idOrdenTrabajo = idOrdenTrabajo,
                    idEstadoOT = idEstadoOT
                }, commandType: CommandType.StoredProcedure);

                return Json(new { data = estado });

            }

        }
        [HttpDelete]
        public JsonResult DeleteAsignacionEmpleadoOTById(int idEmpleadoOrdenTrabajo)
        {
            var procedure = "[DeleteAsignacionEmpleadoOTById]";
            using (var connection = new SqlConnection(connectionString))
            {
                var confirmacion = connection.Query(procedure,
                    new { idEmpleadoOrdenTrabajo = idEmpleadoOrdenTrabajo },
                    commandType: CommandType.StoredProcedure);
                return Json(new { data = confirmacion });
            }

        }
        [HttpPost]
        public JsonResult ObtenerEstadoOT(int id)
        {
            var procedure = "[ObtenerEstadoOT]";
            using (var connection = new SqlConnection(connectionString))
            {
                var estado = connection.Query<ObtenerEstadoOT>(procedure, new { IdOrdenTrabajo = id },
                    commandType: CommandType.StoredProcedure);
                return Json(new { data = estado });
            }
        }
        [HttpPost]
        public JsonResult InsertCajasRecibidas(InsertCajasRecibidas obj)
        {
            var procedure = "[InsertCajasRecibidas]";
            using (var connection = new SqlConnection(connectionString))
            {
                var recibida = connection.Query(procedure,
                new
                {
                    IdOrdenTrabajo = obj.IdOrdenTrabajo,
                    NumeroCajasRecibidas = obj.NumeroCajasRecibidas,
                    NumeroPiezasRecibidas = obj.NumeroPiezasRecibidas
                }
                    , commandType: CommandType.StoredProcedure);
                return Json(new { data = recibida });
            }

        }
        [HttpPost]
        public JsonResult ObtenerCajasRecibidas(int idOrdenTrabajo)
        {
            var procedure = "[ObtenerCajasRecibidas]";
            using (var connection = new SqlConnection(connectionString))
            {
                var recibida = connection.Query(procedure,
                new { idOrdenTrabajo = idOrdenTrabajo }, commandType: CommandType.StoredProcedure);
                return Json(new { data = recibida });
            }
        }

        [HttpPost]
        public JsonResult ObtenerPiezasRealizadas(int idOrdenTrabajo)
        {

            var procedure = "[ObtenerPiezasRealizadas]";
            using (var connection = new SqlConnection(connectionString))
            {

                var piezasRealizadas = connection.Query(procedure,
                 new { IdOrdenTrabajo = idOrdenTrabajo },
                 commandType: CommandType.StoredProcedure);

                return Json(new { data = piezasRealizadas });
            }

        }
        [HttpPost]
        public JsonResult UpdatePiezasRealizadas(int idOrdenTrabajo, int PiezasRealizadas)
        {

            var procedure = "[UpdatePiezasRealizadas]";
            using (var connection = new SqlConnection(connectionString))
            {

                var confirmacion = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = idOrdenTrabajo,
                    PiezasRealizadas = PiezasRealizadas
                },
                        commandType: CommandType.StoredProcedure);
                return Json(new { data = confirmacion });
            }

        }
        [HttpPost]
        public JsonResult UpdateFechaFinalizacion(int IdOrdenTrabajo)
        {
            var procedure = "[UpdateFechaFinalizacionOT]";
            using (var connection = new SqlConnection(connectionString))
            {
                var confirmation = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirmation });
            }
        }
        [HttpPost]
        public JsonResult ObtenerFechaFinalizacion(int IdOrdenTrabajo)
        {
            var procedure = "[ObtenerFechaFinalizacion]";
            using (var connection = new SqlConnection(connectionString))
            {
                var fechafinalizacion = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = fechafinalizacion });
            }
        }
        [HttpPost]
        public JsonResult UpdateFechaInicio(int IdOrdenTrabajo)
        {
            var procedure = "[UpdateFechaInicioOT]";
            using (var connection = new SqlConnection(connectionString))
            {
                var confirmation = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirmation });
            }
        }
        [HttpPost]
        public JsonResult ObtenerFechaInicio(int IdOrdenTrabajo)
        {
            var procedure = "[ObtenerFechaInicio]";
            using (var connection = new SqlConnection(connectionString))
            {
                var fechaInicio = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = fechaInicio });
            }
        }
        [HttpPost]
        public JsonResult ObtenerParteIdByOTId(int IdOrdenTrabajo)
        {
            var procedure = "[ObtenerParteIdByOTId]";
            using (var connection = new SqlConnection(connectionString))
            {
                var IdParte = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);

                return Json(new { data = IdParte });
            }

        }
        [HttpPost]
        public JsonResult ObtenerAccesorioByParteId( int IdParte)
        {
            var procedure = "[ObtenerAccesorioByParteId]";
            using(var connection = new SqlConnection(connectionString))
            {
                var Accesorios = connection.Query(procedure, new
                {
                    IdParte = IdParte
                }, commandType: CommandType.StoredProcedure);
                
                return Json(new { data = Accesorios });
            }
        }
    }
}
