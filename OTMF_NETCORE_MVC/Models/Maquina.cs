using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Maquina
    {
        public Maquina()
        {
            BitacoraOrdenTrabajos = new HashSet<BitacoraOrdenTrabajo>();
            MaquinaOrdenTrabajos = new HashSet<MaquinaOrdenTrabajo>();
            ReporteProduccionMoldeos = new HashSet<ReporteProduccionMoldeo>();
            UsuarioMaquinas = new HashSet<UsuarioMaquina>();
        }

        public int IdMaquina { get; set; }
        public string? NombreMaquina { get; set; }
        public bool? EstadoMaquina { get; set; }

        public virtual ICollection<BitacoraOrdenTrabajo> BitacoraOrdenTrabajos { get; set; }
        public virtual ICollection<MaquinaOrdenTrabajo> MaquinaOrdenTrabajos { get; set; }
        public virtual ICollection<ReporteProduccionMoldeo> ReporteProduccionMoldeos { get; set; }
        public virtual ICollection<UsuarioMaquina> UsuarioMaquinas { get; set; }
    }
}
