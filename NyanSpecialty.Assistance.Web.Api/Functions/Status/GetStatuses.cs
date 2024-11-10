using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class StatusFunction
    {       

        [Function("GetStatuses")]
        public async Task<IActionResult> GetStatuses([HttpTrigger(AuthorizationLevel.Function, "get", Route ="status/getstatuses")] HttpRequest req)
        {
            _logger.LogInformation("StatusFunction.GetStatuses Invoked");
            try
            {
                var data = await _statusDataManager.GetStatusesAsync();
                return new OkObjectResult(data);

            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving statuses :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
