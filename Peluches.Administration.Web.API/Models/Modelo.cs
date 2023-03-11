using System;
using System.Collections.Generic;

namespace Peluches.Administration.Web.API.Models
{
    public partial class Modelo
    {
        public Modelo()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdModelo { get; set; }
        public string? Modelo1 { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
