using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyTypeFunction
    {
        

        [Function("SavePolicyType")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route ="policttype/savepolicyType")] HttpRequest req)
        {
            _logger.LogInformation("PolicyTypeFunction.SavePolicyType Invoked.");

            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid policytype object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("alid policytype object NOT provided");

                var policyType = JsonConvert.DeserializeObject<PolicyType>(requestBody);

                if (policyType == null)
                    return new BadRequestObjectResult("valid policytype object NOT provided");

                var response = await _policyTypeDataManager.InsertOrUpdatePolicyTypeAsync(policyType);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving policyType : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
