using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class WorkFlowFunction
    {
        private readonly ILogger<WorkFlowFunction> _logger;
        private readonly IWorkFlowDataManager _workFlowDataManager;

        public WorkFlowFunction(ILogger<WorkFlowFunction> logger, IWorkFlowDataManager workFlowDataManager)
        {
            _logger = logger;
            _workFlowDataManager = workFlowDataManager;
        }
    }
}
