using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Domain.SeasonRanking
{
    public sealed class Ranking
    {     
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public List<Standing> Standings { get; set; }
    }
}
