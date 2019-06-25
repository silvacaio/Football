using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Football.API.Configurations
{
    public static class ConfigurationsValues
    {
        public static void AddConfigurations(this IServiceCollection services, IConfigurationRoot configuration)
        {
            FootballURL = configuration.GetSection("FootballURL")?.Value;
            Token = configuration.GetSection("Token")?.Value;
        }

        public static string FootballURL { get; private set; }
        public static string Token { get; private set; }
    }
}
