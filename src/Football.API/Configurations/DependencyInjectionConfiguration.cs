using Football.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Football.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
