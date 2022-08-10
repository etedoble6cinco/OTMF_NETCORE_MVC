using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class RolesUsuario
    {
        public int IdRolesUsuarios { get; set; }
        public int? IdUsuariosFk { get; set; }
        public int? IdRolUsuarioFk { get; set; }

        public virtual Role? IdRolUsuarioFkNavigation { get; set; }
        public virtual Usuario? IdUsuariosFkNavigation { get; set; }
    }
}
