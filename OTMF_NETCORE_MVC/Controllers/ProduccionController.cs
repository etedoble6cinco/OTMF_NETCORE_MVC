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
      
            return View();
        }
        public async Task<IActionResult> ObtenerReporteProduccion(DateTime ReporteDate)
        { 
          
             var data = await _reporte.CrearReporteProduccion(ReporteDate);
            return Json( new {data = data});
        }


        public IActionResult ReporteProduccion()
        {


            return View();
        }
    }
}
