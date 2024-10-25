using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IVehicleSizeDataManager
    {
        Task<VehicleSize> InsertOrUpdateVehicleSizeAsync(VehicleSize vehicleSize);
        Task<List<VehicleSize>> GetAllVehicleSizeAsync();
        Task<VehicleSize> GetVehicleSizeAsync(long vehicleSizeId);
    }
}
