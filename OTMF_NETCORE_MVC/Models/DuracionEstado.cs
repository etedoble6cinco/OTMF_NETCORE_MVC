using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class DuracionEstado
    {
        public DuracionEstado()
        {
            BitacoraOrdenTrabajoDuracionEstados = new HashSet<BitacoraOrdenTrabajoDuracionEstado>();
        }

        public int IdDuracionEstados { get; set; }
        public int? IdMotivoCambioEstadoFk { get; set; }
        public DateTime? InicioEstado { get; set; }
        public DateTime? FinalEstado { get; set; }
        public int? IdEstadoOrdenFk { get; set; }
        public int? IdOrdenTrabajoFk { get; set; }
        public bool? TipoMovimientoEstado { get; set; }

        public virtual EstadoOrden? IdEstadoOrdenFkNavigation { get; set; }
        public virtual MotivoCambioEstado? IdMotivoCambioEstadoFkNavigation { get; set; }
        public virtual OrdenTrabajo? IdOrdenTrabajoFkNavigation { get; set; }
        public virtual ICollection<BitacoraOrdenTrabajoDuracionEstado> BitacoraOrdenTrabajoDuracionEstados { get; set; }
    }
}
