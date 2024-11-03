using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IServiceProviderDataManager
    {
        Task<ServiceProvider> InsertOrUpdateServiceProviderAsync(ServiceProvider ServiceProvider);
        Task<List<ServiceProvider>> GetServiceProvidersAsync();
        Task<ServiceProvider> GetServiceProviderAsync(string searchInput);
    }
}