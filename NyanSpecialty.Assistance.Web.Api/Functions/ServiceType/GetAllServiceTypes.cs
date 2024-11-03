using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceTypeFunction
    {        
        [Function("GetAllServiceTypes")]
        public async Task<IActionResult> GetAllServiceTypes([HttpTrigger(AuthorizationLevel.Function, "get", Route ="servicetype/getallservicetypes")] HttpRequest req)
        {
            _logger.LogInformation("ServiceTypeFunction.GetAllServiceTypes Invoked");
            try
            {
                var response = await _serviceTypeDataManager.GetServiceTypesAsync();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving service types : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
