namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerReporteProduccionByDate
    {
        public int? IdBitacoraOrdenTrabajo { get; set; }    
        public int? IdOrdenTrabajoFK { get; set; }
        public int? HorasTrabajadasCalculado { get; set; }  
        public decimal? EstandarPorHorasCalculado { get; set; } 
        public decimal? EstandarCalculado { get; set; }  
        public int? NumeroPiezasRealizadas { get; set; }   
        public int? NumeroCavidades { get; set; }
        public int? IdEmpleadoMoldeoFK { get; set; }
        public int? IdMaquinaFK { get; set; }   
        public string? NombreMaquina { get; set; }   
        public int? TiempoAcumulado { get; set; }    
    }
}
