using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peluches.Administration.Web.API.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El nombre del Empleado no puede ser nulo.")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "El apellido del Empleado no puede ser nulo.")]
        public string? Apellidos { get; set; }
        public int? Dni { get; set; }
        public string? Direccion { get; set; }
        public string? Distrito { get; set; }
        public int? Edad { get; set; }
        public string? Telefono { get; set; }
        public int? IdCargo { get; set; }
        public DateTime? FechaContrato { get; set; }
        public bool Activo { get; set; }

        public virtual Cargo? IdCargoNavigation { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
