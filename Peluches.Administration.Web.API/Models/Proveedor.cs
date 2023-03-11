using System;
using System.Collections.Generic;

namespace Peluches.Administration.Web.API.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProveedor { get; set; }
        public string? NombresProv { get; set; }
        public string? ApellidosProv { get; set; }
        public string? Direccion { get; set; }
        public string? Distrito { get; set; }
        public string? Telefono { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
