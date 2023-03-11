using System;
using System.Collections.Generic;

namespace Peluches.Orders.Web.API.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdEmpleado { get; set; }
        public string? Usuario1 { get; set; }
        public string? Password { get; set; }
    }
}
