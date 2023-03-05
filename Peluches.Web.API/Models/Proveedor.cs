using System;
using System.Collections.Generic;

namespace Peluches.Web.API.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string? NombresProv { get; set; }

    public string? ApellidosProv { get; set; }

    public string? Direccion { get; set; }

    public string? Distrito { get; set; }

    public string? Telefono { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
