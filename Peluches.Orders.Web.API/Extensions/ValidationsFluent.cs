using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Peluches.Orders.Web.API.Extensions
{
    public static class ValidationsFluent
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
