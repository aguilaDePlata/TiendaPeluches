namespace Peluches.Orders.Web.API.Application.Dtos
{
    public class OrderCreateDto
    {
        public OrderCreateDto()
        {
            OrderDetails = new List<OrderDetailsCreateDto>();
        }

        public int? ClientId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? DateOrder { get; set; }
        public DateTime? MaxDateDelivery { get; set; }


        public List<OrderDetailsCreateDto>? OrderDetails { get; set; }
    }
}
