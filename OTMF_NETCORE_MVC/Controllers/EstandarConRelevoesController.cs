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
    public class EstandarConRelevoesController : Controller
    {
        private readonly OTMFContext _context;

        public EstandarConRelevoesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: EstandarConRelevoes
        public async Task<IActionResult> Index()
        {
              return _context.EstandarConRelevos != null ? 
                          View(await _context.EstandarConRelevos.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.EstandarConRelevos'  is null.");
        }

        // GET: EstandarConRelevoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EstandarConRelevos == null)
            {
                return NotFound();
            }

            var estandarConRelevo = await _context.EstandarConRelevos
                .FirstOrDefaultAsync(m => m.IdEstandarConRelevo == id);
            if (estandarConRelevo == null)
            {
                return NotFound();
            }

            return View(estandarConRelevo);
        }

        // GET: EstandarConRelevoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstandarConRelevoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstandarConRelevo,NombreEstandarconRelevo")] EstandarConRelevo estandarConRelevo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estandarConRelevo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estandarConRelevo);
        }

        // GET: EstandarConRelevoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EstandarConRelevos == null)
            {
                return NotFound();
            }

            var estandarConRelevo = await _context.EstandarConRelevos.FindAsync(id);
            if (estandarConRelevo == null)
            {
                return NotFound();
            }
            return View(estandarConRelevo);
        }

        // POST: EstandarConRelevoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstandarConRelevo,NombreEstandarconRelevo")] EstandarConRelevo estandarConRelevo)
        {
            if (id != estandarConRelevo.IdEstandarConRelevo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estandarConRelevo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstandarConRelevoExists(estandarConRelevo.IdEstandarConRelevo))
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
            return View(estandarConRelevo);
        }

        // GET: EstandarConRelevoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EstandarConRelevos == null)
            {
                return NotFound();
            }

            var estandarConRelevo = await _context.EstandarConRelevos
                .FirstOrDefaultAsync(m => m.IdEstandarConRelevo == id);
            if (estandarConRelevo == null)
            {
                return NotFound();
            }

            return View(estandarConRelevo);
        }

        // POST: EstandarConRelevoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EstandarConRelevos == null)
            {
                return Problem("Entity set 'OTMFContext.EstandarConRelevos'  is null.");
            }
            var estandarConRelevo = await _context.EstandarConRelevos.FindAsync(id);
            if (estandarConRelevo != null)
            {
                _context.EstandarConRelevos.Remove(estandarConRelevo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstandarConRelevoExists(int id)
        {
          return (_context.EstandarConRelevos?.Any(e => e.IdEstandarConRelevo == id)).GetValueOrDefault();
        }
    }
}
