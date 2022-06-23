using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Molde
    {
        public Molde()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdMolde { get; set; }
        public string? NombreMolde { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
