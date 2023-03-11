using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peluches.Administration.Web.API.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del Producto no puede ser nulo.")]
        public string? NombreProducto { get; set; }

        [Required(ErrorMessage = "La descripción del Empleado no puede ser nulo.")]
        public string? Descripcion { get; set; }
        public decimal? PrecioVenta { get; set; }
        public int? IdMarca { get; set; }
        public int? IdModelo { get; set; }
        public int? IdProveedor { get; set; }
        public int? Stock { get; set; }
        public bool Activo { get; set; }
        public string? Url { get; set; }

        public virtual Marca? IdMarcaNavigation { get; set; }
        public virtual Modelo? IdModeloNavigation { get; set; }
        public virtual Proveedor? IdProveedorNavigation { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
