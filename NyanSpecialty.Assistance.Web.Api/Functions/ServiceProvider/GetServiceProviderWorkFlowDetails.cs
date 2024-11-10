using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceProviderFunction
    {
        
        [Function("GetServiceProviderWorkFlowDetails")]
        public async Task<IActionResult> GetServiceProviderWorkFlowDetails([HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "serviceprovider/getserviceproviderworkflowdetails")] HttpRequest req)
        {
            _logger.LogInformation("ServiceProviderFunction.GetServiceProviderWorkFlowDetails Invoked");
            try
            {
                var data = await _serviceProviderDataManager.GetServiceProviderWorkFlowDetailsAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving service providers : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
