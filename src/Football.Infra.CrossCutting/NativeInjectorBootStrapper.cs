using Football.Domain.Core.Request;
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

            //Http
            services.AddScoped<IHttpWebRequestFactory, HttpWebRequestFactory>();
            services.AddScoped<IHttpWebRequest, WrapHttpWebRequest>();
            services.AddScoped<IHttpWebResponse, WrapHttpWebResponse>();
        }
    }
}
