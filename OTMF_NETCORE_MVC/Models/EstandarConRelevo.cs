using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class EstandarConRelevo
    {
        public EstandarConRelevo()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdEstandarConRelevo { get; set; }
        public string? NombreEstandarconRelevo { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
