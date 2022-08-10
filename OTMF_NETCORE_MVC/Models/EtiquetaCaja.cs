using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class EtiquetaCaja
    {
        public EtiquetaCaja()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdEtiquetaDeCaja { get; set; }
        public string? NombreEtiquetaDeCaja { get; set; }
        public string? PathEtiquetaDeCaja { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
