using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            RolesUsuarios = new HashSet<RolesUsuario>();
        }

        public int IdUsuarios { get; set; }
        public string? Email { get; set; }
        public string? EmailNormalizado { get; set; }
        public string? PasswordHash { get; set; }

        public virtual ICollection<RolesUsuario> RolesUsuarios { get; set; }
    }
}
