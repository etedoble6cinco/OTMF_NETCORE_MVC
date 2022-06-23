using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Tarima
    {
        public Tarima()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdTarima { get; set; }
        public string? NombreTarima { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
