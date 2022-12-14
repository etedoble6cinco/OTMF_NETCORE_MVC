namespace OTMF_NETCORE_MVC.Entities
{

    public class ReporteProduccionViewModel
    {
    
        public string NombreMaquina { get; set; } = "";
        
        public List<BitacoraOrdenTrabajoReporte>? BitacorasList { get; set; } 
        

    }



    public class BitacoraOrdenTrabajoReporte
    {
        public string ClaveEmpleado { get; set; } = "";
        public int  CantidadPiezasPorOrden { get; set; }    
        public string CodigoParte { get; set; } = "";
        public int NumeroCavidades { get; set; } = 0;
        public decimal HorasTrabajadasCalculado { get; set; } = 0;
        public decimal EstandarPorHoraCalculado { get; set; } = 0;
        public decimal EstandarConRelevoCalculado { get; set; }
        public decimal EstandarCalculado { get; set; } = 0;
        public decimal ScrapCalculado { get; set; } = 0;    
        public decimal TiempoAcumulado { get; set; } = 0;
        public int PiezasRecibidas { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string CreateBy { get; set; } = "Desconocido";
        public DateTime FechaProduccion { get; set; }
        public decimal Eficiencia { get; set; }
        public int ProduccionBitacora { get; set; }
        public int Turno { get; set; }

        public List<DuracionEstadosReporte>? DuracionEstadosList { get; set; }
    }
    public class DuracionEstadosReporte
    {
        
        public string? MotivoCambioEstado { get; set; }
        public DateTime InicioEstado { get; set; }  
        public DateTime FinalEstado { get; set; }   
        public DateTime DuracionEstado { get; set;}
        public string? NombreEstadoOrden { get ; set; }
        
    }
}
