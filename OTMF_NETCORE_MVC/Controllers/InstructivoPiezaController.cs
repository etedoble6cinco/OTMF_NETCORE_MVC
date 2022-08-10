using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class InstructivoPiezaController : Controller
    {
        private readonly OTMFContext _context;
        private IWebHostEnvironment webHostEnvironment;
        private readonly string connectionString;

        public InstructivoPiezaController(OTMFContext context, IWebHostEnvironment environment, IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            webHostEnvironment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return _context.InstructivoPiezas != null ?
                View(await _context.InstructivoPiezas.ToListAsync()) :
                Problem("Tenemos un problema");
        }
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInstructivo,NombreInstructivo")] InstructivoPieza instrcutivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instrcutivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        /// <summary>
        /// GET
        /// </summary>
        // <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InstructivoPiezas == null)
            {
                return NotFound();
            }
            var instructivo = await _context.InstructivoPiezas.FindAsync(id);
            if (instructivo == null)
            {
                return NotFound();

            }
            return View(instructivo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInstructivoPieza,NombreInstructivoPieza")] InstructivoPieza instructivo)
        {
            if (id != instructivo.IdInstructivoPieza)
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
                    if (!InstructivoExists(instructivo.IdInstructivoPieza))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InstructivoPiezas == null)
            {
                return NotFound();
            }
            var instructivo = await _context.InstructivoPiezas.FirstOrDefaultAsync(m => m.IdInstructivoPieza == id);
            if( instructivo  == null)
            {
                return NotFound();
            }
            return View();
        }
        // POST: Instructivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InstructivoPiezas == null)
            {
                return Problem("Entity set 'OTMFContext.Instructivos'  is null.");
            }
            var instructivo = await _context.InstructivoPiezas.FindAsync(id);
            if (instructivo != null)
            {
                _context.InstructivoPiezas.Remove(instructivo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool InstructivoExists(int id)
        {
            return (_context.InstructivoPiezas?.Any(e => e.IdInstructivoPieza == id)).GetValueOrDefault();
             
        }
    }

}
