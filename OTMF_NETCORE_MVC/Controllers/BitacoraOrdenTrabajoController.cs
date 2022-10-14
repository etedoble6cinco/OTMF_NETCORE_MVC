using Microsoft.AspNetCore.Mvc;
using OTMF_NETCORE_MVC.Hubs;
using OTMF_NETCORE_MVC.Models;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class BitacoraOrdenTrabajoController : Controller
    {

        private readonly OTMFContext _context;
        private readonly string con;
        private readonly string connectionString;
        DashboardHub dashboardHub;
        public BitacoraOrdenTrabajoController(DashboardHub dashboardHub, OTMFContext context, IConfiguration configuration)
        {
            this.dashboardHub = dashboardHub;
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            con = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> InsertRegistroBitacoraOrdenTrabajo(
            int CantidadPiezasPorOrden,
            int IdEstadoOrdenFK,
            int NumeroTurnoTerminados,
            int IdTurnoOtFK,
            decimal ScrapCalculado,
            decimal EstandarCalculado,
            decimal EstandarConRelevoCalculado,
            decimal EstandarPorHorasCalculado,
            decimal FracEstandarConRelevo , 
            int IdOrdenTrabajoFK,
            int PiezasRecibidas,
            int CajasRecibidas,
            int NumeroPiezasRecibidas,
            int IdUsuarioFK

            )
        {

            return Json(new { data = "" });
        }
       
    }
}
