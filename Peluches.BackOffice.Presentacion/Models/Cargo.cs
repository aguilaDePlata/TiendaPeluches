using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Cargo
    {
        public Cargo()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdCargo { get; set; }

        [Required(ErrorMessage = "El campo 'Cargo' es obligatorio.")]
        [Display(Name = "Cargo")]
        public string Cargo1 { get; set; }

        [Required(ErrorMessage = "El campo 'Sueldo' es obligatorio.")]
        [Display(Name = "Sueldo")]
        public decimal? Sueldo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
