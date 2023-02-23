using System;
using System.Collections.Generic;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal? PrecioVenta { get; set; }
        public int? IdMarca { get; set; }
        public int? IdModelo { get; set; }
        public int? IdProveedor { get; set; }
        public int? Stock { get; set; }
        public bool? Activo { get; set; }

        public virtual Marca Marca { get; set; }
        public virtual Modelo Modelo { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
