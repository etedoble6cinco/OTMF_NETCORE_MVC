using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class MaquinaOrdenTrabajo
    {
        public int IdMaquinaOrdeTrabajo { get; set; }
        public int? IdMaquinaFk { get; set; }
        public int? IdOrdenTrabajoFk { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual Maquina? IdMaquinaFkNavigation { get; set; }
        public virtual OrdenTrabajo? IdOrdenTrabajoFkNavigation { get; set; }
    }
}
