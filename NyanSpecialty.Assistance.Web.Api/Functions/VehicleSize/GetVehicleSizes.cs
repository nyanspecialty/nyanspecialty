using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleSizeFunctions
    {
        [Function("GetVehicleSizes")]
        public async Task<IActionResult> GetVehicleSizes(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "vehiclesize/getvehiclesizes")] HttpRequest req,
            FunctionContext context)
        {
            var logger = context.GetLogger<VehicleSizeFunctions>();
            logger.LogInformation("VehicleSizeFunctions.GetVehicleSizes Invoked.");
            try
            {
                var response = await _vehicleSizeDataManager.GetAllVehicleSizeAsync();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error retrieving vehicle sizes: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
