using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceProviderFunction
    {       
        [Function("GetAllServiceProviders")]
        public async Task<IActionResult> GetAllServiceProviders([HttpTrigger(AuthorizationLevel.Function, "get", Route ="serviceprovider/getallserviceproviders")] HttpRequest req)
        {
            _logger.LogInformation("ServiceProviderFunction.GetAllServiceProviders Invoked");
            try
            {
                var data = await _serviceProviderDataManager.GetServiceProvidersAsync();
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
