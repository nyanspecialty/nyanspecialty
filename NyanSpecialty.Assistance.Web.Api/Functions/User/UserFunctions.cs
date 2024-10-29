using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class UserFunctions
    {
        private IUserDataManager _userDataManager;  
        private ILogger<UserFunctions> _logger;
        public UserFunctions(IUserDataManager _userDataManager, ILogger<UserFunctions> logger)
        {
            this._userDataManager = _userDataManager;
            this._logger = logger;  
        }
    }
}
