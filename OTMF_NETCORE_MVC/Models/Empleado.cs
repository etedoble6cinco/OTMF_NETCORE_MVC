using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            OrdenTrabajoEmpleados = new HashSet<OrdenTrabajoEmpleado>();
        }

        public int IdEmpleado { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ClaveEmpleado { get; set; }
        public bool? Estado { get; set; }
        public int? IdTipoEmpleadoFk { get; set; }
        public int? IdTurnoFk { get; set; }

        public virtual TipoEmpleado? IdTipoEmpleadoFkNavigation { get; set; }
        public virtual Turno? IdTurnoFkNavigation { get; set; }
        public virtual ICollection<OrdenTrabajoEmpleado> OrdenTrabajoEmpleados { get; set; }
    }
}
