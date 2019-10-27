using GameDeals.Core.Model;
using System.Collections.Generic;

namespace GameDeals.Core.Interfaces
{
    public interface IRssService
    {
        List<Category> GetCategories();
        PagedResult<Post> GetPosts(int categoryId, int? limit = null, int? offset = null);
        List<Post> GetSavedPosts(int categoryId);
        List<Feed> GetFeeds();
        Feed GetFeed(int id);
        void DeleteFeed(int id);
        void UpdateFeed(Feed value);
        void AddFeed(Feed value);
        void DeletePost(int id);
        void SavePost(int id);
    }
}
