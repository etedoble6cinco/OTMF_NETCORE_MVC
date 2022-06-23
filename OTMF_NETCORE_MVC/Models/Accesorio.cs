using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Accesorio
    {
        public Accesorio()
        {
            ParteAccesorios = new HashSet<ParteAccesorio>();
        }

        public int IdAccesorio { get; set; }
        public string? NombreAccesorio { get; set; }

        public virtual ICollection<ParteAccesorio> ParteAccesorios { get; set; }
    }
}
