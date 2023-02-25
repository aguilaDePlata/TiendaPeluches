using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo 'ID usuario' es obligatorio.")]
        [Display(Name = "ID Empleado")]
        public int? IdEmpleado { get; set; }

        [Required(ErrorMessage = "El campo 'Nombre de usuario' es obligatorio.")]
        [Display(Name = "Nombre de usuario")]
        public string Usuario1 { get; set; }

        [Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
