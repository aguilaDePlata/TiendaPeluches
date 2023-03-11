using System;
using System.Collections.Generic;

namespace Peluches.Administration.Web.API.Models
{
    public partial class Comprobante
    {
        public int IdComprobante { get; set; }
        public int? IdPedido { get; set; }
        public string? TipoComprobante { get; set; }
        public DateTime? FechaEmision { get; set; }
        public decimal? SubtotalComp { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Igv { get; set; }
        public decimal? ValorTotal { get; set; }

        public virtual Pedido? IdPedidoNavigation { get; set; }
    }
}
