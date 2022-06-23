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
    public class InsertoesController : Controller
    {
        private readonly OTMFContext _context;

        public InsertoesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Insertoes
        public async Task<IActionResult> Index()
        {
              return _context.Insertos != null ? 
                          View(await _context.Insertos.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Insertos'  is null.");
        }

        // GET: Insertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insertos == null)
            {
                return NotFound();
            }

            var inserto = await _context.Insertos
                .FirstOrDefaultAsync(m => m.IdInserto == id);
            if (inserto == null)
            {
                return NotFound();
            }

            return View(inserto);
        }

        // GET: Insertoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insertoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInserto,NombreInserto")] Inserto inserto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inserto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inserto);
        }

        // GET: Insertoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insertos == null)
            {
                return NotFound();
            }

            var inserto = await _context.Insertos.FindAsync(id);
            if (inserto == null)
            {
                return NotFound();
            }
            return View(inserto);
        }

        // POST: Insertoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInserto,NombreInserto")] Inserto inserto)
        {
            if (id != inserto.IdInserto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inserto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsertoExists(inserto.IdInserto))
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
            return View(inserto);
        }

        // GET: Insertoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insertos == null)
            {
                return NotFound();
            }

            var inserto = await _context.Insertos
                .FirstOrDefaultAsync(m => m.IdInserto == id);
            if (inserto == null)
            {
                return NotFound();
            }

            return View(inserto);
        }

        // POST: Insertoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insertos == null)
            {
                return Problem("Entity set 'OTMFContext.Insertos'  is null.");
            }
            var inserto = await _context.Insertos.FindAsync(id);
            if (inserto != null)
            {
                _context.Insertos.Remove(inserto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsertoExists(int id)
        {
          return (_context.Insertos?.Any(e => e.IdInserto == id)).GetValueOrDefault();
        }
    }
}
