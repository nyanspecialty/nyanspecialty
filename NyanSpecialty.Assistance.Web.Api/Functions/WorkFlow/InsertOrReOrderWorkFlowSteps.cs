using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class WorkFlowFunction
    {
        
        [Function("InsertOrReOrderWorkFlowSteps")]
        public async Task<IActionResult> InsertOrReOrderWorkFlowSteps([HttpTrigger(AuthorizationLevel.Function, "post",
            Route = "workflow/insertorreorderworkflowsteps")] HttpRequest req)
        {
            _logger.LogInformation("WorkFlowFunction.InsertOrReOrderWorkFlowSteps Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid workflow object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("valid workflow object NOT provided");

                var workflow = JsonConvert.DeserializeObject<List<WorkFlowStep>>(requestBody);

                if (workflow == null)
                    return new BadRequestObjectResult("valid workflow object NOT provided");

                var response = await _workFlowDataManager.InsertOrReOrderWorkFlowStepsAsync(workflow);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving work flow : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
