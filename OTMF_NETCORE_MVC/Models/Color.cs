using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Color
    {
        public Color()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdColor { get; set; }
        public string? NombreColor { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
