using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class CaseDataManager : ICaseDataManager
    {
        private readonly ApplicationDBContext _dbcontext;
        public CaseDataManager(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<CaseDetails> CaseStatusProcessAsync(CaseStatusProcess caseStatus)
        {
            var caseDetails = await _dbcontext.cases.FindAsync(caseStatus.CaseId);

            if (caseDetails != null)
            {
                caseDetails.StatusId = caseStatus.StatusId;
                caseDetails.Notes += "||" + caseStatus.Notes;
                caseDetails.ModifiedBy = caseStatus.ModifiedBy;
                caseDetails.ModifiedOn = caseStatus.ModifiedOn;

                var caseStatusUpdate = new CaseStatus
                {
                    CaseId = caseStatus.CaseId,
                    Notes = caseStatus.Notes,
                    StatusId = caseStatus.StatusId,
                    CreatedBy = caseStatus.CreatedBy,
                    CreatedOn = caseStatus.CreatedOn,
                    IsActive = caseStatus.IsActive,
                    ModifiedBy = caseStatus.ModifiedBy,
                    ModifiedOn = caseStatus.ModifiedOn
                };

                await _dbcontext.caseStatuses.AddAsync(caseStatusUpdate);

                if (caseStatus.AssingTo.HasValue && caseStatus.AssingTo.Value > 0)
                {
                    var serviceProvider = new ServiceProviderAssignment
                    {
                        CaseId = caseStatus.CaseId,
                        ServiceProviderId = caseStatus.AssingTo.Value,
                        Response = string.Empty,
                        AssignedOn = caseStatus.CreatedOn,
                        CreatedBy = caseStatus.CreatedBy,
                        CreatedOn = caseStatus.CreatedOn,
                        IsActive = caseStatus.IsActive,
                        ModifiedBy = caseStatus.ModifiedBy,
                        ModifiedOn = caseStatus.ModifiedOn
                    };

                    await _dbcontext.serviceProviderAssignments.AddAsync(serviceProvider);
                }

                await _dbcontext.SaveChangesAsync();
            }

            return await FetchCaseDetailsAsync(caseStatus.CaseId);
        }

        public async Task<List<CaseDetails>> GetCaseDetailAsync()
        {
            var customerCases = await _dbcontext.cases.ToListAsync();

            var caseStatuses = await _dbcontext.caseStatuses.ToListAsync();

            var serviceProviderAssignments = await _dbcontext.serviceProviderAssignments.ToListAsync();

            var caseDetails = customerCases.Select(item => new CaseDetails
            {
                caseDetails = item,
                caseStatuses = caseStatuses.Where(x => x.CaseId == item.CaseId).ToList(),
                serviceProviderAssignment = serviceProviderAssignments.Where(x => x.CaseId == item.CaseId).ToList()
            }).ToList();

            return caseDetails;
        }

        public async Task<CaseDetails> GetCaseDetailsByCaseIdAsync(long caseId)
        {
            return await FetchCaseDetailsAsync(caseId);
        }

        public async Task<CaseDetails> InsertOrUpdateCaseAsync(Case customerCase)
        {
            if (customerCase == null)
            {
                return null;
            }

            if (customerCase.CaseId == 0)
            {
                await _dbcontext.cases.AddAsync(customerCase);
                await _dbcontext.SaveChangesAsync();

                var caseStatus = new CaseStatus
                {
                    CaseId = customerCase.CaseId,
                    CreatedBy = customerCase.CreatedBy,
                    CreatedOn = customerCase.CreatedOn,
                    IsActive = customerCase.IsActive,
                    ModifiedBy = customerCase.ModifiedBy,
                    ModifiedOn = customerCase.ModifiedOn,
                    Notes = "Case created",
                    StatusId = customerCase.StatusId
                };

                await _dbcontext.caseStatuses.AddAsync(caseStatus);
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                var existingCase = await _dbcontext.cases.FindAsync(customerCase.CaseId);
                if (existingCase != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingCase, customerCase, nameof(Case.CreatedBy), nameof(Case.CreatedOn));
                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingCase, customerCase, nameof(Case.CreatedBy), nameof(Case.CreatedOn));
                        await _dbcontext.SaveChangesAsync();
                    }
                }
            }

            return await FetchCaseDetailsAsync(customerCase.CaseId);
        }

        private async Task<CaseDetails> FetchCaseDetailsAsync(long caseId)
        {
            var customerCase = await _dbcontext.cases.Where(x => x.CaseId == caseId).FirstOrDefaultAsync();

            var caseDetails = new CaseDetails
            {
                caseDetails = customerCase,
                caseStatuses = await _dbcontext.caseStatuses.Where(x => x.CaseId == customerCase.CaseId).ToListAsync(),
                serviceProviderAssignment = await _dbcontext.serviceProviderAssignments.Where(x => x.CaseId == customerCase.CaseId).ToListAsync()
            };

            return caseDetails;
        }
    }
}
