using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo 'Apellidos' es obligatorio.")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo 'Tipo de Cliente' es obligatorio.")]
        [Display(Name = "Tipo de Cliente")]
        public string TipoCliente { get; set; }

        [Required(ErrorMessage = "El campo 'Nro de Documento' es obligatorio.")]
        [Display(Name = "Nro de Documento")]
        public string NroDocumento { get; set; }

        [Required(ErrorMessage = "El campo 'Dirección' es obligatorio.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo 'Distrito' es obligatorio.")]
        [Display(Name = "Distrito")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "El campo 'Teléfono' es obligatorio.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Teléfono")]
        public bool? Activo { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
