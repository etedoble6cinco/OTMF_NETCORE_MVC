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
    public class HulesController : Controller
    {
        private readonly OTMFContext _context;

        public HulesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Hules
        public async Task<IActionResult> Index()
        {
              return _context.Hules != null ? 
                          View(await _context.Hules.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Hules'  is null.");
        }

        // GET: Hules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hules == null)
            {
                return NotFound();
            }

            var hule = await _context.Hules
                .FirstOrDefaultAsync(m => m.IdHule == id);
            if (hule == null)
            {
                return NotFound();
            }

            return View(hule);
        }

        // GET: Hules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHule,NombreHule")] Hule hule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hule);
        }

        // GET: Hules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hules == null)
            {
                return NotFound();
            }

            var hule = await _context.Hules.FindAsync(id);
            if (hule == null)
            {
                return NotFound();
            }
            return View(hule);
        }

        // POST: Hules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHule,NombreHule")] Hule hule)
        {
            if (id != hule.IdHule)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HuleExists(hule.IdHule))
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
            return View(hule);
        }

        // GET: Hules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hules == null)
            {
                return NotFound();
            }

            var hule = await _context.Hules
                .FirstOrDefaultAsync(m => m.IdHule == id);
            if (hule == null)
            {
                return NotFound();
            }

            return View(hule);
        }

        // POST: Hules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hules == null)
            {
                return Problem("Entity set 'OTMFContext.Hules'  is null.");
            }
            var hule = await _context.Hules.FindAsync(id);
            if (hule != null)
            {
                _context.Hules.Remove(hule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HuleExists(int id)
        {
          return (_context.Hules?.Any(e => e.IdHule == id)).GetValueOrDefault();
        }
    }
}
