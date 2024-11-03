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
        [Function("SaveServiceProvider")]
        public async Task<IActionResult> SaveServiceProvider([HttpTrigger(AuthorizationLevel.Function, "post", Route ="serviceprovider/saveserviceprovider")] HttpRequest req)
        {
            _logger.LogInformation("ServiceProviderFunction.SaveServiceProvider Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid provider object NOT provided");

                string requestBody= await new StreamReader(req.Body).ReadToEndAsync();
                if (requestBody == null)
                    return new BadRequestObjectResult("valid provider object NOT provided");

                var serviceProvider = JsonConvert.DeserializeObject<ServiceProvider>(requestBody);

                if (serviceProvider == null)
                    return new BadRequestObjectResult("valid provider object NOT provided");

                var response = await _serviceProviderDataManager.InsertOrUpdateServiceProviderAsync(serviceProvider);
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
