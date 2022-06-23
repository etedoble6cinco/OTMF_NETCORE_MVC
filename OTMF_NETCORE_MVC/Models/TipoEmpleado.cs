using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class TipoEmpleado
    {
        public TipoEmpleado()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdTipoEmpleado { get; set; }
        public string? NombreTipoEmpleado { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
