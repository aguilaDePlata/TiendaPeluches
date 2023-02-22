using System;
using System.Collections.Generic;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string TipoCliente { get; set; }
        public string NroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public string Telefono { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
