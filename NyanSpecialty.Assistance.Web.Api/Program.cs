using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Manager;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        // Add Application Insights telemetry
        services.AddApplicationInsightsTelemetryWorkerService();

        // Configure your SQL connection
        var sqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(sqlConnection));

        // Register your data managers
        services.AddSingleton<IVehicleSizeDataManager, VehicleSizeDataManager>();
    })
    .Build();

host.Run();