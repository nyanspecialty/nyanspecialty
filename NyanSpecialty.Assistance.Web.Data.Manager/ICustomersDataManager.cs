using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface ICustomersDataManager
    {
        Task<Customers> InsertOrUpdateCustomersAsync(Customers customers);
        Task<List<Customers>> GetCustomersAsync();
        Task<Customers> GetCustomersByIdAsync(long CustomerId);
    }
}
