
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyCaseFunction
    {
        private ILogger<InsurancePolicyCaseFunction> _logger;
        private ICaseDataManager _caseDataManager;
        public InsurancePolicyCaseFunction(ICaseDataManager caseDataManager,
            ILogger<InsurancePolicyCaseFunction> logger)
        {
            _caseDataManager = caseDataManager;
            _logger = logger;
        }
    }
}
