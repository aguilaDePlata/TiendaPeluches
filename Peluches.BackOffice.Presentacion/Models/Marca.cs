using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El campo 'Marca' es obligatorio.")]
        [Display(Name = "Marca")]
        public string Marca1 { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
