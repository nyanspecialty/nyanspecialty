using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;

[assembly: FunctionsStartup(typeof(NyanSpecialty.Assistance.Web.Api.Startup))]

namespace NyanSpecialty.Assistance.Web.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationInsightsTelemetryWorkerService();

            builder.Services.ConfigureFunctionsApplicationInsights();
        }
    }
}