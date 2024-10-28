using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyFunctions
    {
        
        [Function("SaveInsurancePolicy")]
        public async Task<IActionResult> SaveInsurancePolicy(
            [HttpTrigger(AuthorizationLevel.Function,  "post",
            Route = "insurancepolicy/saveinsurancepolicy")] HttpRequest req)
        {
            _logger.LogInformation("InsurancePolicyFunctions.SaveInsurancePolicy Invoked.");

            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("Valid insurance policy object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("Valid insurance policy object NOT provided");

                var insurancePolicies = JsonConvert.DeserializeObject<InsurancePolicy>(requestBody);

                if (insurancePolicies == null)
                    return new BadRequestObjectResult("Valid insurance policy object NOT provided");

                await _insurancePolicyDataManager.InsertOrUpdateInsurancePolicyAsync(insurancePolicies);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading insurance policies: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
