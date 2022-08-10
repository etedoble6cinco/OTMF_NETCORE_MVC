using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class InstructivoPieza
    {
        public InstructivoPieza()
        {
            Partes = new HashSet<Parte>();
        }

        public int IdInstructivoPieza { get; set; }
        public string? NombreInstructivoPieza { get; set; }

        public virtual ICollection<Parte> Partes { get; set; }
    }
}
