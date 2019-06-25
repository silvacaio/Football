namespace Football.Domain.SeasonRanking.Commands
{
    public class CompetionRankingCommand
    {
        public CompetionRankingCommand(int competitionId)
        {
            CompetitionId = competitionId;
        }

        public int CompetitionId { get; set; }
    }
}
