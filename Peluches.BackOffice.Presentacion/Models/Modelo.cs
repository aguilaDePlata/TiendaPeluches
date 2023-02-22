using System;
using System.Collections.Generic;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Modelo
    {
        public Modelo()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdModelo { get; set; }
        public string Modelo1 { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
