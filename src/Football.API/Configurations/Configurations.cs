using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Football.API.Configurations
{
    public static class Configurations
    {
        public static void AddConfigurations(this IServiceCollection services, IConfigurationRoot configuration)
        {
            FootballURL = configuration.GetSection("FootballURL")?.Value;
        }

        public static string FootballURL { get; private set; }
    }
}
