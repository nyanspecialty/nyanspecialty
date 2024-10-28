using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class CustomersFunctions
    {
       
        [Function("GetCustomersById")]
        public async Task<IActionResult> GetCustomersById([HttpTrigger(AuthorizationLevel.Function, "get", Route ="customers/getcustomersbyid")] HttpRequest req, long customerId)
        {
            _logger.LogInformation("CustomersFunctions.GetCustomersById Invoked");
            try
            {
                var data = await _customersDataManager.GetCustomersByIdAsync(customerId);
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError(" error while retrieving customer by Id");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);  
            }
        }
    }
}
