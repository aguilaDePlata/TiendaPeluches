using Peluches.Orders.Web.API.Application.Dtos;
using Peluches.Orders.Web.API.Base;

namespace Peluches.Orders.Web.API.Application.Services
{
    public interface IOrderService
    {
        Task<ServiceResult<List<OrdersListDto>>> GetOrdersList();
        Task<ServiceResult<OrdersListDto>> Get(int orderId);
        Task<ServiceResult<OrderUpdatedDto>> Update(int orderId, OrderUpdateDto order);
        Task<ServiceResult<OrderCreatedDto>> Create(OrderCreateDto order);
        Task<ServiceResult<OrderDeletedDto>> Delete(int orderId);
    }
}
