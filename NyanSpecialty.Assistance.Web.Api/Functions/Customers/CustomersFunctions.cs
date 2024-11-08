using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class CustomersFunctions
    {
        private readonly ILogger<CustomersFunctions> _logger;
        private ICustomersDataManager _customersDataManager;
        public CustomersFunctions(ILogger<CustomersFunctions> logger, ICustomersDataManager customersDataManager)
        {
            _logger = logger;
            _customersDataManager = customersDataManager;
        }

    }
}
