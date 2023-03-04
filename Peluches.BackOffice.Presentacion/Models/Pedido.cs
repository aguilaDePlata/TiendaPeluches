using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            Comprobantes = new HashSet<Comprobante>();
            DetallePedidos = new HashSet<DetallePedido>();
        }


        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El campo 'ID Cliente' es obligatorio.")]
        [Display(Name = "ID Cliente")]
        public int? IdCliente { get; set; }
        [Required(ErrorMessage = "El campo 'ID Empleado' es obligatorio.")]
        [Display(Name = "ID Empleado")]
        public int? IdEmpleado { get; set; }

        [Display(Name = "Fecha de Pedido")]
        public DateTime? FechaPedido { get; set; }

        [Display(Name = "Fecha máxima de entrega")]
        public DateTime? FechaMaxEntrega { get; set; }

        [Display(Name = "Valor Total")]
        public decimal? ValorTotal { get; set; }

        [Display(Name = "Activo")]
        public bool? Activo { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual ICollection<Comprobante> Comprobantes { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
