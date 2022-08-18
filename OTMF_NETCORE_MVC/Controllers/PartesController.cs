using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class PartesController : Controller
    {
        private readonly OTMFContext _context;
        private readonly string con;
        public PartesController(OTMFContext context , IConfiguration configuration)
        {
            _context = context;
            con = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: Partes
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.Partes.Include(p => 
            p.IdCajaFkNavigation).Include(p => 
            p.IdClienteFkNavigation).Include(p => 
            p.IdColorFkNavigation).Include(p => 
            p.IdEnsambleFkNavigation).Include(p => 
            p.IdEstandarConRelevoFkNavigation).Include(p => 
            p.IdEstandarFkNavigation).Include(p => 
            p.IdEtiquetaFkNavigation).Include(p => 
            p.IdHuleFkNavigation).Include(p => 
            p.IdInsertoFkNavigation).Include(p => 
            p.IdMoldeFkNavigation).Include(p => 
            p.IdPinturaFkNavigation).Include(p => 
            p.IdTarimaFkNavigation);
            return View(await oTMFContext.ToListAsync());
        }

        // GET: Partes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Partes == null)
            {
                return NotFound();
            }

            var parte = await _context.Partes
                .Include(p => p.IdCajaFkNavigation)
                .Include(p => p.IdClienteFkNavigation)
                .Include(p => p.IdColorFkNavigation)
                .Include(p => p.IdEnsambleFkNavigation)
                .Include(p => p.IdEstandarConRelevoFkNavigation)
                .Include(p => p.IdEstandarFkNavigation)
                .Include(p => p.IdEtiquetaFkNavigation)
                .Include(p => p.IdHuleFkNavigation)
                .Include(p => p.IdInsertoFkNavigation)
                .Include(p => p.IdMoldeFkNavigation)
                .Include(p => p.IdPinturaFkNavigation)
                .Include(p => p.IdTarimaFkNavigation)
                .FirstOrDefaultAsync(m => m.IdParte == id);
            if (parte == null)
            {
                return NotFound();
            }

            return View(parte);
        }

        // GET: Partes/Create
        public IActionResult Create()
        {
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "NombreCaja");
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente");
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "NombreColor");
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "NombreEnsamble");
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "NombreEstandarconRelevo");
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "NombreEstandar");
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "NombreEtiqueta");
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "NombreHule");
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "NombreInserto");
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "NombreMolde");
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "NombrePintura");
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "NombreTarima");
            ViewData["Accesorios"] = new SelectList(_context.Accesorios, "IdAccesorios", "NombreAccesorios");
            ViewData["IdInstructivoPiezaFk"] = new SelectList(_context.InstructivoPiezas, "IdInstructivoPieza", "NombreInstructivoPieza");
            ViewData["IdEtiquetaCajaFK"] = new SelectList(_context.EtiquetaCajas, "IdEtiquetaDeCaja", "NombreEtiquetaDeCaja");
            ViewData["IdEstandarPorHoraFK"] = new SelectList(_context.EstandarPorHoras, "IdEstandarPorHora", "NombreEstandarPorHora");
            return View();
        }

        // POST: Partes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParte," +
            "IdCodigoParte," +
            "IdTarimaFk," +
            "Aluminio," +
            "IdEnsambleFk," +
            "IdParteAccesorioFk," +
            "IdEstandarFk," +
            "IdEtiquetaFk," +
            "PiezasPorCaja," +
            "IdColorFk," +
            "CajasPorTarima," +
            "StdPintura," +
            "Costo," +
            "EstandarPorHora," +
            "IdCajaFk," +
            "IdHuleFk," +
            "IdPinturaFk," +
            "IdInsertoFk," +
            "IdMoldeFk," +
            "IdClienteFk," +
            "Scrap," +
            "IdEstandarConRelevoFk," +
            "IdInstructivoPiezaFk," +
            "IdEtiquetaCajaFk," +
            "IdEstandarPorHoraFk")] Parte parte)
        {
            if (ModelState.IsValid)
            {
                   
             
                    parte.IdEstandarFk = 2;
                
                    parte.StdPintura = 0; 
                
                    parte.EstandarPorHora = 0;
                   
                    parte.Scrap = 0; 
                    parte.IdEstandarConRelevoFk = 2; 
                   

                _context.Add(parte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "NombreCaja", parte.IdCajaFk);
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", parte.IdClienteFk);
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "NombreColor", parte.IdColorFk);
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "NombreEnsamble", parte.IdEnsambleFk);
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "NombreEstandarconRelevo", parte.IdEstandarConRelevoFk);
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "NombreEstandar", parte.IdEstandarFk);
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "NombreEtiqueta", parte.IdEtiquetaFk);
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "NombreHule", parte.IdHuleFk);
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "NombreInserto", parte.IdInsertoFk);
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "NombreMolde", parte.IdMoldeFk);
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "NombrePintura", parte.IdPinturaFk);
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "NombreTarima", parte.IdTarimaFk);
            ViewData["IdInstructivoPiezaFk"] = new SelectList(_context.InstructivoPiezas, "IdInstructivoPieza", "NombreInstructivoPieza", parte.IdInstructivoPiezaFk);
            ViewData["IdEtiquetaCajaFK"] = new SelectList(_context.EtiquetaCajas, "IdEtiquetaDeCaja", "NombreEtiquetaDeCaja", parte.IdEtiquetaCajaFk);
            ViewData["IdEstandarPorHora"] = new SelectList(_context.EstandarPorHoras, "IdEstandarPorHora", "NombreEstandarPorHora",parte.IdEstandarPorHoraFk);
            ///ViewData["Accesorios"] = new SelectList(_context.Accesorios, "IdAccesorios", "NombreAccesorios");
            return View(parte);
        }

        public async Task<IActionResult> CallRegistro (int id)
        {
            _context.Estandars.FindAsync(id);

            return null;
        }

        // GET: Partes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Partes == null)
            {
                return NotFound();
            }

            var parte = await _context.Partes.FindAsync(id);
            if (parte == null)
            {
                return NotFound();
            }
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "NombreCaja", parte.IdCajaFk);
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", parte.IdClienteFk);
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "NombreColor", parte.IdColorFk);
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "NombreEnsamble", parte.IdEnsambleFk);
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "NombreEstandarconRelevo", parte.IdEstandarConRelevoFk);
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "NombreEstandar", parte.IdEstandarFk);
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "NombreEtiqueta", parte.IdEtiquetaFk);
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "NombreHule", parte.IdHuleFk);
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "NombreInserto", parte.IdInsertoFk);
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "NombreMolde", parte.IdMoldeFk);
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "NombrePintura", parte.IdPinturaFk);
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "NombreTarima", parte.IdTarimaFk);
            ViewData["Accesorios"] = new SelectList(_context.Accesorios, "IdAccesorio", "NombreAccesorio");
            ViewData["IdInstructivoPiezaFk"] = new SelectList(_context.InstructivoPiezas, "IdInstructivoPieza", "NombreInstructivoPieza",parte.IdInstructivoPiezaFk);
            ViewData["IdEtiquetaCajaFK"] = new SelectList(_context.EtiquetaCajas, "IdEtiquetaDeCaja", "NombreEtiquetaDeCaja",parte.IdEtiquetaCajaFk);
            ViewData["IdEstandarPorHoraFK"] = new SelectList(_context.EstandarPorHoras, "IdEstandarPorHora", "NombreEstandarPorHora",parte.IdEstandarPorHoraFk);
            return View(parte);
        }

        // POST: Partes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdParte," +
            "IdCodigoParte," +
            "IdTarimaFk," +
            "Aluminio," +
            "IdEnsambleFk," +
            "IdParteAccesorioFk," +
            "IdEstandarFk," +
            "IdEtiquetaFk," +
            "PiezasPorCaja," +
            "IdColorFk," +
            "CajasPorTarima," +
            "StdPintura," +
            "Costo," +
            "EstandarPorHora," +
            "IdCajaFk," +
            "IdHuleFk," +
            "IdPinturaFk," +
            "IdInsertoFk," +
            "IdMoldeFk," +
            "IdClienteFk," +
            "Scrap," +
            "IdEstandarConRelevoFk," +
            "IdInstructivoPiezaFk," +
            "IdEtiquetaCajaFk," +
            "IdEstandarPorHoraFk")] Parte parte)
        {
            if (id != parte.IdParte)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParteExists(parte.IdParte))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "NombreCaja", parte.IdCajaFk);
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "NombreCliente", parte.IdClienteFk);
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "NombreColor", parte.IdColorFk);
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "NombreEnsamble", parte.IdEnsambleFk);
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "NombreEstandarconRelevo", parte.IdEstandarConRelevoFk);
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "NombreEstandar", parte.IdEstandarFk);
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "NombreEtiqueta", parte.IdEtiquetaFk);
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "NombreHule", parte.IdHuleFk);
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "NombreInserto", parte.IdInsertoFk);
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "NombreMolde", parte.IdMoldeFk);
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "NombrePintura", parte.IdPinturaFk);
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "NombreTarima", parte.IdTarimaFk);
            ViewData["IdInstructivoPiezaFk"] = new SelectList(_context.InstructivoPiezas, "IdInstructivoPiezas", "NombreInstructivoPiezas", parte.IdInstructivoPiezaFk);
            ViewData["IdEtiquetaCajaFK"] = new SelectList(_context.EtiquetaCajas, "IdEtiquetaDeCaja", "NombreEtiquetaDeCaja", parte.IdEtiquetaCajaFk);
            ViewData["IdEstandarPorHora"] = new SelectList(_context.EstandarPorHoras, "IdEstandarPorHora", "NombreEstandarPorHora", parte.IdEstandarPorHoraFk);
            return View(parte);
        }

        // GET: Partes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Partes == null)
            {
                return NotFound();
            }

            var parte = await _context.Partes
                .Include(p => p.IdCajaFkNavigation)
                .Include(p => p.IdClienteFkNavigation)
                .Include(p => p.IdColorFkNavigation)
                .Include(p => p.IdEnsambleFkNavigation)
                .Include(p => p.IdEstandarConRelevoFkNavigation)
                .Include(p => p.IdEstandarFkNavigation)
                .Include(p => p.IdEtiquetaFkNavigation)
                .Include(p => p.IdHuleFkNavigation)
                .Include(p => p.IdInsertoFkNavigation)
                .Include(p => p.IdMoldeFkNavigation)
                .Include(p => p.IdPinturaFkNavigation)
                .Include(p => p.IdTarimaFkNavigation)
                .FirstOrDefaultAsync(m => m.IdParte == id);
            if (parte == null)
            {
                return NotFound();
            }

            return View(parte);
        }

        // POST: Partes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Partes == null)
            {
                return Problem("Entity set 'OTMFContext.Partes'  is null.");
            }
            var parte = await _context.Partes.FindAsync(id);
            if (parte != null)
            {
                _context.Partes.Remove(parte);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParteExists(int id)
        {
          return (_context.Partes?.Any(e => e.IdParte == id)).GetValueOrDefault();
        }
        [HttpGet]
        public JsonResult ObtenerPartes ()
        {
            var OP = "[ObtenerPartesAllInfo]";
            using ( var connection = new SqlConnection(con))
            {
                var partes = connection.Query<ObtenerPartes>(OP, commandType: CommandType.StoredProcedure);

                return Json(new { data = partes });
            }

               
        }
        [HttpPost]
        public JsonResult InsertAccesorioByParteId(int  IdParte , int IdAccesorio)
        {
            var procedure = "[InsertAccesorioByParteId]";
            using (var connection = new SqlConnection(con))
            {
                var confirm =  connection.Query(procedure, new
                {
                    IdParte = IdParte,
                    IdAccessorio = IdAccesorio
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirm });
            }
        }
        [HttpPost]
        public JsonResult DeleteAccesorioByParteId(int IdParteAccesorio)
        {
            var procedure = "[DeleteAccesorioByParteId]";
            using (var connection = new SqlConnection(con))
            {
                var confirm = connection.Query(procedure, new
                {
                    IdParteAccesorio = IdParteAccesorio
                }, commandType: CommandType.StoredProcedure);

                return Json(new { data = confirm });
            }

        }
        [HttpPost]
        public JsonResult UpdateAsignacionAccesorioById(int IdParte , int IdParteAccesorio)
        {
            var procedure = "[UpdateAsignacionAccesorioById]";
            using(var connection  = new SqlConnection(con))
            {
                var confirm = connection.Query(procedure, new
                {
                    IdParte = IdParte,
                    IdParteAccesorio = IdParteAccesorio
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirm });
            }
        }
        [HttpPost]
        public JsonResult ObtenerDetalleParteById (int IdParte)
        {
            var procedure = "[ObtenerDetalleParteById]";

            using (var connection = new SqlConnection(con))
            {
                var DetalleParte = connection.Query(procedure, new
                {
                    IdParte = IdParte
                }, commandType: CommandType.StoredProcedure);
                return Json( new {data = DetalleParte });
            }
        }
    }
}
