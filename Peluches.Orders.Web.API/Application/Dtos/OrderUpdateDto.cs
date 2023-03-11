namespace Peluches.Orders.Web.API.Application.Dtos
{
    public class OrderUpdateDto
    {
        public OrderUpdateDto()
        {
            OrderDetails = new List<OrderDetailsUpdateDto>();
        }

        public int? OrderId { get; set; }

        public DateTime? MaxDateDelivery { get; set; }

        public bool? IsActive { get; set; }

        public List<OrderDetailsUpdateDto>? OrderDetails { get; set; }
    }
}
