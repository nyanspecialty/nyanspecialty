using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class RoleFunctions
    {
        [Function("SaveRole")]
        public async Task<IActionResult> SaveRole([HttpTrigger(AuthorizationLevel.Function, "post",Route ="role/saverole")] HttpRequest req)
        {
            _logger.LogInformation("RoleFunctions.SaveRole Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid role object NOT provided");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult(" valid role object not provided");

                var role = JsonConvert.DeserializeObject<Role>(requestBody);
                if (role == null)
                    return new BadRequestObjectResult("valid role object not provided");

                var response = await _roleDataManagaer.InsertOrUpdateRoleAsync(role);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error save role : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
