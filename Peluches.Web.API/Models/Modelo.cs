using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Modelo
{

    [Required(ErrorMessage = "El campo 'Id Modelo' es obligatorio.")]
    [Display(Name = "Id Modelo")]
    public int IdModelo { get; set; }

    [Required(ErrorMessage = "El campo 'Modelo' es obligatorio.")]
    [Display(Name = "Modelo")]
    public string? Modelo1 { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
