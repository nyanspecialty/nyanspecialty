using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class PolicyCategoryFunction
    {
        private IPolicyCategoryDataManager _policyCategoryDataManager;
        private readonly ILogger<PolicyCategoryFunction> _logger;

        public PolicyCategoryFunction(ILogger<PolicyCategoryFunction> logger, IPolicyCategoryDataManager policyCategoryDataManager)
        {
            _logger = logger;
            _policyCategoryDataManager = policyCategoryDataManager;
        }

    }
}
