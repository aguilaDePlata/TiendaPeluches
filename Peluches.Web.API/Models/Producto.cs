using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Producto
{
    public int IdProducto { get; set; }


    [Required(ErrorMessage = "El campo 'Nombre del producto' es obligatorio.")]
    [Display(Name = "Nombre del producto")]
    public string? NombreProducto { get; set; }

    [Required(ErrorMessage = "El campo 'Descripción' es obligatorio.")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El campo 'Precio de venta' es obligatorio.")]
    [Display(Name = "Precio de venta")]
    public decimal? PrecioVenta { get; set; }

    [Required(ErrorMessage = "El campo 'Id Marca' es obligatorio.")]
    [Display(Name = "Id Marca")]
    public int? IdMarca { get; set; }

    [Required(ErrorMessage = "El campo 'Id Modelo' es obligatorio.")]
    [Display(Name = "Id Modelo")]
    public int? IdModelo { get; set; }

    [Required(ErrorMessage = "El campo 'Id Proveedor' es obligatorio.")]
    [Display(Name = "Id Proveedor")]
    public int? IdProveedor { get; set; }

    [Required(ErrorMessage = "El campo 'Stock' es obligatorio.")]
    public int? Stock { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; } = new List<DetallePedido>();

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual Modelo? IdModeloNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}
