namespace OTMF_NETCORE_MVC.Entities
{
    public class CausasTiempoMuertoReporteProduccion
    {
    
        public string IdEstadoOrdenFK { get; set; }
        
        public string NombreEstadoOrden { get; set; }   
        public int IdMotivoCambioEstadoFK { get; set; }   
        public string  NombreMotivoCambioEstado { get; set; }   
    }
}
