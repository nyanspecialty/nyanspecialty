using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class CustomersFunctions
    {
        [Function("GetCustomers")]
        public async Task<IActionResult> GetCustomers([HttpTrigger(AuthorizationLevel.Function, "get", Route ="customers/getcustomers")] HttpRequest req)
        {
            _logger.LogInformation("CustomersFunctions.GetCustomers Invoked");
            try
            {
                var data= await _customersDataManager.GetCustomersAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($" errror retrieving customers:  {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
