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
    public class EstandarsController : Controller
    {
        private readonly OTMFContext _context;

        public EstandarsController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Estandars
        public async Task<IActionResult> Index()
        {
              return _context.Estandars != null ? 
                          View(await _context.Estandars.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Estandars'  is null.");
        }

        // GET: Estandars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estandars == null)
            {
                return NotFound();
            }

            var estandar = await _context.Estandars
                .FirstOrDefaultAsync(m => m.IdEstandar == id);
            if (estandar == null)
            {
                return NotFound();
            }

            return View(estandar);
        }

        // GET: Estandars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estandars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstandar,NombreEstandar")] Estandar estandar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estandar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estandar);
        }

        // GET: Estandars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estandars == null)
            {
                return NotFound();
            }

            var estandar = await _context.Estandars.FindAsync(id);
            if (estandar == null)
            {
                return NotFound();
            }
            return View(estandar);
        }

        // POST: Estandars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstandar,NombreEstandar")] Estandar estandar)
        {
            if (id != estandar.IdEstandar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estandar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstandarExists(estandar.IdEstandar))
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
            return View(estandar);
        }

        // GET: Estandars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estandars == null)
            {
                return NotFound();
            }

            var estandar = await _context.Estandars
                .FirstOrDefaultAsync(m => m.IdEstandar == id);
            if (estandar == null)
            {
                return NotFound();
            }

            return View(estandar);
        }

        // POST: Estandars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estandars == null)
            {
                return Problem("Entity set 'OTMFContext.Estandars'  is null.");
            }
            var estandar = await _context.Estandars.FindAsync(id);
            if (estandar != null)
            {
                _context.Estandars.Remove(estandar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstandarExists(int id)
        {
          return (_context.Estandars?.Any(e => e.IdEstandar == id)).GetValueOrDefault();
        }
    }
}
