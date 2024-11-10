using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IStatusDataManager
    {
        Task<Status> InsertOrUpdateStatusAsync(Status status);
        Task<List<Status>> GetStatusesAsync();
        Task<Status> GetStatusByID(long statusId);

    }
}
