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
    public class AccesoriosController : Controller
    {
        private readonly OTMFContext _context;

        public AccesoriosController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Accesorios
        public async Task<IActionResult> Index()
        {
              return _context.Accesorios != null ? 
                          View(await _context.Accesorios.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Accesorios'  is null.");
        }

        // GET: Accesorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accesorios == null)
            {
                return NotFound();
            }

            var accesorio = await _context.Accesorios
                .FirstOrDefaultAsync(m => m.IdAccesorio == id);
            if (accesorio == null)
            {
                return NotFound();
            }

            return View(accesorio);
        }

        // GET: Accesorios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accesorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAccesorio,NombreAccesorio")] Accesorio accesorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accesorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accesorio);
        }

        // GET: Accesorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accesorios == null)
            {
                return NotFound();
            }

            var accesorio = await _context.Accesorios.FindAsync(id);
            if (accesorio == null)
            {
                return NotFound();
            }
            return View(accesorio);
        }

        // POST: Accesorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAccesorio,NombreAccesorio")] Accesorio accesorio)
        {
            if (id != accesorio.IdAccesorio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accesorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccesorioExists(accesorio.IdAccesorio))
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
            return View(accesorio);
        }

        // GET: Accesorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accesorios == null)
            {
                return NotFound();
            }

            var accesorio = await _context.Accesorios
                .FirstOrDefaultAsync(m => m.IdAccesorio == id);
            if (accesorio == null)
            {
                return NotFound();
            }

            return View(accesorio);
        }

        // POST: Accesorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accesorios == null)
            {
                return Problem("Entity set 'OTMFContext.Accesorios'  is null.");
            }
            var accesorio = await _context.Accesorios.FindAsync(id);
            if (accesorio != null)
            {
                _context.Accesorios.Remove(accesorio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccesorioExists(int id)
        {
          return (_context.Accesorios?.Any(e => e.IdAccesorio == id)).GetValueOrDefault();
        }
    }
}
