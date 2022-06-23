using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Estandar
    {
        public Estandar()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdEstandar { get; set; }
        public string? NombreEstandar { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
