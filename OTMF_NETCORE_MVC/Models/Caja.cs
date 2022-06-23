using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Caja
    {
        public Caja()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdCaja { get; set; }
        public string? NombreCaja { get; set; }
        public bool? LogoCaja { get; set; }
        public string? EtiquetaDeCaja { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
