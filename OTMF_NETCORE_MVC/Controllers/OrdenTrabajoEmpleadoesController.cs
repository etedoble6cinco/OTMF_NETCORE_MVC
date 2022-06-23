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
    public class OrdenTrabajoEmpleadoesController : Controller
    {
        private readonly OTMFContext _context;

        public OrdenTrabajoEmpleadoesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: OrdenTrabajoEmpleadoes
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.OrdenTrabajoEmpleados.Include(o => o.IdEmpleadoFkNavigation).Include(o => o.IdOrdenTrabajoFkNavigation);
            return View(await oTMFContext.ToListAsync());
        }

        // GET: OrdenTrabajoEmpleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenTrabajoEmpleados == null)
            {
                return NotFound();
            }

            var ordenTrabajoEmpleado = await _context.OrdenTrabajoEmpleados
                .Include(o => o.IdEmpleadoFkNavigation)
                .Include(o => o.IdOrdenTrabajoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleadoOrdenTrabajo == id);
            if (ordenTrabajoEmpleado == null)
            {
                return NotFound();
            }

            return View(ordenTrabajoEmpleado);
        }

        // GET: OrdenTrabajoEmpleadoes/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleadoFk"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado");
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo");
            return View();
        }

        // POST: OrdenTrabajoEmpleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleadoOrdenTrabajo,IdEmpleadoFk,IdOrdenTrabajoFk")] OrdenTrabajoEmpleado ordenTrabajoEmpleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenTrabajoEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleadoFk"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", ordenTrabajoEmpleado.IdEmpleadoFk);
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", ordenTrabajoEmpleado.IdOrdenTrabajoFk);
            return View(ordenTrabajoEmpleado);
        }

        // GET: OrdenTrabajoEmpleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenTrabajoEmpleados == null)
            {
                return NotFound();
            }

            var ordenTrabajoEmpleado = await _context.OrdenTrabajoEmpleados.FindAsync(id);
            if (ordenTrabajoEmpleado == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleadoFk"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", ordenTrabajoEmpleado.IdEmpleadoFk);
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", ordenTrabajoEmpleado.IdOrdenTrabajoFk);
            return View(ordenTrabajoEmpleado);
        }

        // POST: OrdenTrabajoEmpleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleadoOrdenTrabajo,IdEmpleadoFk,IdOrdenTrabajoFk")] OrdenTrabajoEmpleado ordenTrabajoEmpleado)
        {
            if (id != ordenTrabajoEmpleado.IdEmpleadoOrdenTrabajo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenTrabajoEmpleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenTrabajoEmpleadoExists(ordenTrabajoEmpleado.IdEmpleadoOrdenTrabajo))
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
            ViewData["IdEmpleadoFk"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", ordenTrabajoEmpleado.IdEmpleadoFk);
            ViewData["IdOrdenTrabajoFk"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdOrdenTrabajo", ordenTrabajoEmpleado.IdOrdenTrabajoFk);
            return View(ordenTrabajoEmpleado);
        }

        // GET: OrdenTrabajoEmpleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenTrabajoEmpleados == null)
            {
                return NotFound();
            }

            var ordenTrabajoEmpleado = await _context.OrdenTrabajoEmpleados
                .Include(o => o.IdEmpleadoFkNavigation)
                .Include(o => o.IdOrdenTrabajoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleadoOrdenTrabajo == id);
            if (ordenTrabajoEmpleado == null)
            {
                return NotFound();
            }

            return View(ordenTrabajoEmpleado);
        }

        // POST: OrdenTrabajoEmpleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenTrabajoEmpleados == null)
            {
                return Problem("Entity set 'OTMFContext.OrdenTrabajoEmpleados'  is null.");
            }
            var ordenTrabajoEmpleado = await _context.OrdenTrabajoEmpleados.FindAsync(id);
            if (ordenTrabajoEmpleado != null)
            {
                _context.OrdenTrabajoEmpleados.Remove(ordenTrabajoEmpleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenTrabajoEmpleadoExists(int id)
        {
          return (_context.OrdenTrabajoEmpleados?.Any(e => e.IdEmpleadoOrdenTrabajo == id)).GetValueOrDefault();
        }
    }
}
