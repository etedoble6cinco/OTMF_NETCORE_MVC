using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Hule
    {
        public Hule()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdHule { get; set; }
        public string? NombreHule { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
