using System.ServiceModel.Syndication;

namespace GameDeals.Core.Interfaces
{
    public interface ISyndicationFeedService
    {
        SyndicationItem[] GetPosts(string feedUrl);
    }
}
