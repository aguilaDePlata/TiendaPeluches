using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;
using System.Net;

namespace Peluches.Administration.Web.API.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public SuppliersController(TiendaPeluchesDBAzureContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<Proveedor>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var suppliers = await _context.Proveedors.ToListAsync();

                return Ok(new ServiceResult<List<Proveedor>>((int)HttpStatusCode.OK, 
                            "Consulta exitosa de productos.", suppliers));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Proveedor>((int)ex.StatusCode!,
                            "Consulta fallida de productos.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Proveedor>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de productos.", default!, ex.Message));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<Proveedor>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var supplier = await _context.Proveedors.FindAsync(id);
                if (supplier == null)
                    return NotFound(new ServiceResult<Proveedor>((int)HttpStatusCode.NotFound,
                        "Proveedor no encontrado.", default!));

                return Ok(new ServiceResult<Proveedor>((int)HttpStatusCode.OK,
                    "Consulta de Proveedor exitosa.", supplier));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Proveedor>((int)ex.StatusCode!,
                            "Consulta fallida del Proveedor.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Proveedor>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida del Proveedor.", default!, ex.Message));
            }
        }


        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, Proveedor supplier)
        {
            try
            {
                if (id != supplier.IdProveedor)
                    return BadRequest(new ServiceResult<Proveedor>((int)HttpStatusCode.BadRequest,
                                "Actualización fallida del proveedor.", default!));

                var supplierToUpdate = _context.Productos.Any(b => b.IdProducto == supplier.IdProveedor);
                if (!supplierToUpdate)
                    return NotFound(new ServiceResult<Proveedor>((int)HttpStatusCode.NotFound,
                                "No se encuentra el proveedor a actualizar.", default!));

                _context.Entry(supplier).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Proveedor>((int)HttpStatusCode.NotFound,
                                "Proveedor actualizado exitosamente.", supplier,
                                LocationGetSuppliersById(supplier.IdProveedor)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Proveedor>((int)ex.StatusCode!,
                            "No se puede crear el proveedor.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Proveedor>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el proveedor.", default!, ex.Message));
            }
        }


        [HttpPost]
        [Route("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Proveedor supplier)
        {
            try
            {
                _context.Proveedors.Add(supplier);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, new ServiceResult<Proveedor>((int)HttpStatusCode.Created,
                            "Proveedor creado satisfactoriamente.", supplier, 
                            this.LocationGetSuppliersById(supplier.IdProveedor)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Proveedor>((int)ex.StatusCode!,
                            "No se puede crear el proveedor.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Proveedor>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el proveedor.", default!, ex.Message));
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
                var supplierToDelete = await _context.Proveedors.FirstOrDefaultAsync(b => b.IdProveedor == id);
                if (supplierToDelete == null)
                    return NotFound(new ServiceResult<Proveedor>((int)HttpStatusCode.NotFound,
                                "No se encuentra el proveedor a eliminar.", default!));

                supplierToDelete.Activo = false;

                _context.Entry(supplierToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Proveedor>((int)HttpStatusCode.OK,
                            "Proveedor eliminado satisfactoriamente.", supplierToDelete,
                            this.LocationGetSuppliersById(supplierToDelete.IdProveedor)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Producto>((int)ex.StatusCode!,
                            "No se puede eliminar el proveedor.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Producto>((int)HttpStatusCode.InternalServerError,
                            "No se puede eliminar el proveedor.", default!, ex.Message));
            }
        }

        private string LocationGetSuppliersById(int supplierId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/suppliers/get/{supplierId}";
        }
    }
}
