using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class ReporteProduccionMoldeo
    {
        public int IdReporteProduccionMoldeo { get; set; }
        public int? IdParteFk { get; set; }
        public DateTime? FechaReporteProduccion { get; set; }
        public int? IdMaquinaFk { get; set; }
        public int? IdMoldeFk { get; set; }
        public int? IdEmpleadoFk { get; set; }
        public decimal? HorasTrabajoCalculado { get; set; }
        public int? IdOrdenTrabajoFk { get; set; }
        public int? NumeroCavidades { get; set; }
        public int? EstandarPorHora { get; set; }
        public decimal? Produccion { get; set; }
        public decimal? Eficiencia { get; set; }
        public decimal? EstandarPropuesto { get; set; }
        public string? CausaTiempoMuerto1 { get; set; }
        public string? CausaTiempoMuerto2 { get; set; }
        public int? TurnoOtFk { get; set; }

        public virtual Empleado? IdEmpleadoFkNavigation { get; set; }
        public virtual Maquina? IdMaquinaFkNavigation { get; set; }
        public virtual Molde? IdMoldeFkNavigation { get; set; }
        public virtual OrdenTrabajo? IdOrdenTrabajoFkNavigation { get; set; }
        public virtual Parte? IdParteFkNavigation { get; set; }
    }
}
