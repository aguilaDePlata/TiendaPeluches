namespace Peluches.Orders.Web.API.Application.Dtos
{
    public class OrderDetailsUpdateDto
    {
        public int? OrderDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? SalePrice { get; set; }
    }
}
