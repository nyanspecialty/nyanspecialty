using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceProviderFunction
    {

        [Function("GetServiceProviderWorkFlowDetailsByProvider")]
        public async Task<IActionResult> GetServiceProviderWorkFlowDetailsByProvider([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "serviceprovider/getserviceproviderworkflowdetailsbyprovider/{providerId}")]
            HttpRequest req, long providerId)
        {
            _logger.LogInformation("ServiceProviderFunction.GetServiceProviderWorkFlowDetailsByProvider Invoked");
            try
            {
                var data = await _serviceProviderDataManager.GetServiceProviderWorkFlowDetailsAsync(providerId);
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
