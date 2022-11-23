using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Hubs;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Services;
using System.Data;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class BitacoraOrdenTrabajoController : Controller
    {

        private readonly OTMFContext _context;
        private readonly string con;
        private readonly string connectionString;
        private readonly IServicioUsuarios _servicio;
        DashboardHub dashboardHub;
        public BitacoraOrdenTrabajoController(
            DashboardHub dashboardHub,
            OTMFContext context,
            IConfiguration configuration,
            IServicioUsuarios servicio)
        {
            _servicio = servicio;
            this.dashboardHub = dashboardHub;
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            con = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Index()
        {
            return View();
        }
        //INSERTAR EL PRIMER REGISTRO DE ESA BITACORA PARA ESA JORNADA DE TRABAJO Y/O TURNO 
        [HttpPost]
        public async Task<IActionResult> InsertRegistroBitacoraOrdenTrabajo(
            int IdOrdenTrabajoFK)
        {
            var procedure = "[InsertRegistroBitacoraOrdenTrabajo]";
            using (var connection = new SqlConnection(connectionString))
            {
                int IdUsuarioFK = _servicio.ObtenerUsuarioId();
                var confirm = await connection.QueryAsync(procedure, new
                {
                    IdOrdenTrabajoFK = IdOrdenTrabajoFK,
                    IdUsuarioFK = IdUsuarioFK
                }, commandType: CommandType.StoredProcedure);
                return Json(new { data = confirm });
            }

        }
        //ACTUALIZAR EL REGISTRO DE LA BITACORA METODO PRINCIPAL - PRIMERA ACTUALIZACION 
        [HttpPost]
        public async Task<IActionResult> UpdateRegistroBitacoraOrdenTrabajo(
        int IdOrdenTrabajo)
        {
            List<BitacoraOrdenTrabajo> bitacoraOrdenTrabajo = new List<BitacoraOrdenTrabajo>();
            bitacoraOrdenTrabajo.Add(await ObtenerCalculoRegistro(IdOrdenTrabajo));

            return Json(new { data = "Terminado" });
        }
        //OBTENER EL ULTIMO REGISTRO DE LA BITACORA  DE LA ORDEN DE TRABAJO 
        public async Task<int> ObtenerUltimoRegistroBitacoraByOtIdAndDate(int IdOrdenTrabajoFK)
        {
            var procedure = "[ObtenerUltimoRegistroBitacoraByOtIdAndDate]";

            using (var connection = new SqlConnection(connectionString))
            {
                var UltimoRegistroBitacoraOT = await connection.QueryFirstOrDefaultAsync<ObtenerUltimoRegistroBitacoraByOtIdAndDate>(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajoFK

                }, commandType: CommandType.StoredProcedure);
                ObtenerUltimoRegistroBitacoraByOtIdAndDate n = new ObtenerUltimoRegistroBitacoraByOtIdAndDate();

                n.IdBitacoraOrdenTrabajo = UltimoRegistroBitacoraOT.IdBitacoraOrdenTrabajo;
                return n.IdBitacoraOrdenTrabajo;
            }
        }
        //CALCULO DE ESTANDARES DE LA ORDEN DE TRABAJO TRAS PAUSAR ORDEN DE TRABAJO 
        public async Task<BitacoraOrdenTrabajo> ObtenerCalculoRegistro(int IdOrdenTrabajoFK)
        {
            TiempoEstadosOrdenTrabajo DuracionDetenida = new TiempoEstadosOrdenTrabajo();

            DuracionDetenida = await ObtenerTiemposMuertos(IdOrdenTrabajoFK);

            int IdBitacoraOrdenTrabajo = await ObtenerUltimoRegistroBitacoraByOtIdAndDate(IdOrdenTrabajoFK);
            var CurrentBitacoraOrdenTrabajo = await _context.BitacoraOrdenTrabajos.FirstOrDefaultAsync(m => m.IdBitacoraOrdenTrabajo == IdBitacoraOrdenTrabajo);

            var turnoOt = await _context.TurnoOts.FirstOrDefaultAsync(m => m.IdTurnoOt == CurrentBitacoraOrdenTrabajo.IdTurnoOtFk);
            var fraccionEstandarRelevo = await _context.FraccionEstandarRelevos.FirstOrDefaultAsync(z => z.IdFraccionEstandarRelevo == 1);
            var PorcentajeEstandarConRelevo = await _context.ScrapPermitidos.FirstOrDefaultAsync(x => x.IdScrapPermitido == 1);
            var OriginalOrdenTrabajo = await _context.OrdenTrabajos.FirstOrDefaultAsync(x => x.IdOrdenTrabajo == CurrentBitacoraOrdenTrabajo.IdOrdenTrabajoFk);
            var Parte = await _context.Partes.FirstOrDefaultAsync(z => z.IdParte == OriginalOrdenTrabajo.IdParteFk);
            var EstandarPorHoras = await _context.EstandarPorHoras.FirstOrDefaultAsync(x => x.IdEstandarPorHora == Parte.IdEstandarPorHoraFk);
            if (CurrentBitacoraOrdenTrabajo.HorasTrabajadasCalculado > 0  && CurrentBitacoraOrdenTrabajo.HorasTrabajadasCalculado is not null)
            {
                decimal NuevasHorasTrabajadas = await CalcularHorasTrabajadas((decimal)CurrentBitacoraOrdenTrabajo.HorasTrabajadasCalculado, DuracionDetenida.DuracionEstado);
                CurrentBitacoraOrdenTrabajo.ScrapCalculado = await CalcularScrap((decimal)EstandarPorHoras.NombreEstandarPorHora, NuevasHorasTrabajadas, (decimal)PorcentajeEstandarConRelevo.PorcentajeScrapPermitido);
                CurrentBitacoraOrdenTrabajo.EstandarCalculado = await CalcularEstandar((decimal)EstandarPorHoras.NombreEstandarPorHora,NuevasHorasTrabajadas);
                CurrentBitacoraOrdenTrabajo.EstandarConRelevoCalculado = await CalcularEstandarConRelevo((decimal)EstandarPorHoras.NombreEstandarPorHora, NuevasHorasTrabajadas, (decimal)fraccionEstandarRelevo.FracEstandarRelevo);
                CurrentBitacoraOrdenTrabajo.EstandarPorHorasCalculado = (decimal)EstandarPorHoras.NombreEstandarPorHora;
                CurrentBitacoraOrdenTrabajo.HorasTrabajadasCalculado = NuevasHorasTrabajadas;
                CurrentBitacoraOrdenTrabajo.PorcentajeScrapCalculado = (decimal)CurrentBitacoraOrdenTrabajo.PorcentajeScrapCalculado;
                CurrentBitacoraOrdenTrabajo.FracEstandarConRelevo = (decimal)CurrentBitacoraOrdenTrabajo.FracEstandarConRelevo;

            }
            else
            {
                CurrentBitacoraOrdenTrabajo.ScrapCalculado = await CalcularScrap((decimal)EstandarPorHoras.NombreEstandarPorHora, await CalcularHorasTrabajadas((decimal)turnoOt.HorasTrabajadas, DuracionDetenida.DuracionEstado), (decimal)PorcentajeEstandarConRelevo.PorcentajeScrapPermitido);
                CurrentBitacoraOrdenTrabajo.EstandarCalculado = await CalcularEstandar((decimal)EstandarPorHoras.NombreEstandarPorHora, await CalcularHorasTrabajadas((decimal)turnoOt.HorasTrabajadas, DuracionDetenida.DuracionEstado));
                CurrentBitacoraOrdenTrabajo.EstandarConRelevoCalculado = await CalcularEstandarConRelevo((decimal)EstandarPorHoras.NombreEstandarPorHora, await CalcularHorasTrabajadas((decimal)turnoOt.HorasTrabajadas, DuracionDetenida.DuracionEstado), (decimal)fraccionEstandarRelevo.FracEstandarRelevo);
                CurrentBitacoraOrdenTrabajo.EstandarPorHorasCalculado = (decimal)EstandarPorHoras.NombreEstandarPorHora;
                CurrentBitacoraOrdenTrabajo.HorasTrabajadasCalculado = await CalcularHorasTrabajadas((decimal)turnoOt.HorasTrabajadas, DuracionDetenida.DuracionEstado);
                CurrentBitacoraOrdenTrabajo.PorcentajeScrapCalculado = (decimal)PorcentajeEstandarConRelevo.PorcentajeScrapPermitido;
                CurrentBitacoraOrdenTrabajo.FracEstandarConRelevo = (decimal)fraccionEstandarRelevo.FracEstandarRelevo;

            }
                
            

            
            _context.Update(CurrentBitacoraOrdenTrabajo);
            _context.SaveChanges();
            return CurrentBitacoraOrdenTrabajo;
        }
        public async Task<decimal> CalcularHorasTrabajadas(decimal HorasTrabajadas, decimal TiemposMuertosDetenido)
        {
            decimal result = (HorasTrabajadas * 3600);
            decimal NuevasHorasTrabajadasSegundos = (result - TiemposMuertosDetenido);
            decimal NuevasHorasTrabajadasHoras = (NuevasHorasTrabajadasSegundos / 3600);
            return NuevasHorasTrabajadasHoras;
        }
        public async Task<decimal> CalcularScrap(decimal estandarPorHora, decimal horasTrabajadas, decimal porcentajeScrapPermitido)
        {
            decimal result = (estandarPorHora * horasTrabajadas);
            return result * porcentajeScrapPermitido;
        }
        public async Task<decimal> CalcularEstandar(decimal estandarPorHora, decimal horasTrabajadas)
        {
            return estandarPorHora * horasTrabajadas;
        }
        public async Task<decimal> CalcularEstandarConRelevo(decimal estandarPorHora, decimal horasTrabajadas, decimal fracEstandarConRelevo)
        {
            decimal result = horasTrabajadas + fracEstandarConRelevo;
            return result * estandarPorHora;
        }
        //OBTENER BITACORA ORDEN TRABAJO POR MEDIO DEL ID DE LA ORDEN DE TRABAJO 

        [HttpPost]
        public async Task<IActionResult> ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo(int IdOrdenTrabajo)
        { 
            int UltimoIdBitacoraOrdenTrabajo = await  ObtenerUltimoRegistroBitacoraByOtIdAndDate(IdOrdenTrabajo);
            //Obtener el ultimo id de la bitacora de orden de trabajo por medio de la id de la orden de trabajo
            var UltimaBitacoraOrdenTrabajo =  await _context.BitacoraOrdenTrabajos.FirstOrDefaultAsync(
                m => m.IdBitacoraOrdenTrabajo == UltimoIdBitacoraOrdenTrabajo);
            return Json(new { data = UltimaBitacoraOrdenTrabajo });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBitacoraOrdenTrabajoProdTerminada(int IdOrdenTrabajo)
        {
            //METODO PARA CALCULAR LA PRODUCCION TERMINADA SI LA ORDEN DE TRABAJO ES VALIDA PARA HACER UN CONTEO DE LA MISMA
            int IdUltimoRegistroBitacora = await ObtenerUltimoRegistroBitacoraByOtIdAndDate(IdOrdenTrabajo);
            var UltimoRegistroBitacora = await _context.BitacoraOrdenTrabajos.FirstOrDefaultAsync(m => m.IdBitacoraOrdenTrabajo == IdUltimoRegistroBitacora);
            var OrdenTrabajoOriginal = await _context.OrdenTrabajos.FirstOrDefaultAsync(m => m.IdOrdenTrabajo == UltimoRegistroBitacora.IdOrdenTrabajoFk);
            UltimoRegistroBitacora.CajasRecibidas = (await ObtenerCajasRecibidas(IdOrdenTrabajo)).NumeroCajasRecibidas;
            UltimoRegistroBitacora.PiezasRecibidas = (await ObtenerCajasRecibidas(IdOrdenTrabajo)).NumeroPiezasSueltasRecibidas;
            UltimoRegistroBitacora.NumeroCavidades = OrdenTrabajoOriginal.NumeroCabidadesPieza;
            if ( await ValidarRegistroProduccion(IdOrdenTrabajo) == true)
            // VALIDAR SI LA ORDEN DE TRABAJO YA TERMINO O EXCEDE  LA DEMANDA ESTABLECIDA EN LA ORDEN DE TRABAJO ORIGINAL
            {// VALIDAR SI LA ORDEN DE TRABAJO TIENE MAS COPIAS DENTRO DEL LA BASE DE DATOS 
             // PARA SUMAR TODOS SUS RESULTADOS , OBTENER LA PRODUCCION RESTANTE

                int ProduccionRestante = await CalcularProdTerminada((int)OrdenTrabajoOriginal.CantidadPiezasPororden, await ObtenerSumProduccionByIdBitacoraOrdenTrabajo(IdOrdenTrabajo, (int)OrdenTrabajoOriginal.PiezasRealizadas));
                UltimoRegistroBitacora.CantidadPiezasPorOrdenRealizadas = ProduccionRestante;

                // RESULTADO DE LAS ORDEN DE TRABAJO , INCLUYENDO LA ULTIMA 
            }
            else
            {
                UltimoRegistroBitacora.CantidadPiezasPorOrdenRealizadas = 0;
            }
           // SE REGISTRAN LA CANTIDAD DE PIEZAS POR ORDEN TOMANDO LAS DE LA ORDEN DE TRABAJO ORIGINAL 
            UltimoRegistroBitacora.CantidadPiezasPorOrden = OrdenTrabajoOriginal.CantidadPiezasPororden;
           // SE REGISTRAN EL NUMERO DE PIEZAS REALIZADAS
            UltimoRegistroBitacora.NumeroPiezasRealizadas = OrdenTrabajoOriginal.PiezasRealizadas;
            _context.Update(UltimoRegistroBitacora);
            await _context.SaveChangesAsync();




            return Json(new { data = "Terminada" });
        }
        //OBTENER CAJAS RECIBIDAS 
        public async Task<ObtenerCajasRecibidas> ObtenerCajasRecibidas(int IdOrdenTrabajo)
        {
            var procedure = "[ObtenerCajasRecibidas]";
            using(var  connection =  new SqlConnection(connectionString))
            {
                var data = await connection.QuerySingleOrDefaultAsync<ObtenerCajasRecibidas>(procedure, new
                {
                    idOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
               
                return data; 
            }
        }
        //OBTENER LA SUMA DE TODAS LAS PRODUCCIONES LIGADAS A UNA ORDEN DE TRABAJO ,AGREGANDO LA ULTIMA PRODUCCION AL FINAL 
        public async Task<int> ObtenerSumProduccionByIdBitacoraOrdenTrabajo(int IdOrdenTrabajo ,  int NumeroPiezasRealizadas)
        {
            var procedure = "[SumProduccionByIdBitacoraOrdenTrabajo]";
            using (var connection = new SqlConnection(connectionString))
            {
                var NumeroPiezasRealizadasTotal = await connection.QuerySingleOrDefaultAsync<int>(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo,
                    NumeroPiezasRealizadas = NumeroPiezasRealizadas

                }, commandType: CommandType.StoredProcedure);
            return NumeroPiezasRealizadasTotal;
            }
            
                
        }
        // VALIDAR EL REGISTRO DE PRODUCCION SI LA ORDEN DE TRABAJO ES ELEGIBLE PARA CALCULAR PRODUCCION
        public async Task<bool> ValidarRegistroProduccion(int IdOrdenTrabajo)
        {
            bool validar = true;
            var  OrdenTrabajo = await _context.OrdenTrabajos.FirstOrDefaultAsync(m => m.IdOrdenTrabajo == IdOrdenTrabajo);
            if(OrdenTrabajo.CantidadPiezasOtflag == true)
            {
                return true;
            }
            return false;
        }
        //CALCULO DE PRODUCCION TERMINADA PIEZAS FALTANTES -  PIEZAS TERMINADAS 
        public async Task<int> CalcularProdTerminada ( int PiezasTerminadas , int PiezasFaltantes )
        {
            int PiezasRestantesResult = PiezasTerminadas - PiezasFaltantes;
            if (PiezasFaltantes == 0 ) { return 0;} if( PiezasTerminadas == 0) { return 0; }
            return PiezasRestantesResult;
        }
        //CALCULO DE TIEMPOS MUERTOS - OBTENER TIEMPO DE PAUSA POR CUALQUIER RAZON DE LA PAUSA 
        public async Task<TiempoEstadosOrdenTrabajo> ObtenerTiemposMuertos(int IdOrdenTrabajo)
        {
        
            var procedure = "[ObtenerTiemposMuertos]";
            using (var connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<ObtenerTiempoMuertos>(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);
                
                List<ObtenerTiempoMuertos> result = new List<ObtenerTiempoMuertos>();
                result =  data.ToList();
                int DetenidaSegundos = 0;
                for (int x = 0; x < result.Count; x++)
                {
                    if (result[x].NombreEstadoOrden.Equals("PAUSADA"))
                    {
                        DetenidaSegundos = DetenidaSegundos + result[x].Duracion;
                    }
                }
                TiempoEstadosOrdenTrabajo DuracionDetenida = new TiempoEstadosOrdenTrabajo();
                DuracionDetenida.NombreEstado = "PAUSADA";
                DuracionDetenida.DuracionEstado = DetenidaSegundos;
                return DuracionDetenida;
            }





        }
        [HttpPost]
        public async Task<bool> ValidateIfExistsBitacoraOrdenTrabajo(int IdOrdenTrabajo)
        {
            return await _context.BitacoraOrdenTrabajos.AnyAsync(q => q.IdOrdenTrabajoFk == IdOrdenTrabajo);
          
        }
        public async Task<bool> ValidateSumBitacoraOrdenTrabajo(int IdOrdenTrabajo)
        {
            if ( _context.BitacoraOrdenTrabajos.Any(q => q.IdOrdenTrabajoFk == IdOrdenTrabajo)) return true;
            return false ;
        }
        [HttpPost]
        public async Task<IActionResult> ValidateSumBitacoraOrdenTrabajoProduccion(int IdOrdenTrabajo)
        {
            var BitacoraOrdenTrabajo = await _context.BitacoraOrdenTrabajos.FirstOrDefaultAsync(q => q.IdOrdenTrabajoFk == IdOrdenTrabajo);
            if ( await ObtenerSumBitacoraOrdenTrabajoProduccion(IdOrdenTrabajo) > 0 ) {
                return Json(new { data = true }); } 
         
            
            return Json(new {data = false});
        }
        [HttpPost]
       
        public async Task<int> ObtenerSumBitacoraOrdenTrabajoProduccion(int IdOrdenTrabajo)
        {
            var procedure = "[ObtenerSumBitacoraOrdenTrabajoProduccion]";
            using(var connection =  new SqlConnection(connectionString))
            {
                var data = await connection.QueryFirstOrDefaultAsync<int>(procedure, new
                {
                    IdOrdenTrabajo = IdOrdenTrabajo
                }, commandType: CommandType.StoredProcedure);

                return data;
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> EvaluarProduccionCompletada(int IdOrdenTrabajo)
        {
            var IdUltimaBitacoraOrdenTrabajo = await ObtenerUltimoRegistroBitacoraByOtIdAndDate(IdOrdenTrabajo);
            var OrdenTrabajoOriginal = await _context.OrdenTrabajos.FirstOrDefaultAsync(m => m.IdOrdenTrabajo == IdOrdenTrabajo);
            var UltimaBitacoraOrdenTrabajo = await _context.BitacoraOrdenTrabajos.FirstOrDefaultAsync(m => m.IdBitacoraOrdenTrabajo == IdUltimaBitacoraOrdenTrabajo);
            if (OrdenTrabajoOriginal.CantidadPiezasOtflag == true)
            {
                
                if (UltimaBitacoraOrdenTrabajo.CantidadPiezasPorOrdenRealizadas < 1)
                {
                    if(UltimaBitacoraOrdenTrabajo.CantidadPiezasPorOrdenRealizadas  <= 0 && await ObtenerSumBitacoraOrdenTrabajoProduccion(IdOrdenTrabajo) > 0)
                    {
                        return Json(new { data = true });
                    }
                    else
                    {
                        return Json(new { data = false });
                    }
                   
                    
                }
                else
                {
                    return Json(new { data = false });
                     
                }
             
            }
            else
            {
                return Json(new { data = "No necesita calculo de produccion" });
            }
            
          

            
            
        }
        //OBTENER TODAS LAS BITACORAS POR ID ORDEN DE TRABAJO
        [HttpGet]
        public async Task<IActionResult> ObtenerAllBitacorasOtByOtId(int? Id)
        {
            var procedure = "[ObtenerAllBitacorasOtByOtId]";
            using (var connection = new SqlConnection(connectionString))
            {
                var data  = await  connection.QueryAsync<BitacoraOrdenTrabajo>(procedure, new
                {
                    IdOrdenTrabajo = Id
                },commandType:CommandType.StoredProcedure);    
                
                return Json( new { data = data });  
            }

        }

    }
}
