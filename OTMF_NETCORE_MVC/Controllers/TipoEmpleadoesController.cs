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
    public class TipoEmpleadoesController : Controller
    {
        private readonly OTMFContext _context;

        public TipoEmpleadoesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: TipoEmpleadoes
        public async Task<IActionResult> Index()
        {
              return _context.TipoEmpleados != null ? 
                          View(await _context.TipoEmpleados.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.TipoEmpleados'  is null.");
        }

        // GET: TipoEmpleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoEmpleados == null)
            {
                return NotFound();
            }

            var tipoEmpleado = await _context.TipoEmpleados
                .FirstOrDefaultAsync(m => m.IdTipoEmpleado == id);
            if (tipoEmpleado == null)
            {
                return NotFound();
            }

            return View(tipoEmpleado);
        }

        // GET: TipoEmpleadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEmpleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoEmpleado,NombreTipoEmpleado")] TipoEmpleado tipoEmpleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEmpleado);
        }

        // GET: TipoEmpleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoEmpleados == null)
            {
                return NotFound();
            }

            var tipoEmpleado = await _context.TipoEmpleados.FindAsync(id);
            if (tipoEmpleado == null)
            {
                return NotFound();
            }
            return View(tipoEmpleado);
        }

        // POST: TipoEmpleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoEmpleado,NombreTipoEmpleado")] TipoEmpleado tipoEmpleado)
        {
            if (id != tipoEmpleado.IdTipoEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEmpleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEmpleadoExists(tipoEmpleado.IdTipoEmpleado))
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
            return View(tipoEmpleado);
        }

        // GET: TipoEmpleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoEmpleados == null)
            {
                return NotFound();
            }

            var tipoEmpleado = await _context.TipoEmpleados
                .FirstOrDefaultAsync(m => m.IdTipoEmpleado == id);
            if (tipoEmpleado == null)
            {
                return NotFound();
            }

            return View(tipoEmpleado);
        }

        // POST: TipoEmpleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoEmpleados == null)
            {
                return Problem("Entity set 'OTMFContext.TipoEmpleados'  is null.");
            }
            var tipoEmpleado = await _context.TipoEmpleados.FindAsync(id);
            if (tipoEmpleado != null)
            {
                _context.TipoEmpleados.Remove(tipoEmpleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEmpleadoExists(int id)
        {
          return (_context.TipoEmpleados?.Any(e => e.IdTipoEmpleado == id)).GetValueOrDefault();
        }
    }
}
