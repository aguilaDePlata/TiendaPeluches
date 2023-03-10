using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class DetallePedido
{
    public int IdDetalle { get; set; }

    public int? IdPedido { get; set; }

    public int? IdProducto { get; set; }

    [Required(ErrorMessage = "El campo 'Cantidad' es obligatorio.")]
    public int? Cantidad { get; set; }

    public decimal? PrecioVenta { get; set; }

    public decimal? SubtotalProd { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
