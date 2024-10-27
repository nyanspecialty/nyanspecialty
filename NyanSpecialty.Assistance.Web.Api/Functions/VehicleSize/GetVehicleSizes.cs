using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;


namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleSizeFunctions
    {
        
        [Function("GetVehicleSizes")]
        public void Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "vehiclesize/getvehiclesizes")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
        }
    }
}
