using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceTypeFunction
    {

        [Function("GetServiceTypeById")]
        public async Task<IActionResult> GetServiceTypeById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "servicetype/getservicetypebyid/{servicetypeid}")] HttpRequest req,
            long servicetypeid)
        {
            _logger.LogInformation("ServiceTypeFunction.GetServiceTypeById Invoked");
            try
            {
                var responce = await _serviceTypeDataManager.GetServiceTypeAsync(servicetypeid);
                return new OkObjectResult(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving service types: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
