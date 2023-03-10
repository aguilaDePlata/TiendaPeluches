using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Peluches.Web.API.Models;

public partial class Cliente
{
    [Display(Name = "Id Cliente")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El campo 'Apellidos' es obligatorio.")]
    public string? Apellidos { get; set; }


    [Required(ErrorMessage = "El campo 'Tipo de Cliente' es obligatorio.")]
    [Display(Name = "Tipo de Cliente")]
    public string TipoCliente { get; set; } = null!;


    [Required(ErrorMessage = "El campo 'Nro de Documento' es obligatorio.")]
    [Display(Name = "Nro de Documento")]
    public string NroDocumento { get; set; } = null!;

    [Required(ErrorMessage = "El campo 'Dirección' es obligatorio.")]
    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }


    [Required(ErrorMessage = "El campo 'Distrito' es obligatorio.")]
    public string? Distrito { get; set; }


    [Required(ErrorMessage = "El campo 'Teléfono' es obligatorio.")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El campo 'Activo' es obligatorio.")]
    public bool? Activo { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; } = new List<Pedido>();
}
