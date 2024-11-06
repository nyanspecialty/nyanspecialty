using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class WorkFlowDataManager : IWorkFlowDataManager
    {
       private readonly ApplicationDBContext dbContext;
        public WorkFlowDataManager(ApplicationDBContext _dBContext)
        {
            this.dbContext = _dBContext;                
        }
        public async Task<List<WorkFlow>> GetAllWorkFlowsAsync()
        {
           return await dbContext.workFlows.ToListAsync();
        }

        public async Task<WorkFlow> GetWorkFlowAsync(long workFlowId)
        {
          return await dbContext.workFlows.Where(x => x.WorkFlowId == workFlowId).FirstOrDefaultAsync();
        }

        public async Task<WorkFlow> InsertOrUpdateWorkFlow(WorkFlow workFlow)
        {
            if (workFlow != null)
            {
                if (workFlow.WorkFlowId == 0)
                {
                    await dbContext.workFlows.AddAsync(workFlow);
                    await dbContext.SaveChangesAsync();
                    return workFlow;
                }
                else
                {
                    var existworkFlow = await dbContext.workFlows.FindAsync(workFlow.WorkFlowId);
                    if (existworkFlow != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existworkFlow, workFlow, nameof(workFlow.CreatedBy), nameof(workFlow.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existworkFlow, workFlow, nameof(workFlow.CreatedBy), nameof(workFlow.CreatedOn));
                            await dbContext.SaveChangesAsync();
                            return existworkFlow;
                        }
                    }

                }

            }
            return null;
        }
    }
}
