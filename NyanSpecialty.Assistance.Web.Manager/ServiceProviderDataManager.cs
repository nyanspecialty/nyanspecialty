using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

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

        public async Task<List<ServiceProviderWorkFlowDetails>> GetServiceProviderWorkFlowDetailsAsync()
        {
            var serviceProviders = await dbContext.serviceProviders.ToListAsync();

            var serviceProvidersWorkFlows = await dbContext.serviceProviderWorkFlows.ToListAsync();

            List<ServiceProviderWorkFlowDetails> serviceProviderDetails = new List<ServiceProviderWorkFlowDetails>();

            if (serviceProviders != null && serviceProviders.Any())
            {
                foreach (var item in serviceProviders)
                {
                    if (serviceProvidersWorkFlows != null && serviceProvidersWorkFlows.Any())
                    {
                        var workFlow = serviceProvidersWorkFlows.Where(x => x.ServiceProviderId == item.ProviderId).FirstOrDefault();
                        if (workFlow != null)
                        {
                            serviceProviderDetails.Add(new ServiceProviderWorkFlowDetails
                            {
                                serviceProvider = item,
                                serviceProviderWorkFlow = workFlow
                            });
                        }
                    }
                }
            }
            return serviceProviderDetails;
        }

        public async Task<ServiceProviderWorkFlowDetails> GetServiceProviderWorkFlowDetailsAsync(long providerId)
        {
            var serviceProviders = await dbContext.serviceProviders.Where(sp => sp.ProviderId == providerId).FirstOrDefaultAsync();

            if (serviceProviders == null)
            {
                return null;
            }

            var serviceProviderWorkFlow = await dbContext.serviceProviderWorkFlows.Where(wf => wf.ServiceProviderId == providerId).FirstOrDefaultAsync();

            return new ServiceProviderWorkFlowDetails
            {
                serviceProvider = serviceProviders,
                serviceProviderWorkFlow = serviceProviderWorkFlow
            };
        }

        public async Task<ServiceProviderAssignment> InsertOrUpdateServiceProviderAssignmentAsync(ServiceProviderAssignment serviceProviderAssignment)
        {
            if (serviceProviderAssignment != null)
            {
                if (serviceProviderAssignment.AssignmentId == 0) 
                {
                    await dbContext.serviceProviderAssignments.AddAsync(serviceProviderAssignment);
                    await dbContext.SaveChangesAsync();
                    return serviceProviderAssignment;
                }
                else 
                {
                    var existingAssignment = await dbContext.serviceProviderAssignments.FindAsync(serviceProviderAssignment.AssignmentId);
                    if (existingAssignment != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existingAssignment, serviceProviderAssignment,
                            nameof(serviceProviderAssignment.CreatedBy), nameof(serviceProviderAssignment.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existingAssignment, serviceProviderAssignment,
                                nameof(serviceProviderAssignment.CreatedBy), nameof(serviceProviderAssignment.CreatedOn));
                            await dbContext.SaveChangesAsync();
                            return existingAssignment;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<ServiceProvider> InsertOrUpdateServiceProviderAsync(ServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                if (serviceProvider.ProviderId == 0) 
                {
                    await dbContext.serviceProviders.AddAsync(serviceProvider);
                    await dbContext.SaveChangesAsync();
                    return serviceProvider;
                }
                else 
                {
                    var existProvide = await dbContext.serviceProviders.FindAsync(serviceProvider.ProviderId);
                    if (existProvide != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existProvide, serviceProvider,
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
