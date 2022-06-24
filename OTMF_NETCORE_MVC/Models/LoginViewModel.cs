using System.ComponentModel.DataAnnotations;

namespace OTMF_NETCORE_MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }    
    }
}
