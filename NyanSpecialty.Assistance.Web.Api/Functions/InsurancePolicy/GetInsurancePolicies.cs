using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyFunctions
    {
      
        [Function("GetInsurancePolicies")]
        public async Task<IActionResult> GetInsurancePolicies([HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "insurancepolicy/getinsurancepolicies")] HttpRequest req)
        {
            _logger.LogInformation("InsurancePolicyFunctions.GetInsurancePolicies Invoked.");
            try
            {
                var data = await _insurancePolicyDataManager.GetAllInsurancePoliciesAsync();
                return new OkObjectResult(data);

            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving policys :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
