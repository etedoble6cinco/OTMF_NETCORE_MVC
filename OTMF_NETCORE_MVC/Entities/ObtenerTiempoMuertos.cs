namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerTiempoMuertos
    {
        public int IdDuracionEstados { get; set; }   
        public string NombreEstadoOrden { get; set; }   
        public string NombreMotivoCambioEstado { get; set; }
        public string NombreMotivoCambioEstadoDerivado { get; set; }
        public DateTime InicioEstado { get; set; }  
        public DateTime FinalEstado { get; set; }
        public int IdEstadOrdenFK { get; set; }
        public int Duracion { get; set; }   

    }
}
