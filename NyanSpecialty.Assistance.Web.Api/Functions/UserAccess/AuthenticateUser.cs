using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class UserAccessFunctions
    {
       
        [Function("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([HttpTrigger(AuthorizationLevel.Function, "post",Route = "useraccess/authenticateuser")] HttpRequest req)
        {
            _logger.LogInformation("UserAccessFunctions.AuthenticateUser Invoked");

            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid user object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("valid user object NOT provided");

                var user = JsonConvert.DeserializeObject<UserAuthentication>(requestBody);

                if (user == null)
                    return new BadRequestObjectResult("valid policytype object NOT provided");

                var response = await _authenticationDataManager.AuthenticateUserAsync(user);

                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error authenticating user details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
