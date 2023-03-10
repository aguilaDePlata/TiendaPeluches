using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Cargo
{

    [Display(Name = "Id Cargo")]
    public int IdCargo { get; set; }

    [Required(ErrorMessage = "El campo 'Cargo' es obligatorio.")]
    [Display(Name = "Cargo")]
    public string? Cargo1 { get; set; }

    [Required(ErrorMessage = "El campo 'Sueldo' es obligatorio.")]
    public decimal? Sueldo { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
