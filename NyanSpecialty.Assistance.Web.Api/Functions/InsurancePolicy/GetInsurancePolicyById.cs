using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyFunctions
    {
        
        [Function("GetInsurancePolicyById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "insurancepolicy/getinsurancepolicy/{insurancepolicyid}")] HttpRequest req,
            long insurancepolicyid)
        {
            _logger.LogInformation("InsurancePolicyFunctions.GetInsurancePolicyById invoked");
            try
            {
                var data = await _insurancePolicyDataManager.GetInsurancePolicyByIdAsync(insurancepolicyid);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving policy :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
