using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Hubs;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    [Authorize( Policy = "RequireAmin")]
    public class OrdenTrabajoesController : Controller
    {
        private readonly OTMFContext _context;
        private readonly string con;
        private readonly string connectionString;
        DashboardHub dashboardHub;
        public OrdenTrabajoesController(DashboardHub dashboardHub,OTMFContext context ,IConfiguration configuration)

        {
            this.dashboardHub = dashboardHub;
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            con = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: OrdenTrabajoes
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.OrdenTrabajos.Include(o => o.IdEstadoOrdenFkNavigation).Include(o => o.IdInstructivoFkNavigation).Include(o => o.IdParteFkNavigation);
            return View(await oTMFContext.ToListAsync());
        }
        [HttpPost]
        public  JsonResult ObtenerMaquinaById(int IdMaquina)
        {
            var data = "no se encontro";
            if (IdMaquina == null || _context.OrdenTrabajos == null)
            {
               
                return Json(data);
            }
            var maquina = _context.Maquinas.FirstOrDefault(m => m.IdMaquina == IdMaquina);
            if (maquina == null)
            {

                return Json(data);
            }
            return Json(new { data = maquina });
        }


        // GET: OrdenTrabajoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenTrabajos == null)
            {
                return NotFound();
            }

            var ordenTrabajo = await _context.OrdenTrabajos
                .Include(o => o.IdEstadoOrdenFkNavigation)
                .Include(o => o.IdInstructivoFkNavigation)
                .Include(o => o.IdParteFkNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenTrabajo == id);
            if (ordenTrabajo == null)
            {
                return NotFound();
            }

            return View(ordenTrabajo);
        }

        // GET: OrdenTrabajoes/Create
        public IActionResult Create()
        {
            ViewData["IdEstadoOrdenFk"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "NombreEstadoOrden");
            ViewData["IdInstructivoFk"] = new SelectList(_context.Instructivos, "IdInstructivo", "NombreInstructivo");
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdCodigoParte");
            ViewData["IdTurnoOtFk"] = new SelectList(_context.TurnoOts, "IdTurnoOt", "NombreTurno");
            return View();
        }
    
        // POST: OrdenTrabajoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleadoMoldeadorFk,IdEmpleadoEmpacadorFk,IdOrdenTrabajo,IdMaquinaFk,FechaOrdenTrabajo,IdParteFk,CantidadPiezasPororden,CajasRecibidas,PiezasRealizadas,IdInstructivoFk,HoraInicio,HoraFinalizacion,IdEmpeadoSupervisorFk,IdEstadoOrdenFk,EtiquetaDeCaja,IdEstandarConRelevoFk,IdEstandarPorHoraFk,MaxScrap,IdCodigoOrdenTrabajo,Otespecial,IdTurnoOtFk,NumeroCabidadesPieza")] OrdenTrabajo ordenTrabajo)
        {
            if (ModelState.IsValid)
            {

                if(ordenTrabajo.Otespecial == false){
                    // AGREGAR PREFIJO A LA ORDEN DE TRABAJO 
                    var prefix = _context.PrefixOts.FirstOrDefault(m => m.IdPrefixOt == 1);
                    ordenTrabajo.IdCodigoOrdenTrabajo =   prefix.NombrePrefix + ordenTrabajo.IdCodigoOrdenTrabajo;
                   
                }
                    var turnoOt = await _context.TurnoOts
                     .FirstOrDefaultAsync(m => m.IdTurnoOt == ordenTrabajo.IdTurnoOtFk);
                    var fraccionEstandarRelevo = await _context.FraccionEstandarRelevos.FirstOrDefaultAsync(x => x.IdFraccionEstandarRelevo
                    == 1);
                    var PorcentajeScrapPermitido = await _context.ScrapPermitidos.FirstOrDefaultAsync(y => y.IdScrapPermitido
                    == 1);
                    var Parte = await _context.Partes.FirstOrDefaultAsync(g => g.IdParte == ordenTrabajo.IdParteFk);
                    var EstandarPorHoras = await _context.EstandarPorHoras.FirstOrDefaultAsync(d => d.IdEstandarPorHora == Parte.IdEstandarPorHoraFk);
                    ordenTrabajo.ScrapCalculado = CalularScrap((decimal)EstandarPorHoras.NombreEstandarPorHora, (decimal)turnoOt.HorasTrabajadas, (decimal)PorcentajeScrapPermitido.PorcentajeScrapPermitido);
                    ordenTrabajo.EstandarCalculado = CalcularEstandar((decimal)EstandarPorHoras.NombreEstandarPorHora, (decimal)turnoOt.HorasTrabajadas);
                    ordenTrabajo.EstandarConRelevoCalculado = CalcularEstandarConRelevo((decimal)EstandarPorHoras.NombreEstandarPorHora, (decimal)turnoOt.HorasTrabajadas, (decimal)fraccionEstandarRelevo.FracEstandarRelevo);
                    ordenTrabajo.EstandarPorHorasCalculado = (decimal)EstandarPorHoras.NombreEstandarPorHora;
                    ordenTrabajo.HorasTrabajadasCalculado = (decimal)turnoOt.HorasTrabajadas;
                    ordenTrabajo.PorcentajeScrapCalculado = (decimal)PorcentajeScrapPermitido.PorcentajeScrapPermitido;
                    ordenTrabajo.FracEstandarConRelevo = (decimal)fraccionEstandarRelevo.FracEstandarRelevo;
                    ordenTrabajo.IdInstructivoFk = 3;   
                _context.Add(ordenTrabajo);
             
                await _context.SaveChangesAsync();
                //SE CREA LA RELACION DE LA TABLA DE CAJAS RECIBIDAS , YA QUE LA TABLA NO ESTA RELACIONADA CON FK , SI NO SOLAMENTE CON UN ID "POSTIZO"
                using (var connection = new SqlConnection(connectionString))
                {
                    var procedure = "[InsertCajasRecibidas]";
                    var confirm = connection.Query(procedure, new
                    {
                        IdOrdenTrabajo = ordenTrabajo.IdOrdenTrabajo,
                        NumeroCajasRecibidas = 0,
                        NumeroPiezasRecibidas = 0
                    }, commandType: CommandType.StoredProcedure);
                }
               
                return RedirectToAction(nameof(Index));
            }
         
            ViewData["IdEstadoOrdenFk"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "NombreEstadoOrden", ordenTrabajo.IdEstadoOrdenFk);
            ViewData["IdInstructivoFk"] = new SelectList(_context.Instructivos, "IdInstructivo", "NombreInstructivo", ordenTrabajo.IdInstructivoFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdCodigoParte", ordenTrabajo.IdParteFk);
            return View(ordenTrabajo);
        }
        public decimal CalularScrap(decimal estandarPorHora , decimal horasTrabajadas , decimal porcentajeScrapPermitido)
        {
            decimal result = (estandarPorHora * horasTrabajadas);
            return result * porcentajeScrapPermitido;
        }
        public decimal CalcularEstandar(decimal estandarPorHora , decimal horasTrabajadas)
        {
            return estandarPorHora * horasTrabajadas;
        }
        public decimal CalcularEstandarConRelevo(decimal estandarPorHora , decimal horasTrabajadas , decimal fracEstandarConRelevo)
        {
            decimal result = horasTrabajadas + fracEstandarConRelevo;
            return result * estandarPorHora;
        }
        // GET: OrdenTrabajoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenTrabajos == null)
            {
                return NotFound();
            }

            var ordenTrabajo = await _context.OrdenTrabajos.FindAsync(id);
            if (ordenTrabajo == null)
            {
                return NotFound();
            }
            using (var connection = new SqlConnection(connectionString))
            {
                var OE = "[ObtenerEmpleadoConcatType]";
                var empleados = connection.Query<ObtenerEmpleadoConcatType>(OE,
               commandType: CommandType.StoredProcedure);
                ViewData["Empleado"] = new SelectList(empleados, "IdEmpleado", "NombreEmpleado");
            }
            ViewData["IdEstadoOrdenFk"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "NombreEstadoOrden", ordenTrabajo.IdEstadoOrdenFk);
            ViewData["IdInstructivoFk"] = new SelectList(_context.Instructivos, "IdInstructivo", "NombreInstructivo", ordenTrabajo.IdInstructivoFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdCodigoParte", ordenTrabajo.IdParteFk);
            ViewData["Maquinas"] = new SelectList(_context.Maquinas, "IdMaquina", "NombreMaquina");
            return View(ordenTrabajo);
        }

         
        [HttpPost]
        public JsonResult UpdateOrdenTrabajo (int IdOrdenTrabajo ,int IdParteFK , int CantidadPiezasPorOrden , int IdInstructivoFK  , int IdEstadoOrdenFK , int NumeroCabidadesPieza)
        {
            IdInstructivoFK = 3;
            using(var connection  =  new SqlConnection(connectionString))
            {
                var procedure = "[UpdateOrdenTrabajo]";

                var confirm = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo , 
                    IdParteFK = IdParteFK , 
                    CantidadPiezasPorOrden = CantidadPiezasPorOrden ,
                    IdInstructivoFK = IdInstructivoFK ,
                    IdEstadoOrdenFK = IdEstadoOrdenFK ,
                    NumeroCabidadesPieza = NumeroCabidadesPieza

                } ,commandType: CommandType.StoredProcedure);
                return Json( new { data = confirm});
            }


        }


        // GET: OrdenTrabajoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenTrabajos == null)
            {
                return NotFound();
            }

            var ordenTrabajo = await _context.OrdenTrabajos
                .Include(o => o.IdEstadoOrdenFkNavigation)
                .Include(o => o.IdInstructivoFkNavigation)
                .Include(o => o.IdParteFkNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenTrabajo == id);
            if (ordenTrabajo == null)
            {
                return NotFound();
            }

            return View(ordenTrabajo);
        }

        // POST: OrdenTrabajoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenTrabajos == null)
            {
                return Problem("Entity set 'OTMFContext.OrdenTrabajos'  is null.");
            }
            var ordenTrabajo = await _context.OrdenTrabajos.FindAsync(id);
            if (ordenTrabajo != null)
            {
                _context.OrdenTrabajos.Remove(ordenTrabajo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenTrabajoExists(int id)
        {
          return (_context.OrdenTrabajos?.Any(e => e.IdOrdenTrabajo == id)).GetValueOrDefault();
        }


        public JsonResult ObtenerOrdenesTrabajo()
        {
            var OOT = "[ObtenerOrdenesTrabajoAllInfo]";
            using (var connection = new SqlConnection(con))
            {
                var ordenestrabajo = connection.Query<ObtenerOrdenesTrabajo>(OOT,commandType: CommandType.StoredProcedure);

                return Json(new { data = ordenestrabajo });
            } 
                
               
        }
     
        [HttpGet]
        public JsonResult ObtenerUltimoIdOT()
        {
            var procedure = "[ObtenerUltimoIdOT]";
            using (var connection  =  new SqlConnection(con))
            {
                var id = connection.Query(procedure,commandType: CommandType.StoredProcedure);

                return Json(new {data = id});
            }
        }
        [HttpPost]
        public JsonResult ObtenerMaquinasAsignadasByOTId(int IdOrdenTrabajo)
        {
            var procedure = "[ObtenerMaquinasAsignadasByOTId]";
            using (var connection = new SqlConnection(connectionString))
            {
                var Maquinas = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = Maquinas });
            };
        }
        [HttpPost]
        public JsonResult UpsertMaquinasAsignadasByOTId( int IdMaquinaOrdenTrabajo, int IdOrdenTrabajo , int IdMaquina)
        {
            var procedure = "[UpsertMaquinasAsignadasByOTId]";
            using(var connection = new SqlConnection(connectionString))
            {
                var confirm = connection.Query(procedure, new
                {
                    IdMaquinaOrdenTrabajo = IdMaquinaOrdenTrabajo,
                    IdOrdenTrabajo = IdOrdenTrabajo,
                    IdMaquina = IdMaquina
                      
                }, commandType: CommandType.StoredProcedure);
                dashboardHub.SendOrdenTrabajo();

                return Json(new { data = confirm });
            }
        }

        [HttpPost]
        public JsonResult DeleteMaquinasAsignadasByOTId(int IdMaquinaOrdenTrabajo)
        {
            var procedure = "[DeleteMaquinasAsignadasByOTId]";
            using (var connection = new SqlConnection(connectionString))
            {
                var confirm = connection.Query(procedure, new
                {
                    IdMaquinaOrdenTrabajo = IdMaquinaOrdenTrabajo

                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirm });
            } 
        }
        [HttpPost]
        public JsonResult ObtenerOrdenesTrabajoDetallesById(int IdOrdenTrabajo )
        {
            var procedure = "[ObtenerOrdenesTrabajoDetallesById]";
            using (var connection = new SqlConnection(connectionString))
            {
                var OrdenTrabajo = connection.Query(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                return Json(new {data = OrdenTrabajo});
            }
        }
        [HttpGet]
        public JsonResult ObtenerPrefixOT()
        {
            var prefix = _context.PrefixOts.FirstOrDefault(m => m.IdPrefixOt == 1);
            return Json(new { data = prefix });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePrefixOT(string prefixOt)
        {
            PrefixOt prefix = new PrefixOt();

            prefix.IdPrefixOt = 1;
            prefix.NombrePrefix = prefixOt;
            _context.Update(prefix);
            await _context.SaveChangesAsync();
            return Json(new {data = "Guardado"});
        }
        [HttpPost]
        public JsonResult ObtenerOrdenTrabajoByDateRange(string dateStart , string dateEnd)
        {
            var procedure = "[ObtenerOrdenTrabajoByDateRange]";
            using(var connection  =  new SqlConnection(connectionString))
            {
                var OrdenTrabajo = connection.Query(procedure, new
                {
                    start = dateStart,
                    end = dateEnd
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = OrdenTrabajo });
            }

            
        }

        [HttpPost]

        public JsonResult ObtenerOrdenTrabajoByOtCode(string IdClaveOrdenTrabajo)
        {
            var procedure = "[ObtenerOrdenesTrabajoDetallesByOtCode]";
            using (var connection = new SqlConnection(connectionString))
            {
                var OrdenTrabajo = connection.Query(procedure, new
                {
                    IdOtCode = IdClaveOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = OrdenTrabajo });
            }
             
        }
       
        [HttpPost]
        public JsonResult ObtenerOrdenesTrabajoDetallesByState(int EstadoOrden)
        {
            var procedure = "[ObtenerOrdenesTrabajoDetallesByState]";
            using (var connection = new SqlConnection(connectionString))
            {
                var OrdenTrabajo = connection.Query(procedure, new
                {
                    IdEstadoOrden = EstadoOrden   
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = OrdenTrabajo });
            }
            
        }


    }
}
