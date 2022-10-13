using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class UsuarioMaquina
    {
        public int IdUsuarioMaquina { get; set; }
        public string? NombreUsuarioMaquina { get; set; }
        public int? IdUsuarioFk { get; set; }
        public int? IdMaquinaFk { get; set; }

        public virtual Maquina? IdMaquinaFkNavigation { get; set; }
        public virtual Usuario? IdUsuarioFkNavigation { get; set; }
    }
}
