using Football.Domain.Core.Request;
using Football.Domain.Core.Results;
using Football.Domain.SeasonRanking.Commands;
using Football.Domain.SeasonRanking.Handlers.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Football.Domain.SeasonRanking.Handlers
{
    public class RankingTeamsHandler : IRankingTeamsHandler
    {
        private readonly IHttpWebRequestFactory _webRequestFactory;

        public RankingTeamsHandler(IHttpWebRequestFactory webRequestFactory)
        {
            _webRequestFactory = webRequestFactory;
        }

        public Result<Ranking> GetRanking(CompetionRankingCommand command)
        {
            try
            {
                if (command.Invalid())
                    return "Parâmetros inválidos";

                var request = _webRequestFactory.Create(string.Format(command.FootballURL, command.CompetitionId)) as IHttpWebRequest;
                request.Method = "GET";                
                request.Headers.Add("X-Auth-Token", command.Token);

                using (var response = request.GetResponse())
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = JsonConvert.DeserializeObject<Ranking>(streamReader.ReadToEnd());
                    return result;
                }

            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                    throw ex;

                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
