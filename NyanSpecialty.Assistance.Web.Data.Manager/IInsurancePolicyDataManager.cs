using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IInsurancePolicyDataManager
    {
        Task<InsurancePolicy> InsertOrUpdateInsurancePolicyAsync(InsurancePolicy insurancePolicy);

        Task InsertOrUpdateInsurancePolicyAsync(List<InsurancePolicy> insurancePolicy);

        // Retrieves all insurance policies
        Task<List<InsurancePolicy>> GetAllInsurancePoliciesAsync();

        // Retrieves an insurance policy by its ID
        Task<InsurancePolicy> GetInsurancePolicyByIdAsync(long insurancePolicyId);

        // Deletes an insurance policy by its ID
        Task<bool> DeleteInsurancePolicyAsync(long insurancePolicyId);


    }
}
