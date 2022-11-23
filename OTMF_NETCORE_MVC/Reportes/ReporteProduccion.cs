using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using System.Data;
using System.Data.SqlClient;


namespace OTMF_NETCORE_MVC.Reportes
{
    public interface IReporteProduccion
    {
         Task<string> CrearReporteProduccion(DateTime datetime);
    }
    public class ReporteProduccion : IReporteProduccion
    {
        private readonly string connectionString;
        private readonly OTMFContext _context;
        public IWebHostEnvironment webHostEnvironment;
        private readonly HttpContext _httpContext;
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
        public async Task<string>  CrearReporteProduccion(DateTime ReporteDate)
        {
            //List<CausasTiempoMuertoReporteProduccion> result = new List<CausasTiempoMuertoReporteProduccion>();
            string fileName = "ReporteProduccion_" + Guid.NewGuid().ToString() + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".xlsx";
            FileInfo file = new FileInfo(GetFilePath("Template-Registro-Produccion-Limpio.xlsx"));
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                //VALIDACION DE TURNO : EVALUAR SI EL REPORTE SERA PARA LA PRODUCCION DEL TURNO 1 O 2
               
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Support SMIMX";
                excelPackage.Workbook.Properties.Title = "Reporte de Produccion";
                excelPackage.Workbook.Properties.Subject = DateTime.Now.ToString();
                excelPackage.Workbook.Properties.Created = DateTime.Now;


                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(m => m.Name.Equals("Sheet1"));

                //Add some text to cell A1
               /* worksheet.Cells["A1"].Value = "Empleado";
                worksheet.Cells["B1"].Value = "Nombre";
                worksheet.Cells["C1"].Value = "Hora Salida";
                worksheet.Cells["D1"].Value = "Hora Entrada";
                worksheet.Cells["E1"].Value = "Duracion En Minutos";
                worksheet.Cells["F1"].Value = "Cerrado por Sistema";

                worksheet.Column(1).Width = 8;
                worksheet.Column(2).Width = 25;
                worksheet.Column(3).Width = 18;
                worksheet.Column(4).Width = 18;
                worksheet.Column(5).Width = 16;
                worksheet.Column(6).Width = 16;

                //color column 
                worksheet.Row(1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Row(1).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffe252"));
                //border column 
                //worksheet.Cells["A1:D5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;

                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Font.Name = "Arial";
                worksheet.Row(1).Style.Font.Size = 8;

                worksheet.HeaderFooter.FirstFooter.LeftAlignedText = "Registros de Entradas / Salidas";
                worksheet.HeaderFooter.FirstHeader.CenteredText = "Registros de Entradas / Salidas";

                */
                int initialRow = 7;
                int initialRowMaquinas = 7;
                int mp = 1;
                var maquinas = await _context.Maquinas.OrderBy(m => m.NombreMaquina).ToListAsync();
               
                worksheet.Cells[4, 9].Value = ReporteDate.Day.ToString();
                worksheet.Cells[4, 10].Value = ReporteDate.Month.ToString();
                worksheet.Cells[4, 11].Value = ReporteDate.Year.ToString();
                foreach (var n in maquinas)
                {
                   
                    var bitacora = await RegistrarReporteProduccion(ReporteDate, n.IdMaquina);
                   
                    foreach (var m in bitacora)
                    {
                            
                            int IdParteFK = (int)(await _context.OrdenTrabajos.FirstOrDefaultAsync(r => r.IdOrdenTrabajo == m.IdOrdenTrabajoFK)).IdParteFk;
                            worksheet.Cells[initialRow, 1].Value = m.NombreMaquina;
                            worksheet.Cells[initialRow, 2].Value = (await _context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == m.IdEmpleadoMoldeoFK)) == null ? "NO REGISTRADO" : (await _context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == m.IdEmpleadoMoldeoFK)).ClaveEmpleado;
                            worksheet.Cells[initialRow, 3].Value = (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)) == null ? "NO REGISTRADO" : (await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == IdParteFK)).IdCodigoParte;
                            worksheet.Cells[initialRow, 4].Value = m.NumeroCavidades == null ? 0 : m.NumeroCavidades.Value;
                            worksheet.Cells[initialRow, 5].Value = m.HorasTrabajadasCalculado == null ? 0 : m.HorasTrabajadasCalculado.Value;
                            worksheet.Cells[initialRow, 6].Value = "";
                            worksheet.Cells[initialRow, 7].Value = m.EstandarPorHorasCalculado == null ? 0 : m.EstandarPorHorasCalculado.Value;
                            worksheet.Cells[initialRow, 8].Value = m.EstandarCalculado == null ? 0 : m.EstandarCalculado.Value;
                            worksheet.Cells[initialRow, 9].Value = "";
                            worksheet.Cells[initialRow, 10].Value = m.NumeroPiezasRealizadas == null ? 0 : m.NumeroPiezasRealizadas.Value;
                        

                       initialRow++;    



                     
                        
                       
                        
                    }
                
                    

                }
                // var ReporteProduccion = await _context.ReporteProduccionMoldeos.FirstOrDefaultAsync(m => m.IdReporteProduccionMoldeo == IdReporteProduccion);
                // result = await ObtenerCausasTiempoMuertoReporteProduccion(IdOrdenTrabajo ,ReporteDate);
                /*  for (int x = 0; x <= result.Count; x++)
                  {
                      if (result[0].NombreEstadoOrden is not null)
                      {
                          ReporteProduccion.CausaTiempoMuerto1 = result[0].NombreMotivoCambioEstado;
                      }
                      else
                      {
                          ReporteProduccion.CausaTiempoMuerto1 = "NO HAY";
                      }
                      if (result[1].NombreEstadoOrden is not null)
                      {
                          ReporteProduccion.CausaTiempoMuerto2 = result[1].NombreMotivoCambioEstado;
                      }
                      else
                      {
                          ReporteProduccion.CausaTiempoMuerto2 = "NO HAY";


                      }

                  } */
                //_context.Update(ReporteProduccion);
                //await _context.SaveChangesAsync();

                //Crear cliente web para descargar para 

                excelPackage.SaveAs(new FileInfo(Path.Combine(@"P:\\BITACORAMF\\Registro de producción\\2022\\REGISTRO PRODUCCION SISTEMA\\", fileName)));

                var uri = new System.Uri(GetFilePath(fileName));
                var converted = uri.AbsoluteUri;


                return webHostEnvironment.WebRootPath;

            }

           
           
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
