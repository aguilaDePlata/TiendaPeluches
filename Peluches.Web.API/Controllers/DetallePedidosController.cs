using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Web.API.Models;

namespace Peluches.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidosController : ControllerBase
    {
        private readonly C3BdPedidosContext _context;

        public DetallePedidosController(C3BdPedidosContext context)
        {
            _context = context;
        }

        // GET: api/Detalle Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetallePedidos()
        {
            return await _context.DetallePedidos.ToListAsync();
        }

        // GET: api/Detalle Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetallePedido(int id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);

            if (detallePedido == null)
            {
                return NotFound();
            }

            return detallePedido;
        }
        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePedido(int id, DetallePedido detallePedido)
        {
            if (id != detallePedido.IdDetalle)
            {
                return BadRequest();
            }

            _context.Entry(detallePedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Detalle Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallePedido>> PostDetallePedido(DetallePedido detallePedido)
        {
            _context.DetallePedidos.Add(detallePedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDellePedios", new { id = detallePedido.IdDetalle }, detallePedido);
        }

        // DELETE: api/Detalle Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallePedido(int id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            _context.DetallePedidos.Remove(detallePedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleExists(int id)
        {
            return _context.DetallePedidos.Any(e => e.IdDetalle == id);
        }
    }
}
