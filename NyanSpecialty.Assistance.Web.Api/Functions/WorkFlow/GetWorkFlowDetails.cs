using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class WorkFlowFunction
    {

        [Function("GetWorkFlowDetails")]
        public async Task<IActionResult> GetWorkFlowDetails([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "workflow/getworkflowdetails")] HttpRequest req)
        {
            _logger.LogInformation("WorkFlowFunction.GetWorkFlowDetails  Invoked");
            try
            {
                var response = await _workFlowDataManager.GetWorkFlowDetailsAsync();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving work flows : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
