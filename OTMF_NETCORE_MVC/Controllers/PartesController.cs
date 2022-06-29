using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class PartesController : Controller
    {
        private readonly OTMFContext _context;

        public PartesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Partes
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.Partes.Include(p => p.IdCajaFkNavigation).Include(p => p.IdClienteFkNavigation).Include(p => p.IdColorFkNavigation).Include(p => p.IdEnsambleFkNavigation).Include(p => p.IdEstandarConRelevoFkNavigation).Include(p => p.IdEstandarFkNavigation).Include(p => p.IdEtiquetaFkNavigation).Include(p => p.IdHuleFkNavigation).Include(p => p.IdInsertoFkNavigation).Include(p => p.IdMoldeFkNavigation).Include(p => p.IdPinturaFkNavigation).Include(p => p.IdTarimaFkNavigation);
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
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "IdCaja");
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "IdColor");
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "IdEnsamble");
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "IdEstandarConRelevo");
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "IdEstandar");
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "IdEtiqueta");
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "IdHule");
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "IdInserto");
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "IdMolde");
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "IdPintura");
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "IdTarima");
            return View();
        }

        // POST: Partes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParte,IdCodigoParte,IdTarimaFk,Aluminio,IdEnsambleFk,IdParteAccesorioFk,IdEstandarFk,IdEtiquetaFk,PiezasPorCaja,IdColorFk,CajasPorTarima,StdPintura,Costo,EstandarPorHora,IdCajaFk,IdHuleFk,IdPinturaFk,IdInsertoFk,IdMoldeFk,IdClienteFk,Scrap,IdEstandarConRelevoFk")] Parte parte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "IdCaja", parte.IdCajaFk);
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", parte.IdClienteFk);
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "IdColor", parte.IdColorFk);
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "IdEnsamble", parte.IdEnsambleFk);
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "IdEstandarConRelevo", parte.IdEstandarConRelevoFk);
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "IdEstandar", parte.IdEstandarFk);
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "IdEtiqueta", parte.IdEtiquetaFk);
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "IdHule", parte.IdHuleFk);
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "IdInserto", parte.IdInsertoFk);
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "IdMolde", parte.IdMoldeFk);
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "IdPintura", parte.IdPinturaFk);
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "IdTarima", parte.IdTarimaFk);
            return View(parte);
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
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "IdCaja", parte.IdCajaFk);
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", parte.IdClienteFk);
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "IdColor", parte.IdColorFk);
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "IdEnsamble", parte.IdEnsambleFk);
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "IdEstandarConRelevo", parte.IdEstandarConRelevoFk);
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "IdEstandar", parte.IdEstandarFk);
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "IdEtiqueta", parte.IdEtiquetaFk);
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "IdHule", parte.IdHuleFk);
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "IdInserto", parte.IdInsertoFk);
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "IdMolde", parte.IdMoldeFk);
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "IdPintura", parte.IdPinturaFk);
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "IdTarima", parte.IdTarimaFk);
            return View(parte);
        }

        // POST: Partes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdParte,IdCodigoParte,IdTarimaFk,Aluminio,IdEnsambleFk,IdParteAccesorioFk,IdEstandarFk,IdEtiquetaFk,PiezasPorCaja,IdColorFk,CajasPorTarima,StdPintura,Costo,EstandarPorHora,IdCajaFk,IdHuleFk,IdPinturaFk,IdInsertoFk,IdMoldeFk,IdClienteFk,Scrap,IdEstandarConRelevoFk")] Parte parte)
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
            ViewData["IdCajaFk"] = new SelectList(_context.Cajas, "IdCaja", "IdCaja", parte.IdCajaFk);
            ViewData["IdClienteFk"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", parte.IdClienteFk);
            ViewData["IdColorFk"] = new SelectList(_context.Colors, "IdColor", "IdColor", parte.IdColorFk);
            ViewData["IdEnsambleFk"] = new SelectList(_context.Ensambles, "IdEnsamble", "IdEnsamble", parte.IdEnsambleFk);
            ViewData["IdEstandarConRelevoFk"] = new SelectList(_context.EstandarConRelevos, "IdEstandarConRelevo", "IdEstandarConRelevo", parte.IdEstandarConRelevoFk);
            ViewData["IdEstandarFk"] = new SelectList(_context.Estandars, "IdEstandar", "IdEstandar", parte.IdEstandarFk);
            ViewData["IdEtiquetaFk"] = new SelectList(_context.Etiqueta, "IdEtiqueta", "IdEtiqueta", parte.IdEtiquetaFk);
            ViewData["IdHuleFk"] = new SelectList(_context.Hules, "IdHule", "IdHule", parte.IdHuleFk);
            ViewData["IdInsertoFk"] = new SelectList(_context.Insertos, "IdInserto", "IdInserto", parte.IdInsertoFk);
            ViewData["IdMoldeFk"] = new SelectList(_context.Moldes, "IdMolde", "IdMolde", parte.IdMoldeFk);
            ViewData["IdPinturaFk"] = new SelectList(_context.Pinturas, "IdPintura", "IdPintura", parte.IdPinturaFk);
            ViewData["IdTarimaFk"] = new SelectList(_context.Tarimas, "IdTarima", "IdTarima", parte.IdTarimaFk);
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

        public JsonResult ObtenerPartes ()
        {

            return null;
        }
    }
}
