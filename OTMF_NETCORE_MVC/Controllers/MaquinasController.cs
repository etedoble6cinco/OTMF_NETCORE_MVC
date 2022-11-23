using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Services;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class MaquinasController : Controller
    {
        private readonly OTMFContext _context;
        private readonly IServicioMaquinas _servicioMaquinas;

        public MaquinasController(OTMFContext context, IServicioMaquinas servicioMaquinas)
        {
            _servicioMaquinas = servicioMaquinas;
            _context = context;
        }

        // GET: Maquinas
        public async Task<IActionResult> Index()
        {
            return _context.Maquinas != null ?
                        View(await _context.Maquinas.ToListAsync()) :
                        Problem("Entity set 'OTMFContext.Maquinas'  is null.");
        }

        // GET: Maquinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Maquinas == null)
            {
                return NotFound();
            }

            var maquina = await _context.Maquinas
                .FirstOrDefaultAsync(m => m.IdMaquina == id);
            if (maquina == null)
            {
                return NotFound();
            }

            return View(maquina);
        }

        // GET: Maquinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maquinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMaquina,NombreMaquina,EstadoMaquina")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maquina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maquina);
        }

        // GET: Maquinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Maquinas == null)
            {
                return NotFound();
            }

            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null)
            {
                return NotFound();
            }
            return View(maquina);
        }

        // POST: Maquinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMaquina,NombreMaquina,EstadoMaquina")] Maquina maquina)
        {
            if (id != maquina.IdMaquina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maquina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaquinaExists(maquina.IdMaquina))
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
            return View(maquina);
        }

        // GET: Maquinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Maquinas == null)
            {
                return NotFound();
            }

            var maquina = await _context.Maquinas
                .FirstOrDefaultAsync(m => m.IdMaquina == id);
            if (maquina == null)
            {
                return NotFound();
            }

            return View(maquina);
        }

        // POST: Maquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Maquinas == null)
            {
                return Problem("Entity set 'OTMFContext.Maquinas'  is null.");
            }
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina != null)
            {
                _context.Maquinas.Remove(maquina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaquinaExists(int id)
        {
            return (_context.Maquinas?.Any(e => e.IdMaquina == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> EstadisticasMaquinas()
        {

            return View();
        }
        [HttpPost]  //OBTENER POR CADA DIA DEL MES NUMERO DE MAQUINAS ACTIVAS
        public async Task<IActionResult> ObtenerFechaSumIdMaquinasBitacoraOrdenTrabajo([FromBody] MaquinasActivasViewModel input)
        {
         

            List<MaquinasActivas> DiasMaquinasActivas = new List<MaquinasActivas>();
            if (_context.Maquinas != null)
            {
                var maquinas = await _context.Maquinas.ToListAsync();
           
                int MaquinasActivasCounter = 0;
              

                for (int x = 1; x <= input.dias; x++)
                {
                    foreach (var maquina in maquinas)
                    {
                        int SumMaquina = await _servicioMaquinas.ObtenerSumIdMaquinaBitacoraOrdenTrabajo(
                    maquina.IdMaquina, input.date.Year, x, input.date.Month, 3);
                        if (SumMaquina > 0)
                        {
                            MaquinasActivasCounter++; //CONTADOR DE MAQUINAS ACTIVAS

                        }
                    }
                    MaquinasActivas DiaMaquinaActiva = new MaquinasActivas();
                    DiaMaquinaActiva.DiaMaquinasActivas = x;
                    DiaMaquinaActiva.NumeroMaquinasActivas = MaquinasActivasCounter;
                    DiasMaquinasActivas.Add(DiaMaquinaActiva);
                    MaquinasActivasCounter = 0;
                  //ASIGNACION DE VALOR DE CONTADOR DE MAQUINAS A 0 
                }

            }

            return Json(new { data = DiasMaquinasActivas });

        }
        //UNA FECHA PARA TODAS LAS MAQUINAS 
        //OBTENER ANIO Y MES , RECORRER TODOS LOS DIAS DE ESE MES  
    }
}
