using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using GameDeals.Core.Interfaces;
using GameDeals.Data.Contracts;
using GameDeals.Data.Contracts.Model;

namespace GameDeals.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IHtmlParser _htmlParser;
        private readonly ILogger _logger;
        private readonly ISyndicationFeedService _syndicationFeedService;
        private readonly Func<IUnitOfWork> _uowFactory;

        public UpdateService(IHtmlParser htmlParser, ISyndicationFeedService syndicationFeedService,
            Func<IUnitOfWork> uowFactory, ILogger logger)
        {
            _htmlParser = htmlParser;
            _logger = logger;
            _syndicationFeedService = syndicationFeedService;
            _uowFactory = uowFactory;
        }

        public void UpdatePosts()
        {
            using (var uow = _uowFactory())
            {
                var feeds = uow.Repository<Feed>()
                    .ToList();

                foreach (var feed in feeds)
                {
                    Update(feed, uow);
                }

                TrySave(uow);
            }
        }

        private void TrySave(IUnitOfWork uow)
        {
            try
            {
                uow.Save();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        private void Update(Feed feed, IUnitOfWork uow)
        {
            var expiryDate = feed.DaysToExpire == 0
                ? DateTime.MinValue
                : DateTime.Now.AddDays(-1 * feed.DaysToExpire);

            var posts = uow.Repository<Post>()
                .Where(p => p.FeedId == feed.Id)
                .ToList();

            var expiredPosts = posts.Where(p => p.PublishedAt < expiryDate).ToList();

            expiredPosts.ForEach(p =>
            {
                uow.Repository<Post>().Delete(p);
            });

            var newPosts = GetNewPosts(feed, uow)
                .Where(p => feed.DaysToExpire == 0 || p.PublishDate.Date >= expiryDate)
                .Select(item => new Post
                {
                    Title = item.Title.Text,
                    PublishedAt = GetPublishDate(item),
                    Url = item.Links.First()?.Uri.ToString(),
                    Summary = _htmlParser.StripHtmlTags(item.Summary.Text),
                    FetchedAt = DateTime.Now,
                    FeedId = feed.Id,
                    Seen = false
                })
                .ToList();

            foreach (var post in newPosts)
            {
                if (posts.All(p => p.Url != post.Url))
                {
                    uow.Repository<Post>().Insert(post);
                }
            }
        }

        private static DateTime GetPublishDate(SyndicationItem item)
        {
            return item.PublishDate.DateTime == DateTime.MinValue
                ? DateTime.Now
                : item.PublishDate.DateTime;
        }

        private IEnumerable<SyndicationItem> GetNewPosts(Feed feed, IUnitOfWork uow)
        {
            SyndicationItem[] items;
            var errored = false;

            try
            {
                items = _syndicationFeedService.GetPosts(feed.FeedUrl);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Feed", $"{feed.Id} - {feed.Title}");
                ex.Data.Add("Feed URL", feed.FeedUrl);
                _logger.Log(ex);
                errored = true;
                items = new SyndicationItem[] {};
            }

            UpdateFeed(feed, errored, uow);

            return items;
        }

        private static void UpdateFeed(Feed feed, bool errored, IUnitOfWork uow)
        {
            if (!errored && feed.ConsecutiveErroredFetches == 0)
                return;

            if (errored)
            {
                feed.ConsecutiveErroredFetches++;
            }
            else
            {
                feed.ConsecutiveErroredFetches = 0;
            }

            uow.Repository<Feed>().Update(feed);
        }
    }
}
