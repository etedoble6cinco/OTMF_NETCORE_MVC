using Dapper;
using Microsoft.AspNetCore.Mvc;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Services
{
    public interface IRepositorioOrdenTrabajo
    {
        Task<List<ObtenerOrdenesTrabajo>> ObtenerOrdenesTrabajo();
    }
    public class RepositorioOrdenTrabajo : IRepositorioOrdenTrabajo 

    {
        private readonly string connectionString;


        public RepositorioOrdenTrabajo (string con)
        {
            connectionString = con;
        }
        public async Task<List<ObtenerOrdenesTrabajo>> ObtenerOrdenesTrabajo()
        {
            var procedure = "[ObtenerOrdenesTrabajoAllInfoDashBoard]";
            using ( var  connection  =  new SqlConnection(connectionString))
            {
                var ot = await connection.QueryAsync<ObtenerOrdenesTrabajo>(procedure, commandType: CommandType.StoredProcedure);
                return ot.ToList();
            }
           
        }
        public async Task<List<ObtenerOrdenesTrabajo>> ObtenerOrdenTrabajoNoFilter()
        {
            var procedure = "[ObtenerOrdenesTrabajoAllInfoDashBoardNoFilter]";
            using(var connection =  new SqlConnection(connectionString))
            {
                var ot = await connection.QueryAsync<ObtenerOrdenesTrabajo>(procedure, commandType: CommandType.StoredProcedure);
                return ot.ToList();
            }    
        }
       
        
    }
}
