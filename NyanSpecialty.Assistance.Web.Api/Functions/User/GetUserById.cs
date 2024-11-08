using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class UserFunctions
    {
       
        [Function("GetUserById")]
        public async Task<IActionResult> GetUserById([HttpTrigger(AuthorizationLevel.Function, "get",Route = "user/getuserbyid/{id}")] HttpRequest req,long id)
        {
            _logger.LogInformation("UserFunctions.GetUserById Invoked");
            try
            {
                var data = await _userDataManager.FetchUserByIdAsync(id);
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
