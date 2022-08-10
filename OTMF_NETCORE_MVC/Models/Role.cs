using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Role
    {
        public Role()
        {
            RolesUsuarios = new HashSet<RolesUsuario>();
        }

        public int IdRolUsuario { get; set; }
        public string? NombreRolUsuario { get; set; }

        public virtual ICollection<RolesUsuario> RolesUsuarios { get; set; }
    }
}
