using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleClassFunctions
    {
        [Function("GetVehicleClasses")]
        public async Task<IActionResult> GetVehicleClasses([HttpTrigger(AuthorizationLevel.Function, "get", Route = "vehicleclass/getvehicleclasses")]
        HttpRequest req)
        {
            _logger.LogInformation("VehicleClassFunctions.GetVehicleClasses Invoked.");

            try
            {
                var response = await _vehicleClassDataManager.GetAllVehicleClassesAsync();
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
