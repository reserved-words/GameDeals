using System;
using System.Collections.Generic;
using System.Linq;
using GameDeals.Core.Interfaces;
using GameDeals.Data.Contracts;
using Post = GameDeals.Core.Model.Post;
using Feed = GameDeals.Core.Model.Feed;
using Category = GameDeals.Core.Model.Category;
using PostEntity = GameDeals.Data.Contracts.Model.Post;
using FeedEntity = GameDeals.Data.Contracts.Model.Feed;
using CategoryEntity = GameDeals.Data.Contracts.Model.Category;
using GameDeals.Core.Model;

namespace GameDeals.Services
{
    public class RssService : IRssService
    {
        private readonly IMapper _mapper;
        private readonly Func<IUnitOfWork> _uowFactory;

        public RssService(IMapper mapper, Func<IUnitOfWork> uowFactory)
        {
            _mapper = mapper;
            _uowFactory = uowFactory;
        }

        public List<Category> GetCategories()
        {
            using (var uow = _uowFactory())
            {
                var categories = uow.Repository<CategoryEntity>()
                    .ToList();

                return _mapper.Map<List<CategoryEntity>, List<Category>>(categories);
            }
        }

        public PagedResult<Post> GetPosts(int categoryId, int? limit = null, int? offset = null)
        {
            using (var uow = _uowFactory())
            {
                var page = limit.HasValue && offset.HasValue
                    ? (int)(offset / limit + 1)
                    : 1;

                var query = uow.Repository<PostEntity>()
                    .Where(p => p.Feed.CategoryId == categoryId && !p.Saved && !p.Deleted);

                var total = query.Count();
                var posts = GetResults(query, limit, offset);
                
                MarkPostsAsSeen(posts, uow);

                var results = _mapper.Map<List<PostEntity>, List<Post>>(posts);

                return new PagedResult<Post>(results, limit ?? 0, page, total);
            }
        }

        private List<PostEntity> GetResults(IQueryable<PostEntity> query, int? limit = null, int? offset = null)
        {
            query = query.OrderByDescending(p => p.PublishedAt);

            if (offset.HasValue)
                query = query.Skip(offset.Value);

            if (limit.HasValue)
                query = query.Take(limit.Value);

            return query.ToList();
        }

        private void MarkPostsAsSeen(List<PostEntity> posts, IUnitOfWork uow)
        {
            var unseenPosts = posts.Where(p => !p.Seen).ToList();

            if (!unseenPosts.Any())
                return;

            unseenPosts.ForEach(p =>
            {
                p.Seen = true;
            });

            uow.Save();
        }

        public List<Post> GetSavedPosts(int categoryId)
        {
            using (var uow = _uowFactory())
            {
                var posts = uow.Repository<PostEntity>()
                    .Where(p => p.Feed.CategoryId == categoryId && p.Saved && !p.Deleted)
                    .ToList();

                return _mapper.Map<List<PostEntity>, List<Post>>(posts);
            }
        }

        public List<Feed> GetFeeds()
        {
            using (var uow = _uowFactory())
            {
                var feeds = uow.Repository<FeedEntity>()
                    .ToList();

                return _mapper.Map<List<FeedEntity>, List<Feed>>(feeds);
            }
        }

        public Feed GetFeed(int id)
        {
            using (var uow = _uowFactory())
            {
                var feed = uow.Repository<FeedEntity>().GetById(id);
                return _mapper.Map<FeedEntity, Feed>(feed);
            }
        }

        public void DeleteFeed(int id)
        {
            using (var uow = _uowFactory())
            {
                uow.Repository<FeedEntity>().Delete(id);
                uow.Save();
            }
        }

        public void UpdateFeed(Feed value)
        {
            using (var uow = _uowFactory())
            {
                var feed = _mapper.Map<Feed, FeedEntity>(value);
                uow.Repository<FeedEntity>().Update(feed);
                uow.Save();
            }
        }

        public void AddFeed(Feed value)
        {
            using (var uow = _uowFactory())
            {
                var feed = _mapper.Map<Feed, FeedEntity>(value);
                uow.Repository<FeedEntity>().Insert(feed);
                uow.Save();
            }
        }

        public void DeletePost(int id)
        {
            using (var uow = _uowFactory())
            {
                var post = uow.Repository<PostEntity>().GetById(id);
                post.Deleted = true;
                uow.Save();
            }
        }

        public void SavePost(int id)
        {
            using (var uow = _uowFactory())
            {
                var post = uow.Repository<PostEntity>().GetById(id);
                post.Saved = true;
                uow.Save();
            }
        }
    }
}
