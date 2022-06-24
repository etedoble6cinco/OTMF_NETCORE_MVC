namespace OTMF_NETCORE_MVC.Models
{
    public class Usuario
    {
        public int IdUsuarios { get; set; }
        public string Email { get; set; }   
        public string EmailNormalizado { get; set; }    
        public string PasswordHash { get; set; }    
    }
}
