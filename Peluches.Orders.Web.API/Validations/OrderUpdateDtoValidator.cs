using FluentValidation;
using Peluches.Orders.Web.API.Application.Dtos;

namespace Peluches.Orders.Web.API.Validations
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.OrderId).NotNull().WithMessage("Id del Pedido no puede ser nulo.");
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Id del Pedido debe ser mayor a cero.");

            RuleFor(x => x.MaxDateDelivery).NotNull().WithMessage("La Fecha de Entrega del pedido no puede ser nulo.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("Activo no puede ser nulo");

            RuleFor(d => d.OrderDetails).NotNull().WithMessage("No se puede actualizar pedido sin detalles.");
            RuleForEach(d => d.OrderDetails).NotNull().WithMessage("Item del Pedido no puede ser nulo.");
            RuleForEach(d => d.OrderDetails).SetValidator(new OrderDetailUpdateDtoValidator());
        }
    }

    public class OrderDetailUpdateDtoValidator : AbstractValidator<OrderDetailsUpdateDto>
    {
        public OrderDetailUpdateDtoValidator()
        {
            RuleFor(x => x.OrderDetailId).NotNull().WithMessage("Id del Detalle de Pedido no puede ser nulo.");
            RuleFor(x => x.OrderDetailId).GreaterThan(0).WithMessage("Id del Detalle de Pedido debe ser mayor a cero.");

            RuleFor(x => x.ProductId).NotNull().WithMessage("Id del Producto no puede ser nulo.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Id del Producto debe ser mayor a cero.");

            RuleFor(x => x.Quantity).NotNull().WithMessage("La Cantidad del producto pedido no puede ser nulo.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La Cantidad del producto Pedido debe ser mayor a cero.");

            RuleFor(x => x.SalePrice).NotNull().WithMessage("El precio del producto pedido no puede ser nulo");
            RuleFor(x => x.SalePrice).GreaterThan(0).WithMessage("El precio del producto pedido debe ser mayor a cero.");
        }
    }
}
