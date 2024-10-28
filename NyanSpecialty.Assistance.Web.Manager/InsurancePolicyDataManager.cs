using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class InsurancePolicyDataManager : IInsurancePolicyDataManager
    {
        private readonly ApplicationDBContext dbContext;

        public InsurancePolicyDataManager(ApplicationDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<InsurancePolicy> GetInsurancePolicyByIdAsync(long insurancePolicyId)
        {
            return await dbContext.insurancePolicies.Where(x => x.InsurancePolicyId == insurancePolicyId).FirstOrDefaultAsync();
        }

        public async Task<InsurancePolicy> InsertOrUpdateInsurancePolicyAsync(InsurancePolicy insurancePolicy)
        {
            if (insurancePolicy != null)
            {
                if (insurancePolicy.InsurancePolicyId == 0) // Insert
                {
                    await dbContext.insurancePolicies.AddAsync(insurancePolicy);
                    await dbContext.SaveChangesAsync();
                    return insurancePolicy;
                }
                else // Update
                {
                    var existingPolicy = await dbContext.insurancePolicies.FindAsync(insurancePolicy.InsurancePolicyId);
                    if (existingPolicy != null)
                    {
                        // Check if there are changes to update
                        bool hasChanges = EntityUpdater.HasChanges(insurancePolicy, existingPolicy,
                            nameof(insurancePolicy.CreatedBy), nameof(insurancePolicy.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(insurancePolicy, existingPolicy,
                                nameof(insurancePolicy.CreatedBy), nameof(insurancePolicy.CreatedOn));
                            await dbContext.SaveChangesAsync();
                            return insurancePolicy;
                        }
                    }
                }
            }
            return null; // Return null if no action was taken
        }

        public async Task InsertOrUpdateInsurancePolicyAsync(List<InsurancePolicy> insurancePolicies)
        {
            if (insurancePolicies != null && insurancePolicies.Any())
            {
                insurancePolicies.ForEach(policy =>
                {
                    policy.CustomerName = RandomNameGeneratorUtility.GenerateRandomName();
                    policy.CustomerPhone = RandomPhoneNumberGenerator.GenerateRandomPhoneNumber();
                    policy.CustomerEmail = RandomEmaiGenerator.GenerateRandomEmailNumber(policy.CustomerName);
                    policy.CreatedBy = -1;
                    policy.CreatedOn = DateTimeOffset.Now;
                    policy.ModifiedBy = -1;
                    policy.ModifiedOn = DateTimeOffset.Now;
                    policy.IsActive = true;
                });
                await dbContext.BulkInsertAsync(insurancePolicies);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteInsurancePolicyAsync(long insurancePolicyId)
        {
            var policyToDelete = await dbContext.insurancePolicies.FindAsync(insurancePolicyId);
            if (policyToDelete != null)
            {
                dbContext.insurancePolicies.Remove(policyToDelete);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false; // Return false if the policy was not found
        }

        public async Task<List<InsurancePolicy>> GetAllInsurancePoliciesAsync()
        {
            return await dbContext.insurancePolicies.ToListAsync();
        }
    }
}
