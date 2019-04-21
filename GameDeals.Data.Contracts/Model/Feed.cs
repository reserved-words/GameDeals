using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameDeals.Data.Contracts.Model
{ 
    public class Feed
    {
        public Feed()
        {
            Posts = new List<Post>();
        }

        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Url { get; set; }
        [MaxLength(255)]
        public string FeedUrl { get; set; }
        [MaxLength(60)]
        public string LogoFileName { get; set; }
        public int DaysToExpire { get; set; }
        public int ConsecutiveErroredFetches { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
