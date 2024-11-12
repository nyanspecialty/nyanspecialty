using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface ICaseDataManager
    {
        Task<CaseDetails> InsertOrUpdateCaseAsync(Case customerCase);

        Task<CaseDetails> CaseStatusProcessAsync(CaseStatusProcess caseStatus);

        Task<List<CaseDetails>> GetCaseDetailAsync();

        Task<CaseDetails> GetCaseDetailsByCaseIdAsync(long caseId);
    }
}
