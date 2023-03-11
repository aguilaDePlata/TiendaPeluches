namespace Peluches.Orders.Web.API.Application.Dtos
{
    public class OrdersListDto
    {
        public OrdersListDto()
        {
            OrderDetailsList = new List<OrderDetailsListDto>();
        }

        public int OrderId { get; set; }
        public string IdentityDocumentClient { get; set; } = string.Empty;
        public string Client { get; set; } = string.Empty;
        public DateTime DateOrder { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }

        public List<OrderDetailsListDto> OrderDetailsList { get; set; }
    }

    public class OrderDetailsListDto
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
