using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdCliente { get; set; }
        public string? NombreCliente { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
