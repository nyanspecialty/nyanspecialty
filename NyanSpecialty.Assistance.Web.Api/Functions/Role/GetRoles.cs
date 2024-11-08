using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class RoleFunctions
    {
       
        [Function("GetRoles")]
        public async  Task<IActionResult> GetRoles([HttpTrigger(AuthorizationLevel.Function, "get", Route = "role/getroles")] HttpRequest req)
        {
            _logger.LogInformation("RoleFunctions.GetRoles Invoked.");
            try
            {
                var data = await _roleDataManagaer.GetRolesAsync();
                return new OkObjectResult(data);

            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving roles :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
