namespace OTMF_NETCORE_MVC.Entities
{
    public class GeneralViewDetails
    {
        public int IdMaquina { get; set; }  
        public string NombreMaquina { get; set; } 
        public int IdOrdenTrabajo { get; set; } 
        public string NumeroOrdenTrabajo { get; set; }  
        public string NumeroParte { get; set; } 
        public int PiezasRealizadasTotal { get; set; }  
        public int PiezasFaltantesTotal { get; set; }   
        public int IdEstadoOrdenFK { get; set; }
        
    }
}
