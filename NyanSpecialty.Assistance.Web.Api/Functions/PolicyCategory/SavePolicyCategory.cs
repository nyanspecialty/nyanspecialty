using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyCategoryFunction
    {      

        [Function("SavePolicyCategory")]
        public async Task<IActionResult> SavePolicyCategory([HttpTrigger(AuthorizationLevel.Function, "post", Route ="policycategory/savepolicycategory")] HttpRequest req)
        {
            _logger.LogInformation("PolicyCategoryFunction.SavePolicyCategory Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid policy  category object NOT provided");
                string requestBody=  await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new  BadRequestObjectResult(" valid policy category object not provided");

                var policyCategory =  JsonConvert.DeserializeObject<PolicyCategory>(requestBody);
                if (policyCategory == null)
                    return new BadRequestObjectResult("valid policy category object not provided");

                var response = await _policyCategoryDataManager.InsertOrUpdatePolicyCategoryAsync(policyCategory);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error savingpolicy category : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
