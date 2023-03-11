using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;
using System.Net;

namespace Peluches.Administration.Web.API.Controllers
{
    [Route("api/charges")]
    [ApiController]
    public class ChargesController : ControllerBase
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public ChargesController(TiendaPeluchesDBAzureContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<Cargo>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var charge = await _context.Cargos.ToListAsync();

                return Ok(new ServiceResult<List<Cargo>>((int)HttpStatusCode.OK,
                            "Consulta exitosa de cargos.", charge));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cargo>((int)ex.StatusCode!,
                            "Consulta fallida de cargos.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cargo>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de cargos.", default!, ex.Message));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<Cargo>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var charge = await _context.Cargos.FindAsync(id);
                if (charge == null)
                    return NotFound(new ServiceResult<Cargo>((int)HttpStatusCode.NotFound,
                        "Cargo no encontrado.", default!));

                return Ok(new ServiceResult<Cargo>((int)HttpStatusCode.OK,
                    "Consulta de cargo exitosa.", charge));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cargo>((int)ex.StatusCode!,
                            "Consulta fallida del Cargo.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cargo>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida del Cargo.", default!, ex.Message));
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, Cargo charge)
        {
            try
            {
                if (id != charge.IdCargo)
                    return BadRequest(new ServiceResult<Cargo>((int)HttpStatusCode.BadRequest,
                                "Actualización fallida del cargo.", default!));

                var chargeToUpdate = _context.Productos.Any(b => b.IdProducto == charge.IdCargo);
                if (!chargeToUpdate)
                    return NotFound(new ServiceResult<Cargo>((int)HttpStatusCode.NotFound,
                                "No se encuentra el cargo a actualizar.", default!));

                _context.Entry(charge).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Cargo>((int)HttpStatusCode.NotFound,
                                "Cargo actualizado exitosamente.", charge,
                                LocationGetChargeById(charge.IdCargo)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cargo>((int)ex.StatusCode!,
                            "No se puede crear el cargo.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cargo>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el cargo.", default!, ex.Message));
            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Cargo charge)
        {
            try
            {
                _context.Cargos.Add(charge);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, new ServiceResult<Cargo>((int)HttpStatusCode.Created,
                            "Cargo creado satisfactoriamente.", charge, this.LocationGetChargeById(charge.IdCargo)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cargo>((int)ex.StatusCode!,
                            "No se puede crear el cargo.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cargo>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el cargo.", default!, ex.Message));
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
                var chargeToDelete = await _context.Cargos.FirstOrDefaultAsync(b => b.IdCargo == id);
                if (chargeToDelete == null)
                    return NotFound(new ServiceResult<Cargo>((int)HttpStatusCode.NotFound,
                                "No se encuentra el cargo a eliminar.", default!));

                //chargeToDelete.Activo = false;

                _context.Entry(chargeToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Cargo>((int)HttpStatusCode.OK,
                            "Cargo eliminado satisfactoriamente.", chargeToDelete,
                            this.LocationGetChargeById(chargeToDelete.IdCargo)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cargo>((int)ex.StatusCode!,
                            "No se puede eliminar el cargo.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cargo>((int)HttpStatusCode.InternalServerError,
                            "No se puede eliminar el cargo.", default!, ex.Message));
            }
        }

        private string LocationGetChargeById(int chargeId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/charges/get/{chargeId}";
        }
    }
}
