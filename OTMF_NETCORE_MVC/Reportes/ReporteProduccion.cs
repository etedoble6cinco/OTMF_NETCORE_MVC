using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Services;
using System.Data;
using System.Data.SqlClient;


namespace OTMF_NETCORE_MVC.Reportes
{
    public interface IReporteProduccion
    {
         Task<List<BitacoraOrdenTrabajoReporte>> CrearReporteProduccion(DateTime datetime);
        Task<List<ReporteProduccionViewModel>> CrearReporteProduccionDetallado(DateTime dateTime);
    }
    public class ReporteProduccion : IReporteProduccion
    {
        private readonly string connectionString;
        private readonly OTMFContext _context;
        public IWebHostEnvironment webHostEnvironment;
        private readonly HttpContext _httpContext;
        private readonly IServicioUsuarios _servicioUsuarios;
        public ReporteProduccion(OTMFContext context,IWebHostEnvironment environment, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            webHostEnvironment = environment;
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public string GetFilePath(string FileName)
        {
            return Path.Combine(webHostEnvironment.WebRootPath + "\\Uploads\\TemplatesReporteProduccion\\", FileName);
        }

    
        public async Task<List<ReporteProduccionViewModel>>  CrearReporteProduccionDetallado(DateTime ReporteDate)
        {
            DateTime d1 = new DateTime(ReporteDate.Year,ReporteDate.Month,ReporteDate.Day,12,00,00,DateTimeKind.Local);
                var maquinas = await _context.Maquinas.OrderBy(m => m.NombreMaquina).ToListAsync();
            List<ReporteProduccionViewModel> ReporteProduccionList = new List<ReporteProduccionViewModel>();
                     
        
            foreach (var n in maquinas)
                {
                ReporteProduccionViewModel reporteProduccion = new ReporteProduccionViewModel();
              
                 reporteProduccion.NombreMaquina = n.NombreMaquina;
                 reporteProduccion.IdMaquinaFK = n.IdMaquina;
              
                var bitacora = await RegistrarReporteProduccion(ReporteDate, n.IdMaquina);
                List<BitacoraOrdenTrabajoReporte> Bitacoras = new List<BitacoraOrdenTrabajoReporte>();
                foreach (var m in bitacora)
                    {

                    BitacoraOrdenTrabajoReporte BitacoraOrdenTrabajoList = new BitacoraOrdenTrabajoReporte();
                  
                    int IdParteFK = (int)(await _context.OrdenTrabajos.FirstOrDefaultAsync(r => r.IdOrdenTrabajo == m.IdOrdenTrabajoFK)).IdParteFk;

                    BitacoraOrdenTrabajoList.ClaveEmpleado = m.ClaveEmpleado;
                    BitacoraOrdenTrabajoList.CodigoParte = (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)) == null ? "NO REGISTRADO" : (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)).IdCodigoParte;
                    BitacoraOrdenTrabajoList.NumeroCavidades = m.NumeroCavidades;
                    BitacoraOrdenTrabajoList.EstandarPorHorasCalculado = m.EstandarPorHorasCalculado;
                    BitacoraOrdenTrabajoList.EstandarCalculado = m.EstandarCalculado;
                    BitacoraOrdenTrabajoList.HorasTrabajadasAcumulado = m.HorasTrabajadasAcumulado;
                    BitacoraOrdenTrabajoList.FechaReporteProduccion = ReporteDate;
                    BitacoraOrdenTrabajoList.Eficiencia = 0;
                    BitacoraOrdenTrabajoList.NumeroPiezasRealizadas = m.NumeroPiezasRealizadas;
                    BitacoraOrdenTrabajoList.IdBitacoraOrdenTrabajo = m.IdBitacoraOrdenTrabajo;
                    BitacoraOrdenTrabajoList.NombreMaquina = m.NombreMaquina;
                    BitacoraOrdenTrabajoList.IdOrdenTrabajoFK = m.IdOrdenTrabajoFK;
                 
                     List<DuracionEstadosReporte> duracionPausada = new List<DuracionEstadosReporte>();  
                     List<DuracionEstadosReporte> duracionActiva = new List<DuracionEstadosReporte>();  
                     List<DuracionEstadosReporte> duracionParaLiberar = new List<DuracionEstadosReporte>();  
                     List<DuracionEstadosReporte> duracionEstados = new List<DuracionEstadosReporte>();  
                   
                     var procedure = "[ObtenerDuracionEstadoByIdBitacoraOrdenTrabajo]";
                     using (var connection = new SqlConnection(connectionString))
                         {
                             var result = await connection.QueryAsync<DuracionEstadosReporte>(procedure, new
                                         {
                                             IdBitacoraOrdenTrabajoFK = m.IdBitacoraOrdenTrabajo
                                         }, commandType: CommandType.StoredProcedure);

                              duracionEstados = result.ToList();

                         }

                    foreach (var d in duracionEstados)
                    {
                        if(d.IdEstadoOrdenFK == 12)
                        { 
                            DuracionEstadosReporte e = new DuracionEstadosReporte();
                             
                            e.IdDuracionEstados = d.IdDuracionEstados;
                            e.NombreMotivoCambioEstado = d.NombreMotivoCambioEstado;
                            e.NombreMotivoCambioEstadoDerivado = d.NombreMotivoCambioEstadoDerivado;
                            e.InicioEstado = d.InicioEstado;
                            e.FinalEstado = d.FinalEstado;
                            e.Duracion = d.Duracion;
                            e.NombreEstadoOrden = d.NombreEstadoOrden;
                            duracionPausada.Add(e);
                        }
                    }
                    foreach (var d in duracionEstados)
                    {
                        if(d.IdEstadoOrdenFK == 9)
                        {
                            DuracionEstadosReporte e = new DuracionEstadosReporte();
                            e.IdDuracionEstados = d.IdDuracionEstados;
                            e.NombreMotivoCambioEstado = d.NombreMotivoCambioEstado;
                            e.NombreMotivoCambioEstadoDerivado = d.NombreMotivoCambioEstadoDerivado;
                            e.InicioEstado = d.InicioEstado;
                            e.FinalEstado = d.FinalEstado;
                            e.Duracion = d.Duracion;
                            e.NombreEstadoOrden = d.NombreEstadoOrden;
                            duracionActiva.Add(e);
                        }
                    }
                    
                    foreach (var d in duracionEstados)
                    {
                        if(d.IdEstadoOrdenFK == 10)
                        {
                            DuracionEstadosReporte e = new DuracionEstadosReporte();
                            e.IdDuracionEstados = d.IdDuracionEstados;
                            e.NombreMotivoCambioEstado = d.NombreMotivoCambioEstado;
                            e.NombreMotivoCambioEstadoDerivado = d.NombreMotivoCambioEstadoDerivado;
                            e.InicioEstado = d.InicioEstado;
                            e.FinalEstado = d.FinalEstado;
                            e.Duracion = d.Duracion;
                            e.NombreEstadoOrden = d.NombreEstadoOrden;
                            duracionParaLiberar.Add(e);
                        }
                    }
                    BitacoraOrdenTrabajoList.DuracionActiva = duracionActiva;
                    BitacoraOrdenTrabajoList.DuracionPausada = duracionPausada;
                    BitacoraOrdenTrabajoList.DuracionPorLiberar = duracionParaLiberar;

                    Bitacoras.Add(BitacoraOrdenTrabajoList);
                   
                }
                reporteProduccion.BitacorasList = Bitacoras;
                ReporteProduccionList.Add(reporteProduccion);
                
                }
            return ReporteProduccionList.ToList();
        }
        public async Task<List<BitacoraOrdenTrabajoReporte>> CrearReporteProduccion(DateTime ReporteDate)
        {
            DateTime d1 = new DateTime(ReporteDate.Year, ReporteDate.Month, ReporteDate.Day, 12, 00, 00, DateTimeKind.Local);
            var maquinas = await _context.Maquinas.OrderBy(m => m.NombreMaquina).ToListAsync();
         
            List<BitacoraOrdenTrabajoReporte> Bitacoras = new List<BitacoraOrdenTrabajoReporte>();

            foreach (var n in maquinas)
            {
                ReporteProduccionViewModel reporteProduccion = new ReporteProduccionViewModel();

                reporteProduccion.NombreMaquina = n.NombreMaquina;
                reporteProduccion.IdMaquinaFK = n.IdMaquina;

                var bitacora = await RegistrarReporteProduccion(ReporteDate, n.IdMaquina);
               
                foreach (var m in bitacora)
                {

                    BitacoraOrdenTrabajoReporte BitacoraOrdenTrabajoList = new BitacoraOrdenTrabajoReporte();

                    int IdParteFK = (int)(await _context.OrdenTrabajos.FirstOrDefaultAsync(r => r.IdOrdenTrabajo == m.IdOrdenTrabajoFK)).IdParteFk;

                    BitacoraOrdenTrabajoList.ClaveEmpleado = m.ClaveEmpleado;
                    BitacoraOrdenTrabajoList.CodigoParte = (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)) == null ? "NO REGISTRADO" : (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)).IdCodigoParte;
                    BitacoraOrdenTrabajoList.NumeroCavidades = m.NumeroCavidades;
                    BitacoraOrdenTrabajoList.EstandarPorHorasCalculado = m.EstandarPorHorasCalculado;
                    BitacoraOrdenTrabajoList.EstandarCalculado = m.EstandarCalculado;
                    BitacoraOrdenTrabajoList.HorasTrabajadasAcumulado = m.HorasTrabajadasAcumulado;
                    BitacoraOrdenTrabajoList.FechaReporteProduccion = ReporteDate;
                    BitacoraOrdenTrabajoList.Eficiencia = 0;
                    BitacoraOrdenTrabajoList.NumeroPiezasRealizadas = m.NumeroPiezasRealizadas;
                    BitacoraOrdenTrabajoList.IdBitacoraOrdenTrabajo = m.IdBitacoraOrdenTrabajo;
                    BitacoraOrdenTrabajoList.NombreMaquina = m.NombreMaquina;
                    BitacoraOrdenTrabajoList.IdOrdenTrabajoFK = m.IdOrdenTrabajoFK;
                    BitacoraOrdenTrabajoList.IdCodigoOrdenTrabajo = m.IdCodigoOrdenTrabajo;
                    BitacoraOrdenTrabajoList.HorasTrabajadasCalculado = m.HorasTrabajadasCalculado;
                    var tiempos = await ObtenerTiemposMuertosByBitacoraOrdenTrabajo(m.IdBitacoraOrdenTrabajo);
                    int activa = 0;
                    int pausa = 0;
                    int liberar = 0;
                    foreach(var t in tiempos)
                    {
                        if (t.NombreEstadoOrden.Equals("ACTIVA"))
                        {
                            activa = t.Duracion + activa;   
                        }
                      
                    }   foreach(var t in tiempos)
                    {
                      
                        if (t.NombreEstadoOrden.Equals("PAUSADA"))
                        {
                            pausa = t.Duracion + pausa;
                        }

                    }   foreach(var t in tiempos)
                    {
                        if (t.NombreEstadoOrden.Equals("PARA LIBERAR"))
                        {
                            liberar = t.Duracion + liberar;
                        }
                       

                    }

                    BitacoraOrdenTrabajoList.Activa = activa;
                    BitacoraOrdenTrabajoList.Pausa = pausa; 
                    BitacoraOrdenTrabajoList.Liberar = liberar;
                    BitacoraOrdenTrabajoList.HorasTrabajadasAcumulado = BitacoraOrdenTrabajoList.HorasTrabajadasAcumulado+pausa;
                    Bitacoras.Add(BitacoraOrdenTrabajoList);

                }
                reporteProduccion.BitacorasList = Bitacoras;
              

            }
            return Bitacoras.ToList();
        }
        public async Task<List<ObtenerTiemposMuertosByBitacoraOrdenTrabajo>> ObtenerTiemposMuertosByBitacoraOrdenTrabajo(int IdBitacoraOrdenTrabajo)
        {

            var procedure = "[ObtenerTiemposMuertosByBitacoraOrdenTrabajo]";
            using (var connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<ObtenerTiemposMuertosByBitacoraOrdenTrabajo>(procedure, new
                {
                    IdBitacoraOrdenTrabajo= IdBitacoraOrdenTrabajo,
                }, commandType: CommandType.StoredProcedure);

                List<ObtenerTiemposMuertosByBitacoraOrdenTrabajo> result = new List<ObtenerTiemposMuertosByBitacoraOrdenTrabajo>();
                result = data.ToList();
                            
                return result;
            }





        }
        public async Task<List<BitacoraOrdenTrabajoReporte>> RegistrarReporteProduccion(DateTime ReporteDate , int IdMaquinaFK  )
        {
            var procedure = "[ObtenerReporteProduccionByDate]";
            using (var connection = new SqlConnection(connectionString)) {

                var IdReporteProduccion = await connection.QueryAsync<BitacoraOrdenTrabajoReporte>(procedure, new
                {
                    IdMaquinaFK = IdMaquinaFK,
                    ReporteDate = ReporteDate
                }, commandType: CommandType.StoredProcedure);
              
                return IdReporteProduccion.ToList();

            }
            
        }
    



    }
}
