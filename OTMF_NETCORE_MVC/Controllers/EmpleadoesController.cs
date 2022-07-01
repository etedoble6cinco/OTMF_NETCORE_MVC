using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class EmpleadoesController : Controller
    {
        private readonly OTMFContext _context;
        private readonly string con;

        public EmpleadoesController(OTMFContext context , IConfiguration configuracion)
        {
            _context = context;
            con = configuracion.GetConnectionString("DefaultConnection");
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index()
        {
            var oTMFContext = _context.Empleados.Include(e => e.IdTipoEmpleadoFkNavigation).Include(e => e.IdTurnoFkNavigation);
            return View(await oTMFContext.ToListAsync());
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdTipoEmpleadoFkNavigation)
                .Include(e => e.IdTurnoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            ViewData["IdTipoEmpleadoFk"] = new SelectList(_context.TipoEmpleados, "IdTipoEmpleado", "NombreTipoEmpleado");
            ViewData["IdTurnoFk"] = new SelectList(_context.Turnos, "IdTurno", "NombreTurno");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,NombreEmpleado,ClaveEmpleado,Estado,IdTipoEmpleadoFk,IdTurnoFk")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoEmpleadoFk"] = new SelectList(_context.TipoEmpleados, "IdTipoEmpleado", "NombreTipoEmpleado", empleado.IdTipoEmpleadoFk);
            ViewData["IdTurnoFk"] = new SelectList(_context.Turnos, "IdTurno", "NombreTurno", empleado.IdTurnoFk);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["IdTipoEmpleadoFk"] = new SelectList(_context.TipoEmpleados, "IdTipoEmpleado", "NombreTipoEmpleado", empleado.IdTipoEmpleadoFk);
            ViewData["IdTurnoFk"] = new SelectList(_context.Turnos, "IdTurno", "NombreTurno", empleado.IdTurnoFk);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,NombreEmpleado,ClaveEmpleado,Estado,IdTipoEmpleadoFk,IdTurnoFk")] Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.IdEmpleado))
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
            ViewData["IdTipoEmpleadoFk"] = new SelectList(_context.TipoEmpleados, "IdTipoEmpleado", "NombreTipoEmpleado", empleado.IdTipoEmpleadoFk);
            ViewData["IdTurnoFk"] = new SelectList(_context.Turnos, "IdTurno", "NombreTurno", empleado.IdTurnoFk);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdTipoEmpleadoFkNavigation)
                .Include(e => e.IdTurnoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'OTMFContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleados?.Any(e => e.IdEmpleado == id)).GetValueOrDefault();
        }
    
        public JsonResult ObtenerEmpleados()
        {
            var OE = "[ObtenerEmpleados]";
            using(var connection =  new SqlConnection(con))
            {
                var empleados = connection.Query<ObtenerEmpleados>(OE,
                    commandType: CommandType.StoredProcedure);
             return Json(new { data = empleados });    
            }
           
        }

    }
}
