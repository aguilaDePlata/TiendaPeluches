using System;
using System.Collections.Generic;

namespace Peluches.Web.API.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEmpleado { get; set; }

    public DateTime? FechaPedido { get; set; }

    public DateTime? FechaMaxEntrega { get; set; }

    public decimal? ValorTotal { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Comprobante> Comprobantes { get; } = new List<Comprobante>();

    public virtual ICollection<DetallePedido> DetallePedidos { get; } = new List<DetallePedido>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }
}
