using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IPolicyCategoryDataManager
    {
        Task<PolicyCategory> InsertOrUpdatePolicyCategoryAsync(PolicyCategory policyCategory);
        Task<List<PolicyCategory>> GetAllPolicyCategoryAsync();
        Task<PolicyCategory> GetPolicyCategoryByIdAsync(long PolicyCategoryId);
    }
}
