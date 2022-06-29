namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerEmpleados
    {

      public  int IdEmpleado { get; set; }  
        public string NombreEmpleado { get; set; }  
        public string ClaveEmpleado { get; set; }
        public bool Estado { get; set; }    
        
        public int IdTipoEmpleado { get; set; } 
        public string NombreTipoEmpleado { get; set; }  
        public int IdTurno { get; set; }    
        public string NombreTurno { get; set; } 

    }
}
