using System;
using System.Collections.Generic;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdMarca { get; set; }
        public string Marca1 { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
