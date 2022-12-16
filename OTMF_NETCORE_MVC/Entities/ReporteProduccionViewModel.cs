namespace OTMF_NETCORE_MVC.Entities
{

    public class ReporteProduccionViewModel
    {

    
     public int   IdMaquinaFK { get; set; } 
     public string?  NombreMaquina { get; set; }        


        public List<BitacoraOrdenTrabajoReporte> BitacorasList { get; set; } 
        

    }



    public class BitacoraOrdenTrabajoReporte
    {
        public int IdOrdenTrabajoFK { get; set; }
        public int IdEmpleadoMoldeoFK { get; set; }
        public string ClaveEmpleado { get; set; }
        public int NumeroCavidades { get; set; }
        public decimal EstandarPorHorasCalculado { get; set; }
        public decimal EstandarCalculado { get; set; }
        public int NumeroPiezasRealizadas { get; set; }
        public int IdMaquinaFK { get; set; }
        public int IdBitacoraOrdenTrabajo { get; set; }
        public string NombreMaquina { get; set; }
        public int HorasTrabajadasAcumulado { get; set; }   
        public string  CodigoParte { get; set; }
        public DateTime FechaReporteProduccion { get; set; }
        public decimal Eficiencia { get; set; }
        public string IdCodigoOrdenTrabajo { get; set; }    
        public decimal HorasTrabajadasCalculado { get; set; }
        public int Activa { get; set; }
        public int Pausa { get; set; }
        public int Liberar { get; set; }
        public List<DuracionEstadosReporte>DuracionPausada { get; set; }
        public List<DuracionEstadosReporte> DuracionActiva { get; set; }
        public List<DuracionEstadosReporte> DuracionPorLiberar { get; set; }
    }
    public class DuracionEstadosReporte
    {
        public int IdBitacoraOrdenTrabajo { get; set; } 
	public int IdDuracionEstados { get; set; }   
	public string  NombreEstadoOrden { get; set; }     
 public DateTime InicioEstado { get; set; } 
  public DateTime FinalEstado { get; set; } 
  public int IdMotivoCambioEstadoFK { get; set; }  
  public int IdEstadoOrdenFK { get; set; }  
	public string Duracion { get; set; }     
	public string  NombreMotivoCambioEstado { get; set; }   
	
  public int  IdMotivoCambioEstadoDerivadoFK { get; set; }   
 public string NombreMotivoCambioEstadoDerivado { get; set; }   

    }
}
