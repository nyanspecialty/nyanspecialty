using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleSizeFunctions
    {

        [Function("GetVehicleSizeById")]
        public async Task<IActionResult> GetVehicleSizeById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "vehiclesize/getvehiclesizebyid/{vehiclesizeid}")]
        HttpRequest req,  long vehiclesizeid)
        {
            _logger.LogInformation("VehicleSizeFunctions.GetVehicleSizeById Invoked.");

            try
            {
                var response = await _vehicleSizeDataManager.GetVehicleSizeAsync(vehiclesizeid);
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
