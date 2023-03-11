using FluentValidation;
using Peluches.Orders.Web.API.Application.Dtos;

namespace Peluches.Orders.Web.API.Validations
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.ClientId).NotNull().WithMessage("Id del Cliente no puede ser nulo.");
            RuleFor(x => x.ClientId).GreaterThan(0).WithMessage("Id del Cliente debe ser mayor a cero.");

            RuleFor(x => x.EmployeeId).NotNull().WithMessage("Id del Empleado no puede ser nulo.");
            RuleFor(x => x.EmployeeId).GreaterThan(0).WithMessage("Id del Empleado debe ser mayor a cero.");

            RuleFor(x => x.DateOrder).NotNull().WithMessage("La Fecha de Creación del pedido no puede ser nulo.");
            RuleFor(x => x.MaxDateDelivery).NotNull().WithMessage("La Fecha de Entrega del pedido no puede ser nulo.");

            RuleFor(x => x.OrderDetails).NotNull().WithMessage("No se puede crear pedido sin detalles.");
        }
    }

    public class OrderDetailCreateDtoValidator : AbstractValidator<OrderDetailsCreateDto>
    {
        public OrderDetailCreateDtoValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage("Id del Producto no puede ser nulo.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Id del Producto debe ser mayor a cero.");

            RuleFor(x => x.Quantity).NotNull().WithMessage("La Cantidad del producto pedido no puede ser nulo.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La Cantidad del producto Pedido debe ser mayor a cero.");

            RuleFor(x => x.SalePrice).NotNull().WithMessage("El precio del producto pedido no puede ser nulo");
            RuleFor(x => x.SalePrice).GreaterThan(0).WithMessage("El precio del producto pedido debe ser mayor a cero.");
        }
    }
}
