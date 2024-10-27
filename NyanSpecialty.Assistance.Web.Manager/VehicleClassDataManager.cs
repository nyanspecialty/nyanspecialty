using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class VehicleClassDataManager : IVehicleClassDataManager
    {
        private readonly ApplicationDBContext dbContext;
        public VehicleClassDataManager(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<VehicleClass>> GetAllVehicleClassesAsync()
        {
            return await dbContext.vehicleClasses.ToListAsync();
        }

        public async Task<VehicleClass> GetVehicleClassAsync(long vehicleClassId)
        {
            return await dbContext.vehicleClasses.Where(x => x.VehicleClassId == vehicleClassId).FirstOrDefaultAsync();
        }

        public async Task<VehicleClass> InsertOrUpdateVehicleClassAsync(VehicleClass vehicleClass)
        {
            if (vehicleClass != null)
            {
                if (vehicleClass.VehicleClassId == 0)
                {
                    await dbContext.vehicleClasses.AddAsync(vehicleClass);
                    await dbContext.SaveChangesAsync();
                    return vehicleClass;
                }
                else
                {
                    // Fetch the existing vehicleClass from the repository
                    var existingvehicleClass = await dbContext.vehicleClasses.FindAsync(vehicleClass.VehicleClassId);
                    if (existingvehicleClass != null)
                    {
                        // Check for changes
                        bool hasChanges = EntityUpdater.HasChanges(existingvehicleClass, vehicleClass, nameof(vehicleClass.CreatedBy), nameof(vehicleClass.CreatedOn));

                        if (hasChanges)
                        {
                            // Update properties (excluding CreatedBy and CreatedOn)
                            EntityUpdater.UpdateProperties(existingvehicleClass, vehicleClass, nameof(vehicleClass.CreatedBy), nameof(vehicleClass.CreatedOn));

                            // Update the vehicleClass in the repository
                            await dbContext.SaveChangesAsync();
                            return existingvehicleClass;

                        }
                    }
                }
            }
            return null;
        }
    }
}
