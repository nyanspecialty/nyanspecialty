using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class UserFunctions
    {
        [Function("SaveUser")]
        public async Task<IActionResult> SaveUser([HttpTrigger(AuthorizationLevel.Function, "post", Route ="user/saveuser")] HttpRequest req)
        {
            _logger.LogInformation("UserFunctions.SaveUser Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid user object NOT provided");
                string requestBody= await new StreamReader(req.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("valid user object NOT provided");
                var user = JsonConvert.DeserializeObject<UserRegistration>(requestBody);
                if (user == null)
                    return new BadRequestObjectResult("valid policytype object NOT provided");

                var response = await _userDataManager.InsertOrUpdateUserAsync(user);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving user details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
