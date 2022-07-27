using Dapper;
using OTMF_NETCORE_MVC.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Services
{
    public interface IRepositorioOrdenTrabajo
    {
        List<ObtenerOrdenesTrabajo> ObtenerOrdenesTrabajo();
    }
    public class RepositorioOrdenTrabajo : IRepositorioOrdenTrabajo 

    {
        private readonly string connectionString;


        public RepositorioOrdenTrabajo (string con)
        {
            connectionString = con;
        }
        public  List<ObtenerOrdenesTrabajo> ObtenerOrdenesTrabajo()
        {
            var procedure = "[ObtenerOrdenesTrabajoAllInfoDashBoard]";
            using ( var  connection  =  new SqlConnection(connectionString))
            {
                var ot = connection.Query<ObtenerOrdenesTrabajo>(procedure, commandType: CommandType.StoredProcedure);
                return ot.ToList();
            }
           
        }
    }
}
