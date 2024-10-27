using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleClassFunctions
    {
       

        [Function("SaveVehicleClass")]
        public async Task<IActionResult> SaveVehicleClass([HttpTrigger(AuthorizationLevel.Function, "post",Route = "vehicleclass/savevehicleclass")]
        HttpRequest req)
        {
            _logger.LogInformation("VehicleClassFunctions.SaveVehicleClass Invoked.");

            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("alid vehicleclass object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if(string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("alid vehicleclass object NOT provided");

                var vehicleCass = JsonConvert.DeserializeObject<VehicleClass>(requestBody);

                if (vehicleCass == null)
                    return new BadRequestObjectResult("alid vehicleclass object NOT provided");

                var response = await _vehicleClassDataManager.InsertOrUpdateVehicleClassAsync(vehicleCass);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving vehicle classes: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
