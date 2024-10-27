using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyCategoryFunction
    {
       
        [Function("GetPolicyCategories")]
        public async Task<IActionResult> GetPolicyCategories([HttpTrigger(AuthorizationLevel.Function, "get", Route ="policycategory/getpolicycategories")] HttpRequest req)
        {
            _logger.LogInformation("PolicyCategoryFunction.GetPolicyCategoriescs Invoked.");
            try
            {
                var data = await _policyCategoryDataManager.GetAllPolicyCategoryAsync();
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
