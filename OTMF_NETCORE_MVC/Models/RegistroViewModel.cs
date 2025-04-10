﻿using System.ComponentModel.DataAnnotations;

namespace OTMF_NETCORE_MVC.Models
{
    public class RegistroViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
      
        public  string Password { get; set; }   
    }
}
