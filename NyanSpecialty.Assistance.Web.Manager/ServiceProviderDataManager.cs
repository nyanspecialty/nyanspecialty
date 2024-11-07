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

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class ServiceProviderDataManager : IServiceProviderDataManager
    {
        private readonly ApplicationDBContext dbContext;
        public ServiceProviderDataManager(ApplicationDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<ServiceProvider> GetServiceProviderAsync(string searchInput)
        {
            return await dbContext.serviceProviders.Where
            (x => x.Name.ToLower().Trim() == searchInput.ToLower().Trim()
            || (x.ProviderId.ToString() == searchInput)
            || (x.ContactNumber.ToString() == searchInput)
            || (x.Email.ToLower().Trim()) == searchInput.ToLower().Trim()
            || (x.ContactNumber.ToLower().Trim()) == searchInput.ToLower().Trim())
            .FirstOrDefaultAsync();
        }

        public async Task<List<ServiceProvider>> GetServiceProvidersAsync()
        {
            return await dbContext.serviceProviders.ToListAsync();
        }

        public async Task<ServiceProvider> InsertOrUpdateServiceProviderAsync(ServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                if (serviceProvider.ProviderId == 0) //create new
                {
                    await dbContext.serviceProviders.AddAsync(serviceProvider);
                    await dbContext.SaveChangesAsync();
                    return serviceProvider;
                }
                else // update if  exist
                {
                    var existProvide = await dbContext.serviceProviders.FindAsync(serviceProvider.ProviderId);
                    if (existProvide != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existProvide,serviceProvider,
                            nameof(serviceProvider.CreatedBy), nameof(serviceProvider.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existProvide, serviceProvider, 
                                 nameof(serviceProvider.CreatedBy), nameof(serviceProvider.CreatedOn));
                            await dbContext.SaveChangesAsync();
                            return serviceProvider;
                        }
                    }

                }

            }
            return null;
        }
    }
}
