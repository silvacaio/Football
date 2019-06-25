using System;

namespace Football.Domain.SeasonRanking
{
    public sealed class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vode { get; set; }
        public string Plan { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
