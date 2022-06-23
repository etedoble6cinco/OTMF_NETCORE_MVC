using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Maquina
    {
        public Maquina()
        {
            MaquinaOrdenTrabajos = new HashSet<MaquinaOrdenTrabajo>();
        }

        public int IdMaquina { get; set; }
        public string? NombreMaquina { get; set; }
        public bool? EstadoMaquina { get; set; }

        public virtual ICollection<MaquinaOrdenTrabajo> MaquinaOrdenTrabajos { get; set; }
    }
}
