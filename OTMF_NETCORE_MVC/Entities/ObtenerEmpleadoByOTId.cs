namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerEmpleadoByOTId
    {
       public int IdEmpleado { get; set; }  
        public string NombreEmpleado { get; set; }    
        public int IdTipoEmpleadoFK { get; set; }   
        public int idTurnoFK { get; set; }  
        public int IdEmpleadoOrdenTrabajo { get; set; } 
    }
}
