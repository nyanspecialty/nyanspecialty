using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;


namespace NyanSpecialty.Assistance.Web.Manager
{
    public class CustomersDataManager : ICustomersDataManager
    {
        private readonly ApplicationDBContext dbContext;
        public CustomersDataManager(ApplicationDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public async Task<List<Customers>> GetCustomersAsync()
        {
           return await dbContext.customers.ToListAsync();
        }

        public async Task<Customers> GetCustomersByIdAsync(long customerId)
        {
            return await dbContext.customers.Where(x => x.CustomerID == customerId).FirstOrDefaultAsync();
        }

        public async Task<Customers> InsertOrUpdateCustomersAsync(Customers customers)
        {
            if (customers != null)
            {
                if (customers.CustomerID == 0) // Insert
                {
                    await dbContext.customers.AddAsync(customers);
                    await dbContext.SaveChangesAsync();
                    return customers;
                }
                else // Update
                {
                    var existingCustomer = await dbContext.customers.FindAsync(customers.CustomerID);
                    if (existingCustomer != null)
                    {
                        // Check if there are changes to update
                        bool hasChanges = EntityUpdater.HasChanges(existingCustomer, customers,
                            nameof(customers.CreatedBy), nameof(customers.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(customers, existingCustomer,
                                nameof(customers.CreatedBy), nameof(customers.CreatedOn));
                            await dbContext.SaveChangesAsync();
                            return customers;
                        }
                    }
                }
            }
            return null;
        }
    }
}
