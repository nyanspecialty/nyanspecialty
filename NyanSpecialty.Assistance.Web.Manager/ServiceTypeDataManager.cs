using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class ServiceTypeDataManager : IServiceTypeDataManager
    {
        private readonly ApplicationDBContext dbContext;
        public ServiceTypeDataManager(ApplicationDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        } 
        public async Task<ServiceType> AddEditServiceTypeAsync(ServiceType serviceType)
        {
            if (serviceType != null)
            {
                if (serviceType.ServiceTypeId == 0)
                {
                    await dbContext.serviceTypes.AddAsync(serviceType);
                    await dbContext.SaveChangesAsync();
                    return serviceType;
                }
                else
                {
                    var existserviceType = await dbContext.serviceTypes.FindAsync(serviceType.ServiceTypeId);
                    if (existserviceType != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existserviceType, serviceType, nameof(serviceType.CreatedBy), nameof(serviceType.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existserviceType, serviceType, nameof(serviceType.CreatedBy), nameof(serviceType.CreatedOn));
                            await dbContext.SaveChangesAsync();
                            return existserviceType;
                        }
                    }

                }

            }
            return null;
        }

        public async Task<ServiceType> GetServiceTypeAsync(long serviceTypeId)
        {
            return await dbContext.serviceTypes.Where(x => x.ServiceTypeId == serviceTypeId).FirstOrDefaultAsync();
        }

        public async Task<List<ServiceType>> GetServiceTypesAsync()
        {
            return await dbContext.serviceTypes.ToListAsync();
        }
    }      
    
}
