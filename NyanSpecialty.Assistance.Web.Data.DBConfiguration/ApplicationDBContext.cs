﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<User> users { get; set; }
        public DbSet<ServiceProvider> serviceProviders { get; set; }
        public DbSet<ServiceType> serviceTypes { get; set; }
        public DbSet<WorkFlow> workFlows { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Case> cases { get; set; }
        public DbSet<CaseStatus> caseStatuses { get; set; }
        public DbSet<ServiceProviderAssignment> serviceProviderAssignments { get; set; }
        public DbSet<WorkFlowStep> workFlowSteps { get; set; }
        public DbSet<ServiceProviderWorkFlow> serviceProviderWorkFlows { get; set; }
        public DbSet<FaultType> faultTypes { get; set; }
        public DbSet<Status> statuses { get; set; }
    }
}
