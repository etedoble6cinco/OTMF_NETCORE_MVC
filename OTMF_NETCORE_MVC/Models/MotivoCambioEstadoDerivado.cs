using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class MotivoCambioEstadoDerivado
    {
        public MotivoCambioEstadoDerivado()
        {
            MotivoCambioEstados = new HashSet<MotivoCambioEstado>();
        }

        public int IdMotivoCambioEstadoDerivado { get; set; }
        public string? NombreMotivoCambioEstadoDerivado { get; set; }

        public virtual ICollection<MotivoCambioEstado> MotivoCambioEstados { get; set; }
    }
}
