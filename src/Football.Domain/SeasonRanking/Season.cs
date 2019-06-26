namespace Football.Domain.SeasonRanking
{
    public sealed class Season
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }        
        public object Winner { get; set; }
    }
}
