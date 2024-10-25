using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NyanSpecialty.Assistance.Web.Data.Manager;

namespace NyanSpecialty.Assistance.Web.Api.Functions
{
    public partial class VehicleSizeFunctions
    {
        private IVehicleSizeDataManager _vehicleSizeDataManager;
        public VehicleSizeFunctions(IVehicleSizeDataManager vehicleSizeDataManager)
        {
            _vehicleSizeDataManager = vehicleSizeDataManager;
        }
    }
}
