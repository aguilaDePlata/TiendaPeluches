using Newtonsoft.Json;

namespace Peluches.Administration.Web.API.Base
{
    //OLD: ModelValidationResult
    public class BasicValidationInformation
    {
        [JsonProperty(propertyName: "Key")]
        public string Properties { get; set; } = string.Empty;

        [JsonProperty(propertyName: "Errors")]
        public List<ErrorsMessages> Errors { get; set; } = default!;
    }

    public class ErrorsMessages
    {
        public string ErrorMessage { get; set; } = default!;
    }
}
