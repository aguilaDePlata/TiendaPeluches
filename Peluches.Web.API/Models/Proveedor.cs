using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    [Required(ErrorMessage = "El campo 'Nombres' es obligatorio.")]
    [Display(Name = "Nombres")]
    public string? NombresProv { get; set; }

    [Required(ErrorMessage = "El campo 'Apellidos' es obligatorio.")]
    [Display(Name = "Apellidos")]
    public string? ApellidosProv { get; set; }

    [Required(ErrorMessage = "El campo 'Dirección' es obligatorio.")]
    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El campo 'Distrito' es obligatorio.")]
    public string? Distrito { get; set; }

    [Required(ErrorMessage = "El campo 'Teléfono' es obligatorio.")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
