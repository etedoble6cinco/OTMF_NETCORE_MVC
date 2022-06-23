using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class EstadoOrden
    {
        public EstadoOrden()
        {
            OrdenTrabajos = new HashSet<OrdenTrabajo>();
        }

        public int IdEstadoOrden { get; set; }
        public string? NombreEstadoOrden { get; set; }
        public int? EstadoOrdenNumero { get; set; }

        public virtual ICollection<OrdenTrabajo> OrdenTrabajos { get; set; }
    }
}
