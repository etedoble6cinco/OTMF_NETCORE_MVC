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
    public class MoldesController : Controller
    {
        private readonly OTMFContext _context;

        public MoldesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Moldes
        public async Task<IActionResult> Index()
        {
              return _context.Moldes != null ? 
                          View(await _context.Moldes.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Moldes'  is null.");
        }

        // GET: Moldes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Moldes == null)
            {
                return NotFound();
            }

            var molde = await _context.Moldes
                .FirstOrDefaultAsync(m => m.IdMolde == id);
            if (molde == null)
            {
                return NotFound();
            }

            return View(molde);
        }

        // GET: Moldes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moldes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMolde,NombreMolde")] Molde molde)
        {
            if (ModelState.IsValid)
            {
                _context.Add(molde);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(molde);
        }

        // GET: Moldes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Moldes == null)
            {
                return NotFound();
            }

            var molde = await _context.Moldes.FindAsync(id);
            if (molde == null)
            {
                return NotFound();
            }
            return View(molde);
        }

        // POST: Moldes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMolde,NombreMolde")] Molde molde)
        {
            if (id != molde.IdMolde)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(molde);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoldeExists(molde.IdMolde))
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
            return View(molde);
        }

        // GET: Moldes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Moldes == null)
            {
                return NotFound();
            }

            var molde = await _context.Moldes
                .FirstOrDefaultAsync(m => m.IdMolde == id);
            if (molde == null)
            {
                return NotFound();
            }

            return View(molde);
        }

        // POST: Moldes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Moldes == null)
            {
                return Problem("Entity set 'OTMFContext.Moldes'  is null.");
            }
            var molde = await _context.Moldes.FindAsync(id);
            if (molde != null)
            {
                _context.Moldes.Remove(molde);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoldeExists(int id)
        {
          return (_context.Moldes?.Any(e => e.IdMolde == id)).GetValueOrDefault();
        }
    }
}
