using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IPolicyTypeDataManager
    {
        Task<PolicyType> InsertOrUpdatePolicyTypeAsync(PolicyType policyType);
        Task<List<PolicyType>> GetAllPolicyTypeAsync();
        Task<PolicyType> GetPolicyTypeByIdAsync(long policyTypeId);
    }
}
