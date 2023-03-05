using System;
using System.Collections.Generic;

namespace Peluches.Web.API.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string? Marca1 { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
