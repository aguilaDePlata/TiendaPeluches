using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Web.API.Models;

namespace Peluches.Web.API.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly C3BdPedidosContext _context;

        public OrdersController(C3BdPedidosContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetOrders()
        {
            return await _context.Pedidos.Include(d => d.DetallePedidos)
                .ToListAsync();
        }
    }
}
