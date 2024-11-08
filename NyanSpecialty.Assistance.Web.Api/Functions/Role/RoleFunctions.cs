using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class RoleFunctions
    {
        private IRoleDataManagaer _roleDataManagaer;
        private ILogger<RoleFunctions> _logger;
        public RoleFunctions(IRoleDataManagaer roleDataManagaer,
            ILogger<RoleFunctions> logger)
        {
            _roleDataManagaer = roleDataManagaer;
            _logger = logger;
        }
    }
}
