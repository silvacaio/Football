using Football.Domain.Core.Deserialize;
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
        private readonly IStreamReaderFactory _streamFactory;
        private readonly IDeserializeToObject<Ranking> _deserealize;

        public RankingTeamsHandler(IHttpWebRequestFactory webRequestFactory, IStreamReaderFactory streamFactory, IDeserializeToObject<Ranking> deserealize)
        {
            _webRequestFactory = webRequestFactory;
            _streamFactory = streamFactory;
            _deserealize = deserealize;
        }

        public Result<Ranking> GetRanking(CompetionRankingCommand command)
        {
            try
            {
                if (command.Invalid())
                    return "Parâmetros inválidos";

                var request = _webRequestFactory.Create(string.Format(command.FootballURL, command.CompetitionId));
                request.Method = "GET";
                request.Headers.Add("X-Auth-Token", command.Token);


                using (var response = request.GetResponse())
                using (var streamReader = _streamFactory.Create(response.GetResponseStream()))
                {

                    var result = _deserealize.Deserialize(streamReader.ReadToEnd());
                    if (result == null)
                        return "Competição não encontrada";

                    return result;
                }

            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                    return ex.Message;

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
