using System;
using System.Collections.Generic;

namespace Peluches.Orders.Web.API.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            Comprobantes = new HashSet<Comprobante>();
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public int IdPedido { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEmpleado { get; set; }
        public DateTime? FechaPedido { get; set; }
        public DateTime? FechaMaxEntrega { get; set; }
        public decimal? ValorTotal { get; set; }
        public bool Activo { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual ICollection<Comprobante> Comprobantes { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
