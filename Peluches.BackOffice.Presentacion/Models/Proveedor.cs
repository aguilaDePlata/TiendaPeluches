using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El campo 'Nombre de proveedor' es obligatorio.")]
        [Display(Name = "Nombre de Proveedor")]
        public string NombresProv { get; set; }
        [Required(ErrorMessage = "El campo 'Apellido' es obligatorio.")]
        [Display(Name = "Apellido")]
        public string ApellidosProv { get; set; }
        [Required(ErrorMessage = "El campo 'Dirección' es obligatorio.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo 'Distrito' es obligatorio.")]
        [Display(Name = "Nombre de Producto")]
        public string Distrito { get; set; }
        [Required(ErrorMessage = "El campo 'Teléfono' es obligatorio.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        public bool? Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
