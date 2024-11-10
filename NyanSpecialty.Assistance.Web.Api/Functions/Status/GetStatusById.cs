using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class StatusFunction
    {        

        [Function("GetStatusById")]
        public async Task<IActionResult> GetStatusById([HttpTrigger(AuthorizationLevel.Function, "get", Route ="status/getstatusbyid")] HttpRequest req, long statusId)
        {
            _logger.LogInformation("StatusFunction.GetStatusById Invoked");
            try
            {
                var data = await _statusDataManager.GetStatusByID(statusId);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving status by id :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
