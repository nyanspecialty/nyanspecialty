using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceTypeFunction
    {        
        [Function("SaveServiceType")]
        public async Task<IActionResult> SaveServiceType([HttpTrigger(AuthorizationLevel.Function, "post", Route ="servicetype/saveservicetype")] HttpRequest req)
        {
            _logger.LogInformation("ServiceTypeFunction.SaveServiceType Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid serviceType object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("valid serviceType object NOT provided");

                var serviceType = JsonConvert.DeserializeObject<ServiceType>(requestBody);

                if (serviceType == null)
                    return new BadRequestObjectResult("valid serviceType object NOT provided");

                var response = await _serviceTypeDataManager.AddEditServiceTypeAsync(serviceType);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving service Type : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
