using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Etiquetum
    {
        public Etiquetum()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdEtiqueta { get; set; }
        public string? NombreEtiqueta { get; set; }
        public string? PathNombreEtiqueta { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
