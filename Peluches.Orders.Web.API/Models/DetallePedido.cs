using System;
using System.Collections.Generic;

namespace Peluches.Orders.Web.API.Models
{
    public partial class DetallePedido
    {
        public int IdDetalle { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioVenta { get; set; }
        public decimal? SubtotalProd { get; set; }

        public virtual Pedido? IdPedidoNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
