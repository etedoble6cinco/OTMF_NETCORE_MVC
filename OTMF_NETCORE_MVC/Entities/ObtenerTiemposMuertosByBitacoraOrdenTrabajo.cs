namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerTiemposMuertosByBitacoraOrdenTrabajo
    {
        public int IdDuracionEstados { get; set; }
        public string NombreEstadoOrden { get; set; }
        public DateTime InicioEstado { get; set; }
        public DateTime FinalEstado { get; set; }
        public int IdMotivoCambioEstadoFK { get; set; }
        public int Duracion { get; set; }
        public string NombreMotivoCambioEstado { get; set; }

        public int IdMotivoCambioEstadoDerivadoFK { get; set; } 
        public string NombreMotivoCambioEstadoDerivado { get; set; }   
    }
}
