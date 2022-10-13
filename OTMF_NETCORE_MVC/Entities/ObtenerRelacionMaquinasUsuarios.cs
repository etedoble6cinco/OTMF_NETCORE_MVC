namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerRelacionMaquinasUsuarios
    {
        public int IdMaquinaFK { get; set; }
        public int IdUsuarioFK { get; set; }    
        public int IdUsuarioMaquina { get; set; }   
        public string NombreMaquinaUsuario { get; set; }    
        public string Email { get; set; }   
        public string NombreMaquina { get; set; }
    }
}
