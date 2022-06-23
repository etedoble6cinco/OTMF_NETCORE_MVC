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
    public class PinturasController : Controller
    {
        private readonly OTMFContext _context;

        public PinturasController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Pinturas
        public async Task<IActionResult> Index()
        {
              return _context.Pinturas != null ? 
                          View(await _context.Pinturas.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Pinturas'  is null.");
        }

        // GET: Pinturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pinturas == null)
            {
                return NotFound();
            }

            var pintura = await _context.Pinturas
                .FirstOrDefaultAsync(m => m.IdPintura == id);
            if (pintura == null)
            {
                return NotFound();
            }

            return View(pintura);
        }

        // GET: Pinturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pinturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPintura,NombrePintura")] Pintura pintura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pintura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pintura);
        }

        // GET: Pinturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pinturas == null)
            {
                return NotFound();
            }

            var pintura = await _context.Pinturas.FindAsync(id);
            if (pintura == null)
            {
                return NotFound();
            }
            return View(pintura);
        }

        // POST: Pinturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPintura,NombrePintura")] Pintura pintura)
        {
            if (id != pintura.IdPintura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pintura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PinturaExists(pintura.IdPintura))
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
            return View(pintura);
        }

        // GET: Pinturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pinturas == null)
            {
                return NotFound();
            }

            var pintura = await _context.Pinturas
                .FirstOrDefaultAsync(m => m.IdPintura == id);
            if (pintura == null)
            {
                return NotFound();
            }

            return View(pintura);
        }

        // POST: Pinturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pinturas == null)
            {
                return Problem("Entity set 'OTMFContext.Pinturas'  is null.");
            }
            var pintura = await _context.Pinturas.FindAsync(id);
            if (pintura != null)
            {
                _context.Pinturas.Remove(pintura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PinturaExists(int id)
        {
          return (_context.Pinturas?.Any(e => e.IdPintura == id)).GetValueOrDefault();
        }
    }
}
