using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class BitacoraOrdenTrabajoDuracionEstado
    {
        public int IdBitacoraOrdenTrabajoDuracionEstado { get; set; }
        public int? IdDuracionEstadoFk { get; set; }
        public int? IdBitacoraOrdenTrabajoFk { get; set; }

        public virtual BitacoraOrdenTrabajo? IdBitacoraOrdenTrabajoFkNavigation { get; set; }
        public virtual DuracionEstado? IdDuracionEstadoFkNavigation { get; set; }
    }
}
