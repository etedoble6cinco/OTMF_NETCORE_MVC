using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Turno
    {
        public Turno()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdTurno { get; set; }
        public string? NombreTurno { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime? HoraEntrada { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
