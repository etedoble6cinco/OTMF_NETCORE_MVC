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
         Task<List<ReporteProduccionViewModel>> CrearReporteProduccion(DateTime datetime);
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

        //P:\BITACORAMF\Registro de producción\2022\REGISTRO PRODUCCION SISTEMA
        public async Task<List<ReporteProduccionViewModel>>  CrearReporteProduccion(DateTime ReporteDate)
        {
                var maquinas = await _context.Maquinas.OrderBy(m => m.NombreMaquina).ToListAsync();
            List<ReporteProduccionViewModel> ReporteProduccionList = new List<ReporteProduccionViewModel>();
            ReporteProduccionViewModel reporteProduccion = new ReporteProduccionViewModel();
            foreach (var n in maquinas)
                {
               
                reporteProduccion.NombreMaquina = n.NombreMaquina;
                var bitacora = await RegistrarReporteProduccion(ReporteDate, n.IdMaquina);
                   
                    foreach (var m in bitacora)
                    {

                    BitacoraOrdenTrabajoReporte BitacoraOrdenTrabajoList = new BitacoraOrdenTrabajoReporte();
                            int IdUsuarioFK = _servicioUsuarios.ObtenerUsuarioId();
                            int IdParteFK = (int)(await _context.OrdenTrabajos.FirstOrDefaultAsync(r => r.IdOrdenTrabajo == m.IdOrdenTrabajoFK)).IdParteFk;
                         
                            BitacoraOrdenTrabajoList.ClaveEmpleado = (await _context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == m.IdEmpleadoMoldeoFK)) == null ? "NO REGISTRADO" : (await _context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == m.IdEmpleadoMoldeoFK)).ClaveEmpleado;
                            BitacoraOrdenTrabajoList.ClaveEmpleado = (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)) == null ? "NO REGISTRADO" : (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)).IdCodigoParte;
                            BitacoraOrdenTrabajoList.NumeroCavidades = m.NumeroCavidades == null ? 0 : m.NumeroCavidades.Value;
                            BitacoraOrdenTrabajoList.HorasTrabajadasCalculado = m.HorasTrabajadasCalculado == null ? 0 : m.HorasTrabajadasCalculado.Value;
                            BitacoraOrdenTrabajoList.EstandarPorHoraCalculado = m.EstandarPorHorasCalculado == null ? 0 : m.EstandarPorHorasCalculado.Value;
                            BitacoraOrdenTrabajoList.EstandarCalculado = m.EstandarCalculado == null ? 0 : m.EstandarCalculado.Value;
                            BitacoraOrdenTrabajoList.TiempoAcumulado = m.TiempoAcumulado == null ? 0 : m.TiempoAcumulado.Value;
                            BitacoraOrdenTrabajoList.CreationDateTime = DateTime.Now;
                            BitacoraOrdenTrabajoList.CreateBy = await ObtenerEmailUsuario(); 
                            BitacoraOrdenTrabajoList.FechaProduccion = ReporteDate;
                            BitacoraOrdenTrabajoList.Eficiencia = 0;
                            BitacoraOrdenTrabajoList.ProduccionBitacora = (int)m.NumeroPiezasRealizadas;
                            var DuracionEstados = _context.DuracionEstados.Where(x => x.IdBitacoraOrdenTrabajoFk == m.IdBitacoraOrdenTrabajo).ToList();
                    foreach (var d in DuracionEstados)
                    {
                        if(d.IdEstadoOrdenFk == 1)
                        {
                            DuracionEstado duracionPausada = new DuracionEstado();
                        }
                        if(d.IdEstadoOrdenFk == 2)
                        {
                            DuracionEstado duracionActiva = new DuracionEstado();
                        }
                        if(d.IdEstadoOrdenFk == 3)
                        {
                            DuracionEstado duracionPorLiberar = new DuracionEstado();
                        }
                      
                        
                      

                        

                    }
                }
                    
                
                }
            return ReporteProduccionList.ToList();
        }
        public async Task<string> ObtenerEmailUsuario()
        {
            int idUsaurio = _servicioUsuarios.ObtenerUsuarioId();
            string email = (await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuarios == idUsaurio)).Email;
            
            return email;
        }
        public async Task<List<CausasTiempoMuertoReporteProduccion>> ObtenerCausasTiempoMuertoReporteProduccion(int IdOrdenTrabajo , DateTime ReporteDate)
        {

            var procedure = "[ObtenerCausasTiempoMuertoReporteProduccion]";
            using (var connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<CausasTiempoMuertoReporteProduccion>(procedure, new
                {
                    ReporteDate= ReporteDate
                }, commandType: CommandType.StoredProcedure);

                List<CausasTiempoMuertoReporteProduccion> result = new List<CausasTiempoMuertoReporteProduccion>();
                result = data.ToList();
                            
                return result;
            }





        }
        public async Task<List<ObtenerReporteProduccionByDate>> RegistrarReporteProduccion(DateTime ReporteDate , int IdMaquinaFK  )
        {
            var procedure = "[ObtenerReporteProduccionByDate]";
            using (var connection = new SqlConnection(connectionString)) {

                var IdReporteProduccion = await connection.QueryAsync<ObtenerReporteProduccionByDate>(procedure, new
                {
                    IdMaquinaFK = IdMaquinaFK,
                    ReporteDate = ReporteDate
                }, commandType: CommandType.StoredProcedure);
              
                return IdReporteProduccion.ToList();

            }
            
        }
    



    }
}
