using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Instructivo
    {
        public Instructivo()
        {
            OrdenTrabajos = new HashSet<OrdenTrabajo>();
        }

        public int IdInstructivo { get; set; }
        public string? NombreInstructivo { get; set; }

        public virtual ICollection<OrdenTrabajo> OrdenTrabajos { get; set; }
    }
}
