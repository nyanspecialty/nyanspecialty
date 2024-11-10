using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;


namespace NyanSpecialty.Assistance.Web.Manager
{
    public class StatusDataManager : IStatusDataManager
    {
        private readonly ApplicationDBContext _dbcontext;
        public StatusDataManager(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Status> GetStatusByID(long statusId)
        {
            return await _dbcontext.statuses.FindAsync(statusId);
        }

        public async Task<List<Status>> GetStatusesAsync()
        {
            return await _dbcontext.statuses.ToListAsync();
        }

        public async Task<Status> InsertOrUpdateStatusAsync(Status status)
        {
            if (status != null)
            {
                if (status.StatusId == 0)
                {
                    await _dbcontext.statuses.AddAsync(status);
                    await _dbcontext.SaveChangesAsync();
                    return status;
                }
                else
                {
                    var existingStatus = await _dbcontext.statuses.FindAsync(status.StatusId);
                    if (existingStatus != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existingStatus, status, nameof(status.CreatedBy), nameof(status.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existingStatus, status, nameof(status.CreatedBy), nameof(status.CreatedOn));
                            await _dbcontext.SaveChangesAsync();
                            return existingStatus;
                        }
                    }
                }
            }
            return null;
        }
    }
}
