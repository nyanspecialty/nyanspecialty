using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;


namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleSizeFunctions
    {

        [Function("SaveVehicleSize")]
        public async Task<IActionResult> SaveVehicleSize([HttpTrigger(AuthorizationLevel.Function,"post",
            Route = "vehiclesize/savevehiclesize")] HttpRequest req)
        {
            _logger.LogInformation("VehicleSizeFunctions.SaveVehicleSize Invoked.");

            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("Valid vehiclesize object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var vehiclesize = JsonConvert.DeserializeObject<VehicleSize>(requestBody);

                if (vehiclesize == null)
                    return new BadRequestObjectResult("Valid vehiclesize object NOT provided");

                var response = await _vehicleSizeDataManager.InsertOrUpdateVehicleSizeAsync(vehiclesize);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving vehicle sizes: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
