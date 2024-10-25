using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;

[assembly: FunctionsStartup(typeof(NyanSpecialty.Web.Api.Startup))]

namespace NyanSpecialty.Web.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Add Application Insights Telemetry
            builder.Services.AddApplicationInsightsTelemetryWorkerService();

            // Optionally configure custom settings for Application Insights
            builder.Services.Configure<TelemetryConfiguration>((config) =>
            {
                // Custom configuration logic here
            });
        }
    }
}
