
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Peluches.Administration.Web.API.Base
{
    public class ServiceResult<T>
    {
        public ServiceResult(int code, string result, T data, string details = "", ModelStateDictionary validationDetail = default!)
        {
            Code = code;
            Result = result;
            Details = details;
            ValidationDetails = validationDetail != default ? this.GetMessageErrors(validationDetail).ToList() : default!;
            Data = data;
        }

        public int Code { get; set; }
        public string Result { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public List<object> ValidationDetails { get; set; } = default!;
        public T Data { get; set; }


        private IEnumerable<object> GetMessageErrors(ModelStateDictionary ValidationDetail)
        {
            string jsonFormatValidations = ValidationDetail.Where(s => s.Value!.ValidationState == ModelValidationState.Invalid).Select(c => c.Value).ToJson();
            IEnumerable<BasicValidationInformation> resultValidations =
                JsonConvert.DeserializeObject<IEnumerable<BasicValidationInformation>>(jsonFormatValidations);

            return resultValidations;
        }
    }
}
