using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class FaultTypeFunction
    {
       
        [Function("GetFaultTypeById")]
        public async Task<IActionResult> GetFaultTypeById([HttpTrigger(AuthorizationLevel.Function, "get", Route ="faulttype/getfaulttypebyid")] HttpRequest req, long faultTypeId)
        {
            _logger.LogInformation("FaultTypeFunction.GetFaultTypeById Inovked");
            try
            {
                var data = await _faultTypeDataManager.GetFaultTypeByID(faultTypeId);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error retrieving faulttype :{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
