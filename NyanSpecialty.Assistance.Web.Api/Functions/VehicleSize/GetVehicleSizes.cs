using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;


namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleSizeFunctions
    {

        [Function("GetVehicleSizes")]
        public async Task<IActionResult> GetVehicleSizes([HttpTrigger(AuthorizationLevel.Function, 
            "get", Route = "vehiclesize/getvehiclesizes")]  HttpRequestData req)
        {
            _logger.LogInformation("VehicleSizeFunctions.GetVehicleSizes Invoked.");

            try
            {
                var response = await _vehicleSizeDataManager.GetAllVehicleSizeAsync();
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
