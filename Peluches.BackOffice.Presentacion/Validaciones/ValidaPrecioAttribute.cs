namespace Peluches.BackOffice.Presentacion.Validaciones
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ValidaPrecioAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            decimal precioVenta;
            if (value == null)
                return new ValidationResult("El campo precio de venta es obligatorio.");

            precioVenta = (decimal)value;
            if (precioVenta <= 0)
                return new ValidationResult("El precio de venta debe se mayor a cero.");

            return ValidationResult.Success;
        }
    }
}
