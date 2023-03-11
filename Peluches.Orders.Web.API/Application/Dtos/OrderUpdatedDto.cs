namespace Peluches.Orders.Web.API.Application.Dtos
{
    public class OrderUpdatedDto
    {
        public int OrderId { get; set; }
        public DateTime MaxDateDelivery { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
