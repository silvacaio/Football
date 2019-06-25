using System;

namespace Football.Domain.SeasonRanking.Commands
{
    public class CompetionRankingCommand
    {

        public CompetionRankingCommand(int competitionId, string footballURL, string token)
        {
            CompetitionId = competitionId;
            FootballURL = footballURL;
            Token = token;
        }

        public int CompetitionId { get; private set; }
        public string FootballURL { get; private set; }
        public string Token { get; private set; }

        internal bool Invalid() =>
           CompetitionId <= 0 || string.IsNullOrWhiteSpace(FootballURL) || string.IsNullOrWhiteSpace(Token);
    }
}
