using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class CustomersFunctions    {
       

        [Function("SaveCustomers")]
        public async Task<IActionResult> SaveCustomers([HttpTrigger(AuthorizationLevel.Function, "post", Route ="customres/savecustomers")] HttpRequest req)
        {
            _logger.LogInformation("SaveCustomers.SaveCustomers Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid customer details NOT provided");
                string requestBody= await new StreamReader(req.Body).ReadToEndAsync();  

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult($"{nameof(SaveCustomers)}");
                var customers= JsonConvert.DeserializeObject<Customers>(requestBody);
                if (customers == null)
                    return new BadRequestObjectResult("valid customer details NOT provided");
                await _customersDataManager.InsertOrUpdateCustomersAsync(customers);
                return new OkObjectResult(customers);   
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading customer details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
