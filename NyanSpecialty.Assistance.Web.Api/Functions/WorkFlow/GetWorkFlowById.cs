using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class WorkFlowFunction
    {        
        [Function("GetWorkFlowById")]
        public async Task<IActionResult> GetWorkFlowById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "workflow/getworkflowbyid/{workflowbyid}")] HttpRequest req,
            long workFlowById)
        {
            _logger.LogInformation("WorkFlowFunction.GetWorkFlowById Inovked");
            try
            {
                var response = await _workFlowDataManager.GetWorkFlowAsync(workFlowById);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving work flow : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
