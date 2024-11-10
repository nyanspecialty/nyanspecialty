using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class FaultTypeFunction
    {       

        [Function("GetFaultTypes")]
        public async Task<IActionResult> GetFaultTypes([HttpTrigger(AuthorizationLevel.Function, "get", Route ="faulttype/getfaulttypes")] HttpRequest req)
        {
            _logger.LogInformation("FaultTypeFunction. GetFaultTypes Invoked");
            try
            {
                var data = await _faultTypeDataManager.GetFaultTypesAsync();
                return new OkObjectResult(data);

            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving fault types :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
