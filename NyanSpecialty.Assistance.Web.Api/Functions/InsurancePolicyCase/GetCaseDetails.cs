using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyCaseFunction
    {
        

        [Function("GetCaseDetails")]
        public async Task<IActionResult> GetCaseDetails([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "insurancepolicycase/getcasedetails")] HttpRequest req)
        {
            _logger.LogInformation("InsurancePolicyCaseFunction.IActionResult Invoked");
            try
            {
                var data = await _caseDataManager.GetCaseDetailAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
