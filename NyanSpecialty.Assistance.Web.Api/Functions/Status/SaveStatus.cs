using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class StatusFunction
    {
        [Function("SaveStatus")]
        public async Task<IActionResult> SaveStatus([HttpTrigger(AuthorizationLevel.Function, "post", Route ="status/savestatus")] HttpRequest req)
        {
            _logger.LogInformation("StatusFunction.SaveStatus Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("Valid status  object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("Valid status object NOT provided");

                var status = JsonConvert.DeserializeObject<Status>(requestBody);

                if (status == null)
                    return new BadRequestObjectResult("Valid insurance policy object NOT provided");

                await _statusDataManager.InsertOrUpdateStatusAsync(status);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error save status : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
