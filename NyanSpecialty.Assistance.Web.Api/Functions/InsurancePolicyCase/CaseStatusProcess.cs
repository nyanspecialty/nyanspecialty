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
       

        [Function("CaseStatusProcess")]
        public async Task< IActionResult> CaseStatusProcess([HttpTrigger(AuthorizationLevel.Function, "post",
            Route = "insurancepolicycase/casestatusprocess")] HttpRequest req)
        {
            _logger.LogInformation("InsurancePolicyCaseFunction.CaseStatusProcess Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid case details NOT provided");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult($"{nameof(CaseStatusProcess)}");
                var caseDetails = JsonConvert.DeserializeObject<CaseStatusProcess>(requestBody);
                if (caseDetails == null)
                    return new BadRequestObjectResult("valid case details NOT provided");
                var response = await _caseDataManager.CaseStatusProcessAsync(caseDetails);
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
