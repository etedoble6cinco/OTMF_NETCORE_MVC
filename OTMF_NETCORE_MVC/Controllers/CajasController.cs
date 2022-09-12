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
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Tools;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class CajasController : Controller
    {
        private readonly OTMFContext _context;
       
        private IWebHostEnvironment webHostEnvironment;
        private readonly string connectionString;
        public CajasController(OTMFContext context, IWebHostEnvironment Environment , IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            webHostEnvironment = Environment;
        }

        // GET: Cajas
        public async Task<IActionResult> Index()
        {
              return _context.Cajas != null ? 
                          View(await _context.Cajas.ToListAsync()) :
                          Problem("Entity set 'OTMFContext.Cajas'  is null.");
        }

        // GET: Cajas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cajas == null)
            {
                return NotFound();
            }

            var caja = await _context.Cajas
                .FirstOrDefaultAsync(m => m.IdCaja == id);
            if (caja == null)
            {
                return NotFound();
            }

            return View(caja);
        }

        // GET: Cajas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCaja,NombreCaja,LogoCaja,EtiquetaDeCaja")] Caja caja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caja);
        }

        // GET: Cajas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cajas == null)
            {
                return NotFound();
            }

            var caja = await _context.Cajas.FindAsync(id);
            if (caja == null)
            {
                return NotFound();
            }
            return View(caja);
        }

        // POST: Cajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCaja,NombreCaja,LogoCaja,EtiquetaDeCaja")] Caja caja)
        {
            if (id != caja.IdCaja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CajaExists(caja.IdCaja))
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
            return View(caja);
        }

        // GET: Cajas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cajas == null)
            {
                return NotFound();
            }

            var caja = await _context.Cajas
                .FirstOrDefaultAsync(m => m.IdCaja == id);
            if (caja == null)
            {
                return NotFound();
            }

            return View(caja);
        }

        // POST: Cajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cajas == null)
            {
                return Problem("Entity set 'OTMFContext.Cajas'  is null.");
            }
            var caja = await _context.Cajas.FindAsync(id);
            if (caja != null)
            {
                _context.Cajas.Remove(caja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CajaExists(int id)
        {
          return (_context.Cajas?.Any(e => e.IdCaja == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage() {
            string guid = string.Empty;
            string Result = string.Empty;
            string NombreCaja = string.Empty;
            var Files = Request.Form.Files;
            var keys = Request.Form.Keys;
            int IdCaja = 0;
            foreach(var key in keys)
            {
                NombreCaja = key.ToString();
                
            }
            foreach(IFormFile source in Files)
            {
                Guid id = Guid.NewGuid();
                guid = id.ToString();
                string FileName = NombreCaja+"_"+source.Name+".jpeg";
                string imagepath =  GetEtiquetasCajasImagePath(FileName); 
                try
                {
                    if(System.IO.File.Exists(imagepath))
                        System.IO.File.Delete(imagepath);
                    using(FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Result = "pass";
                        UpsertImagenEtiquetaCaja(NombreCaja,true, NombreCaja+"_"+ source.Name, IdCaja );
                        
                    }
                 
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }

            }
            return RedirectToAction(nameof(Index));

        }
        public string GetEtiquetasCajasImagePath(string FileName)
        {
            return Path.Combine(webHostEnvironment.WebRootPath + "\\Uploads\\Etiquetas\\Cajas\\", FileName);
        }

        public void UpsertImagenEtiquetaCaja(string nombreCaja, bool logoCaja, string etiquetaCaja , int IdCaja)
        {
            var procedure = "[UpsertImagenEtiquetaCaja]";
            
            using( var connection  = new SqlConnection(connectionString))
            {
                var confirm = connection.Query(procedure, new
                {
                    NombreCaja =  nombreCaja ,
                    LogoCaja = logoCaja ,
                    EtiquetaDeCaja = etiquetaCaja,
                    IdCaja = IdCaja

                }, commandType: CommandType.StoredProcedure);
            }

           
        }
        

    }
}
