using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class TurnoOt
    {
        public TurnoOt()
        {
            BitacoraOrdenTrabajos = new HashSet<BitacoraOrdenTrabajo>();
            OrdenTrabajos = new HashSet<OrdenTrabajo>();
        }

        public int IdTurnoOt { get; set; }
        public string? NombreTurno { get; set; }
        public decimal? HorasTrabajadas { get; set; }

        public virtual ICollection<BitacoraOrdenTrabajo> BitacoraOrdenTrabajos { get; set; }
        public virtual ICollection<OrdenTrabajo> OrdenTrabajos { get; set; }
    }
}
