using System;

namespace GameDeals.Core.Model
{
    public class Post
    {
        public int Id { get; set; }
        public int FeedId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime FetchedAt { get; set; }
        public string Summary { get; set; }
        public bool Saved { get; set; }
        public bool IsNew { get; set; }
        public Feed Feed { get; set; }
    }
}
