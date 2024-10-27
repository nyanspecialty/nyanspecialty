using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleClassFunctions
    {
        [Function("GetVehicleClassById")]
        public async Task<IActionResult> GetVehicleClassById([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "vehicleclass/getvehicleclassbyid/{vehicleClassId}")]
            HttpRequest req,
            long vehicleClassId)
        {
            _logger.LogInformation("VehicleClassFunctions.GetVehicleClassById Invoked.");

            try
            {
                var response = await _vehicleClassDataManager.GetVehicleClassAsync(vehicleClassId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving vehicle classes: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

}


