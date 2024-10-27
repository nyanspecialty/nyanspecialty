using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleSizeFunctions
    {
        private IVehicleSizeDataManager _vehicleSizeDataManager;
        private readonly ILogger<VehicleSizeFunctions> _logger;
        public VehicleSizeFunctions(IVehicleSizeDataManager vehicleSizeDataManager, ILogger<VehicleSizeFunctions> logger)
        {
            _vehicleSizeDataManager = vehicleSizeDataManager;
            _logger = logger;
        }
    }
}
