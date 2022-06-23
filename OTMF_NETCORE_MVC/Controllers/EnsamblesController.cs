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
    public class EnsamblesController : Controller
    {
        private readonly OTMFContext _context;

        public EnsamblesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Ensambles
        public async Task<IActionResult> Index()
        {
              return _context.Ensambles != null ? 
                          View(await _context.Ensambles.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Ensambles'  is null.");
        }

        // GET: Ensambles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ensambles == null)
            {
                return NotFound();
            }

            var ensamble = await _context.Ensambles
                .FirstOrDefaultAsync(m => m.IdEnsamble == id);
            if (ensamble == null)
            {
                return NotFound();
            }

            return View(ensamble);
        }

        // GET: Ensambles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ensambles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEnsamble,NombreEnsamble")] Ensamble ensamble)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ensamble);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ensamble);
        }

        // GET: Ensambles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ensambles == null)
            {
                return NotFound();
            }

            var ensamble = await _context.Ensambles.FindAsync(id);
            if (ensamble == null)
            {
                return NotFound();
            }
            return View(ensamble);
        }

        // POST: Ensambles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEnsamble,NombreEnsamble")] Ensamble ensamble)
        {
            if (id != ensamble.IdEnsamble)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ensamble);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnsambleExists(ensamble.IdEnsamble))
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
            return View(ensamble);
        }

        // GET: Ensambles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ensambles == null)
            {
                return NotFound();
            }

            var ensamble = await _context.Ensambles
                .FirstOrDefaultAsync(m => m.IdEnsamble == id);
            if (ensamble == null)
            {
                return NotFound();
            }

            return View(ensamble);
        }

        // POST: Ensambles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ensambles == null)
            {
                return Problem("Entity set 'OTMFContext.Ensambles'  is null.");
            }
            var ensamble = await _context.Ensambles.FindAsync(id);
            if (ensamble != null)
            {
                _context.Ensambles.Remove(ensamble);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnsambleExists(int id)
        {
          return (_context.Ensambles?.Any(e => e.IdEnsamble == id)).GetValueOrDefault();
        }
    }
}
