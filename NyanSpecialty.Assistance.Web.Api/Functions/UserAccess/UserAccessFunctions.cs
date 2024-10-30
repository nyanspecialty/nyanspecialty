using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class UserAccessFunctions
    {
        private IAuthenticationDataManager _authenticationDataManager;
        private ILogger<UserAccessFunctions> _logger;
        public UserAccessFunctions(IAuthenticationDataManager authenticationDataManager,
            ILogger<UserAccessFunctions> logger)
        {
            _authenticationDataManager = authenticationDataManager;
            _logger = logger;
        }
    }
}
