using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Pintura
    {
        public Pintura()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdPintura { get; set; }
        public string? NombrePintura { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
