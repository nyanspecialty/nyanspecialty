using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class PolicyTypeDataManager : IPolicyTypeDataManager

    {
        private readonly ApplicationDBContext dbContext;
        public PolicyTypeDataManager(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<PolicyType>> GetAllPolicyTypeAsync()
        {
            return await dbContext.policyTypes.ToListAsync();
        }


        public async Task<PolicyType> GetPolicyTypeByIdAsync(long policyTypeId)
        {
            return await dbContext.policyTypes.Where(x => x.PolicyTypeId == policyTypeId).FirstOrDefaultAsync();
        }

        public async Task<PolicyType> InsertOrUpdatePolicyTypeAsync(PolicyType policyType)
        {
            if (policyType != null)
            {
                if (policyType.PolicyTypeId == 0)
                {
                    await dbContext.policyTypes.AddAsync(policyType);
                    await dbContext.SaveChangesAsync();
                    return policyType;
                }
                else
                {
                    var existPolicyType = await dbContext.policyTypes.FindAsync(policyType.PolicyTypeId);
                    if (existPolicyType != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existPolicyType, policyType, nameof(policyType.CreatedBy), nameof(policyType.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existPolicyType, policyType, nameof(policyType.CreatedBy), nameof(policyType.CreatedOn));
                            await dbContext.SaveChangesAsync();
                            return existPolicyType;
                        }
                    }

                }
                
            }
            return null;
        }
    }
}
