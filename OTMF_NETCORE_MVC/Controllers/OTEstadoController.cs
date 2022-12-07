using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Services;
using System.Data;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class OTEstadoController : Controller
    {
        private readonly OTMFContext _context;
        private readonly IServicioUsuarios _services;
        private readonly string connectionString;
        public OTEstadoController(OTMFContext context, IConfiguration configuration , IServicioUsuarios servicioUsuario)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            _services = servicioUsuario;
        }
        // GET: OTEstadoController
        public async Task<IActionResult> Index()
        {
            
            var procedure = "ObtenerRelacionMaquinasUsuarios";
            using (var connection = new SqlConnection(connectionString))
            {
               var UsuarioMaquinaRes =
                    await connection.QueryAsync<ObtenerRelacionMaquinasUsuarios>(procedure,
                    new
                    {
                        IdUsuarioFK = _services.ObtenerUsuarioId()
                    }, commandType: CommandType.StoredProcedure);
                return _context.Maquinas != null ?
                      View(UsuarioMaquinaRes) :
                      Problem("Entity set 'OTMFContext.Insertos'  is null.");
            }

         
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
                var empleados = connection.Query<ObtenerEmpleadoConcatType>(OE,commandType: CommandType.StoredProcedure);
                var procedure = "[ObtenerOTAsignadas]";
                var ot = connection.Query<ObtenerOTAsignadas>(procedure, new { idMaquina = id }, commandType: CommandType.StoredProcedure);
                ViewData["Empleado"] = new SelectList(empleados, "IdEmpleado", "NombreEmpleado");
                ViewData["EstadoOrden"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "NombreEstadoOrden");
                ViewData["MotivoCambioEstado"] = new SelectList(_context.MotivoCambioEstados, "IdMotivoCambioEstado", "NombreMotivoCambioEstado");
                ViewData["MotivoCambioEstadoDerivados"] = new SelectList(_context.MotivoCambioEstadoDerivados, "IdMotivoCambioEstadoDerivados", "MotivoCambioEstadoDerivadosNombre");
               
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
        //METODO PARA OBTENER EMPLEADO BY ORDEN DE TRABAJO ID
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
        //METODO PARA ACTUALIZAR E INSERTAR ASIGNACION DEL EMPLEADO 
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
        // METODO PARA ACTULIZAR EL ESTADO DE LA ORDEN DE TRABAJO 
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
        // METODO PARA ELIMINAR LA ASIGNACION DEL EMPLEADO 
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
        // METODO PARA OBTENER ESTADO DE LA ORDEN DE TRABAJO
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
        // METODO PARA INSERTAR LAS CAJAS RECIBIDAS
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
        // METODO PARA ACTUALIZAR LAS CAJAS RECIBIDAS 
        [HttpPost]
        public JsonResult UpdateCajasRecibidas(int IdDetalleCajasRecibidas , int NumeroCajasRecibidas , int NumeroPiezasRecibidas)
        {
            var procedure = "[UpdateCajasRecibidas]";
            using (var  connection  =  new SqlConnection(connectionString))
            {
                var confirm = connection.Query(procedure, new
                {
                    IdDetalleCajasRecibidas = IdDetalleCajasRecibidas,
                    NumeroCajasRecibidas = NumeroCajasRecibidas,
                    NumeroPiezasRecibidas = NumeroPiezasRecibidas
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirm });
            }
        }
        //METODO PARA OBTENER LAS CAJAS RECIBIDAS
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
        //METODO PARA OBTENER LAS PIEZAS REALIZADAS
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
        //METODO PARA ACTUALIZAR LAS PIEZAS REALIZADAS 
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
        //METODO PARA ACTUALIZAR FECHA FINALIZACION
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
        // METODO PARA OBTENER FECHA FINALIZACION
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
        // METODO PARA ACTUALIZAR FECHA DE INICIO
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
        // METODO PARA OBTENER FECHA DE INICIO DE LA ORDEN TRABAJO
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
        // METODO PARA OBTENER ID DE LA PARTE POR MEDIO DEL ID DE LA ORDEN DE TRABAJO
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
        // METODO PARA OBTENER ACCESORIOS POR MEDIO DEL ID DE LA PARTE 
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
        [HttpPost]
        public JsonResult GetIdentificadorOT( int id)
        {

           var ot = _context.OrdenTrabajos.FirstOrDefaultAsync(m => m.IdOrdenTrabajo == id).Result;

            return Json(new { ot });
                
        }
        //REGISTRAR DURACION ESTADO
        [HttpPost] 
     
        public JsonResult RegistrarDuracionEstado (
            int TipoMovimientoEstado,
            int IdOrdenTrabajoFK,
            int IdMotivoCambioEstadoFK,  
            int IdEstadoOrdenTrabajoFK,
            int IdBitacoraOrdenTrabajoFK

            )
        {
           
            var procedure = "[InsertDuracionEstado]";
            using(var connection = new SqlConnection(connectionString))
            {
                var confirm = connection.Query(procedure, new
                {
                   IdOrdenTrabajoFK = IdOrdenTrabajoFK,
                   IdEstadoOrdenFK = IdEstadoOrdenTrabajoFK,
                   IdMotivoCambioEstadoFK = IdMotivoCambioEstadoFK,
                   TipoMovimientoEstado =  TipoMovimientoEstado,
                   IdBitacoraOrdenTrabajoFK = IdBitacoraOrdenTrabajoFK
                 
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirm });
            }
        }

        [HttpPost]
        public JsonResult ObtenerTiemposMuertos (int IdOrdenTrabajo)
        {
            List<TiempoEstadosOrdenTrabajo> tiempoEstadosOrdenTrabajos = new List<TiempoEstadosOrdenTrabajo>(); 
            var procedure = "[ObtenerTiemposMuertos]";
            using(var connection = new SqlConnection(connectionString))
            {
                var data = connection.Query<ObtenerTiempoMuertos>(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure).ToList();

                int ActivaSegundos = 0;
                int ParaLiberarSegundos = 0;
                int DetenidaSegundos = 0;
                for( int x = 0; x <data.Count; x++ )
                {
                    if (data[x].NombreEstadoOrden.Equals("ACTIVA"))
                    {
                        ActivaSegundos = ActivaSegundos + data[x].Duracion;
                    }
                    if (data[x].NombreEstadoOrden.Equals("PARA LIBERAR"))
                    {
                        ParaLiberarSegundos = ParaLiberarSegundos + data[x].Duracion;
                    }
                    if (data[x].NombreEstadoOrden.Equals("PAUSADA"))
                    {
                        DetenidaSegundos = DetenidaSegundos + data[x].Duracion;
                    }
                }

                TiempoEstadosOrdenTrabajo DuracionActiva = new TiempoEstadosOrdenTrabajo();
                DuracionActiva.DuracionEstado = ActivaSegundos;
                DuracionActiva.NombreEstado = "ACTIVA";
                
                TiempoEstadosOrdenTrabajo DuracionParaLiberar = new TiempoEstadosOrdenTrabajo();
                TiempoEstadosOrdenTrabajo DuracionDetenida = new TiempoEstadosOrdenTrabajo();
                DuracionParaLiberar.DuracionEstado = ParaLiberarSegundos;
                DuracionParaLiberar.NombreEstado = "POR LIBERAR";
                DuracionDetenida.DuracionEstado = DetenidaSegundos;
                DuracionDetenida.NombreEstado = "PAUSADA";
                tiempoEstadosOrdenTrabajos.Add(DuracionDetenida);
                tiempoEstadosOrdenTrabajos.Add(DuracionParaLiberar);
                tiempoEstadosOrdenTrabajos.Add(DuracionActiva);

                return Json(new { details = data,
                    total =  tiempoEstadosOrdenTrabajos  });
            }
        }


        [HttpPost]

        public async Task<IActionResult> UpdateNumeroCabidades(int IdOrdenTrabajo, int numCab)
        {

            var OrdenTrabajo = await _context.OrdenTrabajos.FindAsync(IdOrdenTrabajo);
            OrdenTrabajo.NumeroCabidadesPieza = numCab;
            try
            {

                _context.Update(OrdenTrabajo);
                await _context.SaveChangesAsync();

                return Json(new { data = OrdenTrabajo });


            }
            catch (Exception e)
            {
                return Json(new { data = e.Message.ToString() });
            }



        }

        [HttpPost]
        public async Task<IActionResult> ObtenerRelacionMaquinasUsuarios()
        {
            var procedure = "ObtenerRelacionMaquinasUsuarios";
            using (var connection = new SqlConnection(connectionString))
            {
                var UsuarioMaquinaRes =
                     await connection.QueryAsync<ObtenerRelacionMaquinasUsuarios>(procedure,
                     new
                     {
                         IdUsuarioFK = _services.ObtenerUsuarioId()
                     }, commandType: CommandType.StoredProcedure);
                return Json( new { data = UsuarioMaquinaRes} );
            }
        }
        [HttpPost]
        public JsonResult ObtenerUsuario()
        {
            var IdUsuarioFK =  _services.ObtenerUsuarioId();
            return Json(new { data = IdUsuarioFK });
        }
        [HttpPost]
        public async Task<IActionResult> ObtenerOrdenTrabajo( int IdOrdenTrabajo)
        {
            var OrdenTrabajo = await _context.OrdenTrabajos.FindAsync(IdOrdenTrabajo);

            return Json(new { data = OrdenTrabajo });
        }


    }
}
