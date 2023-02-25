using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "El campo nombre de producto es obligatorio.")]
        [Display(Name = "Nombre de Producto")]
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [Display(Name = "Especificaciones Técnicas del Producto")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo precio de venta es obligatorio.")]
        [Display (Name = "Precio de Venta")]
        public decimal? PrecioVenta { get; set; }

        [Required(ErrorMessage = "Seleccione una marca.")]
        [Display(Name = "Marca")]
        public int? IdMarca { get; set; }

        [Required(ErrorMessage = "Seleccione un modelo.")]
        [Display(Name = "Modelo")]
        public int? IdModelo { get; set; }

        [Required(ErrorMessage = "Seleccione una proveedor.")]
        [Display(Name = "Proveedor")]
        public int? IdProveedor { get; set; }

        [Required(ErrorMessage = "Ingrese un stock mayor a cero.")]
        public int? Stock { get; set; }

        public bool? Activo { get; set; }

        public virtual Marca Marca { get; set; }
        public virtual Modelo Modelo { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
