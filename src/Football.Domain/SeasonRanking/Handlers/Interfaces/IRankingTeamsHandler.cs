using Football.Domain.SeasonRanking;
using Football.Domain.Core.Results;
using Football.Domain.SeasonRanking.Commands;
using System.Threading.Tasks;

namespace Football.Domain.SeasonRanking.Handlers.Interfaces
{
    public interface IRankingTeamsHandler
    {
        Result<Ranking> GetRanking(CompetionRankingCommand command);

    }
}
