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

        public async Task<List<WorkFlowDetails>> GetWorkFlowDetailsAsync()
        {
            var workFlows = await dbContext.workFlows.ToListAsync();

            List<WorkFlowDetails> workFlowDetails = new List<WorkFlowDetails>();

            foreach (var workFlow in workFlows)
            {
                var workFlowSteps = await dbContext.workFlowSteps
                    .Where(x => x.WorkFlowId == workFlow.WorkFlowId)
                    .ToListAsync();

                workFlowDetails.Add(new WorkFlowDetails
                {
                    workFlow = workFlow,
                    workFlowSteps = workFlowSteps
                });
            }

            return workFlowDetails;
        }

        public async Task<WorkFlowDetails> GetWorkFlowDetailsByWorkFlowIdAsync(long workFlowId)
        {
            var workFlow = await dbContext.workFlows.FirstOrDefaultAsync(x => x.WorkFlowId == workFlowId);

            if (workFlow == null)
            {
                return null; 
            }

            var workFlowSteps = await dbContext.workFlowSteps.Where(x => x.WorkFlowId == workFlowId).ToListAsync();

            return new WorkFlowDetails
            {
                workFlow = workFlow,
                workFlowSteps = workFlowSteps
            };
        }

        public async Task<List<WorkFlowStep>> InsertOrReOrderWorkFlowStepsAsync(List<WorkFlowStep> workFlowSteps)
        {
            if (workFlowSteps == null || !workFlowSteps.Any())
            {
                return null; 
            }

            List<WorkFlowStep> updatedWorkFlowSteps = new List<WorkFlowStep>();

            foreach (var step in workFlowSteps)
            {
                if (step.WorkFlowStepId > 0)
                {
                    var existingStep = await dbContext.workFlowSteps.FindAsync(step.WorkFlowStepId);

                    if (existingStep != null)
                    {
                        existingStep.StepOrder = step.StepOrder;

                        dbContext.workFlowSteps.Update(existingStep);

                        updatedWorkFlowSteps.Add(existingStep);
                    }
                }
                else
                {
                    await dbContext.workFlowSteps.AddAsync(step);
                    updatedWorkFlowSteps.Add(step);
                }
            }
           
            await dbContext.SaveChangesAsync();

            return updatedWorkFlowSteps;
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
