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
    public class TarimasController : Controller
    {
        private readonly OTMFContext _context;

        public TarimasController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Tarimas
        public async Task<IActionResult> Index()
        {
              return _context.Tarimas != null ? 
                          View(await _context.Tarimas.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Tarimas'  is null.");
        }

        // GET: Tarimas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarimas == null)
            {
                return NotFound();
            }

            var tarima = await _context.Tarimas
                .FirstOrDefaultAsync(m => m.IdTarima == id);
            if (tarima == null)
            {
                return NotFound();
            }

            return View(tarima);
        }

        // GET: Tarimas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarimas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarima,NombreTarima")] Tarima tarima)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarima);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarima);
        }

        // GET: Tarimas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarimas == null)
            {
                return NotFound();
            }

            var tarima = await _context.Tarimas.FindAsync(id);
            if (tarima == null)
            {
                return NotFound();
            }
            return View(tarima);
        }

        // POST: Tarimas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarima,NombreTarima")] Tarima tarima)
        {
            if (id != tarima.IdTarima)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarima);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarimaExists(tarima.IdTarima))
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
            return View(tarima);
        }

        // GET: Tarimas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarimas == null)
            {
                return NotFound();
            }

            var tarima = await _context.Tarimas
                .FirstOrDefaultAsync(m => m.IdTarima == id);
            if (tarima == null)
            {
                return NotFound();
            }

            return View(tarima);
        }

        // POST: Tarimas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarimas == null)
            {
                return Problem("Entity set 'OTMFContext.Tarimas'  is null.");
            }
            var tarima = await _context.Tarimas.FindAsync(id);
            if (tarima != null)
            {
                _context.Tarimas.Remove(tarima);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarimaExists(int id)
        {
          return (_context.Tarimas?.Any(e => e.IdTarima == id)).GetValueOrDefault();
        }
    }
}
