namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerOrdenesTrabajo
    {

        public int      IdOrdenTrabajo { get; set; }
        public string FechaOrdenTrabajo { get; set; }   
        public int      CantidadPiezasPorOrden { get; set; } = 0;
        public int      CajasRecibidas { get; set; } = 0;
        public int      PiezasRealizadas { get; set; } = 0;  
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinalizacion { get; set; }
       
        public string   EtiquetaDeCaja { get; set;}
        public int      MaxScrap { get; set; }  
  
        public int IdEstadoOrden { get; set; }
        public string NombreEstadoOrden { get; set; }
        public int IdInstructivo { get; set; }
        public string NombreInstructivo { get; set; }
        public int IdParteFK { get; set; }  
        public string IdCodigoOrdenTrabajo { get; set; }
        
        public int IdParte { get; set; }    
        public string IdCodigoParte { get; set; }   
        public int? IdMaquinaFK { get; set; }
        public string? NombreMaquina { get; set; }
        public int NumeroCabidadesPieza { get; set; }

    }
}
