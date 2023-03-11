
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Peluches.Administration.Web.API.Models;
using System.Net;
using Peluches.Administration.Web.API.Base;

namespace Peluches.Administration.Web.API.Validations
{
    public class EmployeeValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(new ServiceResult<Empleado>((int)HttpStatusCode.BadRequest,
                                        "No se puede actualizar o agregar el empleado.", default!, validationDetail: context.ModelState));
        }
    }
}
