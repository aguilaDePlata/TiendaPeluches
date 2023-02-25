using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El campo 'Nombres' es obligatorio.")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo 'Apellidos' es obligatorio.")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo 'DNI' es obligatorio.")]
        [Display(Name = "DNI")]
        public int? Dni { get; set; }

        [Required(ErrorMessage = "El campo 'Dirección' es obligatorio.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo 'Distrito' es obligatorio.")]
        [Display(Name = "Distrito")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "El campo 'Edad' es obligatorio.")]
        [Display(Name = "Edad")]
        public int? Edad { get; set; }

        [Required(ErrorMessage = "El campo 'Teléfono' es obligatorio.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }


        [Display(Name = "Cargo")]
        public int? IdCargo { get; set; }

        [Display(Name = "Fecha de Contrato")]
        public DateTime? FechaContrato { get; set; }

        [Display(Name = "Activo")]
        public bool? Activo { get; set; }

        public virtual Cargo IdCargoNavigation { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
