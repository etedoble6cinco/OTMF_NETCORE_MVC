using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class EstadoOrden
    {
        public EstadoOrden()
        {
            BitacoraOrdenTrabajos = new HashSet<BitacoraOrdenTrabajo>();
            DuracionEstados = new HashSet<DuracionEstado>();
            OrdenTrabajos = new HashSet<OrdenTrabajo>();
        }

        public int IdEstadoOrden { get; set; }
        public string? NombreEstadoOrden { get; set; }
        public int? EstadoOrdenNumero { get; set; }

        public virtual ICollection<BitacoraOrdenTrabajo> BitacoraOrdenTrabajos { get; set; }
        public virtual ICollection<DuracionEstado> DuracionEstados { get; set; }
        public virtual ICollection<OrdenTrabajo> OrdenTrabajos { get; set; }
    }
}
