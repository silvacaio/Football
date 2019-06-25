using Football.Domain.SeasonRanking.Handlers;
using Football.Domain.SeasonRanking.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Football.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRankingTeamsHandler, RankingTeamsHandler>();
        }
    }
}
