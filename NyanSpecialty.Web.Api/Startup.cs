using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(NyanSpecialty.Web.Api.Startup))]

namespace NyanSpecialty.Web.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
