using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;
using System.Net;

namespace Peluches.Administration.Web.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public EmployeesController(TiendaPeluchesDBAzureContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<Empleado>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _context.Empleados.ToListAsync();

                return Ok(new ServiceResult<List<Empleado>>((int)HttpStatusCode.OK,
                            "Consulta exitosa de empleados.", employees));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Empleado>((int)ex.StatusCode!,
                            "Consulta fallida de empleados.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Empleado>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida de empleados.", default!, ex.Message));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<Empleado>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await _context.Empleados.FindAsync(id);
                if (employee == null)
                    return NotFound(new ServiceResult<Empleado>((int)HttpStatusCode.NotFound,
                        "Empleado no encontrado.", default!));

                return Ok(new ServiceResult<Empleado>((int)HttpStatusCode.OK,
                    "Consulta de Empleado exitosa.", employee));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Empleado>((int)ex.StatusCode!,
                            "Consulta fallida del Empleado.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Empleado>((int)HttpStatusCode.InternalServerError,
                            "Consulta fallida del Empleado.", default!, ex.Message));
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, Empleado employee)
        {
            try
            {
                if (id != employee.IdEmpleado)
                    return BadRequest(new ServiceResult<Empleado>((int)HttpStatusCode.BadRequest,
                                "Actualización fallida del empleado.", default!));

                var employeeToUpdate = _context.Empleados.Any(b => b.IdEmpleado == employee.IdEmpleado);
                if (!employeeToUpdate)
                    return NotFound(new ServiceResult<Empleado>((int)HttpStatusCode.NotFound,
                                "No se encuentra el empleado a actualizar.", default!));

                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Empleado>((int)HttpStatusCode.NotFound,
                                "Empleado actualizado exitosamente.", employee,
                                LocationGetEmployeeById(employee.IdEmpleado)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Empleado>((int)ex.StatusCode!,
                            "No se puede crear el empleado.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Empleado>((int)HttpStatusCode.InternalServerError,
                            "No se puede crear el empleado.", default!, ex.Message));
            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Empleado employee)
        {
            try
            {
                _context.Empleados.Add(employee);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, new ServiceResult<Empleado>((int)HttpStatusCode.Created,
                            "Empleado creado satisfactoriamente.", employee, this.LocationGetEmployeeById(employee.IdEmpleado)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Empleado>((int)ex.StatusCode!,
                            "No se puede crear el empleado.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Empleado>((int)HttpStatusCode.InternalServerError, 
                            "No se puede crear el empleado.", default!, ex.Message));
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
                var employeeToDelete = await _context.Empleados.FirstOrDefaultAsync(b => b.IdEmpleado == id);
                if (employeeToDelete == null)
                    return NotFound(new ServiceResult<Empleado>((int)HttpStatusCode.NotFound,
                                "No se encuentra el empleado a eliminar.", default!));

                employeeToDelete.Activo = false;

                _context.Entry(employeeToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ServiceResult<Empleado>((int)HttpStatusCode.OK,
                            "Empleado eliminado satisfactoriamente.", employeeToDelete,
                            this.LocationGetEmployeeById(employeeToDelete.IdEmpleado)));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ServiceResult<Empleado>((int)ex.StatusCode!,
                            "No se puede eliminar el empleado.", default!, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ServiceResult<Empleado>((int)HttpStatusCode.InternalServerError,
                            "No se puede eliminar el empleado.", default!, ex.Message));
            }
        }

        private string LocationGetEmployeeById(int employeeId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/employees/get/{employeeId}";
        }
    }
}
