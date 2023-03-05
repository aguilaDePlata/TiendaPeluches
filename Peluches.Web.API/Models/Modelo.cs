using System;
using System.Collections.Generic;

namespace Peluches.Web.API.Models;

public partial class Modelo
{
    public int IdModelo { get; set; }

    public string? Modelo1 { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
