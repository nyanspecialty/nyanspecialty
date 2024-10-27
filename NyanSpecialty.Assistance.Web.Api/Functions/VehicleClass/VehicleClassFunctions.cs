using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;


namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleClassFunctions
    {
        private IVehicleClassDataManager _vehicleClassDataManager;
        private readonly ILogger<VehicleClassFunctions> _logger;

        public VehicleClassFunctions(IVehicleClassDataManager vehicleClassDataManager, ILogger<VehicleClassFunctions> logger)
        {
            _vehicleClassDataManager = vehicleClassDataManager;
            _logger = logger;
        }

    }
}
