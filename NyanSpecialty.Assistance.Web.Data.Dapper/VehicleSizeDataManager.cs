using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Dapper
{
    public class VehicleSizeDataManager : IVehicleSizeDataManager
    {
        private readonly ApplicationDBContext dbContext;
        public VehicleSizeDataManager(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<VehicleSize>> GetAllVehicleSizeAsync()
        {
            return await dbContext.vehicleSizes.ToListAsync();
        }

        public async Task<VehicleSize> GetVehicleSizeAsync(long vehicleSizeId)
        {
            return await dbContext.vehicleSizes.Where(x => x.VehicleSizeId == vehicleSizeId).FirstOrDefaultAsync();
        }

        public async Task<VehicleSize> InsertOrUpdateVehicleSizeAsync(VehicleSize vehicleSize)
        {
            if (vehicleSize != null)
            {
                if (vehicleSize.VehicleSizeId == 0)
                {
                    await dbContext.vehicleSizes.AddAsync(vehicleSize);
                    await dbContext.SaveChangesAsync();
                    return vehicleSize;
                }
                else
                {
                    // Fetch the existing VehicleSize from the repository
                    var existingVehicleSize =  await dbContext.vehicleSizes.FindAsync(vehicleSize.VehicleSizeId);
                    if (existingVehicleSize != null)
                    {
                        // Check for changes
                        bool hasChanges = EntityUpdater.HasChanges(existingVehicleSize, vehicleSize, nameof(VehicleSize.CreatedBy), nameof(VehicleSize.CreatedOn));

                        if (hasChanges)
                        {
                            // Update properties (excluding CreatedBy and CreatedOn)
                            EntityUpdater.UpdateProperties(existingVehicleSize, vehicleSize, nameof(VehicleSize.CreatedBy), nameof(VehicleSize.CreatedOn));

                            // Update the VehicleSize in the repository
                            await dbContext.SaveChangesAsync();
                            return existingVehicleSize;

                        }
                    }
                }
            }
            return null;
        }
    }
}
