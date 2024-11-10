using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IFaultTypeDataManager
    {
        Task<FaultType> InsertOrUpdateFaultTypeAsync(FaultType faultType);
        Task<List<FaultType>> GetFaultTypesAsync();
        Task<FaultType> GetFaultTypeByID(long faultTypeId);
    }
}
