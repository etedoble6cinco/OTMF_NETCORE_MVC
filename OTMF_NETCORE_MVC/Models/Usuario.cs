﻿using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            BitacoraOrdenTrabajos = new HashSet<BitacoraOrdenTrabajo>();
            RolesUsuarios = new HashSet<RolesUsuario>();
            UsuarioMaquinas = new HashSet<UsuarioMaquina>();
        }

        public int IdUsuarios { get; set; }
        public string? Email { get; set; }
        public string? EmailNormalizado { get; set; }
        public string? PasswordHash { get; set; }

        public virtual ICollection<BitacoraOrdenTrabajo> BitacoraOrdenTrabajos { get; set; }
        public virtual ICollection<RolesUsuario> RolesUsuarios { get; set; }
        public virtual ICollection<UsuarioMaquina> UsuarioMaquinas { get; set; }
    }
}
