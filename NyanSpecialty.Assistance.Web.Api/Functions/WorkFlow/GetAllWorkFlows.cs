using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class WorkFlowFunction
    {
        [Function("GetAllWorkFlows")]
        public async Task<IActionResult> GetAllWorkFlows([HttpTrigger(AuthorizationLevel.Function, "get", Route ="workflow/getallworkflows")] HttpRequest req)
        {
            _logger.LogInformation("WorkFlowFunction.GetAllWorkFlows  Invoked");
            try
            {
                var response = await _workFlowDataManager.GetAllWorkFlowsAsync();
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
