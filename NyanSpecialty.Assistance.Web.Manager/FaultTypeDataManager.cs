using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class FaultTypeDataManager : IFaultTypeDataManager
    {
        private readonly ApplicationDBContext _dbcontext;

        public FaultTypeDataManager(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<FaultType> GetFaultTypeByID(long faultTypeId)
        {
            return await _dbcontext.faultTypes.FindAsync(faultTypeId);
        }

        public async Task<List<FaultType>> GetFaultTypesAsync()
        {
            return await _dbcontext.faultTypes.ToListAsync();
        }

        public async Task<FaultType> InsertOrUpdateFaultTypeAsync(FaultType faultType)
        {
            if (faultType != null)
            {
                if (faultType.FaultTypeId == 0)
                {

                    await _dbcontext.faultTypes.AddAsync(faultType);
                    await _dbcontext.SaveChangesAsync();
                    return faultType;
                }
                else
                {
                    var existingFaultType = await _dbcontext.faultTypes.FindAsync(faultType.FaultTypeId);
                    if (existingFaultType != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existingFaultType, faultType, nameof(faultType.CreatedBy), nameof(faultType.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existingFaultType, faultType, nameof(faultType.CreatedBy), nameof(faultType.CreatedOn));
                            await _dbcontext.SaveChangesAsync();
                            return existingFaultType;
                        }
                    }
                }
            }
            return null;
        }
    }
}
