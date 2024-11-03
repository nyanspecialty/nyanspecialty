using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceTypeFunction
    {
        private readonly ILogger<ServiceTypeFunction> _logger;
        private readonly IServiceTypeDataManager _serviceTypeDataManager;

        public ServiceTypeFunction(ILogger<ServiceTypeFunction> logger, IServiceTypeDataManager serviceTypeDataManager)
        {
            _logger = logger;
            _serviceTypeDataManager = serviceTypeDataManager;

        }

    }
}
