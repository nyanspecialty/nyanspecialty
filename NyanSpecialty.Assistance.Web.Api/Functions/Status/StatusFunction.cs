using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class StatusFunction
    {
        private readonly ILogger<StatusFunction> _logger;
        private readonly IStatusDataManager _statusDataManager; 

        public StatusFunction(ILogger<StatusFunction> logger, IStatusDataManager statusDataManager)
        {
            _logger = logger;
            _statusDataManager = statusDataManager;
        }

    }
}
