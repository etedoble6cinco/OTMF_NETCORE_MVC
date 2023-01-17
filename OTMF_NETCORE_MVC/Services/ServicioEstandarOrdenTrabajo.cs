using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Services
{
    public interface IServicioEstandarOrdenTrabajo
    {
        Task<ObtenerEstandarByCavidadNumber> ObtenerEstandarByCavidadNumber(int IdParteFK);
        
    }
    public class ServicioEstandarOrdenTrabajo : IServicioEstandarOrdenTrabajo   
    {
        private readonly string connectionString;
        private readonly OTMFContext _context;

        public ServicioEstandarOrdenTrabajo(
             OTMFContext context,
            IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }
        public async Task<ObtenerCalculoByCavidadNumber> ObtenerCalculoEstandarByCavidadNumber()
        {
            ObtenerCalculoByCavidadNumber obtenerCalculoByCavidadNumber = new ObtenerCalculoByCavidadNumber();
            
            //METODO PARA REALIZAR CALCULO DE ESTANDAR 
            return null;
        }
        public async Task<ObtenerEstandarByCavidadNumber> ObtenerEstandarByCavidadNumber(int IdParteFK)
        {


            ///aqui regla de 3 inversa para obtener el estandar que es numero de cavidades 
            ///variables =  NumeroCavidadesMolde - split nombre del molde para obtener cavidades  
            ///variables =  Al iniciar la bitacora el numero de cavidades debe ser
            ///actualizado al que tiene en el molde para que al momento de ser actualizado sea recalculado
            ///y coincida el numero de cabidades con el estandar por hora , esto debe ser actualizado en el metodo  
            /// de la bitacora de orden de trabajo en el backend para que ala hora de mostrar ya solo se muestren las cifras 
            /// en los casos en donde el molde marca solo un 1  es por que solo se ocupa una cavidad
            /// para tener ese estandar por hora , pero en los casos donde se especifique mas de una cavidad se debe recalcular
            /// aunque el molde marque un numero diferente de cavidades debe cambiar el estandar por hora segun 
            /// sumado deacuerdo a cuando era uno multiplicando ese numero por el numero actual de cavidades 
            
            var ObjParte = await _context.Partes.FirstOrDefaultAsync(m => m.IdParte == IdParteFK);
            ObtenerEstandarByCavidadNumber ObjObtenerEstandarByCavidadNumber = new ObtenerEstandarByCavidadNumber();
            if (ObjParte == null)
            {
            
            
            }   else
            {
                var ObjMolde = await _context.Moldes.FirstOrDefaultAsync(m => m.IdMolde == ObjParte.IdMoldeFk);
                string NombreMolde = ObjMolde.NombreMolde;
                //if(ObjMolde.NombreMolde != null || ObjMolde.NombreMolde != "" )
                //{

                string[] strNombreMolde = ObjMolde.NombreMolde.Split('-');
                int NumeroCavidades = int.Parse(strNombreMolde[0]);
                if(NumeroCavidades > 1)
                {


                }
                else
                {
                    var ObjEstandarPorHora = await _context.EstandarPorHoras.FirstOrDefaultAsync(m => m.IdEstandarPorHora == ObjParte.IdEstandarPorHoraFk);
                    ObjObtenerEstandarByCavidadNumber.EstandarPorHora = (int)ObjEstandarPorHora.NombreEstandarPorHora;
                    ObjObtenerEstandarByCavidadNumber.NumeroCavidadesByMoldeName = NumeroCavidades;
                    return ObjObtenerEstandarByCavidadNumber;
                    
                }  

                //}

            }
            return ObjObtenerEstandarByCavidadNumber;
        }
       


    }
}
