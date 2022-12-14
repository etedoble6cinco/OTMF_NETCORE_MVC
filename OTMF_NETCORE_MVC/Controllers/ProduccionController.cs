using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Reportes;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class ProduccionController : Controller
    {
        private readonly OTMFContext _context;
        private readonly IReporteProduccion _reporte;
        public ProduccionController(IReporteProduccion reporte , OTMFContext context)
        {
            _context = context; 
            _reporte= reporte;  
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Reporte()
        {
            ViewData["IdOrdenTrabajo"] = new SelectList(_context.OrdenTrabajos, "IdOrdenTrabajo", "IdCodigoOrdenTrabajo");
            return View();
        }
        public async Task<IActionResult> ObtenerReporteProduccion(DateTime ReporteDate)
        { 
            string fileLocation = string.Empty;
            //fileLocation = await _reporte.CrearReporteProduccion(ReporteDate);
            return Json( new {data = fileLocation});
        }
    }
}
