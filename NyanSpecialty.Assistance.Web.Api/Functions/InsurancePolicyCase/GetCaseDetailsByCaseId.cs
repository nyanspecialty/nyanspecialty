using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyCaseFunction
    {
      
        [Function("GetCaseDetailsByCaseId")]
        public async Task<IActionResult> GetCaseDetailsByCaseId([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "insurancepolicycase/getcasedetailsbycaseid/{caseId}")] HttpRequest req,long caseId)
        {
            _logger.LogInformation("InsurancePolicyCaseFunction.GetCaseDetailsByCaseId Invoked");
            try
            {
                var data = await _caseDataManager.GetCaseDetailsByCaseIdAsync(caseId);
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError("error while retrieving customer by Id");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
