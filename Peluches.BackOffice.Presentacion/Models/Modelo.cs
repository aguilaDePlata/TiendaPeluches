using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Modelo
    {
        public Modelo()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdModelo { get; set; }

        [Required(ErrorMessage = "El campo 'Modelo' es obligatorio.")]
        [Display(Name = "Modelo")]
        public string Modelo1 { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
