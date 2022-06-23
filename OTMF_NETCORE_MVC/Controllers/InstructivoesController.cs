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
    public class InstructivoesController : Controller
    {
        private readonly OTMFContext _context;

        public InstructivoesController(OTMFContext context)
        {
            _context = context;
        }

        // GET: Instructivoes
        public async Task<IActionResult> Index()
        {
              return _context.Instructivos != null ? 
                          View(await _context.Instructivos.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Instructivos'  is null.");
        }

        // GET: Instructivoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Instructivos == null)
            {
                return NotFound();
            }

            var instructivo = await _context.Instructivos
                .FirstOrDefaultAsync(m => m.IdInstructivo == id);
            if (instructivo == null)
            {
                return NotFound();
            }

            return View(instructivo);
        }

        // GET: Instructivoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructivoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInstructivo,NombreInstructivo")] Instructivo instructivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructivo);
        }

        // GET: Instructivoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Instructivos == null)
            {
                return NotFound();
            }

            var instructivo = await _context.Instructivos.FindAsync(id);
            if (instructivo == null)
            {
                return NotFound();
            }
            return View(instructivo);
        }

        // POST: Instructivoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInstructivo,NombreInstructivo")] Instructivo instructivo)
        {
            if (id != instructivo.IdInstructivo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructivoExists(instructivo.IdInstructivo))
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
            return View(instructivo);
        }

        // GET: Instructivoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Instructivos == null)
            {
                return NotFound();
            }

            var instructivo = await _context.Instructivos
                .FirstOrDefaultAsync(m => m.IdInstructivo == id);
            if (instructivo == null)
            {
                return NotFound();
            }

            return View(instructivo);
        }

        // POST: Instructivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Instructivos == null)
            {
                return Problem("Entity set 'OTMFContext.Instructivos'  is null.");
            }
            var instructivo = await _context.Instructivos.FindAsync(id);
            if (instructivo != null)
            {
                _context.Instructivos.Remove(instructivo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructivoExists(int id)
        {
          return (_context.Instructivos?.Any(e => e.IdInstructivo == id)).GetValueOrDefault();
        }
    }
}
