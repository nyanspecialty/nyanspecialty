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

        [Function("GenarateUserClaims")]
        public async Task<IActionResult> GenarateUserClaims([HttpTrigger(AuthorizationLevel.Function, "post", Route = "useraccess/genarateuserclaims")] HttpRequest req)
        {
            _logger.LogInformation("UserAccessFunctions.GenarateUserClaims Invoked");

            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid  object NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult("valid  object NOT provided");

                var user = JsonConvert.DeserializeObject<AuthResponse>(requestBody);

                if (user == null)
                    return new BadRequestObjectResult("valid  object NOT provided");

                var response = await _authenticationDataManager.GenarateUserClaimsAsync(user);

                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error genarating user claimes details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
