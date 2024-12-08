using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyTypeFunction
    {

        [Function("GetPolicyTypeById")]
        public async Task<IActionResult> GetPolicyTypeById(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "policytype/getpolicytypebyid/{policytypeid}")]
            HttpRequest req, long policytypeid)
        {
            _logger.LogInformation("PolicyTypeFunction.GetPolicyTypeById Invoked");
            try
            {
                var response = await _policyTypeDataManager.GetPolicyTypeByIdAsync(policytypeid);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving policy types: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
