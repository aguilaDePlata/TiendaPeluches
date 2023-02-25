using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Comprobante
    {
        public int IdComprobante { get; set; }

        [Display(Name = "ID Pedido")]
        public int? IdPedido { get; set; }

        [Required(ErrorMessage = "El campo 'Tipo de Comprobante' es obligatorio.")]
        [Display(Name = "Tipo de Comprobante")]
        public string TipoComprobante { get; set; }

        [Display(Name = "Fecha de Emisión")]
        public DateTime? FechaEmision { get; set; }

        [Display(Name = "Subtotal de la Compra")]
        public decimal? SubtotalComp { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Igv { get; set; }

        [Display(Name = "Valor Total")]
        public decimal? ValorTotal { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
