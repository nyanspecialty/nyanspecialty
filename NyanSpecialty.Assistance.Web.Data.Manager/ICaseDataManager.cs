using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface ICaseDataManager
    {
        Task<Case> InsertOrUpdateCaseAsync(Case customerCase);
       
    }
}
