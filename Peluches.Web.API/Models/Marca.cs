using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Marca
{
    [Required(ErrorMessage = "El campo 'Id Marca' es obligatorio.")]
    [Display(Name = "Id Marca")]
    public int IdMarca { get; set; }

    [Required(ErrorMessage = "El campo 'Marca' es obligatorio.")]
    [Display(Name = "Marca")]
    public string? Marca1 { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
