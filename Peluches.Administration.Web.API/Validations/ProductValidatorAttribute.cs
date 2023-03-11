using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Peluches.Administration.Web.API.Base;
using Peluches.Administration.Web.API.Models;

namespace Peluches.Administration.Web.API.Validations
{
    public class ProductValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(new ServiceResult<Producto>((int)HttpStatusCode.BadRequest,
                                        "No se puede actualizar o agregar el producto.", default!, validationDetail: context.ModelState));
        }
    }
}
