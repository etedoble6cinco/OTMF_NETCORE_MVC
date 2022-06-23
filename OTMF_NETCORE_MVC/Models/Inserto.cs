using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Inserto
    {
        public Inserto()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdInserto { get; set; }
        public string? NombreInserto { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
