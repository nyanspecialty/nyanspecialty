using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Manager;

[assembly: FunctionsStartup(typeof(NyanSpecialty.Assistance.Web.Api.Startup))]

namespace NyanSpecialty.Assistance.Web.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationInsightsTelemetryWorkerService();

            builder.Services.ConfigureFunctionsApplicationInsights();

            var sqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(sqlConnection));

            builder.Services.AddScoped<IVehicleSizeDataManager, VehicleSizeDataManager>();
        }
    }
}