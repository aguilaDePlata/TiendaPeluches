using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;
using System.Net;

namespace Peluches.Administration.Web.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public BrandsController(TiendaPeluchesDBAzureContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<Marca>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var brands = await _context.Marcas.ToListAsync();

                return Ok(new ServiceResult<List<Marca>>((int)HttpStatusCode.OK,
                            "Consulta exitosa de marcas.", brands)); ;
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Marca>((int)ex.StatusCode!,
                            "Consulta fallida de marcas.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Marca>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de marcas.", default!, ex.Message));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<Marca>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var brand = await _context.Marcas.FindAsync(id);
                if (brand == null)
                    return NotFound(new ServiceResult<Marca>((int)HttpStatusCode.NotFound,
                        "Marca no encontrado.", default!));

                return Ok(new ServiceResult<Marca>((int)HttpStatusCode.OK,
                    "Consulta de Marca exitosa.", brand));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Marca>((int)ex.StatusCode!,
                            "Consulta fallida de la Marca.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Marca>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de la Marca.", default!, ex.Message));
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, Marca brand)
        {
            try
            {
                if (id != brand.IdMarca)
                    return BadRequest(new ServiceResult<Marca>((int)HttpStatusCode.BadRequest,
                                "Actualización fallida de la marca.", default!));

                var brandToUpdate = _context.Marcas.Any(b => b.IdMarca == brand.IdMarca);
                if (!brandToUpdate)
                    return NotFound(new ServiceResult<Marca>((int)HttpStatusCode.NotFound,
                                "No se encuentra la marca a actualizar.", default!));

                _context.Entry(brand).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Marca>((int)HttpStatusCode.NotFound,
                                "Marca actualizada exitosamente.", brand,
                                LocationGetBrandById(brand.IdMarca)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Marca>((int)ex.StatusCode!,
                            "No se puede crear la marca.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Marca>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear la marca.", default!, ex.Message));
            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Marca brand)
        {
            try
            {
                _context.Marcas.Add(brand);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, new ServiceResult<Marca>((int)HttpStatusCode.Created,
                            "Marca creada satisfactoriamente.", brand, this.LocationGetBrandById(brand.IdMarca)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Marca>((int)ex.StatusCode!,
                            "No se puede crear la marca.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Marca>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear la marca.", default!, ex.Message));
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
                var brandToDelete = await _context.Marcas.FirstOrDefaultAsync(b => b.IdMarca == id);
                if (brandToDelete == null)
                    return NotFound(new ServiceResult<Marca>((int)HttpStatusCode.NotFound,
                                "No se encuentra la marca a eliminar.", default!));

                //brandToDelete.Activo = false;

                _context.Entry(brandToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Marca>((int)HttpStatusCode.OK,
                            "Marca eliminada satisfactoriamente.", brandToDelete,
                            this.LocationGetBrandById(brandToDelete.IdMarca)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Marca>((int)ex.StatusCode!,
                            "No se puede eliminar la marca.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Marca>((int)HttpStatusCode.InternalServerError,
                            "No se puede eliminar la marca.", default!, ex.Message));
            }
        }

        private string LocationGetBrandById(int brandId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/brands/get/{brandId}";
        }
    }
}
