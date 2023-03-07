using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Comprobante
{
    [Display(Name = "Id Comprobante")]
    public int IdComprobante { get; set; }

    [Display(Name = "Id Pedido")]
    public int? IdPedido { get; set; }

    [Required(ErrorMessage = "El campo 'Tipo de Comprobante' es obligatorio.")]
    [Display(Name = "Tipo de Comprobante")]
    public string? TipoComprobante { get; set; }

    [Required(ErrorMessage = "El campo 'Fecha de Emisión' es obligatorio.")]
    [Display(Name = "Fecha de Emisión")]
    public DateTime? FechaEmision { get; set; }

    [Required(ErrorMessage = "El campo 'SubTotal de la Compra' es obligatorio.")]
    [Display(Name = "SubTotal de la Compra")]
    public decimal? SubtotalComp { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? Igv { get; set; }

    public decimal? ValorTotal { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
