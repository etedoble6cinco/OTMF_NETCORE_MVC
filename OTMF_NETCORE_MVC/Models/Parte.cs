using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Parte
    {
        public Parte()
        {
            OrdenTrabajos = new HashSet<OrdenTrabajo>();
            ParteAccesorios = new HashSet<ParteAccesorio>();
        }

        public int IdParte { get; set; }
        public string? IdCodigoParte { get; set; }
        public int? IdTarimaFk { get; set; }
        public decimal? Aluminio { get; set; }
        public int? IdEnsambleFk { get; set; }
        public int? IdParteAccesorioFk { get; set; }
        public int? IdEstandarFk { get; set; }
        public int? IdEtiquetaFk { get; set; }
        public int? PiezasPorCaja { get; set; }
        public int? IdColorFk { get; set; }
        public int? CajasPorTarima { get; set; }
        public int? StdPintura { get; set; }
        public decimal? Costo { get; set; }
        public int? EstandarPorHora { get; set; }
        public int? IdCajaFk { get; set; }
        public int? IdHuleFk { get; set; }
        public int? IdPinturaFk { get; set; }
        public int? IdInsertoFk { get; set; }
        public int? IdMoldeFk { get; set; }
        public int? IdClienteFk { get; set; }
        public int? Scrap { get; set; }
        public int? IdEstandarConRelevoFk { get; set; }

        public virtual Caja? IdCajaFkNavigation { get; set; }
        public virtual Cliente? IdClienteFkNavigation { get; set; }
        public virtual Color? IdColorFkNavigation { get; set; }
        public virtual Ensamble? IdEnsambleFkNavigation { get; set; }
        public virtual EstandarConRelevo? IdEstandarConRelevoFkNavigation { get; set; }
        public virtual Estandar? IdEstandarFkNavigation { get; set; }
        public virtual Etiquetum? IdEtiquetaFkNavigation { get; set; }
        public virtual Hule? IdHuleFkNavigation { get; set; }
        public virtual Inserto? IdInsertoFkNavigation { get; set; }
        public virtual Molde? IdMoldeFkNavigation { get; set; }
        public virtual Pintura? IdPinturaFkNavigation { get; set; }
        public virtual Tarima? IdTarimaFkNavigation { get; set; }
        public virtual ICollection<OrdenTrabajo> OrdenTrabajos { get; set; }
        public virtual ICollection<ParteAccesorio> ParteAccesorios { get; set; }
    }
}
