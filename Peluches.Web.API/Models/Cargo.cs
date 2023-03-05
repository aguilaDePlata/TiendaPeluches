using System;
using System.Collections.Generic;

namespace Peluches.Web.API.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string? Cargo1 { get; set; }

    public decimal? Sueldo { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
