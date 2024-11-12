using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyCaseFunction
    {

        [Function("InsertOrUpdateCase")]
        public async Task<IActionResult> InsertOrUpdateCase([HttpTrigger(AuthorizationLevel.Function, "post", 
            Route = "insurancepolicycase/insertorupdatecase")] HttpRequest req)
        {
            _logger.LogInformation("InsurancePolicyCaseFunction.InsertOrUpdateCase Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid case details NOT provided");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult($"{nameof(InsertOrUpdateCase)}");
                var caseDetails = JsonConvert.DeserializeObject<Case>(requestBody);
                if (caseDetails == null)
                    return new BadRequestObjectResult("valid case details NOT provided");
                var response = await _caseDataManager.InsertOrUpdateCaseAsync(caseDetails);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading case details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
