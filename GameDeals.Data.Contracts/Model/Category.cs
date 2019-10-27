using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameDeals.Data.Contracts.Model
{
    public class Category
    {
        public Category()
        {
            Feeds = new List<Feed>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Value { get; set; }
        
        public virtual ICollection<Feed> Feeds { get; set; }
    }
}
