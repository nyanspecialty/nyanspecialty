using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyCategoryFunction
    {       
        [Function("GetPolicyCategoryById")]
        public async Task <IActionResult> GetPolicyCategoryById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "PolicyCategory/getpolicycategoriesbyid")] HttpRequest req, long policyCategoryId)
        {
            _logger.LogInformation("PolicyCategoryFunction.GetPolicyCategoryById invoked");
            try
            {
                var data = await _policyCategoryDataManager.GetPolicyCategoryByIdAsync(policyCategoryId);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving policy categories :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
