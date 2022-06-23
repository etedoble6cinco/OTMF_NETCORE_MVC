using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Ensamble
    {
        public Ensamble()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdEnsamble { get; set; }
        public string? NombreEnsamble { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
