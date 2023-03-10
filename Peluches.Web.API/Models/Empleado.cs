using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    [Required(ErrorMessage = "El campo 'Nombres' es obligatorio.")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "El campo 'Apellidos' es obligatorio.")]
    public string? Apellidos { get; set; }

    [Required(ErrorMessage = "El campo 'DNI' es obligatorio.")]
    [Display(Name = "DNI")]
    public int? Dni { get; set; }

    [Required(ErrorMessage = "El campo 'Dirección' es obligatorio.")]
    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El campo 'Distrito' es obligatorio.")]
    public string? Distrito { get; set; }

    [Required(ErrorMessage = "El campo 'Edad' es obligatorio.")]
    public int? Edad { get; set; }

    [Required(ErrorMessage = "El campo 'Teléfono' es obligatorio.")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El campo 'Id Cargo' es obligatorio.")]
    [Display(Name = "Id Cargo")]
    public int? IdCargo { get; set; }

    [Required(ErrorMessage = "El campo 'Fecha de contrato' es obligatorio.")]
    [Display(Name = "Fecha de contrato")]
    public DateTime? FechaContrato { get; set; }

    public bool? Activo { get; set; }

    public virtual Cargo? IdCargoNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; } = new List<Pedido>();
}
