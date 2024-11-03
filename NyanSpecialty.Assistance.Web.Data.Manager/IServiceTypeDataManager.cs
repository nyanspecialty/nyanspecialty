using NyanSpecialty.Assistance.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IServiceTypeDataManager
    {
        Task<List<ServiceType>> GetServiceTypesAsync();
        Task<ServiceType> GetServiceTypeAsync(long serviceTypeId);
        Task<ServiceType> AddEditServiceTypeAsync(ServiceType serviceType);

    }
}
