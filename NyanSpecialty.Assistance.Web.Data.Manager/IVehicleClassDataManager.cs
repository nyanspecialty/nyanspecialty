using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IVehicleClassDataManager
    {
        Task<VehicleClass> InsertOrUpdateVehicleClassAsync(VehicleClass vehicleClass);
        Task<List<VehicleClass>> GetAllVehicleClassesAsync();
        Task<VehicleClass> GetVehicleClassAsync(long vehicleClassId);
    }
}
