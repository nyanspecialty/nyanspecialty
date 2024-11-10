using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IWorkFlowDataManager
    {
        Task<WorkFlow> GetWorkFlowAsync(long workFlowId);
        Task<List<WorkFlow>> GetAllWorkFlowsAsync();
        Task<WorkFlow> InsertOrUpdateWorkFlow(WorkFlow workFlow);
        Task<List<WorkFlowStep>> InsertOrReOrderWorkFlowStepsAsync(List<WorkFlowStep> workFlowSteps);
        Task<List<WorkFlowDetails>> GetWorkFlowDetailsAsync();
        Task<WorkFlowDetails> GetWorkFlowDetailsByWorkFlowIdAsync(long workFlowId);
    }
}
