using System.Collections.Generic;

namespace GameDeals.Core.Model
{
    public class Feed
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string FeedUrl { get; set; }
        public string LogoFileName { get; set; }
        public int DaysToExpire { get; set; }
        public int ConsecutiveErroredFetches { get; set; }
    }
}
