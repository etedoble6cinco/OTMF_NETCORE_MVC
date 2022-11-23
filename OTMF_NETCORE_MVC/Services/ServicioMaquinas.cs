using Dapper;
using Microsoft.AspNetCore.Mvc;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Services
{
    public interface IServicioMaquinas
    {
        Task<int> ObtenerSumIdMaquinaBitacoraOrdenTrabajo(int IdMaquinaFK, int mes, int dia, int anio, int op);
        Task<List<PieChartData>> ObtenerPieChartDataService(int IdMaquinaFK, int mes, int dia, int anio, int op);
    }
    public class ServicioMaquinas : IServicioMaquinas
    {

        private readonly string connectionString;
        private readonly OTMFContext _context;

        public ServicioMaquinas( 
            OTMFContext context,
            IConfiguration configuration 
            )
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context; 
        }
        public async Task<int> ObtenerSumIdMaquinaBitacoraOrdenTrabajo(
            int IdMaquinaFK ,
            int anio , 
            int dia  , 
            int mes , 
            int op)
        {
            var procedure = "[ObtenerSumIdMaquinaBitacoraOrdenTrabajoByIdMaquina]";
            using (var connection =  new SqlConnection(connectionString))
            {
               
                int SumMaquina = await connection.QueryFirstOrDefaultAsync<int>(procedure, new
                {
                    IdMaquinaFK = IdMaquinaFK,
                    mes = mes,
                    dia = dia,
                    anio = anio,
                    op = op
                }, commandType: CommandType.StoredProcedure);
                return SumMaquina;

            }



        }
        public async Task<List<PieChartData>> ObtenerPieChartDataService (int IdMaquinaFK, int mes, int dia, int anio, int op)
        {
            var procedure = "[ObtenerSumIdMaquinaBitacoraOrdenTrabajoByIdMaquina]";
            using (var connection = new SqlConnection(connectionString))
            {

                var Bitacora = await connection.QueryAsync<PieChartData>(procedure, new
                {
                    IdMaquinaFK = IdMaquinaFK,
                    mes = mes,
                    dia = dia,
                    anio = anio,
                    op = op
                }, commandType: CommandType.StoredProcedure);
                return Bitacora.ToList();

            }
        }
        
            
        
    }
}
