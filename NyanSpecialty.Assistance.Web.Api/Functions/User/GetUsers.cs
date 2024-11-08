using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class UserFunctions
    {
      
        [Function("GetUsers")]
        public async Task<IActionResult> GetUsers([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/getusers")] HttpRequest req)
        {
            _logger.LogInformation("UserFunctions.GetUsers Invoked");
            try
            {
                var data = await _userDataManager.FetchUsersAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving users : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
