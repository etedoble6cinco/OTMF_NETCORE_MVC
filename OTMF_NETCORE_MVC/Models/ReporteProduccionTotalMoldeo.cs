using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class ReporteProduccionTotalMoldeo
    {
        public int IdProduccionTotal { get; set; }
        public decimal? TotalProduccion { get; set; }
        public decimal? TotalEficiencia { get; set; }
        public int? IdReporteProduccionMoldeoFk { get; set; }
        public DateTime? FechaReporteProduccionTotalMoldeo { get; set; }
    }
}
