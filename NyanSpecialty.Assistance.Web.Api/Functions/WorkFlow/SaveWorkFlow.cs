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
        [Function("SaveWorkFlow")]
        public async Task<IActionResult> SaveWorkFlow([HttpTrigger(AuthorizationLevel.Function, "post", Route ="workflow/saveworkflow")] HttpRequest req)
        {
            _logger.LogInformation("WorkFlowFunction.SaveWorkFlow Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid workflow object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("valid workflow object NOT provided");

                var workflow = JsonConvert.DeserializeObject<WorkFlow>(requestBody);

                if (workflow == null)
                    return new BadRequestObjectResult("valid workflow object NOT provided");

                var response = await _workFlowDataManager.InsertOrUpdateWorkFlow(workflow);
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
