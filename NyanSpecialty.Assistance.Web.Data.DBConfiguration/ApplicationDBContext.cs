using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.DBConfiguration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<VehicleSize> vehicleSizes { get; set; }
        public DbSet<PolicyType> policyTypes { get; set; }
        public DbSet<PolicyCategory> policyCategories { get; set; }
        public DbSet<VehicleClass> vehicleClasses { get; set; }
        public DbSet<InsurancePolicy> insurancePolicies { get; set; }
        public DbSet<Customers> customers { get; set; }
    }
}
