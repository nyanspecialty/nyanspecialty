using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class FaultTypeFunction
    {
        private readonly ILogger<FaultTypeFunction> _logger;

        private readonly IFaultTypeDataManager _faultTypeDataManager;

        public FaultTypeFunction(ILogger<FaultTypeFunction> logger, IFaultTypeDataManager faultTypeDataManager)
        {
            _logger = logger;
            _faultTypeDataManager = faultTypeDataManager;
        }

    }
}
