using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class OrdenTrabajoEmpleado
    {
        public int IdEmpleadoOrdenTrabajo { get; set; }
        public int? IdEmpleadoFk { get; set; }
        public int? IdOrdenTrabajoFk { get; set; }

        public virtual Empleado? IdEmpleadoFkNavigation { get; set; }
        public virtual OrdenTrabajo? IdOrdenTrabajoFkNavigation { get; set; }
    }
}
