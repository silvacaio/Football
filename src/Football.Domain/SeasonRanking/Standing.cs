using System.Collections.Generic;

namespace Football.Domain.SeasonRanking
{
    public sealed class Standing
    {
        public string Stage { get; set; }
        public string Type { get; set; }
        public object Group { get; set; }
        public List<Table> Table { get; set; }
    }
}
