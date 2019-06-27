using Football.Domain.Core.Deserialize;
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
            //Handlers
            services.AddScoped<IRankingTeamsHandler, RankingTeamsHandler>();

            //Json
            services.AddScoped(typeof(IDeserializeToObject<>), typeof(DeserializeToObject<>));            

            //Http
            services.AddScoped<IHttpWebRequestFactory, HttpWebRequestFactory>();
            services.AddScoped<IHttpWebRequest, WrapHttpWebRequest>();
            services.AddScoped<IHttpWebResponse, WrapHttpWebResponse>();

            //Stream
            services.AddScoped<IStreamReaderFactory, StreamReaderFactory>();
        }
    }
}
