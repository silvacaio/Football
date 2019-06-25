using Football.Domain.Core.Results;
using Football.Domain.SeasonRanking.Commands;
using Football.Domain.SeasonRanking.Handlers.Interfaces;
using System;
using System.Threading.Tasks;

namespace Football.Domain.SeasonRanking.Handlers
{
    public class RankingTeamsHandler : IRankingTeamsHandler
    {

        public Result<Ranking> GetRanking(CompetionRankingCommand command)
        {
            try
            {

            }
            catch (Exception e)
            {                
                return e.Message;
            }

            //https://api.football-data.org/v2/competitions/2021/standings
            throw new NotImplementedException();
        }
    }
}
