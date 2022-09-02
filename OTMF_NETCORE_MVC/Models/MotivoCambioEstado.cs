using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class MotivoCambioEstado
    {
        public MotivoCambioEstado()
        {
            DuracionEstados = new HashSet<DuracionEstado>();
        }

        public int IdMotivoCambioEstado { get; set; }
        public string? NombreMotivoCambioEstado { get; set; }
        public int? IdMotivoCambioEstadoDerivadoFk { get; set; }

        public virtual MotivoCambioEstadoDerivado? IdMotivoCambioEstadoDerivadoFkNavigation { get; set; }
        public virtual ICollection<DuracionEstado> DuracionEstados { get; set; }
    }
}
