using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class OrdenTrabajo
    {
        public OrdenTrabajo()
        {
            BitacoraOrdenTrabajos = new HashSet<BitacoraOrdenTrabajo>();
            DuracionEstados = new HashSet<DuracionEstado>();
            MaquinaOrdenTrabajos = new HashSet<MaquinaOrdenTrabajo>();
            OrdenTrabajoEmpleados = new HashSet<OrdenTrabajoEmpleado>();
        }

        public int? IdEmpleadoMoldeadorFk { get; set; }
        public int? IdEmpleadoEmpacadorFk { get; set; }
        public int IdOrdenTrabajo { get; set; }
        public int? IdMaquinaFk { get; set; }
        public DateTime? FechaOrdenTrabajo { get; set; }
        public int? IdParteFk { get; set; }
        public int? CantidadPiezasPororden { get; set; }
        public int? CajasRecibidas { get; set; }
        public int? PiezasRealizadas { get; set; }
        public int? IdInstructivoFk { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFinalizacion { get; set; }
        public int? IdEmpeadoSupervisorFk { get; set; }
        public int? IdEstadoOrdenFk { get; set; }
        public int? EtiquetaDeCaja { get; set; }
        public int? IdEstandarConRelevoFk { get; set; }
        public int? IdEstandarPorHoraFk { get; set; }
        public int? MaxScrap { get; set; }
        public string? IdCodigoOrdenTrabajo { get; set; }
        public bool? Otespecial { get; set; }
        public int? IdTurnoOtFk { get; set; }
        public decimal? ScrapCalculado { get; set; }
        public decimal? EstandarCalculado { get; set; }
        public decimal? EstandarConRelevoCalculado { get; set; }
        public decimal? EstandarPorHorasCalculado { get; set; }
        public decimal? HorasTrabajadasCalculado { get; set; }
        public decimal? PorcentajeScrapCalculado { get; set; }
        public decimal? FracEstandarConRelevo { get; set; }
        public int? NumeroCabidadesPieza { get; set; }
        public bool? CantidadPiezasOtflag { get; set; }

        public virtual EstadoOrden? IdEstadoOrdenFkNavigation { get; set; }
        public virtual Instructivo? IdInstructivoFkNavigation { get; set; }
        public virtual Parte? IdParteFkNavigation { get; set; }
        public virtual TurnoOt? IdTurnoOtFkNavigation { get; set; }
        public virtual ICollection<BitacoraOrdenTrabajo> BitacoraOrdenTrabajos { get; set; }
        public virtual ICollection<DuracionEstado> DuracionEstados { get; set; }
        public virtual ICollection<MaquinaOrdenTrabajo> MaquinaOrdenTrabajos { get; set; }
        public virtual ICollection<OrdenTrabajoEmpleado> OrdenTrabajoEmpleados { get; set; }
    }
}
