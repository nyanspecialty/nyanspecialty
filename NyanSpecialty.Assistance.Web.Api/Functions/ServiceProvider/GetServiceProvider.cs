using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceProviderFunction
    {
        [Function("GetServiceProvider")]
        public async Task<IActionResult> GetServiceProvider([HttpTrigger(AuthorizationLevel.Function, "get", Route = "serviceprovider/getserviceprovider/{searchinput}")] HttpRequest req, 
            string searchInput)
        {
            _logger.LogInformation("ServiceProviderFunction.GetServiceProvider Invoked");
            try
            {
                var response= await _serviceProviderDataManager.GetServiceProviderAsync(searchInput);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving service provider: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
