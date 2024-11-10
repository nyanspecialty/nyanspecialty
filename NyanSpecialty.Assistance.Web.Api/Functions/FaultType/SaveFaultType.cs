using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class FaultTypeFunction
    {
        
        [Function("SaveFaultType")]
        public async Task<IActionResult> SaveFaultType([HttpTrigger(AuthorizationLevel.Function, "post",Route ="faulttype/savefaulttype")] HttpRequest req)
        {
            _logger.LogInformation("FaultTypeFunction.SaveFaultType Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("Valid faultType  object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("Valid faultType object NOT provided");

                var faultType = JsonConvert.DeserializeObject<FaultType>(requestBody);

                if (faultType == null)
                    return new BadRequestObjectResult("Valid faultType object NOT provided");

                await _faultTypeDataManager.InsertOrUpdateFaultTypeAsync(faultType);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving faultType : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
