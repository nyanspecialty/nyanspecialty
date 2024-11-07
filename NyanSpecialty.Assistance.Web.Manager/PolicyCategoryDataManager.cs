using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class PolicyCategoryDataManager : IPolicyCategoryDataManager
    {
        private readonly ApplicationDBContext dbContext;
        public PolicyCategoryDataManager(ApplicationDBContext _dbContext)
        {
            this.dbContext = _dbContext;

        }
        public async Task<List<PolicyCategory>> GetAllPolicyCategoryAsync()
        {
            return await dbContext.policyCategories.ToListAsync();
        }

        public async Task<PolicyCategory> GetPolicyCategoryByIdAsync(long policyCategoryId)
        {
            return await dbContext.policyCategories.Where(x => x.PolicyCategoryId == policyCategoryId).FirstOrDefaultAsync();
        }

        public async Task<PolicyCategory> InsertOrUpdatePolicyCategoryAsync(PolicyCategory policyCategory)
        {
            if (policyCategory != null)
            {
                if (policyCategory.PolicyCategoryId == 0)
                {
                    await dbContext.policyCategories.AddAsync(policyCategory);
                    await dbContext.SaveChangesAsync();
                    return policyCategory;
                }
                else
                {
                    var existingPolicyCategory = await dbContext.policyCategories.FindAsync(policyCategory.PolicyCategoryId);
                    if (existingPolicyCategory != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existingPolicyCategory,policyCategory, nameof(policyCategory.CreatedBy), nameof(policyCategory.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existingPolicyCategory,policyCategory, nameof(policyCategory.CreatedBy), nameof(policyCategory.CreatedOn));
                            await dbContext.SaveChangesAsync(); 
                            return policyCategory;

                        }
                    }
                }
                
            }
            return null;
        }
    }
}
