using System;
using System.Collections.Generic;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class ParteAccesorio
    {
        public int IdParteAccesorio { get; set; }
        public int? IdAccesorioFk { get; set; }
        public int? IdParteFk { get; set; }

        public virtual Accesorio? IdAccesorioFkNavigation { get; set; }
        public virtual Parte? IdParteFkNavigation { get; set; }
    }
}
