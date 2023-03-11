using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;
using System.Net;

namespace Peluches.Administration.Web.API.Controllers
{
    [Route("api/pulshModels")]
    [ApiController]
    public class PulshModelsController : ControllerBase
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public PulshModelsController(TiendaPeluchesDBAzureContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<Modelo>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pulshModels = await _context.Modelos.ToListAsync();

                return Ok(new ServiceResult<List<Modelo>>((int)HttpStatusCode.OK,
                            "Consulta exitosa de modelos de peluches.", pulshModels));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Modelo>((int)ex.StatusCode!,
                            "Consulta fallida de modelos de peluches.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Modelo>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de modelo de peluches.", default!, ex.Message));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<Modelo>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pulshModel = await _context.Modelos.FindAsync(id);
                if (pulshModel == null)
                    return NotFound(new ServiceResult<Modelo>((int)HttpStatusCode.NotFound,
                        "Modelo de peluche no encontrado.", default!));

                return Ok(new ServiceResult<Modelo>((int)HttpStatusCode.OK,
                    "Consulta de Modelo de peluche exitosa.", pulshModel));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Modelo>((int)ex.StatusCode!,
                            "Consulta fallida del modelo de peluche.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Modelo>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida del modelo de peluche.", default!, ex.Message));
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, Modelo pulshModel)
        {
            try
            {
                if (id != pulshModel.IdModelo)
                    return BadRequest(new ServiceResult<Modelo>((int)HttpStatusCode.BadRequest,
                                "Actualización fallida del modelo de peluche.", default!));

                var pulshModelToUpdate = _context.Modelos.Any(b => b.IdModelo == pulshModel.IdModelo);
                if (!pulshModelToUpdate)
                    return NotFound(new ServiceResult<Modelo>((int)HttpStatusCode.NotFound,
                                "No se encuentra modelo de peluche a actualizar.", default!));

                _context.Entry(pulshModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Modelo>((int)HttpStatusCode.NotFound,
                                "Modelo de peluche actualizado exitosamente.", pulshModel,
                                LocationGetPulshModelById(pulshModel.IdModelo)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Modelo>((int)ex.StatusCode!,
                            "No se puede crear el modelo de peluche.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Modelo>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el modelo de peluche.", default!, ex.Message));
            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Modelo pulshModel)
        {
            try
            {
                _context.Modelos.Add(pulshModel);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, new ServiceResult<Modelo>((int)HttpStatusCode.Created,
                            "Modelo de peluche creado satisfactoriamente.", pulshModel, this.LocationGetPulshModelById(pulshModel.IdModelo)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Modelo>((int)ex.StatusCode!,
                            "No se puede crear el modelo de peluche.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Modelo>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el modelo de peluche.", default!, ex.Message));
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
                var pulshModelToDelete = await _context.Modelos.FirstOrDefaultAsync(b => b.IdModelo == id);
                if (pulshModelToDelete == null)
                    return NotFound(new ServiceResult<Modelo>((int)HttpStatusCode.NotFound,
                                "No se encuentra el modelo de peluche a eliminar.", default!));

                //pulshModelToDelete.Activo = false;

                _context.Entry(pulshModelToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Modelo>((int)HttpStatusCode.OK,
                            "Modelo de peluche eliminado satisfactoriamente.", pulshModelToDelete,
                            this.LocationGetPulshModelById(pulshModelToDelete.IdModelo)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Modelo>((int)ex.StatusCode!,
                            "No se puede eliminar el modelo de peluche.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Modelo>((int)HttpStatusCode.InternalServerError,
                            "No se puede eliminar el modelo de peluche.", default!, ex.Message));
            }
        }

        private string LocationGetPulshModelById(int pulshModelId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/pulshModels/get/{pulshModelId}";
        }
    }
}
