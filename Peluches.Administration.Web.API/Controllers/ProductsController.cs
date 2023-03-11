using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;
using System.Net;

namespace Peluches.Administration.Web.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public ProductsController(TiendaPeluchesDBAzureContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<Producto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _context.Productos.ToListAsync();

                return Ok(new ServiceResult<List<Producto>>((int)HttpStatusCode.OK,
                            "Consulta exitosa de productos.", products));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Producto>((int)ex.StatusCode!,
                            "Consulta fallida de productos.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Producto>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de productos.", default!, ex.Message));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<Producto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _context.Productos.FindAsync(id);
                if (product == null)
                    return NotFound(new ServiceResult<Producto>((int)HttpStatusCode.NotFound,
                        "Producto no encontrado.", default!));

                return Ok(new ServiceResult<Producto>((int)HttpStatusCode.OK,
                    "Consulta de Producto exitosa.", product));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Producto>((int)ex.StatusCode!,
                            "Consulta fallida del Producto.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Producto>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida del Producto.", default!, ex.Message));
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, Producto product)
        {
            try
            {
                if (id != product.IdProducto)
                    return BadRequest(new ServiceResult<Producto>((int)HttpStatusCode.BadRequest,
                                "Actualización fallida del producto.", default!));

                var productToUpdate = _context.Productos.Any(b => b.IdProducto == product.IdProducto);
                if (!productToUpdate)
                    return NotFound(new ServiceResult<Producto>((int)HttpStatusCode.NotFound,
                                "No se encuentra el articulo a actualizar.", default!));

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Producto>((int)HttpStatusCode.NotFound,
                                "Producto actualizado exitosamente.", product,
                                LocationGetProductById(product.IdProducto)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Producto>((int)ex.StatusCode!,
                            "No se puede crear el producto.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Producto>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el producto.", default!, ex.Message));
            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Producto product)
        {
            try
            {
                _context.Productos.Add(product);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, new ServiceResult<Producto>((int)HttpStatusCode.Created,
                            "Producto creado satisfactoriamente.", product, this.LocationGetProductById(product.IdProducto)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Producto>((int)ex.StatusCode!,
                            "No se puede crear el producto.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Producto>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el producto.", default!, ex.Message));
            }
        }


        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var productToDelete = await _context.Productos.FirstOrDefaultAsync(b => b.IdProducto == id);
                if (productToDelete == null)
                    return NotFound(new ServiceResult<Producto>((int)HttpStatusCode.NotFound,
                                "No se encuentra el articulo a eliminar.", default!));

                productToDelete.Activo = false;

                _context.Entry(productToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Producto>((int)HttpStatusCode.OK,
                            "Producto eliminado satisfactoriamente.", productToDelete,
                            this.LocationGetProductById(productToDelete.IdProducto)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Producto>((int)ex.StatusCode!,
                            "No se puede eliminar el producto.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Producto>((int)HttpStatusCode.InternalServerError,
                            "No se puede eliminar el producto.", default!, ex.Message));
            }
        }

        private string LocationGetProductById(int productId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/products/get/{productId}";
        }
    }
}
