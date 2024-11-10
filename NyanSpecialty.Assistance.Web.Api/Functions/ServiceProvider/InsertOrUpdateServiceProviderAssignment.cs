using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceProviderFunction
    {
        
        [Function("InsertOrUpdateServiceProviderAssignment")]
        public async Task<IActionResult> InsertOrUpdateServiceProviderAssignment([HttpTrigger(AuthorizationLevel.Function,"post",
            Route = "serviceprovider/insertorupdateserviceproviderassignment")] HttpRequest req)
        {
            _logger.LogInformation("ServiceProviderFunction.InsertOrUpdateServiceProviderAssignment Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid provider object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                if (requestBody == null)
                    return new BadRequestObjectResult("valid provider object NOT provided");

                var serviceProvider = JsonConvert.DeserializeObject<ServiceProviderAssignment>(requestBody);

                if (serviceProvider == null)
                    return new BadRequestObjectResult("valid provider object NOT provided");

                var response = await _serviceProviderDataManager.InsertOrUpdateServiceProviderAssignmentAsync(serviceProvider);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving service Provider : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
