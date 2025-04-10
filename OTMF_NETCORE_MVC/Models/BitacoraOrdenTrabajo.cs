﻿using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class BitacoraOrdenTrabajo
    {
        public BitacoraOrdenTrabajo()
        {
            DuracionEstados = new HashSet<DuracionEstado>();
        }

        public int IdBitacoraOrdenTrabajo { get; set; }
        public int? CantidadPiezasPorOrden { get; set; }
        public int? IdEstadoOrdenFk { get; set; }
        public int? NumeroTurnosTerminados { get; set; }
        public int? IdTurnoOtFk { get; set; }
        public decimal? ScrapCalculado { get; set; }
        public decimal? EstandarCalculado { get; set; }
        public decimal? EstandarConRelevoCalculado { get; set; }
        public decimal? EstandarPorHorasCalculado { get; set; }
        public decimal? HorasTrabajadasCalculado { get; set; }
        public decimal? PorcentajeScrapCalculado { get; set; }
        public decimal? FracEstandarConRelevo { get; set; }
        public int? IdOrdenTrabajoFk { get; set; }
        public int? PiezasRecibidas { get; set; }
        public int? CajasRecibidas { get; set; }
        public int? NumeroPiezasRealizadas { get; set; }
        public int? IdUsuarioFk { get; set; }
        public DateTime? FechaOrdenTrabajo { get; set; }
        public int? CantidadPiezasPorOrdenRealizadas { get; set; }
        public int? NumeroCavidades { get; set; }
        public int? IdEmpleadoMoldeoFk { get; set; }
        public int? IdMaquinaFk { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? HorasTrabajadasAcumulado { get; set; }

        public virtual Empleado? IdEmpleadoMoldeoFkNavigation { get; set; }
        public virtual EstadoOrden? IdEstadoOrdenFkNavigation { get; set; }
        public virtual Maquina? IdMaquinaFkNavigation { get; set; }
        public virtual OrdenTrabajo? IdOrdenTrabajoFkNavigation { get; set; }
        public virtual TurnoOt? IdTurnoOtFkNavigation { get; set; }
        public virtual Usuario? IdUsuarioFkNavigation { get; set; }
        public virtual ICollection<DuracionEstado> DuracionEstados { get; set; }
    }
}
