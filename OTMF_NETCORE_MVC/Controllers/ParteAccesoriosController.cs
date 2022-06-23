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
    public class ParteAccesoriosController : Controller
    {
        private readonly OTMFContext _context;

        public ParteAccesoriosController(OTMFContext context)
        {
            _context = context;
        }

        // GET: ParteAccesorios
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.ParteAccesorios.Include(p => p.IdAccesorioFkNavigation).Include(p => p.IdParteFkNavigation);
            return View(await oTMFContext.ToListAsync());
        }

        // GET: ParteAccesorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParteAccesorios == null)
            {
                return NotFound();
            }

            var parteAccesorio = await _context.ParteAccesorios
                .Include(p => p.IdAccesorioFkNavigation)
                .Include(p => p.IdParteFkNavigation)
                .FirstOrDefaultAsync(m => m.IdParteAccesorio == id);
            if (parteAccesorio == null)
            {
                return NotFound();
            }

            return View(parteAccesorio);
        }

        // GET: ParteAccesorios/Create
        public IActionResult Create()
        {
            ViewData["IdAccesorioFk"] = new SelectList(_context.Accesorios, "IdAccesorio", "IdAccesorio");
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte");
            return View();
        }

        // POST: ParteAccesorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParteAccesorio,IdAccesorioFk,IdParteFk")] ParteAccesorio parteAccesorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parteAccesorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAccesorioFk"] = new SelectList(_context.Accesorios, "IdAccesorio", "IdAccesorio", parteAccesorio.IdAccesorioFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte", parteAccesorio.IdParteFk);
            return View(parteAccesorio);
        }

        // GET: ParteAccesorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParteAccesorios == null)
            {
                return NotFound();
            }

            var parteAccesorio = await _context.ParteAccesorios.FindAsync(id);
            if (parteAccesorio == null)
            {
                return NotFound();
            }
            ViewData["IdAccesorioFk"] = new SelectList(_context.Accesorios, "IdAccesorio", "IdAccesorio", parteAccesorio.IdAccesorioFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte", parteAccesorio.IdParteFk);
            return View(parteAccesorio);
        }

        // POST: ParteAccesorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdParteAccesorio,IdAccesorioFk,IdParteFk")] ParteAccesorio parteAccesorio)
        {
            if (id != parteAccesorio.IdParteAccesorio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parteAccesorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParteAccesorioExists(parteAccesorio.IdParteAccesorio))
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
            ViewData["IdAccesorioFk"] = new SelectList(_context.Accesorios, "IdAccesorio", "IdAccesorio", parteAccesorio.IdAccesorioFk);
            ViewData["IdParteFk"] = new SelectList(_context.Partes, "IdParte", "IdParte", parteAccesorio.IdParteFk);
            return View(parteAccesorio);
        }

        // GET: ParteAccesorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParteAccesorios == null)
            {
                return NotFound();
            }

            var parteAccesorio = await _context.ParteAccesorios
                .Include(p => p.IdAccesorioFkNavigation)
                .Include(p => p.IdParteFkNavigation)
                .FirstOrDefaultAsync(m => m.IdParteAccesorio == id);
            if (parteAccesorio == null)
            {
                return NotFound();
            }

            return View(parteAccesorio);
        }

        // POST: ParteAccesorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParteAccesorios == null)
            {
                return Problem("Entity set 'OTMFContext.ParteAccesorios'  is null.");
            }
            var parteAccesorio = await _context.ParteAccesorios.FindAsync(id);
            if (parteAccesorio != null)
            {
                _context.ParteAccesorios.Remove(parteAccesorio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParteAccesorioExists(int id)
        {
          return (_context.ParteAccesorios?.Any(e => e.IdParteAccesorio == id)).GetValueOrDefault();
        }
    }
}
