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

        // Register your data managers as scoped
        services.AddScoped<IVehicleSizeDataManager, VehicleSizeDataManager>();
        services.AddScoped<IVehicleClassDataManager, VehicleClassDataManager>();
        services.AddScoped<IPolicyTypeDataManager, PolicyTypeDataManager>();
        services.AddScoped<IPolicyCategoryDataManager, PolicyCategoryDataManager>();
        services.AddScoped<IInsurancePolicyDataManager, InsurancePolicyDataManager>();
        services.AddScoped <IUserDataManager, UserDataManager > ();
        services.AddScoped<IAuthenticationDataManager, AuthenticationDataManager>();
        services.AddScoped<IServiceProviderDataManager, ServiceProviderDataManager>();
        services.AddScoped<IServiceTypeDataManager, ServiceTypeDataManager>();
        services.AddScoped<IWorkFlowDataManager, WorkFlowDataManager>();
        services.AddScoped<IRoleDataManagaer, RoleDataManagaer>();
        services.AddScoped<ICustomersDataManager, CustomersDataManager>();

        // Add CORS policy to allow requests from Angular app
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder.WithOrigins("http://localhost:4200") // Angular app URL
                       .AllowAnyMethod()
                       .AllowAnyHeader()
            );
        });
    })
    .Build();

host.Run();