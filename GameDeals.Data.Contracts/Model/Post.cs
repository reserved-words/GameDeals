using System;
using System.ComponentModel.DataAnnotations;

namespace GameDeals.Data.Contracts.Model
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int FeedId { get; set; }
        [MaxLength(1024)]
        public string Title { get; set; }
        [MaxLength(1024)]
        public string Url { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime FetchedAt { get; set; }
        public string Summary { get; set; }
        public bool Saved { get; set; }
        public bool Deleted { get; set; }
        public bool Seen { get; set; }

        public virtual Feed Feed { get; set; }
    }
}
