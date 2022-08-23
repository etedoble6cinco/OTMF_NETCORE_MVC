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

namespace OTMF_NETCORE_MVC.Controllers
{
    public class EtiquetaDeCajaController : Controller
    {
        private readonly OTMFContext _context;
        private IWebHostEnvironment webHostEnvironment;
        private readonly string connectionString;
        public EtiquetaDeCajaController(OTMFContext context, IWebHostEnvironment environment, IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            webHostEnvironment = environment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.EtiquetaCajas != null ?
                        View(await _context.EtiquetaCajas.ToListAsync()) :
                        Problem("Entity set 'OTMFContext.Etiqueta'  is null.");
        }
     
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage()
        {
            string guid = string.Empty;
            string Result = string.Empty;
            string NombreEtiquetaDeCaja = string.Empty;
            var Files = Request.Form.Files;
            var keys = Request.Form.Keys;
            int IdCaja = 0;
            foreach (var key in keys)
            {
                NombreEtiquetaDeCaja = key.ToString();

            }
            foreach (IFormFile source in Files)
            {
                Guid id = Guid.NewGuid();
                guid = id.ToString();
                string FileName = NombreEtiquetaDeCaja+".jpeg";
                string imagepath = GetEtiquetasCajasImagePath(FileName);
                try
                {
                    if (System.IO.File.Exists(imagepath))
                        System.IO.File.Delete(imagepath);
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Result = "pass";
                        UpsertImagenEtiquetaCaja(0 , NombreEtiquetaDeCaja);

                    }

                }
                catch (Exception e)
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

        public void UpsertImagenEtiquetaCaja(int IdEtiquetaDeCaja , string NombreEtiquetaDeCaja)
        {
            var procedure = "[UpsertImagenEtiquetaCaja]";

            using (var connection = new SqlConnection(connectionString))
            {
                var confirm = connection.Query(procedure, new
                {
                    IdEtiquetaDeCaja = IdEtiquetaDeCaja,
                    NombreEtiquetaDeCaja = NombreEtiquetaDeCaja
                   

                }, commandType: CommandType.StoredProcedure);
            }


        }
    }
}
