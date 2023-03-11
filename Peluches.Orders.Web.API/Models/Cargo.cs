using System;
using System.Collections.Generic;

namespace Peluches.Orders.Web.API.Models
{
    public partial class Cargo
    {
        public Cargo()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdCargo { get; set; }
        public string? Cargo1 { get; set; }
        public decimal? Sueldo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
