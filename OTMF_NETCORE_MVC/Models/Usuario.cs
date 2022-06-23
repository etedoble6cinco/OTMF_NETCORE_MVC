namespace OTMF_NETCORE_MVC.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }   
        public string EmailNormalizado { get; set; }    
        public string PasswordHash { get; set; }    
    }
}
