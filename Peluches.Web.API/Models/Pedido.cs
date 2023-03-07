using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    [Required(ErrorMessage = "El campo 'Id Cliente' es obligatorio.")]
    [Display(Name = "Id Cliente")]
    public int? IdCliente { get; set; }

    [Required(ErrorMessage = "El campo 'Id Empleado' es obligatorio.")]
    [Display(Name = "Id Empleado")]
    public int? IdEmpleado { get; set; }

    [Required(ErrorMessage = "El campo 'Fecha de pedido' es obligatorio.")]
    [Display(Name = "Fecha de pedido")]
    public DateTime? FechaPedido { get; set; }

    [Required(ErrorMessage = "El campo 'Fecha máxima de entrega' es obligatorio.")]
    [Display(Name = "Fecha máxima de entrega")]
    public DateTime? FechaMaxEntrega { get; set; }

    [Display(Name = "Valor Total")]
    public decimal? ValorTotal { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Comprobante> Comprobantes { get; } = new List<Comprobante>();

    public virtual ICollection<DetallePedido> DetallePedidos { get; } = new List<DetallePedido>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }
}
