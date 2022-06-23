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
    public class MaquinaOrdenTrabajoesController : Controller
    {
        private readonly OTMFContext _context;

        public MaquinaOrdenTrabajoesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: MaquinaOrdenTrabajoes
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.MaquinaOrdenTrabajos.Include(m => m.IdMaquinaFkNavigation).Include(m => m.IdOrdenTrabajoFkNavigation);
            return View(await oTMFContext.ToListAsync());
        }

        // GET: MaquinaOrdenTrabajoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaquinaOrdenTrabajos == null)
            {
                return NotFound();
            }

            var maquinaOrdenTrabajo = await _context.MaquinaOrdenTrabajos
                .Include(m => m.IdMaquinaFkNavigation)
                .Include(m => m.IdOrdenTrabajoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdMaquinaOrdeTrabajo == id);
            if (maquinaOrdenTrabajo == null)
            {
                return NotFound();
            }

            return View(maquinaOrdenTrabajo);
        }

        // GET: MaquinaOrdenTrabajoes/Create
        public IActionResult Create()
        {
            ViewData["IdMaquinaFk"] = new SelectList(_context.Maquinas, "IdMaquina", "IdMaquina");
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo");
            return View();
        }

        // POST: MaquinaOrdenTrabajoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMaquinaOrdeTrabajo,IdMaquinaFk,IdOrdenTrabajoFk")] MaquinaOrdenTrabajo maquinaOrdenTrabajo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maquinaOrdenTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMaquinaFk"] = new SelectList(_context.Maquinas, "IdMaquina", "IdMaquina", maquinaOrdenTrabajo.IdMaquinaFk);
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", maquinaOrdenTrabajo.IdOrdenTrabajoFk);
            return View(maquinaOrdenTrabajo);
        }

        // GET: MaquinaOrdenTrabajoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaquinaOrdenTrabajos == null)
            {
                return NotFound();
            }

            var maquinaOrdenTrabajo = await _context.MaquinaOrdenTrabajos.FindAsync(id);
            if (maquinaOrdenTrabajo == null)
            {
                return NotFound();
            }
            ViewData["IdMaquinaFk"] = new SelectList(_context.Maquinas, "IdMaquina", "IdMaquina", maquinaOrdenTrabajo.IdMaquinaFk);
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", maquinaOrdenTrabajo.IdOrdenTrabajoFk);
            return View(maquinaOrdenTrabajo);
        }

        // POST: MaquinaOrdenTrabajoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMaquinaOrdeTrabajo,IdMaquinaFk,IdOrdenTrabajoFk")] MaquinaOrdenTrabajo maquinaOrdenTrabajo)
        {
            if (id != maquinaOrdenTrabajo.IdMaquinaOrdeTrabajo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maquinaOrdenTrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaquinaOrdenTrabajoExists(maquinaOrdenTrabajo.IdMaquinaOrdeTrabajo))
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
            ViewData["IdMaquinaFk"] = new SelectList(_context.Maquinas, "IdMaquina", "IdMaquina", maquinaOrdenTrabajo.IdMaquinaFk);
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", maquinaOrdenTrabajo.IdOrdenTrabajoFk);
            return View(maquinaOrdenTrabajo);
        }

        // GET: MaquinaOrdenTrabajoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaquinaOrdenTrabajos == null)
            {
                return NotFound();
            }

            var maquinaOrdenTrabajo = await _context.MaquinaOrdenTrabajos
                .Include(m => m.IdMaquinaFkNavigation)
                .Include(m => m.IdOrdenTrabajoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdMaquinaOrdeTrabajo == id);
            if (maquinaOrdenTrabajo == null)
            {
                return NotFound();
            }

            return View(maquinaOrdenTrabajo);
        }

        // POST: MaquinaOrdenTrabajoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaquinaOrdenTrabajos == null)
            {
                return Problem("Entity set 'OTMFContext.MaquinaOrdenTrabajos'  is null.");
            }
            var maquinaOrdenTrabajo = await _context.MaquinaOrdenTrabajos.FindAsync(id);
            if (maquinaOrdenTrabajo != null)
            {
                _context.MaquinaOrdenTrabajos.Remove(maquinaOrdenTrabajo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaquinaOrdenTrabajoExists(int id)
        {
          return (_context.MaquinaOrdenTrabajos?.Any(e => e.IdMaquinaOrdeTrabajo == id)).GetValueOrDefault();
        }
    }
}
