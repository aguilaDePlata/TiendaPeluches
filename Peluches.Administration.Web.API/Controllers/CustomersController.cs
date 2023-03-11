using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;
using System.Net;

namespace Peluches.Administration.Web.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public CustomersController(TiendaPeluchesDBAzureContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<Cliente>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clients = await _context.Clientes.ToListAsync();

                return Ok(new ServiceResult<List<Cliente>>((int)HttpStatusCode.OK,
                            "Consulta exitosa de clientes.", clients));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cliente>((int)ex.StatusCode!,
                            "Consulta fallida de clientes.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cliente>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de clientes.", default!, ex.Message));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<Cliente>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var client = await _context.Clientes.FindAsync(id);
                if (client == null)
                    return NotFound(new ServiceResult<Cliente>((int)HttpStatusCode.NotFound,
                        "Cliente no encontrado.", default!));

                return Ok(new ServiceResult<Cliente>((int)HttpStatusCode.OK,
                    "Consulta de Cliente exitosa.", client));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cliente>((int)ex.StatusCode!,
                            "Consulta fallida del Cliente.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cliente>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida del Cliente.", default!, ex.Message));
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, Cliente client)
        {
            try
            {
                if (id != client.IdCliente)
                    return BadRequest(new ServiceResult<Cliente>((int)HttpStatusCode.BadRequest,
                                "Actualización fallida del cliente.", default!));

                var clientToUpdate = _context.Clientes.Any(b => b.IdCliente== client.IdCliente);
                if (!clientToUpdate)
                    return NotFound(new ServiceResult<Cliente>((int)HttpStatusCode.NotFound,
                                "No se encuentra el cliente a actualizar.", default!));

                _context.Entry(client).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Cliente>((int)HttpStatusCode.NotFound,
                                "Cliente actualizado exitosamente.", client,
                                LocationGetClientById(client.IdCliente)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cliente>((int)ex.StatusCode!,
                            "No se puede crear el cliente.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cliente>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el cliente.", default!, ex.Message));
            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Cliente client)
        {
            try
            {
                _context.Clientes.Add(client);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, new ServiceResult<Cliente>((int)HttpStatusCode.Created,
                            "Cliente creado satisfactoriamente.", client, this.LocationGetClientById(client.IdCliente)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cliente>((int)ex.StatusCode!,
                            "No se puede crear el cliente.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cliente>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el cliente.", default!, ex.Message));
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
                var clientToDelete = await _context.Clientes.FirstOrDefaultAsync(b => b.IdCliente == id);
                if (clientToDelete == null)
                    return NotFound(new ServiceResult<Cliente>((int)HttpStatusCode.NotFound,
                                "No se encuentra el cliente a eliminar.", default!));

                clientToDelete.Activo = false;

                _context.Entry(clientToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Cliente>((int)HttpStatusCode.OK,
                            "Cliente eliminado satisfactoriamente.", clientToDelete,
                            this.LocationGetClientById(clientToDelete.IdCliente)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Cliente>((int)ex.StatusCode!,
                            "No se puede eliminar el cliente.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Cliente>((int)HttpStatusCode.InternalServerError,
                            "No se puede eliminar el cliente.", default!, ex.Message));
            }
        }

        private string LocationGetClientById(int clientId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/customers/get/{clientId}";
        }
    }
}
