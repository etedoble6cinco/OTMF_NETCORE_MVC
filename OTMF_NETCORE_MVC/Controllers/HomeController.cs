using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using OTMF_NETCORE_MVC.Entities;
using System.Globalization;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OTMFContext _context;
        private readonly string con;
        private readonly IServicioUsuarios _services;
        private readonly UserManager<Usuario> userManager;
        private readonly IClaimsTransformation claims;
        private readonly IServicioNotify _servicioNotify;
        private readonly IServicioMaquinas _servicioMaquinas;
        public HomeController(ILogger<HomeController> logger, OTMFContext context,
            IConfiguration configuration, IServicioUsuarios servicioUsuarios,
            UserManager<Usuario> userManager, IClaimsTransformation claims, IServicioNotify servicioNotify,
            IServicioMaquinas servicioMaquinas)
        {
            con = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
            _context = context;
            _services = servicioUsuarios;
            this.userManager = userManager;
            this.claims = claims;
            _servicioNotify = servicioNotify;
            _servicioMaquinas = servicioMaquinas;

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCantidadMeta(int CantidadMeta)
        {
            var meta = await _context.Meta.FirstOrDefaultAsync(m => m.IdMeta == 1);
            meta.CantidadMeta = CantidadMeta;
            _context.Update(meta);
            await _context.SaveChangesAsync();
            return Json(new { data = "" });
        }

        [HttpGet]
        public async Task<int> ObtenerTotalPiezas()
        {
            var procedure = "[ObtenerSumTotalProduccion]";
            using (var connection = new SqlConnection(con))
            {
                int TotalPiezas = await connection.QueryFirstAsync<int>(procedure, commandType: CommandType.StoredProcedure);
                return TotalPiezas;
            }

        }
        [HttpGet]
        public async Task<int> ObtenerCantidadMeta()
        {

            var meta = await _context.Meta.FirstOrDefaultAsync(m => m.IdMeta == 1);
            return (int)meta.CantidadMeta;
        }

        public async Task<IActionResult> CheckRole()
        {
            var estaAutenticado = User.Identity.IsAuthenticated;
            if (estaAutenticado)
            {

                var claims2 = User.Claims; //ver 
            }



            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Privacy()
        {
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GeneralView()
        {
            return _context.Maquinas != null ?
                        View(await _context.Maquinas.OrderBy(m => m.NombreMaquina).ToListAsync()) :
                        Problem("Entity set 'OTMFContext.Maquinas'  is null.");
        }
        //OBTENER EL ULTIMO REGISTRO DE LA BITACORA  DE LA ORDEN DE TRABAJO 
        public async Task<int> ObtenerUltimoRegistroBitacoraByOtIdAndDate(int IdOrdenTrabajoFK)
        {
            var procedure = "[ObtenerUltimoRegistroBitacoraByOtIdAndDate]";

            using (var connection = new SqlConnection(con))
            {
                if ((await _context.BitacoraOrdenTrabajos.AnyAsync(m => m.IdOrdenTrabajoFk == IdOrdenTrabajoFK)))
                {
                    var UltimoRegistroBitacoraOT = await connection.QueryFirstOrDefaultAsync<ObtenerUltimoRegistroBitacoraByOtIdAndDate>(procedure, new
                    {
                        IdOrdenTrabajo = IdOrdenTrabajoFK

                    }, commandType: CommandType.StoredProcedure);
                    ObtenerUltimoRegistroBitacoraByOtIdAndDate n = new ObtenerUltimoRegistroBitacoraByOtIdAndDate();

                    n.IdBitacoraOrdenTrabajo = UltimoRegistroBitacoraOT.IdBitacoraOrdenTrabajo;
                    return n.IdBitacoraOrdenTrabajo;
                } else
                {
                    return 0;
                }






            }
        }
        public async Task<int> ObtenerSumBitacoraOrdenTrabajoProduccion(int IdOrdenTrabajo)
        {
            var procedure = "[ObtenerSumBitacoraOrdenTrabajoProduccion]";
            using (var connection = new SqlConnection(con))
            {
                var data = await connection.QueryFirstOrDefaultAsync<int>(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);

                return data;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerInfoByMaquina(int? id)
        {
            List<GeneralViewDetails> details = new List<GeneralViewDetails>();
            List<MaquinaOrdenTrabajo> maquinaOrdenTrabajos = new List<MaquinaOrdenTrabajo>();
            if (id == null) {
                return Json(new { data = details });
            }
            else {
                if ((await _context.MaquinaOrdenTrabajos.AnyAsync(m => m.IdMaquinaFk == id)))
                {
                    int ot = 0;
                    maquinaOrdenTrabajos = await ObtenerRelacionMaquinaOrdenTrabajo((int)id);
                    foreach (var item in maquinaOrdenTrabajos)
                    {

                        int BOtUltima = await ObtenerUltimoRegistroBitacoraByOtIdAndDate((int)item.IdOrdenTrabajoFk);
                        if (BOtUltima == 0) { }
                        else
                        {
                            var Ot = await _context.OrdenTrabajos.FirstOrDefaultAsync(m => m.IdOrdenTrabajo == item.IdOrdenTrabajoFk);


                            var UltimaBitacoraOrdenTrabajo = await _context.BitacoraOrdenTrabajos.FirstOrDefaultAsync(
                             m => m.IdBitacoraOrdenTrabajo == BOtUltima);
                            GeneralViewDetails generalView = new GeneralViewDetails();
                            generalView.IdMaquina = (int)id;
                            generalView.NombreMaquina = (await _context.Maquinas.FirstOrDefaultAsync(m => m.IdMaquina == id)).NombreMaquina;
                            generalView.IdOrdenTrabajo = (int)item.IdOrdenTrabajoFk;
                            if ((await _context.Partes.AnyAsync(m => m.IdParte == Ot.IdParteFk)))
                            {
                                generalView.NumeroParte = (await _context.Partes.FirstOrDefaultAsync(m => m.IdParte == Ot.IdParteFk)).IdCodigoParte;
                            }

                            generalView.NumeroOrdenTrabajo = Ot.IdCodigoOrdenTrabajo;
                            if (UltimaBitacoraOrdenTrabajo.CantidadPiezasPorOrdenRealizadas > 0)
                            {
                                generalView.PiezasFaltantesTotal = (int)UltimaBitacoraOrdenTrabajo.CantidadPiezasPorOrdenRealizadas;
                            }

                            if ((await ObtenerSumBitacoraOrdenTrabajoProduccion((int)item.IdOrdenTrabajoFk)) > 0)
                            {
                                generalView.PiezasRealizadasTotal = await ObtenerSumBitacoraOrdenTrabajoProduccion((int)item.IdOrdenTrabajoFk);
                            }

                            generalView.IdEstadoOrdenFK = (int)Ot.IdEstadoOrdenFk;
                            details.Add(generalView);




                        }
                    }

                    return Json(new { data = details });


                }
                else
                {


                    return Json(new { data = details });

                }
            }




        }


        public async Task<List<MaquinaOrdenTrabajo>> ObtenerRelacionMaquinaOrdenTrabajo(int IdMaquinaFK)
        {
            var procedure = "[ObtenerRelacionMaquinaOrdenTrabajo]";

            using (var connection = new SqlConnection(con))
            {
                var ListaMaquinasRelacionadas = await connection.QueryAsync<MaquinaOrdenTrabajo>(procedure, new
                {
                    IdMaquinaFK = IdMaquinaFK
                }, commandType: CommandType.StoredProcedure);
                return ListaMaquinasRelacionadas.ToList();
            }
        }
        public async Task<List<ObtenerOrdenesTrabajo>> ObtenerAllInfoOrdenTrabajoByIdMaquina(int IdMaquinaFK)
        {
            var procedure = "[ObtenerOrdenesTrabajo]";

            using (var connection = new SqlConnection(con))
            {
                var ListaOrdenesTrajo = await connection.QueryAsync<ObtenerOrdenesTrabajo>(procedure, new
                {
                    IdMaquinaFK = IdMaquinaFK
                }, commandType: CommandType.StoredProcedure);
                return ListaOrdenesTrajo.ToList();
            }


        }
        [HttpPost]
        public async Task<int> ObtenerSumTotalProduccionByMonth(int mes)
        {
            var procedure = "[ObtenerSumTotalProduccionByMonth]";
            using( var connection = new SqlConnection(con))
            {
                int SumTotalProd = await connection.QueryFirstOrDefaultAsync<int>(procedure, new
                {
                    mes = mes
                }, commandType: CommandType.StoredProcedure);
                return SumTotalProd;    
            }

        }


        [HttpGet]
        public async Task<int> ObtenerSumTotalProduccionByMonthChart(int id)
        {
            var procedure = "[ObtenerSumTotalProduccionByMonth]";
            using (var connection = new SqlConnection(con))
            {
                int SumTotalProd = await connection.QueryFirstOrDefaultAsync<int>(procedure, new
                {
                    mes = id
                }, commandType: CommandType.StoredProcedure);
                return SumTotalProd;
            }

        }
        [HttpGet]
        public async Task<bool> SendNotify(int? id)
        {
            var idMaquinaOrdenTrabajo = await _context.MaquinaOrdenTrabajos.FirstOrDefaultAsync(m => m.IdOrdenTrabajoFk == id);
            var ot = await _context.OrdenTrabajos.FirstOrDefaultAsync(m => m.IdOrdenTrabajo == id);
            var maquina = await _context.Maquinas.FirstOrDefaultAsync(m => m.IdMaquina == idMaquinaOrdenTrabajo.IdMaquinaFk);
            var estado = await _context.EstadoOrdens.FirstOrDefaultAsync(m => m.IdEstadoOrden == ot.IdEstadoOrdenFk);
           
            if ( await _servicioNotify.SendNotify((int)id, maquina.NombreMaquina, estado.NombreEstadoOrden))
            {
                return true;
            }
            else
            {
                
                return false;
            }
           
           

        }
        [HttpGet]
        public async Task<IActionResult> ObtenerPieChartData(int? id)
        {

            int MaquinasOtAsignadas = 0;
            int MaquinaOtNoAsignadas = 0;
            List<int> ints = new List<int>();

            if (await _context.Maquinas.ToListAsync() != null &&
                await _context.MaquinaOrdenTrabajos.ToListAsync() != null &&
                await _context.OrdenTrabajos.ToListAsync() != null &&
                await _context.BitacoraOrdenTrabajos.ToListAsync() != null)
            {


                //OBTENER MAQUINAS CON ORDENES DE TRABAJO EN ESTADO ACTIVO 

                var maquinasActivas = await _servicioMaquinas.ObtenerPieChartDataService(0, 0, 0, 0, 2);

                var maquinasTotales = await _context.Maquinas.ToListAsync();

                foreach (var m in maquinasTotales)
                {
                    if (m.NombreMaquina.Equals("NO ASIGNADO"))
                    {

                    }
                    else
                    {
                        ints.Add(m.IdMaquina);
                    }
                }

                decimal totalMaquinas = ints.Count();
                decimal totalMaquinasActivas = maquinasActivas.Count();

                decimal porcentajeMaquinasActivas = (totalMaquinasActivas * 100) / totalMaquinas;
                decimal porcentajeMaquinasInactivas = 100 - porcentajeMaquinasActivas;
                return Json(new { totalActivas = porcentajeMaquinasActivas , totalInactivas = porcentajeMaquinasInactivas } );

            }

            return Json(new { data = "Empty collections" });
           
            
     
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerNumeroSemanasDashBoard()
        {
            DateTime time = DateTime.Now;
            int WeekNumber = GetIso8601WeekOfYear(time);

            return Json( new {data = WeekNumber});
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}

