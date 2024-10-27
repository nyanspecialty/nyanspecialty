using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyTypeFunction
    {
       
        [Function("GetPolicyTypes")]
        public async Task  <IActionResult> GetAllPolicyTypes([HttpTrigger(AuthorizationLevel.Function, "get", Route = "policytype/getpolicytypes")] HttpRequest req)
        {
            _logger.LogInformation("PolicyTypeFunction.GetPolicyTypes Invoked.");
            try
            {
                var response = await _policyTypeDataManager.GetAllPolicyTypeAsync();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving policy classes: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
