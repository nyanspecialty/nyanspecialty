using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class InsurancePolicyFunctions
    {
        private IInsurancePolicyDataManager _insurancePolicyDataManager;
        private ILogger<InsurancePolicyFunctions> _logger;
        public InsurancePolicyFunctions(IInsurancePolicyDataManager insurancePolicyDataManager,
            ILogger<InsurancePolicyFunctions> logger)
        {
            _insurancePolicyDataManager = insurancePolicyDataManager;
            _logger = logger;
        }
    }
}
