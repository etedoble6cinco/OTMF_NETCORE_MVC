using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class EstandarPorHora
    {
        public EstandarPorHora()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdEstandarPorHora { get; set; }
        public int? NombreEstandarPorHora { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
