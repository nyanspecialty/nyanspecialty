using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyTypeFunction
    {
        private IPolicyTypeDataManager _policyTypeDataManager;
        private readonly ILogger<PolicyTypeFunction> _logger;

        public PolicyTypeFunction(ILogger<PolicyTypeFunction> logger, IPolicyTypeDataManager policyTypeDataManager)
        {
            _logger = logger;
            _policyTypeDataManager = policyTypeDataManager; 
        }

       
    }
}
