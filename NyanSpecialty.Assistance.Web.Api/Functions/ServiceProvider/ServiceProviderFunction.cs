using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class ServiceProviderFunction
    {
        private readonly ILogger<ServiceProviderFunction> _logger;
        private IServiceProviderDataManager _serviceProviderDataManager;

        public ServiceProviderFunction(ILogger<ServiceProviderFunction> logger, IServiceProviderDataManager serviceProviderDataManager)
        {
            _logger = logger;
            _serviceProviderDataManager = serviceProviderDataManager;
        }        
    }
}
