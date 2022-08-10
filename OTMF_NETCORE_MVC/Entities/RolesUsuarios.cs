namespace OTMF_NETCORE_MVC.Entities
{
    public class RolesUsuarios
    {
        public int IdUsuarios { get; set; }   
        public string Email { get; set; }  
        public int IdRolUsuarioFK { get; set; }
        public string NombreRolUsuario { get; set; }    
    }
}
