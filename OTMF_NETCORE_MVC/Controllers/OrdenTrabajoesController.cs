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
    public class OrdenTrabajoesController : Controller
    {
        private readonly OTMFContext _context;

        public OrdenTrabajoesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: OrdenTrabajoes
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.OrdenTrabajos.Include(o => o.IdEstadoOrdenFkNavigation).Include(o => o.IdInstructivoFkNavigation).Include(o => o.IdParteFkNavigation);
            return View(await oTMFContext.ToListAsync());
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
            ViewData["IdEstadoOrdenFk"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "IdEstadoOrden");
            ViewData["IdInstructivoFk"] = new SelectList(_context.Instructivos, "IdInstructivo", "IdInstructivo");
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte");
            return View();
        }

        // POST: OrdenTrabajoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleadoMoldeadorFk,IdEmpleadoEmpacadorFk,IdOrdenTrabajo,IdMaquinaFk,FechaOrdenTrabajo,IdParteFk,CantidadPiezasPororden,CajasRecibidas,PiezasRealizadas,IdInstructivoFk,HoraInicio,HoraFinalizacion,IdEmpeadoSupervisorFk,IdEstadoOrdenFk,EtiquetaDeCaja,IdEstandarConRelevoFk,IdEstandarPorHoraFk,MaxScrap,IdCodigoOrdenTrabajo")] OrdenTrabajo ordenTrabajo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstadoOrdenFk"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "IdEstadoOrden", ordenTrabajo.IdEstadoOrdenFk);
            ViewData["IdInstructivoFk"] = new SelectList(_context.Instructivos, "IdInstructivo", "IdInstructivo", ordenTrabajo.IdInstructivoFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte", ordenTrabajo.IdParteFk);
            return View(ordenTrabajo);
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
            ViewData["IdEstadoOrdenFk"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "IdEstadoOrden", ordenTrabajo.IdEstadoOrdenFk);
            ViewData["IdInstructivoFk"] = new SelectList(_context.Instructivos, "IdInstructivo", "IdInstructivo", ordenTrabajo.IdInstructivoFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte", ordenTrabajo.IdParteFk);
            return View(ordenTrabajo);
        }

        // POST: OrdenTrabajoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleadoMoldeadorFk,IdEmpleadoEmpacadorFk,IdOrdenTrabajo,IdMaquinaFk,FechaOrdenTrabajo,IdParteFk,CantidadPiezasPororden,CajasRecibidas,PiezasRealizadas,IdInstructivoFk,HoraInicio,HoraFinalizacion,IdEmpeadoSupervisorFk,IdEstadoOrdenFk,EtiquetaDeCaja,IdEstandarConRelevoFk,IdEstandarPorHoraFk,MaxScrap,IdCodigoOrdenTrabajo")] OrdenTrabajo ordenTrabajo)
        {
            if (id != ordenTrabajo.IdOrdenTrabajo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenTrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenTrabajoExists(ordenTrabajo.IdOrdenTrabajo))
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
            ViewData["IdEstadoOrdenFk"] = new SelectList(_context.EstadoOrdens, "IdEstadoOrden", "IdEstadoOrden", ordenTrabajo.IdEstadoOrdenFk);
            ViewData["IdInstructivoFk"] = new SelectList(_context.Instructivos, "IdInstructivo", "IdInstructivo", ordenTrabajo.IdInstructivoFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte", ordenTrabajo.IdParteFk);
            return View(ordenTrabajo);
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
    }
}
